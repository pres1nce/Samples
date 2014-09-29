FormUploadModule = function () { }
FormUploadModule.Extend(Module);
FormUploadModule.prototype.initModule = function () { }
FormUploadModule.prototype.action_close = function (e, val) {
    var $form = this.$module.find("form");
    $form.addClass("hide");
}


/*
    Translation Tool Controller : User as Base
*/
var TranslationTool = function () { };
TranslationTool.prototype.something = function () { alert(1); }



var Translation = function (options) {
    this.options = options;
    this.Init();
    //Widen the Page
    $("body").attr("style", "width: auto !important;");
    $("#chartmodule-1").attr("class", "");
    this.targetTranslation = "";
    this.addressBarValue = "";
    this.dialog();
    this.previewPath = "Preview";
    this.submitPath = "Translations/Upload";
};

Translation.prototype.Init = function (options) {

    var self = this;
    this.forumUpload = new FormUploadModule();//.init("div.form");
    this.forumUpload.init("div.form");


    if (this.options["addressBar"]) {
        this.addressBar();
    }


    $(".submitButton").bind("click", function () {
        $("form.excelUpload").removeClass("hide");
    });

    $("a.vuid").bind("click", function () {
        window.v = new Vuid();
        window.v.init();
        window.v.open();
        $(this).toggleClass("btn-success");

    });


    $("button.preview").bind("click", function (e) {

        e.preventDefault();
        $("form#myForm1").attr("action", WSOD.TranslationHarness.previewPath);
        $("button.submit").trigger("click");
        $("form#myForm1").attr("action", WSOD.TranslationHarness.submitPath);


    });




    $('span.close').bind('click', function () {
        window.location = self.addressBarValue;
    });


}


Translation.prototype.addressBar = function () {
    this.linkValue = $('iframe');
    $('.addressbar').val(this.linkValue.attr('src'));

    $('a.langLink').bind('click', function (e) {
        e.preventDefault();
        e.linkValue = $(this).attr('href');

        linkValue.attr('src', e.linkValue);
        $('.addressbar').val(e.linkValue);
    });

    $(".languageSelect").change(function (e) {
        self.targetTranslation = $(this).find("option:selected").val().toLowerCase();
        self.addressBarValue = baseURL + "&language=" + self.targetTranslation;
        $("iframe").attr("src", baseURL + "&language=" + self.targetTranslation);

        $("input.addressbar").val(baseURL + "&language=" + self.targetTranslation);

    });

    $('.addressbarButton').bind('click', function (e) {
        $url = $("input.addressbar").attr("value");
        linkValue.attr('src', $url);
        self.addressBarValue = $url;


    });
}


Translation.prototype.dialog = function () {
    var body = $("body");
}


Translation.prototype.renderErrors = function (errors) {

    for (var i = 0; i < errors.length; i++) {

        var error = errors[i];
        var message = error.Error != null ? error.Error : "error";
        var html = $("<div rowValue='" + error.Id + "'>" + error.Id + ":/ " + message + "</div>");
        $("div.errors").append(html);
    }

    $("div.errors").removeClass("hide");

    $("div.errors div").on("click", function () {
        var rowValue = parseInt($(this).attr("rowValue"));
        grid.scrollRowIntoView(rowValue);

    });




}


Translation.prototype.output = function (responseText) {


    WSOD.output = responseText;
    s = new SpreadSheet(WSOD.output);
    WSOD.spreadSheet = s;
    var example = s.BuildCells();
    var rows = s.BuildRows();
    var errors = s.BuildErrors();

    console.log(errors);


    $("#output1").append($("<div id='myGrid' style='width:100%;height:720px;'> </div>"));

    grid = new Slick.Grid("#myGrid", rows, s.columns, options);
    WSOD.TranslationHarness.renderErrors(errors);

}



/*
 SpreadSheet 
*/


var SpreadSheet = function (input) {
    this.columns = this.BuildColumns(input.Row.Columns);

};

SpreadSheet.prototype.ShowRowCount = function () {


    return this.columns.unshift({ id: "#", name: "#", field: "#", "width": 20 });

}

SpreadSheet.prototype.BuildColumns = function (obj) {

    var colArray = [];


    for (var i = 0; i < obj.length; i++) {
        var col = { id: obj[i], name: obj[i], field: obj[i], "width": 400 };
        colArray.push(col);
    }

    return colArray;

};


SpreadSheet.prototype.BuildRows = function () {
    var rows = [];

    options.showRowCount ? this.ShowRowCount() : false;
    var output = WSOD.output;

    for (var r = 0; r < output.Rows.length; r++) {
        var row = {};
        var cell = output.Rows[r];
        var colValue = "";

        for (var i = 0; i < this.columns.length; i++) {
            colValue = this.columns[i];
            if (colValue.name == "#") {
                row[colValue.name] = r;


            } else {
                var ii = options.showRowCount ? i - 1 : i;

                try {
                    var val = cell.Cells[ii].Value
                } catch (e) {
                    val = "error";
                }


                if (r == 371) {
                    console.info("HI");
                }

                row[colValue.name] = val;
            }
        }

        rows.push(row);
    }

    return rows;

};


SpreadSheet.prototype.BuildErrors = function () {
    var rows = [];
    //options.showRowCount ? this.ShowRowCount() : false;
    var output = WSOD.output;

    for (var r = 0; r < output.Errors.length; r++) {
        var row = output.Errors[r];
        rows.push(row);
    }

    return rows;

};

SpreadSheet.prototype.BuildCells = function (obj) {

    var cellArray = [];

    for (var i = 0; i < 5 ; i++) {
        var cell = {
            "EN": "1",
            "FR": "2",
            "JP": "3",
        };
        cellArray.push(cell);
    }

    return cellArray;

};




var options = {
    enableCellNavigation: true,
    enableColumnReorder: false,
    forceFitColumns: false,
    showRowCount: true
};






/*
End of SpreadSheet
*/


/*

Ajax Form Handler

*/

// pre-submit callback 
function showRequest(formData, jqForm, options) {
    var output = $("div.output");
    output.children().hide();
    output.addClass("buffer");



    // formData is an array; here we use $.param to convert it to a string to display it 
    // but the form plugin does this for you automatically when it submits the data 
    var queryString = $.param(formData);

    // jqForm is a jQuery object encapsulating the form element.  To access the 
    // DOM element for the form do this: 
    // var formElement = jqForm[0]; 

    // console.info('About to submit: \n\n' + queryString);

    // here we could return false to prevent the form from being submitted; 
    // returning anything other than false will allow the form submit to continue 
    return true;
}

// post-submit callback 
function showResponse(responseText, statusText, xhr, $form) {
    // for normal html responses, the first argument to the success callback 
    // is the XMLHttpRequest object's responseText property 

    // if the ajaxForm method was passed an Options Object with the dataType 
    // property set to 'xml' then the first argument to the success callback 
    // is the XMLHttpRequest object's responseXML property 

    // if the ajaxForm method was passed an Options Object with the dataType 
    // property set to 'json' then the first argument to the success callback 
    // is the json data object returned by the server

    WSOD.output = responseText;

    s = new SpreadSheet(WSOD.output);
    WSOD.spreadSheet = s;
    var example = s.BuildCells();
    var rows = s.BuildRows();
    var errors = s.BuildErrors();

    console.log(errors);
    var output = $("div.output");

    output.removeClass("buffer");
    output.children().show();


    $("#output1").append($("<div id='myGrid' style='width:100%;height:720px;'> </div>"));

    grid = new Slick.Grid("#myGrid", rows, s.columns, options);
    WSOD.TranslationHarness.renderErrors(errors);

    WSOD.TranslationHarness.forumUpload.action_close();

}





/*

DOCUMENT LOAD

*/

$(document).ready(function () {
    WSOD.output = "";
    var tOptions = {
        "Something": true,
        "Address": false
    };
    WSOD.TranslationHarness = new Translation(tOptions);
    var options = {
        target: '#output1',   // target element(s) to be updated with server response 
        beforeSubmit: showRequest,  // pre-submit callback 
        success: showResponse  // post-submit callback 

        // other available options: 
        //url:       url         // override for form's 'action' attribute 
        //type:      type        // 'get' or 'post', override for form's 'method' attribute 
        //dataType:  null        // 'xml', 'script', or 'json' (expected server response type) 
        //clearForm: true        // clear all form fields after successful submit 
        //resetForm: true        // reset the form after successful submit 

        // $.ajax options can be used here too, for example: 
        //timeout:   3000 
    };

    // bind form using 'ajaxForm' 
    $('#myForm1').ajaxForm(options);

});

