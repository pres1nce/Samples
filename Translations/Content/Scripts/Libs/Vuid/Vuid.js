/*!
 * Vuid v0.1
 * www.vinnix.com
 *
 * Copyright (c) Vincent Cifelli
 * Available under the BSD and MIT licenses: www.vinnix.com/license
 */

/*
 * Vuid is a Overlay Engine specializing in hot key implementation and UI 
 *  Requires:jQuery
 *  Dependendts: Tipe
 * Authors        Vincent Cifelli
 * Contributors   //
 */

(function ($) {

	Vuid = function () {
		this.options = { "Fancy": true };
		this.ctr = "div.vuid-core";
		this.inputBar = $(this.ctr).find("input.searchbar");
		this.Tipe = new Tipe();
		this.alive = false;
		this.overlay = {
			"max": 999,
			"min": 0,
			"target": ""
		}

	}



	Vuid.prototype.m = function () {
		return new Mappr();
	}


	/* Helpers */
	Vuid.prototype.ERROR = function (errorMethod, stringError) {
		console.info(stringError);
	}

	Vuid.prototype.t = function () {

	}

	Vuid.prototype.panel = function () {
		var self = this;

		$(window).resize(function () {
			$(self.ctr).css({
				"height": $(window).height(),
				"width": $(window).width()
			});
		});


	}

	/*EndOF Helpers*/

	Vuid.prototype.init = function () {
		var $ctr = $("<div class='vuid-core' style='color:#ffffff;display:none;z-index:99;position:absolute;height:100%;width:100%;overflow-y:hidden;background: rgba(0, 0, 0, 0.9);padding-top:10px;'><div style='margin:0 auto; font-size:30px;width:500px;'> <br /> <br /><div>  </div> </div></div>");

		$("body").prepend($ctr).css("overflow-y", "hidden");
		// $("div.vuid-core").fadeIn();
		this.Tipe.listen();
		this.Tipe.vuid = this;
		this.panel();
		this.inputBar = $(this.ctr).find("input.searchbar");
		console.log($("div.vuid-core").toString() + "loaded");




	}

	Vuid.prototype.open = function (options) {
		if (options == null) {
			this.ERROR(this.close, "Empty Object found");
		}

		console.log(options || this.options);
		$(this.ctr).fadeIn();
		this.alive = true;

		$(this.ctr).find("input.searchbar").val('');
		this.identify(true, $("footer div.errors"));



	}

	Vuid.prototype.sift = function (action, options) {




	}


	Vuid.prototype.identify = function (action, target, options) {
	
		if (action) {
			target.addClass("glow");
			target.css("z-index", this.overlay.max);
			target.focus();
		}
		else {
			target.removeClass("glow");
			target.css("z-index", this.overlay.min);
		}

	}



	Vuid.prototype.close = function (options) {
		var self = this;
		if (this.options == null) {
			this.ERROR(this.close, "Empty Object found");
		}

		console.log(options || this.options);
		// this.Tipe.sleep();
		this.alive = false;
		
		$(this.ctr).find("input.searchbar").val('');
		this.identify(false, $("footer div.errors"));
		this.identify(false, $("footer div.slangbox-container"));

		$(this.ctr).fadeOut();


	}




})(jQuery);