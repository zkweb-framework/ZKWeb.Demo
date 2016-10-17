using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZKWeb.Web;
using ZKWeb.Web.ActionResults;
using ZKWebStandard.Ioc;

namespace ZKWeb.Demo.Plugins.ZKWeb.Demo.src.Controllers {
	/// <summary>
	/// 首页控制器
	/// </summary>
	[ExportMany]
	public class IndexController : IController {
		/// <summary>
		/// 项目介绍页
		/// </summary>
		/// <returns></returns>
		[Action("/", OverrideExists = true)]
		public IActionResult Index() {
			return new TemplateResult("demo/index.html");
		}

		/// <summary>
		/// Demo页，原始的首页
		/// </summary>
		/// <returns></returns>
		[Action("/demo")]
		public IActionResult OriginalIndex() {
			return new TemplateResult("common.base/index.html");
		}
	}
}
