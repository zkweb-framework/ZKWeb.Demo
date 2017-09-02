using System;
using ZKWeb.Web;
using ZKWebStandard.Ioc;

namespace ZKWeb.Demo.Plugins.ZKWeb.Demo.src.Controllers {
	/// <summary>
	/// 首页控制器
	/// </summary>
	[ExportMany]
	public class IndexController : IController {
		/// <summary>
		/// 申请证书使用的页面
		/// </summary>
		/// <returns></returns>
		[Action(".well-known/acme-challenge/removed")]
		public string RequestHttps() {
			return "removed";
		}
	}
}
