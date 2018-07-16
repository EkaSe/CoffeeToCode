function CanvasView () 
{
    this.documentBody = document.body;
    this.context = document.getElementById('canvas').getContext("2d");
    this.canvas = document.getElementById('canvas');
    this.clearButton = document.getElementById('clear-button');
    this.saveButton = document.getElementById('save-button');
    this.imageListPlaceholder = document.getElementById('image-list-placeholder');
    this.imageList = document.getElementsByClassName("image-list")[0];
    //this.imageListItem = //document.getElementsByClassName("imageItem")[0];
    this.imageListItem =this.imageList.content.querySelector("div");
    
    var self = this;

    this.clearRect = function() 
    {
        this.context.clearRect(0, 0, this.context.canvas.width, this.context.canvas.height);
    };

    this.setLineStyle = function(strokeStyle, lineJoin, lineWidth)
    {
        this.context.strokeStyle = strokeStyle;
        this.context.lineJoin = lineJoin;
        this.context.lineWidth = lineWidth;
    };

    this.beginPath = function() 
    {
        this.context.beginPath();
    };

    this.closePath = function()
    {
        this.context.closePath();
    };

    this.stroke = function()
    {
        this.context.stroke();
    };

    this.moveTo = function(x, y)
    {
        this.context.moveTo(x, y);
    };

    this.lineTo = function(x, y)
    {
        this.context.lineTo(x, y);
    };

    this.canvasMousedownEvent = function(e) {console.log("mouse down")};
    this.canvasMousemoveEvent = function(e) {};
    this.canvasMouseleaveEvent = function(e) {};
    this.canvasMouseupEvent = function(e) {};

    $(this.canvas).mousedown(e => this.canvasMousedownEvent(e));

    $(this.canvas).mousemove(e => this.canvasMousemoveEvent(e));

    $(this.canvas).mouseleave(e => this.canvasMouseleaveEvent(e));

    $(this.canvas).mouseup(e => this.canvasMouseupEvent(e));

    this.getRelativeCoordinates = function(e)
    {
        var rect = this.canvas.getBoundingClientRect();

        var xCanvas = (e.pageX - rect.left) / this.canvas.scrollWidth;
        var yCanvas = (e.pageY - rect.top) / this.canvas.scrollHeight;

        return { x: xCanvas, y: yCanvas };
    };

    this.getCoordinates = function(clickX, clickY)
    {
        var xGlobal = clickX * this.canvas.width;
        var yGlobal = clickY * this.canvas.height;

        return { x: xGlobal, y: yGlobal };
    };
};

function CanvasModel() 
{
    this.getSavedData = function(onSuccessFunction){
        $.getJSON(
            "/savedData",
            onSuccessFunction);
    };

    this.saveCanvasData = function(clicks)
    {
        var xhttp = new XMLHttpRequest();
        xhttp.open("POST", "/canvas/save", true);
        xhttp.setRequestHeader("Content-type", "application/json");
        xhttp.send(JSON.stringify(clicks));
    }; 

    this.getImagesList = function(onSuccessFunction){
        $.getJSON(
            "/imagesList",
            onSuccessFunction);
    };
};

function CanvasController(canvasView, canvasModel) {
    this.clicks = new Array();
    this.paint = false;

    this.redraw = () =>
    {
        canvasView.clearRect();
        canvasView.setLineStyle("#df4b26", "round", 5);

        var previousPoint = undefined;

        this.clicks.forEach(function (click)
        {
            canvasView.beginPath();

            var point = canvasView.getCoordinates(click.x, click.y);

            if (click.drag && previousPoint)
            {
                canvasView.moveTo(previousPoint.x, previousPoint.y);
            }
            else
            {
                canvasView.moveTo(point.x, point.y);
            }
            canvasView.lineTo(point.x, point.y);

            canvasView.closePath();
            canvasView.stroke();

            previousPoint = point;
        });
    };
    
    this.documentBodyOnload = function(e)
    {
        var self = this;

        canvasModel.getSavedData(
            function (data)
            {
                self.clicks = data;
                self.redraw();
            });
    };

    this.addClick = (x, y, dragging) =>
    {
        this.clicks.push({ x: x, y: y, drag: dragging });
    };    

    this.clearOnSuccess = e =>
    {
        canvasView.clearRect();

        this.clicks = new Array();
    };

    this.saveOnSuccess = e =>
    {
        canvasModel.saveCanvasData(this.clicks);
    };

    canvasView.canvasMousedownEvent = e =>
    {       
        this.paint = true;

        var point = canvasView.getRelativeCoordinates(e);

        this.addClick(point.x, point.y);

        this.redraw();
    };

    canvasView.canvasMouseleaveEvent = e => { this.paint = false; };

    canvasView.canvasMousemoveEvent = e => 
    {
        if (this.paint)
        {
            var point = canvasView.getRelativeCoordinates(e);

            this.addClick(point.x, point.y, true);

            this.redraw();
        }
    };
    canvasView.canvasMouseupEvent = e => { this.paint = false; };

    this.showImageList = e =>
    {
        canvasModel.getImagesList(
            function (data)
            {
                var a = document.importNode(canvasView.imageList, true);
                    a.textContent += 'test';
                    canvasView.imageListPlaceholder.appendChild(a);
                for (var i in data)
                {
                    a = document.importNode(canvasView.imageListItem, true);
                    a.textContent += data[i];
                    canvasView.imageListPlaceholder.appendChild(a);
                };
            });     
    };
}

var canvasView = new CanvasView();
var canvasModel = new CanvasModel();
var canvasController = new CanvasController(canvasView, canvasModel);

canvasView.documentBody.onload = function(e) 
{
        canvasController.documentBodyOnload(e);
        canvasController.showImageList(e);
};

$(canvasView.clearButton).click(function(e) {canvasController.clearOnSuccess(e)});

$(canvasView.saveButton).click(function(e) {canvasController.saveOnSuccess(e)});
