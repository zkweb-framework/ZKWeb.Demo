using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Concurrent;
using ZKWebStandard.Collections;
using ZKWebStandard.Utils;

namespace ZKWeb.Demo.Plugins.Experiment.RedisCache.src {
	/// <summary>
	/// Redis缓存
	/// </summary>
	/// <typeparam name="TKey">键类型</typeparam>
	/// <typeparam name="TValue">值类型</typeparam>
	public class RedisCache<TKey, TValue> : IKeyValueCache<TKey, TValue> {
		/// <summary>
		/// Redis连接生成器，由Redis缓存生成器传入
		/// </summary>
		private ConnectionMultiplexer RedisConnectionFactory { get; set; }
		/// <summary>
		/// 备用的缓存，序列化失败时使用这个缓存
		/// </summary>
		private IKeyValueCache<TKey, TValue> FallbackCache { get; set; }
		/// <summary>
		/// 唯一的前缀
		/// </summary>
		private string UniquePrefix { get; set; }
		/// <summary>
		/// 序列化失败过的类型集合
		/// </summary>
		private static ConcurrentDictionary<Type, bool> SerializeFailedTypes = new ConcurrentDictionary<Type, bool>();

		/// <summary>
		/// 初始化
		/// </summary>
		public RedisCache(ConnectionMultiplexer redisConnectionFactory) {
			RedisConnectionFactory = redisConnectionFactory;
			FallbackCache = new MemoryCache<TKey, TValue>();
			UniquePrefix = $"ZKWeb.RedisCache.{RandomUtils.RandomString(8)}.";
		}

		/// <summary>
		/// 设置值
		/// </summary>
		public void Put(TKey key, TValue value, TimeSpan keepTime) {
			// 如果以前序列化失败过，直接使用备用的缓存
			if (SerializeFailedTypes.ContainsKey(typeof(TKey)) ||
				SerializeFailedTypes.ContainsKey(typeof(TValue))) {
				FallbackCache.Put(key, value, keepTime);
				return;
			}
			// 尝试序列化key和value，失败时使用备用的缓存
			string keyJson, valueJson;
			try {
				keyJson = JsonConvert.SerializeObject(key);
			} catch {
				SerializeFailedTypes[typeof(TKey)] = true;
				FallbackCache.Put(key, value, keepTime);
				return;
			}
			try {
				valueJson = JsonConvert.SerializeObject(value);
			} catch {
				SerializeFailedTypes[typeof(TValue)] = true;
				FallbackCache.Put(key, value, keepTime);
				return;
			}
			// 保存到Redis
			var db = RedisConnectionFactory.GetDatabase();
			db.StringSet(UniquePrefix + keyJson, valueJson, keepTime);
		}

		/// <summary>
		/// 获取值
		/// </summary>
		public bool TryGetValue(TKey key, out TValue value) {
			// 先从备用的缓存获取
			if (FallbackCache.TryGetValue(key, out value)) {
				return true;
			}
			// 不存在时尝试序列化key，失败时返回false
			string keyJson;
			try {
				keyJson = JsonConvert.SerializeObject(key);
			} catch {
				SerializeFailedTypes[typeof(TKey)] = true;
				return false;
			}
			// 从Redis获取
			var db = RedisConnectionFactory.GetDatabase();
			var redisValue = db.StringGet(UniquePrefix + keyJson);
			if (!redisValue.HasValue) {
				return false;
			}
			// 反序列化值，失败时返回false
			try {
				value = JsonConvert.DeserializeObject<TValue>(redisValue.ToString());
			} catch {
				SerializeFailedTypes[typeof(TValue)] = true;
				return false;
			}
			return true;
		}

		/// <summary>
		/// 删除值
		/// </summary>
		public void Remove(TKey key) {
			// 先从备用的缓存删除
			FallbackCache.Remove(key);
			// 尝试序列化key
			string keyJson;
			try {
				keyJson = JsonConvert.SerializeObject(key);
			} catch {
				SerializeFailedTypes[typeof(TKey)] = true;
				return;
			}
			// 从Redis删除
			var db = RedisConnectionFactory.GetDatabase();
			db.KeyDelete(UniquePrefix + keyJson);
		}

		/// <summary>
		/// 获取缓存对象数量
		/// </summary>
		public int Count() {
			int count = 0;
			foreach (var endpoint in RedisConnectionFactory.GetEndPoints()) {
				var server = RedisConnectionFactory.GetServer(endpoint);
				foreach (var key in server.Keys()) {
					if (key.ToString().StartsWith(UniquePrefix)) {
						count += 1;
					}
				}
			}
			count += FallbackCache.Count();
			return count;
		}

		/// <summary>
		/// 删除所有缓存值
		/// </summary>
		public void Clear() {
			var db = RedisConnectionFactory.GetDatabase();
			foreach (var endpoint in RedisConnectionFactory.GetEndPoints()) {
				var server = RedisConnectionFactory.GetServer(endpoint);
				foreach (var key in server.Keys()) {
					if (key.ToString().StartsWith(UniquePrefix)) {
						db.KeyDelete(key);
					}
				}
			}
			FallbackCache.Clear();
		}
	}
}
