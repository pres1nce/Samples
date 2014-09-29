
(function ($) {

    Date.prototype._Months = [
        "January", "Febuary", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"
    ];

    Date.prototype.formatShortDate = function () {
        var d = this;

        return d.getDate() + "/" + (d.getMonth() + 1) + "/" + d.getFullYear();
    };

    Date.prototype.formatLongDate = function () {
        var d = this;
        return d._Months[d.getMonth()] + " " + d.getDate() + ", " + d.getFullYear();
    };

    $.fn.boundingBox = function () {
        var el = $(this);

        var h = el.height();
        var w = el.width();
        var offset = el.offset();

        return {
            height: h,
            width: w,
            top: offset.top,
            right: offset.left + w,
            bottom: offset.top + h,
            left: offset.left
        };
    };

    $.fn.outerBoundingBox = function () {
        var el = $(this);

        var h = el.outerHeight();
        var w = el.outerWidth();
        var offset = el.offset();

        return {
            height: h,
            width: w,
            top: offset.top,
            right: offset.left + w,
            bottom: offset.top + h,
            left: offset.left
        };
    };


    $.fn.showAjaxLoader = function (notOver) {
        var el = $(this);
        var p = el.parent();
        if (!el.length || !p.length) { return; }

        var loader = p.find("div.loader");
        if (!loader || loader.length == 0) {
            loader = $('<div class="loader hidden"><div class="loaderImg"></div></div>');
            p.append(loader);
        }

        var pos = el.boundingBox();

        var x = pos.left;
        var y = pos.top;
        var height = el.outerHeight(true);
        var width = el.outerWidth(true);

        loader.css("top", y + "px");
        loader.css("left", x + "px");
        loader.width(width);
        loader.height(height);
        loader.fadeIn(1000, function () {
            loader.hideAjaxLoader(true);
        });
        loader.removeClass("hidden");
        return this;
    }

    $.fn.hideAjaxLoader = function (isFancy) {
        var el = $(this);
     
        if (el && el.length) {
            var loader = el.parent().find("div.loader");
            if (!loader || loader.length == 0) { return; }

            if (isFancy) {
                el.fadeOut(1000);
            } else {
                el.remove();
            }
        }

       
    }

} (jQuery));

