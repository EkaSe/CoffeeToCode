        var canvasSetup = document.getElementById("myCanvas");
        var ctx = canvasSetup.getContext("2d");

        ctx.beginPath();
        ctx.lineWidth="6";
        ctx.strokeStyle="red";
        ctx.rect(5,5,290,140);
        ctx.stroke();

        ctx.beginPath();
        ctx.lineWidth="4";
        ctx.strokeStyle="green";
        ctx.rect(30,30,50,50);
        ctx.stroke();

        ctx.beginPath();
        ctx.lineWidth="10";
        ctx.strokeStyle="blue";
        ctx.rect(50,50,150,80);
        ctx.stroke(); 