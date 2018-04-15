var context = document.getElementById('canvas').getContext("2d");
var canvas = document.getElementById('canvas');
var clicks = new Array();

$('#canvas').mousedown(function (e)
{
    paint = true;

    var point = getRelativeCoordinates(canvas, e.pageX, e.pageY);

    addClick(point.x, point.y)
    redraw();
});


function getRelativeCoordinates(canvas, xGlobal, yGlobal)
{
    var rect = canvas.getBoundingClientRect();

    var xCanvas = (xGlobal - rect.left) / canvas.scrollWidth;
    var yCanvas = (yGlobal - rect.top) / canvas.scrollHeight;

    return { x: xCanvas, y: yCanvas }
}

function getGlobalCoordinates(canvas, xCanvas, yCanvas)
{
    var rect = canvas.getBoundingClientRect();

    var xGlobal = xCanvas * canvas.scrollWidth + rect.left;
    var yGlobal = yCanvas * canvas.scrollHeight + rect.top;

    return { x: xGlobal, y: yGlobal }
}

function getCoordinates(canvas, xCanvas, yCanvas)
{
    var xGlobal = xCanvas * canvas.width;
    var yGlobal = yCanvas * canvas.height;

    return { x: xGlobal, y: yGlobal }
}

$('#canvas').mousemove(function (e)
{
    if (paint)
    {
        var point = getRelativeCoordinates(canvas, e.pageX, e.pageY);

        addClick(point.x, point.y, true);
        redraw();
    }
});

$('#canvas').mouseup(function (e)
{
    paint = false;
});

$('#canvas').mouseleave(function (e)
{
    paint = false;
});

var paint;

function addClick(x, y, dragging)
{
    clicks.push({x: x, y: y, drag: dragging});
};

function redraw()
{
    context.clearRect(0, 0, context.canvas.width, context.canvas.height);

    context.strokeStyle = "#df4b26";
    context.lineJoin = "round";
    context.lineWidth = 5;

    var previousPoint = undefined;

    clicks.forEach(function(click)
    {
        context.beginPath();

        var point = getCoordinates(canvas, click.x, click.y);

        if (click.drag && previousPoint)
        {
            context.moveTo(previousPoint.x, previousPoint.y);
        }
        else
        {
            context.moveTo(point.x, point.y);
        }
        context.lineTo(point.x, point.y);

        context.closePath();
        context.stroke();

        previousPoint = point;
    });
};

$('#clear-button').click(function (e)
{
    context.clearRect(0, 0, context.canvas.width, context.canvas.height);

    clicks = new Array();
});

$('#save-button').click(function (e)
{
    console.log(clicks);

    // var canvasData = canvas.toDataURL("image/png");
    // $.ajax(  
    //     {  
    //         type: "POST", 
    //         url: "Canvas/SaveCanvas",   
    //         data: {   
    //             image: canvasData
    //         }  

    //     }); 
    var xhttp = new XMLHttpRequest();
    xhttp.open("POST", "/canvas/save", true);
    xhttp.setRequestHeader("Content-type", "application/json");
    xhttp.send(JSON.stringify(clicks)); 
});



//   function SaveImage() { 
//     var m = confirm("Are you sure to Save "); 
//     if (m) { 
//         // generate the image data 
//         var image_NEW = document.getElementById("canvas").toDataURL("image/png"); 
//         image_NEW = image_NEW.replace('data:image/png;base64,', '');
//         $.ajax({
//             type: 'POST',
//             url: 'Default.aspx/SaveImage',
//             data: '{ "imageData" : "' + image_NEW + '" }',
//             contentType: 'application/json; charset=utf-8',
//             dataType: 'json',
//             success: function (msg) {
//                 alert('Image saved to your root Folder !');
//             }
//         });
//     }      
//}