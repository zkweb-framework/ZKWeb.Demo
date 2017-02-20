using System.Collections.Generic;
using ZKWeb.Localize;
using ZKWebStandard.Extensions;
using ZKWebStandard.Ioc;

namespace ZKWeb.Demo.Plugins.ZKWeb.Demo.src.Components.Translates {
	/// <summary>
	/// 日本语翻译
	/// </summary>
	[ExportMany, SingletonReuse]
	public class ja_JP : ITranslateProvider {
		private static HashSet<string> Codes = new HashSet<string>() { "ja-JP" };
		private static Dictionary<string, string> Translates = new Dictionary<string, string>()
		{
			{ "Demo", "デモ" },
			{ "Trun website into demo mode", "デモモードを有効にする" },
			{ "Edit or delete demo account is not allowed", "デモアカウントの編集と削除は許可されていません" },
			{ "View on GITHUB", "GITHUBを見る" },
			{ "Flexible web framework support .Net Framework and .Net Core",
				".Net Frameworkと.Net Coreをサポートする柔軟なWebフレームワーク" },
			{ "Dynamic Plugins", "動的プラグイン" },
			{ "Powered by roslyn, support automatic recompile after source code changed.",
				"Roslynに基づく、ソースコード変更後自動的再コンパイル及びリロードします。" },
			{ "Disinct Web Layer", "独自のWebレイヤー" },
			{ "Support running on Asp.Net, Asp.Net Core, Owin, no code difference.",
				"同じコードがAsp.Net, Asp.Net Core, Owinの上で動作します。" },
			{ "Custom MVC routing and pipeline.", "独自のMVCルーティングとパイプライン。" },
			{ "Multiple ORM Support", "複数のORMをサポート" },
			{ "Support EFCore, NHibernate, Dapper, MongoDB, InMemory, through the same interface.",
				"統一されたインタフェースを通してEFCore, NHibernate, Dapper, MongoDB, InMemoryをサポートします。" },
			{ "Powered by roslyn, support c# 6.0 features.", "Roslynに基づく、c# 6.0の機能をサポートします。" },
			{ "Support runtime plugin reload.", "動的プラグインリロードをサポートします。" },
			{ "Support automatic recompile and reload after plugin source code changed.",
				"ソースコード変更後自動的再コンパイル及びリロードします。" },
			{ "Directory based, small memory footprint.", "ディレクトリに基づく、メモリ使用量が少ない。" },
			{ "Support automatic database scheme migration for EFCore and NHibernate.",
				"EFCore, NHibernateを使用する時、データベーススキーマの移行を自動的に行えます。" },
			{ "Dotliquid Template", "Dotliquidテンプレート" },
			{ "Dotliquid template engine, django and rails like syntax.", "Dotliquidテンプレートエンジンの文法はDjangoとRailsのテンプレートと似ています。" },
			{ "Support django style template overriding over plugins.", "Django式のプラグインを越えるテンプレートオーバーライドをサポートします。" },
			{ "Support mobile specialized templates.", "モバイル専用のテンプレートを定義できます。" },
			{ "Support dynamic contents (area-widget model).", "エリア・ウィジェットタイプの動的コンテンツシステム。" },
			{ "Support per-widget render cache for extremely fast rending.", "ウィジェットことの描画結果のキャッシュをサポートします、これによりページの描画速度が大幅に上昇します。" },
			{ "Project toolkits", "プロジェクトツールキット" },
			{ "Provide custom project creator, include command-line and gui version.",
				"独自のプロジェクト作成ツールを提供します、コマンドラインとGUIバージョンを含めます。" },
			{ "Provide custom project publisher, include command-line and gui version.",
				"独自のプロジェクトパブリッシュツールを提供します、コマンドラインとGUIバージョンを含めます。" },
			{ "Demo site", "デモサイト" },
			{ "Provide free and open source default plugins.", "フリーでオープンソースのデフォルトプラグイン集を提供します。" },
			{ "Include admin panel, form builder, CRUD page scaffolding.",
				"管理者パネル、フォームビルダ、CRUDページ生成器を含めます。" },
			{ "Visit demo site (Username: demo, Password: 123456)",
				"デモサイトを開きます (アカウント: demo, パスワード: 123456)" },
			{ "ZKWeb demo site", "ZKWebデモサイト" },
			{ "DemoIndex", "デモインデックス" },
			{ "DemoNavMenu", "デモナビメニュー" }
		};

		public bool CanTranslate(string code) {
			return Codes.Contains(code);
		}

		public string Translate(string text) {
			return Translates.GetOrDefault(text);
		}
	}
}
