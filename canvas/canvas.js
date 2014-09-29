

Canvas_Class = function () {

    this.count = 20;// steps 
    this.path = [];
};


Canvas_Class.prototype.build = function () {
    var c = document.getElementById('canvas');
    var ctx = c.getContext('2d');

    for (i = 0; i < 10; i++) {
        ctx.lineWidth = 1 + i;
        ctx.beginPath();
        ctx.moveTo(5 + i * 14, 5);
        ctx.lineTo(5 + i * 14, 140);
        ctx.fillStyle = "#FFA500";
        ctx.strokeStyle = "#FFA500";
        ctx.stroke();
    }

};