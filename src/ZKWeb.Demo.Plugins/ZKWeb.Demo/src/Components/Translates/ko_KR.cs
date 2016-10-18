using System.Collections.Generic;
using ZKWeb.Localize;
using ZKWebStandard.Extensions;
using ZKWebStandard.Ioc;

namespace ZKWeb.Demo.Plugins.ZKWeb.Demo.src.Components.Translates {
	/// <summary>
	/// 韩语翻译
	/// </summary>
	[ExportMany, SingletonReuse]
	public class ko_KR : ITranslateProvider {
		private static HashSet<string> Codes = new HashSet<string>() { "ko-KR" };
		private static Dictionary<string, string> Translates = new Dictionary<string, string>()
		{
			{ "Demo", "표시" },
			{ "Trun website into demo mode", "데모 모드를 활성화 사이트" },
			{ "Edit or delete demo account is not allowed", "편집하거나 데모 계정을 삭제할 수 없습니다" },
			{ "View on GITHUB", "GitHub의에서보기" },
			{ "Flexible web framework support .Net Framework and .Net Core",
				"유연한 웹 프레임 워크 지원 .Net Framework 및 .NET Core" },
			{ "Dynamic Plugins", "동적 플러그인" },
			{ "Powered by roslyn, support automatic recompile after source code changed.",
				"소스 코드를 변경 한 후 Roslyn의에 의해 구동, 자동 재 컴파일을 지원。" },
			{ "Disinct Web Layer", "사용자 지정 웹 층" },
			{ "Support running on Asp.Net, Asp.Net Core, Owin, no code difference.",
				"지원 Asp.Net, Asp.Net Core, Owin, 아니 코드의 차이에서 실행。" },
			{ "Custom MVC routing and pipeline.", "사용자 정의 MVC 라우팅 및 파이프 라인。" },
			{ "Multiple ORM Support", "여러 ORM 지원" },
			{ "Support EFCore, NHibernate, Dapper, MongoDB, InMemory, through the same interface.",
				"통합 인터페이스 동작을 통해 지원 EFCore, NHibernate, Dapper, MongoDB, InMemory。" },
			{ "Powered by roslyn, support c# 6.0 features.", "C # 6.0의 특징은, Roslyn의에 의해 지원 강화。" },
			{ "Support runtime plugin reload.", "위젯 런타임 지원을 다시로드。" },
			{ "Support automatic recompile and reload after plugin source code changed.",
				"자동 재 컴파일을 지원하고 플러그인 소스 코드를 변경 한 후 다시로드。" },
			{ "Directory based, small memory footprint.", "디렉토리를 기반으로, 작은 메모리 풋 프린트。" },
			{ "Support automatic database scheme migration for EFCore and NHibernate.",
				"데이터 테이블을 추가하고 갱신 할 때 사용 EF Core, NHibernate에 자동 지원。" },
			{ "Dotliquid Template", "Dotliquid 템플릿 엔진" },
			{ "Dotliquid template engine, django and rails like syntax.", "비슷한 django 와 rails 템플릿 문법과 DotLiquid 템플릿 엔진。" },
			{ "Support django style template overriding over plugins.", "플러그인을 통해 지원 django 스타일 템플릿 최우선。" },
			{ "Support mobile specialized templates.", "이것은 템플릿 파일의 모바일 전용 버전의 정의를 지원。" },
			{ "Support dynamic contents (area-widget model).", "지역 지원 - 동적 콘텐츠 모델에 대한 시스템 구성 요소。" },
			{ "Support per-widget render cache for extremely fast rending.", "극적 드로잉 페이지의 성능을 향상 도면 요소의 결과를 캐싱하여 지원。" },
			{ "Project toolkits", "프로젝트 툴킷" },
			{ "Provide custom project creator, include command-line and gui version.",
				"커맨드 라인 버전 및 UI 버전을 포함하여 독립적 인 프로젝트 제작 도구를 제공。" },
			{ "Provide custom project publisher, include command-line and gui version.",
				"명령 줄 버전과 UI 버전을 포함하여, 독립적 인 프로젝트 게시 도구를 제공합니다。" },
			{ "Demo site", "샘플 사이트" },
			{ "Provide free and open source default plugins.", "무료 및 오픈 소스 기본 플러그인을 제공。" },
			{ "Include admin panel, form builder, CRUD page scaffolding.",
				"관리 패널, 폼 빌더 및 삭제를 포함하면 검색 페이지 빌더를 변경하려면。" },
			{ "Visit demo site (Username: demo, Password: 123456)",
				"방문 데모 사이트 (사용자 이름: demo, 비밀번호: 123456)" }
		};

		public bool CanTranslate(string code) {
			return Codes.Contains(code);
		}

		public string Translate(string text) {
			return Translates.GetOrDefault(text);
		}
	}
}
