var context = document.getElementById('canvas').getContext("2d");
var canvas = document.getElementById('canvas');
var canvasWidth = '500px';
var canvasHeight = '300px';
canvas.setAttribute('width', canvasWidth);
canvas.setAttribute('height', canvasHeight);

$('#canvas').mousedown(function(e){
    var mouseX = e.pageX - this.offsetLeft;
    var mouseY = e.pageY - this.offsetTop;
          
    paint = true;
    addClick(e.pageX - this.offsetLeft, e.pageY - this.offsetTop);
    redraw();
  });

  $('#canvas').mousemove(function(e){
    if(paint){
      addClick(e.pageX - this.offsetLeft, e.pageY - this.offsetTop, true);
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
    context.clearRect(0, 0, context.canvas.width, context.canvas.height); // Clears the canvas
    
    context.strokeStyle = "#df4b26";
    context.lineJoin = "round";
    context.lineWidth = 5;
              
    for(var i=0; i < clickX.length; i++) {		
      context.beginPath();
      if(clickDrag[i] && i){
        context.moveTo(clickX[i-1], clickY[i-1]);
       }else{
         context.moveTo(clickX[i]-1, clickY[i]);
       }
       context.lineTo(clickX[i], clickY[i]);
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

  $('#save-button').click(function(e) {
    var canvasData = canvas.toDataURL("image/png");
    $.ajax(  
        {  
            type: "POST", 
            url: "Canvas/SaveCanvas",   
            data: {   
                image: canvasData
            }  

        }); 
    
  });

  function SaveImage() { 
    var m = confirm("Are you sure to Save "); 
    if (m) { 
        // generate the image data 
        var image_NEW = document.getElementById("canvas").toDataURL("image/png"); 
        image_NEW = image_NEW.replace('data:image/png;base64,', '');
        $.ajax({
            type: 'POST',
            url: 'Default.aspx/SaveImage',
            data: '{ "imageData" : "' + image_NEW + '" }',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (msg) {
                alert('Image saved to your root Folder !');
            }
        });
    }      
}