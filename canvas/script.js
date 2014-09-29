
Border_Class = function (canvas) {
    this.x = canvas.x;
    this.y = canvas.y;
}


Border_Class.prototype.build = function () {
    console.log(1);
};


Path_Class = function () {

    this.count = 20;// steps 
    this.path = [];

};

//Setup start postion based on border props
Path_Class.prototype.setStartPoint = function (border) {

};


Path_Class.prototype.build = function () {
    for (var i = 0 ; i < this.count; i++) {

        this.path.push(i);
        //console.log(Math.round(Math.random(0,20)));
    };
}




