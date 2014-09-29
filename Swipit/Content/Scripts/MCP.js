$(function () {
    //var connection = $.connection('echo'); //$.connection('swipit/echo');
    var connection = $.connection('swipit/echo');
    connection.received(function (data) {
        // var json = $.parseJSON(data);
        // console.info($.parseJSON(data));
        clu.Add($.parseJSON(data));

    });

    connection.start();

    $("#broadcast").click(function () {
        connection.send($('#msg').val());

    });

});



