/*!
* Tipe v0.1
* www.vinnix.com
*
* Copyright (c) Vincent Cifelli
* Available under the BSD and MIT licenses: www.vinnix.com/license
*/

/*
* Vuid is a Overlay Engine specilizing in hot key ipmlementation and UI 
*  Requires:jQuery
*  Dependendts: Tipe
* Authors        Vincent Cifelli
* Contributors   //
*/

(function ($) {

	Tipe = function (vuid) {
		console.log("Tipe Init");
		this.vuid;
		this.input = [];
	};


	//Definitive mapping for combinations
	Tipe.prototype.map = {
		"16,49": "launch preview",
		"16,50": "Focus : Duplicate Errors",
		"16,51": "Focus : SlangBox"
	};


	Tipe.prototype.thought = function () {

		return this.input.toString();

	}

	Tipe.prototype.action = function () {

		return this.input.toString();

	}


	Tipe.prototype.listen = function () {
		var self = this;
		$(window).on("keydown", function (e) {
			self.input.push(e.keyCode);
			console.log(self.thought());

			//Pull mapping out and make listener a middle interface to actions
			if (self.thought() == "16,86") {
				console.info(v.alive);
				v.alive ? v.close() : v.open();
			}

			if (self.thought() == "16,49" || self.thought() == "16,50") {
				//v.alive ? alert(self.map[self.thought()]) : "false";
			}

			if (self.thought() == "16,51") {
				v.identify(true, $("footer div.slangbox-container"));

			}


		});

		$(window).on("keyup", function (e) {
			self.input = [];
		});

		Tipe.prototype.sleep = function () {
			$(window).off("keydown");
			$(window).off("keyup");
		}

	}





})(jQuery);