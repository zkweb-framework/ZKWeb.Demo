using System.Collections.Generic;
using ZKWeb.Localize;
using ZKWebStandard.Extensions;
using ZKWebStandard.Ioc;

namespace ZKWeb.Demo.Plugins.ZKWeb.Demo.src.Components.Translates {
	/// <summary>
	/// 繁体中文翻译
	/// </summary>
	[ExportMany, SingletonReuse]
	public class zh_TW : ITranslateProvider {
		private static HashSet<string> Codes = new HashSet<string>() { "zh-TW" };
		private static Dictionary<string, string> Translates = new Dictionary<string, string>()
		{
			{ "Demo", "演示" },
			{ "Trun website into demo mode", "啟用網站的演示模式" },
			{ "Edit or delete demo account is not allowed", "不允許編輯或刪除演示用的賬號" },
			{ "View on GITHUB", "在GITHUB上查看" },
			{ "Flexible web framework support .Net Framework and .Net Core",
				"靈活高效的網站開發框架，支持.Net Framework和.Net Core" },
			{ "Dynamic Plugins", "動態插件" },
			{ "Powered by roslyn, support automatic recompile after source code changed.",
				"基於Roslyn，支持修改源代碼後自動重新編譯和加載插件。" },
			{ "Disinct Web Layer", "獨立的Web層" },
			{ "Support running on Asp.Net, Asp.Net Core, Owin, no code difference.",
				"支持同壹份代碼運行在Asp.Net, Asp.Net Core, Owin上。" },
			{ "Custom MVC routing and pipeline.", "擁有獨自的MVC路由和管道。" },
			{ "Multiple ORM Support", "支持不同的ORM" },
			{ "Support EFCore, NHibernate, Dapper, MongoDB, InMemory, through the same interface.",
				"支持EFCore, NHibernate, Dapper, MongoDB, InMemory，通過統壹的接口操作。" },
			{ "Powered by roslyn, support c# 6.0 features.", "基於Roslyn，完整支持c# 6.0的功能。" },
			{ "Support runtime plugin reload.", "支持運行時重新加載插件。" },
			{ "Support automatic recompile and reload after plugin source code changed.",
				"支持修改源代碼後自動重新編譯和加載插件。" },
			{ "Directory based, small memory footprint.", "壹個文件夾壹個插件，較小的內存占用。" },
			{ "Support automatic database scheme migration for EFCore and NHibernate.",
				"使用EFCore, NHibernate時支持自動添加和更新數據表。" },
			{ "Dotliquid Template", "Dotliquid模板引擎" },
			{ "Dotliquid template engine, django and rails like syntax.", "Dotliquid模板引擎擁有類似Django和Rails模板的語法。" },
			{ "Support django style template overriding over plugins.", "支持Django風格的跨插件重載模板文件。" },
			{ "Support mobile specialized templates.", "支持定義手機版專用的模板文件。" },
			{ "Support dynamic contents (area-widget model).", "支持區域-部件模型的動態內容系統。" },
			{ "Support per-widget render cache for extremely fast rending.", "支持按部件緩存描畫結果，大幅提升頁面的描畫性能。" },
			{ "Project toolkits", "項目工具" },
			{ "Provide custom project creator, include command-line and gui version.",
				"提供獨自的項目創建工具，包括命令行版本和界面版本。" },
			{ "Provide custom project publisher, include command-line and gui version.",
				"提供獨自的項目發布工具，包括命令行版本和界面版本。" },
			{ "Demo site", "示例站點" },
			{ "Provide free and open source default plugins.", "提供免費和開源的默認插件。" },
			{ "Include admin panel, form builder, CRUD page scaffolding.",
				"包含管理面板，表單構建器和增刪查改頁面生成器。" },
			{ "Visit demo site (Username: demo, Password: 123456)",
				"訪問示例站點 (用戶名: demo, 密碼: 123456)" }
		};

		public bool CanTranslate(string code) {
			return Codes.Contains(code);
		}

		public string Translate(string text) {
			return Translates.GetOrDefault(text);
		}
	}
}
