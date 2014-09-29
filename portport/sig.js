
$(function () {
    //var connection = $.connection('echo'); //$.connection('swipit/echo');
    var connection = $.connection('portport/echo');
    connection.received(function (data) {
        // var json = $.parseJSON(data);
        // console.info($.parseJSON(data));
        //console.log($.parseJSON(data));
   
        SwingBall($.parseJSON(data));

    });

    connection.start();



});