using System.Text;
using System;
using Microsoft.AspNetCore.Http;
public class Program{
public static string Main(object args_context) {
HttpContext _context = args_context as HttpContext;
var ___flcrbckj8yjtyfpdf1568632ybtKJBKSDs1jlyj4568qg32156thtvtyyjq = new StringBuilder("");
___flcrbckj8yjtyfpdf1568632ybtKJBKSDs1jlyj4568qg32156thtvtyyjq.Append(@"        var canvasSetup = document.getElementById(\"myCanvas\");" + 
 "        var ctx = canvasSetup.getContext(\"2d\");" + 
 "        // Red rectangle" + 
 "        ctx.beginPath();" + 
 "        ctx.lineWidth=\"6\";" + 
 "        ctx.strokeStyle=\"red\";" + 
 "        ctx.rect(5,5,290,140);" + 
 "        ctx.stroke();" + 
 "" + 

 "" + 
 "        // Green rectangle" + 
 "        ctx.beginPath();" + 
 "        ctx.lineWidth=\"4\";" + 
 "        ctx.strokeStyle=\"green\";" + 
 "        ctx.rect(30,30,50,50);" + 
 "        ctx.stroke();" + 
 "" + 
 "        // Blue rectangle" + 
 "        ctx.beginPath();" + 
 "        ctx.lineWidth=\"10\";" + 
 "        ctx.strokeStyle=\"blue\";" + 
 "        ctx.rect(50,50,150,80);" + 
 "        ctx.stroke(); ");
return ___flcrbckj8yjtyfpdf1568632ybtKJBKSDs1jlyj4568qg32156thtvtyyjq.ToString();
}
}