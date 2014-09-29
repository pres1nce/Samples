/*
 Slangbox : 0.1
 Author : Vincent Cifelli
 License : Ask for usage
*/


(function ($) {

	$.slang = function (el, radius, options) {
		//Keep context homie
		var base = this;
		// Access to jQuery and DOM versions of element
		base.$el = $(el);
		base.el = el; //reg
		base.$ctr = base.$el.parent(); //container 
		base.slangbox = "";
		base.map = v.m().AccentCharacters;
		// Add a reverse reference to the DOM object
		base.$el.data("slang", base);
		base.glyphIndex = -1;
		base.slangbox.active = false;


		base.init = function () {
			base.radius = radius;
			base.options = $.extend({}, $.slang.defaultOptions, options);
			base.renderSlangBox();

			base.$el.keydown(function (e) {
				if (e.keyCode == 39 || e.keyCode == 37) {
					e.keyCode == 39 ? base.glyphIndex++ : base.glyphIndex--;

					base.moveGlyph(base.glyphIndex);
					base.slangbox.active = true;
					return false;
				}

				if (base.slangbox.active && e.keyCode == 16) {
					base.useGlyph(base.slangbox.find("li.selector").text());
				}
				base.slangbox.active = false;
				base.glyphIndex = 0;
				base.glyphValue = "";
				base.slangbox.find("ul").empty();
				console.info(v.Tipe.input);

				var a = [];
				for (var i in base.map) {
					for (var j = 0; j < i.length; j++) {
						try {
							if (base.map[i][j].keyCode == e.keyCode) {
								a.push(base.map[i][j]);
							}
						}
						catch (err) { }
					}
				}
				for (var i = 0; i < a.length; i++) {
					var li = $("<li>" + a[i].Character + "</li>");
					base.slangbox.find("ul").append(li);
				}
			}).focus(function () {
				console.log(1);
				base.$ctr.find(".slangbox").addClass("glow");
			}).blur(function () {
				base.$ctr.find(".slangbox").removeClass("glow");
			});

			// Put your initialization code here
		};

		base.moveGlyph = function (dir) {
			base.$ctr.find("li").removeClass("selector");
			base.$ctr.find("li:eq(" + dir + ")").addClass("selector");
		};

		base.useGlyph = function (value) {
			var textarea = base.$el.val();
			base.$el.val(textarea.slice(0, textarea.length - 1) + value);
		};

		base.renderSlangBox = function () {
			var slangbox = $("<div class='slangbox'><ul></ul></div>");
			base.$ctr.prepend(slangbox);
			base.slangbox = base.$ctr.find("div.slangbox");
		};

		// Run initializer
		base.init();
	};

	$.slang.defaultOptions = {
		//define default options here...
	};

	$.fn.slang = function (radius, options) {
		return this.each(function () {
			(new $.slang(this, radius, options));
			//IDK if you need this for anything...
		});
	};

})(jQuery);