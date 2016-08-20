using ZKWeb.Web;
using ZKWebStandard.Ioc;

namespace ZKWeb.Demo.Plugins.ZKWeb.Demo.src.Controllers {
	[ExportMany]
	public class TestController : IController {
		public IGenericResolver Resolver { get; set; }
		public TestController(IGenericResolver resolver) {
			Resolver = resolver;
		}

		[Action("example")]
		public object Example(string name, int age) {
			return new { name, age };
		}
	}
}
