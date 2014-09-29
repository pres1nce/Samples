var stocks = [{ "Status": "SUCCESS", "Name": "Apple Inc", "Symbol": "AAPL", "LastPrice": 606, "Change": -0.940000000000055, "ChangePercent": -0.154875275974575, "Timestamp": "Wed Jul 18 15:59:51 UTC-04:00 2012", "MarketCap": 566647572000, "Volume": 713186, "ChangeYTD": 405, "ChangePercentYTD": 49.6296296296296, "High": 608.26, "Low": 603.58, "Open": 606.39 }, { "Status": "SUCCESS", "Name": "The Cheesecake Factory Inc", "Symbol": "CAKE", "LastPrice": 31.91, "Change": 0.0500000000000007, "ChangePercent": -0.156445556946183, "Timestamp": "Fri Jun 29 15:59:00 UTC-04:00 2012", "MarketCap": 1723459100, "Volume": 102652, "ChangeYTD": 29.35, "ChangePercentYTD": 8.72231686541737, "High": 32.02, "Low": 31.46, "Open": 31.68 }, { "Status": "SUCCESS", "Name": "Microsoft Corp", "Symbol": "MSFT", "LastPrice": 30.58, "Change": -0.0100000000000016, "ChangePercent": -0.0326904217064494, "Timestamp": "Fri Jun 29 15:59:59 UTC-04:00 2012", "MarketCap": 256898482280, "Volume": 4566049, "ChangeYTD": 25.96, "ChangePercentYTD": 17.7966101694915, "High": 30.68, "Low": 30.14, "Open": 30.44 }, { "Status": "SUCCESS", "Name": "Microsoft Corp", "Symbol": "MSFT", "LastPrice": 30.58, "Change": -0.0100000000000016, "ChangePercent": -0.0326904217064494, "Timestamp": "Fri Jun 29 15:59:59 UTC-04:00 2012", "MarketCap": 256898482280, "Volume": 4566049, "ChangeYTD": 25.96, "ChangePercentYTD": 17.7966101694915, "High": 30.68, "Low": 30.14, "Open": 30.44 }, { "Status": "SUCCESS", "Name": "Microsoft Corp", "Symbol": "MSFT", "LastPrice": 30.58, "Change": -0.0100000000000016, "ChangePercent": -0.0326904217064494, "Timestamp": "Fri Jun 29 15:59:59 UTC-04:00 2012", "MarketCap": 256898482280, "Volume": 4566049, "ChangeYTD": 25.96, "ChangePercentYTD": 17.7966101694915, "High": 30.68, "Low": 30.14, "Open": 30.44 }];


var state = "title";
var musicBg = new buzz.sound('PortPort/Content/sounds/intro.mp3');
var musicBg = new buzz.sound('PortPort/Content/sounds/intro.mp3');

var flyingMusic = new buzz.sound('PortPort/Content/sounds/running.mp3');

var startSound = new buzz.sound('PortPort/Content/sounds/start.wav');
musicBg.play();
musicBg.setVolume(30);
var swing = new buzz.sound('PortPort/Content/sounds/swing.wav');

var loseHorn = new buzz.sound('PortPort/Content/sounds/losingHorn.mp3');
var holeIn = new buzz.sound('PortPort/Content/sounds/GolfBallInHole.mp3');
var holePlayed = false;

var stockStart = 0;
var numberOfStrikes = 0;
var changeType = stocks[stockStart].Change > 0 ? "Positive" : "Negative";
var gameWait = 0;


var flag = new Image();
flag.src = "http://www.vinnix.com/img/flag.png";

var intro = new Image();
intro.src = "http://www.vinnix.com/img/introGround.png";

var gameBG = new Image();
gameBG.src = "http://www.vinnix.com/img/gameBG.png"

var endSwing = new Image();
endSwing.src = "http://www.vinnix.com/img/swingEnd.png"



var nyanCat = false;
var blipped = false;

$("document").ready(function () {


    $(document).bind('keydown', '1', function (evt) {
        buildOutSymbolContent();
        $("div.club").hide();
        state = "flyby";
        flyingMusic.play();
        flyingMusic.setTime(23)
        frame = 0;

        $("#nyanCatCtr").show();

    });

    $("div.club").bind("click", function () {
        buildOutSymbolContent();
        $(".stockListBlip").hide();
        $("div.club").hide();
        blipped = false;
        state = "flyby";
        flyingMusic.play();
        flyingMusic.setTime(23)
        frame = 0;
        gameWait = 0;
        $("#nyanCatCtr").show();



    });

    $(".symbolContent").bind("click", function () {

        state = "game";
        frame = 0;
        $(".club").show();

        $(".symbolContent").hide();

    });

    var cvs = $("#myCanvas");
    var pugImg = $("div.pug");

    // Get a reference to the element.
    var elem = document.getElementById('myCanvas');
    // Always check for properties and methods, to make sure your code doesn't break 
    // in other browsers.
    if (elem && elem.getContext) {
        // Get the 2d context.
        // Remember: you can only initialize one context per element.
        var context = elem.getContext('2d');
        if (context) {
            // You are done! Now you can draw your first rectangle.
            // You only need to provide the (x,y) coordinates, followed by the width and 
            // height dimensions.
            context.fillStyle = '#3e3e3e'; // blue
            context.fillRect(0, 0, cvs.width(), cvs.height());
        }
    }





 // example code from mr doob : http://mrdoob.com/lab/javascript/requestanimationframe/
    window.requestAnimFrame = (function () {
        return window.requestAnimationFrame ||
              window.webkitRequestAnimationFrame ||
              window.mozRequestAnimationFrame ||
              window.oRequestAnimationFrame ||
              window.msRequestAnimationFrame ||
              function (/* function */callback, /* DOMElement */element) {
                  window.setTimeout(callback, 1000 / 60);
              };
    })();





    var canvas, context;
    init();
   
    function init() {
        flag.onload = function () {
            intro.onload = function () {
                animate();
            }
        };
    }

    var clubStart = false;

    state = "title";
    function animate() {
        requestAnimFrame(animate);
        if (state == "title") {
            draw();
        }
        else if (state == "game") {
            drawGame();
        }
        else if (state == "flyby") {
            drawFlying();
        }
        else if (state == "swingEnd") {
            drawLanding();

            //if port is bad investment 
            //drawSink();
        }
        else if (state == "gameOver") {
            gameOver();
        }



    }
    var frame = 0;
    var flagFrame = 0;
    var isTitle = false;
    var startClick = false;
    var startClickLife = 0;

    function draw() {
        if (frame >= 100) {
            if (!isTitle) {
                $("div.pug").fadeIn(500);
                isTitle = true;
                flagFrame = 100;
            }
        }


        if (frame >= 150) {
            $("div.start").css("display", "block");
        }

        var yy = 0;
        var y = 500;



        if (startClick) {
            startClickLife == 0 ? startSound.play() : null;
            if (startClickLife <= 60) {
                if (startClickLife % 8 == 0) {
                    $("div.start").hide();
                }
                else {
                    $("div.start").show();
                }
            } else {
                state = "game";
                musicBg.stop();
                $("div.titleContent").remove();



            }
            startClickLife++

        }



        context.fillStyle = '#3e3e3e'; // blue
        context.fillRect(0, 0, cvs.width(), cvs.height());
        context.drawImage(intro, 40, 300);
        var flagAlive = isTitle ? 100 + 20 : (frame + 20);
        context.drawImage(flag, 380, 420 - flagAlive);
        context.fillStyle = 'rgb(255,0,0)';
        //        context.beginPath();
        //        context.arc(x, y, 20, 0, Math.PI * 2, true);
        //        context.closePath();
        context.fill();


        frame++;


    }




    function drawGame() {
        if (!clubStart) {
            $("div.club").show();
            clubStart = true;
        };

        context.fillStyle = '#3e3e3e'; // blue
        context.fillRect(0, 0, cvs.width(), cvs.height());
        context.fillStyle = 'rgb(255,0,0)';
        //        context.beginPath();
        //        context.arc(x, y, 20, 0, Math.PI * 2, true);
        //        context.closePath();
        context.fill();
        context.drawImage(gameBG, 0, 0, cvs.width(), cvs.height());

        if (gameWait == 100 && !blipped) {
            ShowCurrentStockBlip();
            blipped = true;
        }



        gameWait++;



    }

    function drawFlying(argument) {

        context.fillStyle = '#1D5082'; // flyby sky color
        context.fillRect(0, 0, cvs.width(), cvs.height());
        context.fillStyle = 'rgb(255,0,0)';
        // Golf ball

        context.beginPath();

        if (frame * 3 < cvs.width()) {
            context.arc(frame * 3, cvs.height() / 2, 10, 0, Math.PI * 2, true);
        }
        else {
            $("#nyanCatCtr").hide();
            state = "swingEnd";
            frame = 0;
            flyingMusic.stop();
        }




        context.closePath();
        context.fill();





        frame++;



    }

    function gameOver() {
        context.fillStyle = '#3e3e3e'; // blue
        context.fillRect(0, 0, cvs.width(), cvs.height());
        context.fillStyle = 'rgb(255,0,0)';
        //        context.beginPath();
        //        context.arc(x, y, 20, 0, Math.PI * 2, true);
        //        context.closePath();



    }



    function drawLanding(argument) {

        var golfBallMove = -(frame);

        context.fillStyle = '#3e3e3e'; // blue
        context.fillRect(0, 0, cvs.width(), cvs.height());
        context.fillStyle = 'rgb(255,0,0)';
        context.drawImage(endSwing, 0, 0, cvs.width(), cvs.height());

        context.beginPath();

        //if (stocks[stockStart].Change > 0) {


        if (frame < 175) {

            if (frame == 50 && stocks[stockStart - 1].Change < 0) {

                frame = 200;
                loseHorn.play();


                if (numberOfStrikes == 3) {

                    $("li.strike3").css("display", "block");
                    state = "gameOver";
                } else {

                    $("div.symbolContent").fadeIn(500);
                    $("div.strikeZone").show();
                    $("li.strike").eq(numberOfStrikes - 1).css("display", "block");
                }


            }

            context.arc(350, cvs.height() + golfBallMove, 10, 0, Math.PI * 2, true);

        }
        else {

            $("div.symbolContent").fadeIn(500);
            $("div.strikeZone").show();
            if (!holePlayed) {
                holeIn.play();
                holePlayed = true;
            }


        }










        context.closePath();
        context.fill();




        frame++;





    }





    $("div.pug img").bind("click", function () {
        swing.play();
        musicBg.setVolume(20);
    });



    $("div.start img").bind("click", function () {
        startClick = true;

    });










});





function buildOutSymbolContent() {



    changeType = stocks[stockStart].Change > 0 ? "Positive" : "Negative";
    var symbolCrap = "<li class='" + changeType + "'> Last Close : " + stocks[stockStart].Change.toFixed(2) + "</li>";
    $("div.symbolContent h2").html(stocks[stockStart].Name);
    $("div.symbolContent ul").html(symbolCrap);

    if (changeType == "Negative") {

        numberOfStrikes++;
    }

    stockStart++;







}



function ShowCurrentStockBlip() {

    var i = stockStart;
    var sym = stocks[stockStart].Symbol;

    $(".stockListBlip").html("<h2 class='" + stocks[i]["Symbol"] + "'>" + stocks[i]["Symbol"] + "</h2>");



    $(".stockListBlip").fadeIn();

    blipped = true;


}


function SwingBall(data) {

    if (data.event == 0) {
        console.log("Aweshum");
    }


    if (data.event == 1) {
        $("div.club").trigger("click");


    }
    else if (data.event == 2) {
        stockStart++;
        $(".stockListBlip").hide();
        ShowCurrentStockBlip();
    }
    else if (data.event == 3) {
        $("div.symbolContent").trigger("click");

    }



}






function BuildPortList() {

    for (var i = 0; i < stocks.length; i++) {

        $(".stockListBlip ul").append("<li class='" + stocks[i]["Symbol"] + "'>" + stocks[i]["Symbol"] + "</li>");
    };
}


BuildPortList();
