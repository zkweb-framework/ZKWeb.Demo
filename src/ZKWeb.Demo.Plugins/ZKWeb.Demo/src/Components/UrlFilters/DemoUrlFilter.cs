using ZKWeb.Plugins.Common.Base.src.Components.UrlFilters.Interfaces;
using ZKWebStandard.Ioc;

namespace ZKWeb.Demo.Plugins.ZKWeb.Demo.src.Components.UrlFilters {
	/// <summary>
	/// 示例使用的Url过滤器
	/// </summary>
	[ExportMany]
	public class DemoUrlFilter : IUrlFilter {
		/// <summary>
		/// 替换/到/demo
		/// </summary>
		/// <param name="url">url地址</param>
		public void Filter(ref string url) {
			if (url != "/")
				return;
			url = "/demo";
			foreach (var filter in Application.Ioc.ResolveMany<IUrlFilter>()) {
				if (!(filter is DemoUrlFilter)) {
					filter.Filter(ref url);
				}
			}
		}
	}
}
