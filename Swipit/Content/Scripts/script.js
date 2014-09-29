/* Author:
  Vincent Cifelli

	Look into 
	 LightBox 
	 Custom Scroll Bars

*/

var MCP;
var Shortcuts;
var LightBox;

//CLU is Codified Likeness Utility Helper for MCP
var CLU = function () { };
CLU.prototype.Init = function () {
    console.log(swips);
    //this.PopulatePanels();
};

CLU.prototype.PopulatePanels = function () {
    for (var panel in swips) {
        this.FillPanel(swips[panel])
    }


};

CLU.prototype.FillPanel = function (items) {
    for (var i = 0; i < items.length ; i++) {
        var item = items[i];
        this.PushToPanel(item.panelType, item);
    }
};

CLU.prototype.PushToPanel = function (panelType, item) {
    var html = "<div class='postIt'><div class='hidden postItOptions'> <span class='Delete'>Delete</span></div><div class='postItContent'><span class='itemBody'>" + item.body + "</span></div></div>";
    $("div.panel[data-panel='" + panelType + "']").append(html)

}

CLU.prototype.Add = function (swip) {

    if (swip.length > 1) {
        for (var i = 0; i < swip.length; i++) {
            swips[swip[i].panelType].push(swip[i]);

        }
    }
    else {
        this.PushToPanel(swip.panelType, swip)
    }


};


var swips = {

    "Pending": [],
    "Active": [],
    "Completed": [],
    "Future": []


}

var clu = new CLU();
clu.Init();


var newSwip = {
    panelType: "Pending",
    body: "Finish API to support gestures"

}

var notherSwip = {
    panelType: "Active",
    body: "Work On Async Loading for Swips"

}

var compSwip = {
    panelType: "Completed",
    body: "Establish Broadcast from Server "

}



clu.Add([newSwip, compSwip]);

clu.PopulatePanels();




$(document).ready(function () {

    $("div.panel").bind("click", function () {
        // if($(this).hasClass("focused")){
        // 	$(this).removeClass("focused");
        // 	return false;
        // }

        // $("div.panel").removeClass("selected focused");
        // $(this).addClass("focused")


    })

    $(this).bind('keydown', 'Shift+up', function (evt) {
        $("div.panelView").fadeOut('slow', function () {
            $("div.scrumMap").removeClass("hidden").slideDown();
        });

    });

    $(this).bind('keydown', 'Shift+down', function (evt) {
        $("div.scrumView").fadeIn("50", function () {
            $("div.scrumMap").addClass("hidden");
        });
    });

    $(this).bind('keydown', 'Ctrl+left', function (evt) {
        $("div.panel").removeClass("selected");
        $("div.panel").eq(0).toggleClass("selected");
    });
    $(this).bind('keydown', 'Ctrl+up', function (evt) {
        $("div.panel").removeClass("selected");
        $("div.panel").eq(1).toggleClass("selected");
    });
    $(this).bind('keydown', 'Ctrl+right', function (evt) {
        $("div.panel").removeClass("selected");
        $("div.panel").eq(2).toggleClass("selected");
    });

    $(this).bind('keydown', 'Ctrl+down', function (evt) {
        $("div.panel").removeClass("selected");
        $("div.panel").eq(3).toggleClass("selected");
    });
    //$.bind('keydown', 'Ctrl+left', function (evt){jQuery('#_Ctrl_left').addClass('dirty'); return false; });

    //PostIT Live bidning
    $("div.postIt").live("mouseenter", function () {
        // body...
        $(this).find("div.postItContent").addClass("hidden");
        $(this).find("div.postItOptions").removeClass("hidden");

    }).live("mouseleave", function () {
        $(this).find("div.postItContent").removeClass("hidden");
        $(this).find("div.postItOptions").addClass("hidden");

    })

    $("div.postIt span.Delete").live("click", function (ev) {

        ev.stopPropagation();

        $(this).parents("div.postIt").fadeOut();

    });




})











