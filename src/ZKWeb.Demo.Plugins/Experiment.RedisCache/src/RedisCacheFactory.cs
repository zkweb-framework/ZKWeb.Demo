using StackExchange.Redis;
using System;
using System.Linq;
using ZKWeb.Cache;
using ZKWeb.Server;
using ZKWebStandard.Collections;
using ZKWebStandard.Extensions;
using ZKWebStandard.Ioc;

namespace ZKWeb.Demo.Plugins.Experiment.RedisCache.src {
	/// <summary>
	/// Redis缓存生成器
	/// </summary>
	[ExportMany(ClearExists = true), SingletonReuse]
	public class RedisCacheFactory : ICacheFactory {
		/// <summary>
		/// 连接字符串在网站配置中的键
		/// </summary>
		private const string ConnectionStringKey = "Experiment.RedisCache.ConnectionString";
		/// <summary>
		/// Redis连接生成器
		/// </summary>
		private ConnectionMultiplexer RedisConnectionFactory { get; set; }

		/// <summary>
		/// 初始化
		/// </summary>
		public RedisCacheFactory() {
			var configManager = Application.Ioc.Resolve<WebsiteConfigManager>();
			var extra = configManager.WebsiteConfig.Extra;
			var connectionString = extra.GetOrDefault<string>(ConnectionStringKey) ?? "localhost";
			RedisConnectionFactory = ConnectionMultiplexer.Connect(connectionString);
		}

		/// <summary>
		/// 创建缓存对象
		/// </summary>
		public IKeyValueCache<TKey, TValue> CreateCache<TKey, TValue>(CacheFactoryOptions options) {
			options = options ?? CacheFactoryOptions.Default;
			if (options.Lifetime == CacheLifetime.Singleton) {
				if (options.IsolationPolicies.Any()) {
					// 带隔离策略的缓存
					return new IsolatedKeyValueCache<TKey, TValue>(
						options.IsolationPolicies,
						new RedisCache<IsolatedCacheKey<TKey>, TValue>(RedisConnectionFactory));
				} else {
					// 一般的缓存
					return new RedisCache<TKey, TValue>(RedisConnectionFactory);
				}
			} else if (options.Lifetime == CacheLifetime.PerHttpContext) {
				if (options.IsolationPolicies.Any()) {
					// 选项不正确
					throw new ArgumentException("PerHttpContext shouldn't use with isolation policies");
				} else {
					// 跟随Http上下文的缓存
					return new HttpContextCache<TKey, TValue>();
				}
			} else {
				throw new NotSupportedException($"Unsupported cache lifetime: {options.Lifetime}");
			}
		}
	}
}
