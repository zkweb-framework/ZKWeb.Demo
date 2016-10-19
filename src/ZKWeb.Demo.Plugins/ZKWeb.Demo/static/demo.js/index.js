/* 延迟显示动画 */

$(function () {
	var visible = function ($elem, delta) {
		var $window = $(window);
		var windowBottom = $window.scrollTop() + $window.height();
		var elementTop = $elem.position().top + delta;
		return elementTop < windowBottom;
	}

	var makeReady = function ($elem) {
		if (!$elem.hasClass("ready")) {
			$elem.addClass("ready");
			$elem.animate({ opacity: 1.0 }, 1000);
		}
	};

	var onScroll = function () {
		$(".introduction").each(function () {
			var $this = $(this);
			if (visible($this, 50) || window.chrome) {
				makeReady($this);
			}
		});
	}

	$(window).scroll(onScroll);
	onScroll();
});