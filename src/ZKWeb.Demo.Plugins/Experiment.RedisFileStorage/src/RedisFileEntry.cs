using System;
using System.IO;
using StackExchange.Redis;
using ZKWeb.Storage;
using Newtonsoft.Json;

namespace ZKWeb.Demo.Plugins.Experiment.RedisFileStorage.src {
	/// <summary>
	/// Redis的文件对象
	/// </summary>
	public class RedisFileEntry : IFileEntry {
		/// <summary>
		/// 文件的前缀
		/// </summary>
		internal const string FileKeyPrefix = "ZKWeb.RedisFileStorage.File:";
		/// <summary>
		/// Redis连接生成器
		/// </summary>
		private ConnectionMultiplexer RedisConnectionFactory { get; set; }
		/// <summary>
		/// 完整路径
		/// </summary>
		private string FullPath { get; set; }
		/// <summary>
		/// 是否只读
		/// </summary>
		private bool ReadOnly { get; set; }
		/// <summary>
		/// 文件内容
		/// </summary>
		private RedisFileBody Body { get; set; }
		private RedisFileBody BodyEnsureNotNull {
			get {
				if (Body == null) {
					throw new FileNotFoundException($"Redis file {FullPath} not exist");
				}
				return Body;
			}
		}
		/// <summary>
		/// 文件名
		/// </summary>
		public string Filename { get { return Path.GetFileName(FullPath); } }
		/// <summary>
		/// 文件唯一标识
		/// </summary>
		public string UniqueIdentifier { get { return FullPath; } }
		/// <summary>
		/// 判断文件是否存在
		/// </summary>
		public bool Exists { get { return Body != null; } }
		/// <summary>
		/// 文件创建时间
		/// </summary>
		public DateTime CreationTimeUtc { get { return BodyEnsureNotNull.CreationTime; } }
		/// <summary>
		/// 文件访问时间
		/// </summary>
		public DateTime LastAccessTimeUtc { get { return BodyEnsureNotNull.LastAccessTime; } }
		/// <summary>
		/// 文件修改时间
		/// </summary>
		public DateTime LastWriteTimeUtc { get { return BodyEnsureNotNull.LastWriteTime; } }
		/// <summary>
		/// 获取文件大小
		/// </summary>
		public long Length { get { return BodyEnsureNotNull.GetContents().Length; } }

		/// <summary>
		/// 初始化
		/// </summary>
		public RedisFileEntry(ConnectionMultiplexer redisConnectionFactory, string fullPath, bool readOnly) {
			fullPath = fullPath.Replace('\\', '/');
			RedisConnectionFactory = redisConnectionFactory;
			FullPath = fullPath;
			ReadOnly = ReadOnly;
			var db = RedisConnectionFactory.GetDatabase();
			var bodyJson = db.StringGet(FileKeyPrefix + FullPath);
			Body = null;
			if (!string.IsNullOrEmpty(bodyJson)) {
				Body = JsonConvert.DeserializeObject<RedisFileBody>(bodyJson);
			}
		}

		/// <summary>
		/// 更新文件内容
		/// </summary>
		internal void UpdateBody(RedisFileBody body) {
			var db = RedisConnectionFactory.GetDatabase();
			db.StringSet(FileKeyPrefix + FullPath, JsonConvert.SerializeObject(body));
			Body = body;
		}

		/// <summary>
		/// 打开读取文件的数据流
		/// </summary>
		public Stream OpenRead() {
			return new RedisFileStream(this, BodyEnsureNotNull);
		}

		/// <summary>
		/// 打开写入文件的数据流
		/// </summary>
		public Stream OpenWrite() {
			if (ReadOnly) {
				throw new NotSupportedException("This file is readonly");
			}
			return new RedisFileStream(this, new RedisFileBody() { CreationTime = DateTime.UtcNow });
		}

		/// <summary>
		/// 打开添加文件的数据流
		/// </summary>
		/// <returns></returns>
		public Stream OpenAppend() {
			if (ReadOnly) {
				throw new NotSupportedException("This file is readonly");
			}
			var stream = new RedisFileStream(this,
				Body ?? new RedisFileBody() { CreationTime = DateTime.UtcNow });
			stream.Seek(stream.Length, SeekOrigin.Begin);
			return stream;
		}

		/// <summary>
		/// 删除文件
		/// </summary>
		public void Delete() {
			var db = RedisConnectionFactory.GetDatabase();
			db.KeyDelete(FileKeyPrefix + FullPath);
			Body = null;
		}
	}
}
