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
        this.ctr, this.Tipe;
    }

    /* Helpers */
    Vuid.prototype.ERROR = function (errorMethod, stringError) {
        console.info(stringError);
    }

    Vuid.prototype.t = function () {
        this.Tipe = new Tipe();

    }

    Vuid.prototype.panel = function () {
        var self = this;

        $(window).resize(function () {
            self.ctr.css({
                "height": $(window).height(),
                "width": $(window).width()
            });
        });


    }

    /*EndOF Helpers*/

    Vuid.prototype.init = function () {
        var $ctr = $("<div class='vuid-core' style='color:#ffffff;display:none;z-index:9999;position:absolute;height:100%;width:100%;overflow-y:hidden;background: rgba(0, 0, 0, 0.9);padding-top:10px;'><div style='margin:0 auto; font-size:30px;width:500px;'>Vuid <br /> <br /><div> Vuid is a Overlay Engine specializing in hot key implementation and UI </div> </div></div>");
        $("body").prepend($ctr).css("overflow-y", "hidden");
        $("div.vuid-core").fadeIn();
        this.ctr = $("div.vuid-core");
        this.panel();
        console.log($("div.vuid-core"));

    }

    Vuid.prototype.open = function (options) {
        if (options == null) {
            this.ERROR(this.close, "Empty Object found");
        }

        console.log(options || this.options);
        this.ctr.fadeIn();


    }

    Vuid.prototype.close = function (options) {
        if (options == null) {
            this.ERROR(this.close, "Empty Object found");
        }

        console.log(options || this.options);
        this.ctr.fadeOut();


    }




})(jQuery);