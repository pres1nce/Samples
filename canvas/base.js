function MOD() {
    var something = "true";
}

var canvas, context, toggle;
var circ = 0;
var pause = false;

var objects = [];
var numberofObjects = 5;


build();
init();
animate();

function build() {

    for (var i = 0; i < numberofObjects; i++) {
        objects[i] = new Shape();
    }
}

function init() {
    ///define canvas
    canvas = document.getElementById("canvas");
    context = canvas.getContext('2d');
    canvas.width = 1000;
    canvas.height = 540;
    //place 'data' here 

    //define array of shapes 



}

function animate() {
    requestAnimFrame(animate);
    if (!pause) {
        update();
        draw();
    }


}


function update() {
    //Updates and calculates full path direction up to an set 'frame?' amount
    //Update should just calculate the velocity += the x and y...and current frame?

    for (var i = 0 ; i < numberofObjects; i++) {
        objects[i].Update();
    }

}

function draw() {

    context.clearRect(0, 0, 1500, 1500);
    for (var i = 0 ; i < numberofObjects; i++) {
        objects[i].Draw();
    }
    context.closePath();
    options();


}

function options() {
    circ++;
    if (circ > 100) {
        build();
        circ = 0;
    }
}


