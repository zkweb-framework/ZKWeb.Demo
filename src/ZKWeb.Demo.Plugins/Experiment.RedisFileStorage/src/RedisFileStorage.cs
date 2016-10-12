using StackExchange.Redis;
using System.Linq;
using ZKWeb.Server;
using ZKWeb.Storage;
using ZKWebStandard.Extensions;
using ZKWebStandard.Ioc;
using ZKWebStandard.Utils;

namespace ZKWeb.Demo.Plugins.Experiment.RedisFileStorage.src {
	/// <summary>
	/// 基于Redis的文件储存
	/// 注意这个储存不适合用来保存比较大的文件
	/// </summary>
	[ExportMany(ClearExists = true), SingletonReuse]
	public class RedisFileStorage : IFileStorage {
		/// <summary>
		/// 连接字符串在网站配置中的键
		/// </summary>
		private const string ConnectionStringKey = "Experiment.RedisFileStorage.ConnectionString";
		/// <summary>
		/// Redis连接生成器
		/// </summary>
		private ConnectionMultiplexer RedisConnectionFactory { get; set; }

		/// <summary>
		/// 初始化
		/// </summary>
		public RedisFileStorage() {
			var configManager = Application.Ioc.Resolve<WebsiteConfigManager>();
			var extra = configManager.WebsiteConfig.Extra;
			var connectionString = extra.GetOrDefault<string>(ConnectionStringKey) ?? "localhost";
			RedisConnectionFactory = ConnectionMultiplexer.Connect(connectionString);
		}

		/// <summary>
		/// 获取模板文件
		/// 首先从Redis获取，失败后从本地获取
		/// </summary>
		public IFileEntry GetTemplateFile(string path) {
			var redisFileEntry = GetStorageFile("templates", path);
			if (redisFileEntry.Exists) {
				return redisFileEntry;
			}
			var pathManager = Application.Ioc.Resolve<LocalPathManager>();
			var fullPath = pathManager.GetTemplateFullPath(path);
			return new LocalFileEntry(fullPath, true);
		}

		/// <summary>
		/// 获取资源文件
		/// 首先从Redis获取，失败后从本地获取
		/// </summary>
		public IFileEntry GetResourceFile(params string[] pathParts) {
			var redisFileEntry = GetStorageFile(pathParts);
			if (redisFileEntry.Exists) {
				return redisFileEntry;
			}
			var pathManager = Application.Ioc.Resolve<LocalPathManager>();
			var fullPath = pathManager.GetResourceFullPath(pathParts);
			return new LocalFileEntry(fullPath, true);
		}

		/// <summary>
		/// 获取储存文件
		/// 首先从Redis获取，失败后从本地获取
		/// </summary>
		public IFileEntry GetStorageFile(params string[] pathParts) {
			var fullPath = PathUtils.SecureCombine(pathParts);
			return new RedisFileEntry(RedisConnectionFactory, fullPath, false);
		}

		/// <summary>
		/// 获取储存文件夹
		/// </summary>
		public IDirectoryEntry GetStorageDirectory(params string[] pathParts) {
			var fullPath = PathUtils.SecureCombine(pathParts);
			return new RedisDirectoryEntry(RedisConnectionFactory, fullPath);
		}
	}
}
