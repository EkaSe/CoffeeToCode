var CanvasView = 
{
    documentBody: document.body,
    context: document.getElementById('canvas').getContext("2d"),
    canvas: document.getElementById('canvas'),
    clearButton: document.getElementById('clear-button'),
    saveButton: document.getElementById('save-button'),

    clearRect() 
    {
        context.clearRect(0, 0, context.canvas.width, context.canvas.height);
    },

    setLineStyle(strokeStyle, loneJoin, lineWidth)
    {
        context.strokeStyle = strokeStyle;
        context.lineJoin = lineJoin;
        context.lineWidth = lineWidth;
    },

    beginPath()
    {
        this.context.beginPath();
    },

    closePath()
    {
        this.context.closePath();
    },

    stroke()
    {
        this.context.stroke();
    },

    moveTo(x, y)
    {
        this.context.moveTo(x, y);
    },

    lineTo(x, y)
    {
        this.context.lineTo(x, y);
    },


    getRelativeCoordinates(e)
    {
        var rect = canvas.getBoundingClientRect();

        var xCanvas = (e.pageX - rect.left) / canvas.scrollWidth;
        var yCanvas = (e.pageY - rect.top) / canvas.scrollHeight;

        return { x: xCanvas, y: yCanvas };
    },

    getCoordinates(clickX, clickY)
    {
        var xGlobal = clickX * canvas.width;
        var yGlobal = clickY * canvas.height;

        return { x: xGlobal, y: yGlobal };
    }
};

var CanvasModel = {
    getSavedData: function(onSuccessFunction){
        $.getJSON(
            "/savedData",
            onSuccessFunction);
    },

    saveCanvasData(clicks)
    {
        var xhttp = new XMLHttpRequest();
        xhttp.open("POST", "/canvas/save", true);
        xhttp.setRequestHeader("Content-type", "application/json");
        xhttp.send(JSON.stringify(clicks));
    } 
}

var CanvasController = {
    clicks: new Array(),
    paint: false,

    redraw()
    {
        CanvasView.clearRect();
        CanvasView.setLineStyle("#df4b26", "round", 5);

        var previousPoint = undefined;

        clicks.forEach(function (click)
        {
            CanvasView.beginPath();

            var point = CanvasView.getCoordinates(click.x, click.y);

            if (click.drag && previousPoint)
            {
                CanvasView.moveTo(previousPoint.x, previousPoint.y);
            }
            else
            {
                CanvasView.moveTo(point.x, point.y);
            }
            CanvasView.lineTo(point.x, point.y);

            CanvasView.closePath();
            CanvasView.stroke();

            previousPoint = point;
        });
    },
    
    documentBodyOnload(e)
    {
        CanvasModel.getSavedData(
            function (data)
            {
                clicks = data;
                CanvasController.redraw();
            });
    },

    canvasMousedown(e)
    {
        paint = true;

        var point = CanvasView.getRelativeCoordinates(e);

        addClick(point.x, point.y);

        redraw();
    },

    canvasMousemove(e)
    {
        if (paint)
        {
            var point = CanvasView.getRelativeCoordinates(e);

            addClick(point.x, point.y, true);

            redraw();
        }
    },

    canvasMouseup(e)
    {
        paint = false;
    },

    canvasMouseleave(e)
    {
        paint = false;
    },

    addClick(x, y, dragging)
    {
        clicks.push({ x: x, y: y, drag: dragging });
    },    

    clearOnSuccess(e)
    {
        CanvasView.clearRect();

        clicks = new Array();
    },

    saveOnSuccess(e)
    {
        CanvasModel.saveCanvasData(clicks);
    }

}


CanvasView.documentBody.onload = function(e) {CanvasController.documentBodyOnload(e);}

CanvasView.canvas.mousedown = function(e) {CanvasController.canvasMousedown(e);}

CanvasView.canvas.mousemove = function(e) {CanvasController.canvasMousemove(e);}

CanvasView.canvas.mouseup = function(e) {CanvasController.canvasMouseup(e);}

CanvasView.canvas.mouseleave = function(e) {CanvasController.canvasMouseleave(e);}

CanvasView.clearButton.click(function(e) {CanvasController.clearOnSuccess(e)});

CanvasView.saveButton.click(function(e) {CanvasController.saveOnSuccess(e)});
