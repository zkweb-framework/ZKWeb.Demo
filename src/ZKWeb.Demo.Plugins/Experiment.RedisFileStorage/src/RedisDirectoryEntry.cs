using StackExchange.Redis;
using System.Collections.Generic;
using System.IO;
using ZKWeb.Storage;

namespace ZKWeb.Demo.Plugins.Experiment.RedisFileStorage.src {
	/// <summary>
	/// Redis的文件夹对象
	/// </summary>
	public class RedisDirectoryEntry : IDirectoryEntry {
		/// <summary>
		/// Redis连接生成器
		/// </summary>
		private ConnectionMultiplexer RedisConnectionFactory { get; set; }
		/// <summary>
		/// 完整路径
		/// </summary>
		private string FullPath { get; set; }
		/// <summary>
		/// 文件夹名称
		/// </summary>
		public string DirectoryName { get { return Path.GetFileName(FullPath); } }
		/// <summary>
		/// 文件夹是否存在
		/// </summary>
		public bool Exists { get { return true; } }

		/// <summary>
		/// 初始化
		/// </summary>
		public RedisDirectoryEntry(ConnectionMultiplexer redisConnectionFactory, string fullPath) {
			fullPath = fullPath.Replace('\\', '/');
			RedisConnectionFactory = redisConnectionFactory;
			FullPath = fullPath;
		}

		/// <summary>
		/// 删除文件夹
		/// </summary>
		public void Delete() {
			var db = RedisConnectionFactory.GetDatabase();
			foreach (var endpoint in RedisConnectionFactory.GetEndPoints()) {
				var server = RedisConnectionFactory.GetServer(endpoint);
				foreach (var key in server.Keys()) {
					if (key.ToString().StartsWith(RedisFileEntry.FileKeyPrefix + FullPath + "/")) {
						db.KeyDelete(key);
					}
				}
			}
		}
		
		/// <summary>
		/// 枚举文件夹下的文件夹
		/// </summary>
		public IEnumerable<IDirectoryEntry> EnumerateDirectories() {
			var db = RedisConnectionFactory.GetDatabase();
			foreach (var endpoint in RedisConnectionFactory.GetEndPoints()) {
				var server = RedisConnectionFactory.GetServer(endpoint);
				foreach (var key in server.Keys()) {
					var keyStr = key.ToString();
					if (keyStr.StartsWith(RedisFileEntry.FileKeyPrefix + FullPath + "/")) {
						var path = keyStr.Substring(RedisFileEntry.FileKeyPrefix.Length);
						var childPathParts = path.Substring(FullPath.Length + 1).Split('/');
						if (childPathParts.Length >= 2) {
							yield return new RedisDirectoryEntry(
								RedisConnectionFactory, FullPath + "/" + childPathParts[0]);
						}
					}
				}
			}
		}

		/// <summary>
		/// 枚举文件
		/// </summary>
		public IEnumerable<IFileEntry> EnumerateFiles() {
			var db = RedisConnectionFactory.GetDatabase();
			foreach (var endpoint in RedisConnectionFactory.GetEndPoints()) {
				var server = RedisConnectionFactory.GetServer(endpoint);
				foreach (var key in server.Keys()) {
					var keyStr = key.ToString();
					if (keyStr.StartsWith(RedisFileEntry.FileKeyPrefix + FullPath + "/")) {
						var path = keyStr.Substring(RedisFileEntry.FileKeyPrefix.Length);
						var childPathParts = path.Substring(FullPath.Length + 1).Split('/');
						if (childPathParts.Length == 1) {
							yield return new RedisFileEntry(RedisConnectionFactory, path, false);
						}
					}
				}
			}
		}
	}
}
