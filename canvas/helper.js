var data = [], totalPoints = 30, brightness = 255, currentVeloctiy = 1;

function getRandomData(lastPoint) {
    if (data.length > 0)
        data = data.slice(1);

    // do a random walk
    while (data.length < totalPoints) {

        var prev = data.length > 0 ? data[data.length - 1] : 50;
        var y = Math.random() * 400;
        if (y < 0)
            y = 0;
        if (y > 100)
            //y = 100;
            data.push(Math.floor(y));
    }
    // zip the generated y values with the x values
    var res = [];
    for (var i = 0; i < data.length; ++i)
        res.push([i, data[i]])

    res[0][1] = 500;
    res[totalPoints - 1][1] = 500;
    return res;
}

//**
//Shape
//

function randomRGB() {

    return 'rgba(' + Math.floor(Math.random() * brightness) + ',' + Math.floor(Math.random() * brightness) + ',' + Math.floor(Math.random() * brightness) + ',' + Math.random().toFixed(2) + ')';
}

function Shape() {
    var direction = Math.round(Math.random() * 1) ? -1 : 1;
    this.x = 20,
    this.y = 20,

    this.velocity = Math.round(Math.random() * currentVeloctiy) * direction,
    this.path = getRandomData(),
    this.fillStyle = randomRGB();

}

Shape.prototype.Size = function () {

};


Shape.prototype.Update = function () {
    //Calculate the difference of velocity to x/y
    this.x = this.x + this.velocity;
}

Shape.prototype.Draw = function () {
    //Draw the shape to the canvas
    context.beginPath();

    for (var i = 0; i < this.path.length; i++) {
        var slice = this.path[i];
        var diff = 300 * this.path[i][0] + this.x;
        var obj = { x: i + diff, y: slice }
        context.lineTo((i + diff), slice[1]);
    }
    context.fillStyle = this.fillStyle;
    context.fill();
    context.closePath();



}

////
////Tester
////
var Tester = function (focus) {
    this.focus = focus;
}


Tester.prototype.Update = function (param, val) {
    for (var i = 0; i < param.length; i++) {
        var selected = param[i];
        if (selected.id == "velocity") {
            currentVeloctiy = parseInt(param[i].value);
        }

        if (selected.id == "width") {
            //canvas.height = parseInt(param[i].value);
        }

        if (selected.id = "brightness") {
            //brightness = parseInt(param[i].value);
        }

    }
};



