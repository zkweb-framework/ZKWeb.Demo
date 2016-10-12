using System;

namespace ZKWeb.Demo.Plugins.Experiment.RedisFileStorage.src {
	/// <summary>
	/// Redis文件内容
	/// </summary>
	public class RedisFileBody {
		/// <summary>
		/// 内容的Base64
		/// </summary>
		public string ContentsBase64 { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreationTime { get; set; }
		/// <summary>
		/// 访问时间
		/// </summary>
		public DateTime LastAccessTime { get; set; }
		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime LastWriteTime { get; set; }

		/// <summary>
		/// 设置内容
		/// </summary>
		public void SetContents(byte[] contents) {
			if (contents == null || contents.Length == 0) {
				ContentsBase64 = null;
			} else {
				ContentsBase64 = Convert.ToBase64String(contents);
			}
		}

		/// <summary>
		/// 获取内容
		/// </summary>
		public byte[] GetContents() {
			return Convert.FromBase64String(ContentsBase64 ?? "");
		}
	}
}
