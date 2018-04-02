var context = document.getElementById('canvas').getContext("2d");
var canvas = document.getElementById('canvas');

$('#canvas').mousedown(function(e)
{        
    paint = true;

    var point = getRelativeCoordinates(canvas, e.pageX, e.pageY);
    //addClick(e.pageX - getGlobalOffset(canvas).left, e.pageY - getGlobalOffset(canvas).top);
    addClick(point.x, point.y)
    redraw();
  });

//   function getGlobalOffset(el) 
//   {
//     var x = 0, y = 0;Clienrect
//     while (el) {
//         x += el.offsetLeft;
//         y += el.offsetTop;
//         el = el.offsetParent;
//     }
//     return { left: x, top: y }
// }

function getRelativeCoordinates(canvas, xGlobal, yGlobal)
{
    var rect = canvas.getBoundingClientRect();
    
    // var xCanvas = (xGlobal - getGlobalOffset(canvas).left) / canvas.scrollWidth;
    // var yCanvas = (yGlobal - getGlobalOffset(canvas).top) / canvas.scrollHeight;

    var xCanvas = (xGlobal - rect.left) / canvas.scrollWidth;
    var yCanvas = (yGlobal - rect.top) / canvas.scrollHeight;

    return {x : xCanvas, y: yCanvas}
}

function getGlobalCoordinates(canvas, xCanvas, yCanvas)
{
    var rect = canvas.getBoundingClientRect();

    var xGlobal = xCanvas * canvas.scrollWidth + rect.left;
    var yGlobal = yCanvas * canvas.scrollHeight + rect.top;

    return {x : xGlobal, y: yGlobal}
}

function getCoordinates(canvas, xCanvas, yCanvas)
{
    var xGlobal = xCanvas * canvas.width;// + getGlobalOffset(canvas).left;
    var yGlobal = yCanvas * canvas.height;// + getGlobalOffset(canvas).top;

    return {x : xGlobal, y: yGlobal}
}

  $('#canvas').mousemove(function(e){
    if(paint)
    {
      //addClick(e.pageX - getGlobalOffset(canvas).left, e.pageY - getGlobalOffset(canvas).top, true);
        var point = getRelativeCoordinates(canvas, e.pageX, e.pageY);

      addClick(point.x, point.y, true);
      redraw();
    }
  });

  $('#canvas').mouseup(function(e){
    paint = false;
  });

  $('#canvas').mouseleave(function(e){
    paint = false;
  });

  var clickX = new Array();
  var clickY = new Array();
  var clickDrag = new Array();
  var paint;
  
  function addClick(x, y, dragging)
  {
    clickX.push(x);
    clickY.push(y);
    clickDrag.push(dragging);
  };

  function redraw(){
    context.clearRect(0, 0, context.canvas.width, context.canvas.height);
    
    context.strokeStyle = "#df4b26";
    context.lineJoin = "round";
    context.lineWidth = 5;
              
    for(var i=0; i < clickX.length; i++) {		
      context.beginPath();

      var point = getCoordinates(canvas, clickX[i], clickY[i]);
      var previousPoint = getCoordinates(canvas, clickX[i-1], clickY[i-1]);

      if(clickDrag[i] && i)
      {
        //context.moveTo(clickX[i-1]*0.6, clickY[i-1]*0.6);
        context.moveTo(previousPoint.x, previousPoint.y);
       }
       else
       {
         //context.moveTo(clickX[i]*0.6-0.6, clickY[i]*0.6);
         var pointI = getCoordinates(canvas, clickX[i], clickY[i])
         context.moveTo(point.x - 1, point.y);
       }
       //context.lineTo(clickX[i]*0.6, clickY[i]*0.6);
       context.lineTo(point.x, point.y);

       context.closePath();
       context.stroke();
    }
  };

  $('#clear-button').click(function(e) {
    context.clearRect(0, 0, context.canvas.width, context.canvas.height);

    clickX = new Array();
    clickY = new Array();
    clickDrag = new Array();
  });

  $('#save-button').click(function(e) 
  {
    for (var i = 0; i < clickX.length; i++)
    {
        console.log(clickX[i] + ", " + clickY[i] + ", dragging: " + clickDrag[i]);
    }  

    // var canvasData = canvas.toDataURL("image/png");
    // $.ajax(  
    //     {  
    //         type: "POST", 
    //         url: "Canvas/SaveCanvas",   
    //         data: {   
    //             image: canvasData
    //         }  

    //     }); 
    
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