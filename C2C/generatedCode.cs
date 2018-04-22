//Index

//using System.Text;
//using System;
//using Microsoft.AspNetCore.Http;
//public class Program{
//public static string Main(object args_context) {
//HttpContext _context = args_context as HttpContext;
//var ___flcrbckj8yjtyfpdf1568632ybtKJBKSDs1jlyj4568qg32156thtvtyyjq = new StringBuilder("");
//___flcrbckj8yjtyfpdf1568632ybtKJBKSDs1jlyj4568qg32156thtvtyyjq.Append("");
//var currentDate = DateTime.Now;
//___flcrbckj8yjtyfpdf1568632ybtKJBKSDs1jlyj4568qg32156thtvtyyjq.Append("" + 
// "<!DOCTYPE>" + 
// "<html>" + 
// "    <head>" + 
// "        <title>Coffee To Code - Login</title>" + 
// "        <link rel=\"stylesheet\" type=\"text/css\" href=\"style.css\">" + 
// "        <META http-equiv=\"refresh\" content=\"0;URL=Login\">" + 
// "    </head>" + 
// "    <body>" + 
// "        <div class=\"topbar\">" + 
// "" + 
// "        </div>" + 
// "        <form action=\"Canvas\" class=\"loginform\">" + 
// "            <label for \"login\">Login</label><br>" + 
// "            <input type=\"text\" id=\"login\" name=\"Login\"><br>" + 
// "            <label for \"password\">Password</label><br>" + 
// "            <input type=\"password\" id=\"password\" name=\"pass\"><br>" + 
// "            <input type=\"submit\" value=\"Login\" class=\"login-submit\">" + 
// "        </form>" + 
// "    <div>Requested file is Index.html. ");
//___flcrbckj8yjtyfpdf1568632ybtKJBKSDs1jlyj4568qg32156thtvtyyjq.Append(currentDate.ToString());
//___flcrbckj8yjtyfpdf1568632ybtKJBKSDs1jlyj4568qg32156thtvtyyjq.Append(" </div></body>" + 
// "</html>");
//return ___flcrbckj8yjtyfpdf1568632ybtKJBKSDs1jlyj4568qg32156thtvtyyjq.ToString();
//}
//}

//style.css

//using System.Text;
//using System;
//using Microsoft.AspNetCore.Http;
//public class Program{
//public static string Main(object args_context) {
//HttpContext _context = args_context as HttpContext;
//var ___flcrbckj8yjtyfpdf1568632ybtKJBKSDs1jlyj4568qg32156thtvtyyjq = new StringBuilder("");
//___flcrbckj8yjtyfpdf1568632ybtKJBKSDs1jlyj4568qg32156thtvtyyjq.Append("body{" + 
// "    font-family: Courier, monospace serif;" + 
// "}" + 
// "" + 
// ".topbar {" + 
// "    background-color:#323436;" + 
// "    height:35px;" + 
// "    margin: -10px;" + 
// "    text-align: left;" + 
// "}" + 
// "" + 
// ".topbar a {" + 
// "    color: white;" + 
// "    text-decoration: none;" + 
// "    padding: 10px;" + 
// "    line-height: 35px;" + 
// "}" + 
// "" + 
// ".topbar .C2C-logo {" + 
// "    color: #C5FFFA;" + 
// "    line-height: 35px;" + 
// "    margin-left: 15px;" + 
// "    display: inline;" + 
// "    font-weight: bold;" + 
// "}" + 
// "" + 
// ".canvas {" + 
// "    text-align: center;" + 
// "}" + 
// "" + 
// ".canvas div {" + 
// "    margin-top:25px;" + 
// "}" + 
// "" + 
// "#canvas {" + 
// "    border:1px solid #323436; " + 
// "    margin-top:25px;" + 
// "    width: 60%;" + 
// "}" + 
// "" + 
// ".loginform {" + 
// "    text-align: center;" + 
// "    margin: 25px;" + 
// "    line-height: 45px;" + 
// "}" + 
// "" + 
// ".login-submit {" + 
// "    margin-top: 15px; " + 
// "    font-family: inherit;" + 
// "}" + 
// "" + 
// ".bottombar {" + 
// "    position: fixed;" + 
// "    height: 35px;" + 
// "    bottom: 0px;" + 
// "    left: 0px;" + 
// "    right: 0px;" + 
// "    margin-bottom: 0px;" + 
// "    color: #323436;" + 
// "    text-align: center;" + 
// "}" + 
// "" + 
// ".bottombar a {" + 
// "    color: #323436;" + 
// "    text-decoration: none;" + 
// "}" + 
// "" + 
// "");
//return ___flcrbckj8yjtyfpdf1568632ybtKJBKSDs1jlyj4568qg32156thtvtyyjq.ToString();
//}
//}

//Canvas

//using System.Text;
//using System;
//using Microsoft.AspNetCore.Http;
//public class Program{
//public static string Main(object args_context) {
//HttpContext _context = args_context as HttpContext;
//var ___flcrbckj8yjtyfpdf1568632ybtKJBKSDs1jlyj4568qg32156thtvtyyjq = new StringBuilder("");
//___flcrbckj8yjtyfpdf1568632ybtKJBKSDs1jlyj4568qg32156thtvtyyjq.Append("<!DOCTYPE>" + 
// "" + 
// "<html>" + 
// "" + 
// "<head>" + 
// "    <title>Coffee To Code</title>" + 
// "    <meta name=\"keywords\" content=\"start\">" + 
// "    <meta name=\"description\" content=\"ASP.NET - let's start\">" + 
// "    <meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\">" + 
// "    <meta content=\"utf-8\" http-equiv=\"encoding\">" + 
// "    <link rel=\"stylesheet\" type=\"text/css\" href=\"style.css\">" + 
// "</head>" + 
// "" + 
// "<body onload=\"getSavedData()\">" + 
// "    <div class=\"topbar\">" + 
// "        <a class=\"C2C-logo\" href=\"https://github.com/EkaSe/CoffeeToCode/\">CoffeeToCode</a>" + 
// "        ");
//string userName = _context.Request.Query["Login"].ToString();
//___flcrbckj8yjtyfpdf1568632ybtKJBKSDs1jlyj4568qg32156thtvtyyjq.Append("" + 
// "        <a href=\"?Login\">Logout</a>" + 
// "    </div>" + 
// "    <form class=\"canvas\" method=\"POST\" accept-charset=utf-8>" + 
// "        <div>Hello, ");
//___flcrbckj8yjtyfpdf1568632ybtKJBKSDs1jlyj4568qg32156thtvtyyjq.Append(userName.ToString());
//___flcrbckj8yjtyfpdf1568632ybtKJBKSDs1jlyj4568qg32156thtvtyyjq.Append("!</div>" + 
// "        <canvas id=\"canvas\"></canvas>" + 
// "        <input type=\"button\" value=\"Clear\" id=\"clear-button\">" + 
// "        <input type=\"button\" value=\"Save\" id=\"save-button\">" + 
// "        <input type=\"hidden\" name=\"imageData\" id=\"imageData\" />" + 
// "    </form>" + 
// "    <div class=\"bottombar\">" + 
// "        <a href=\"https://github.com/EkaSe/CoffeeToCode/\">CoffeeToCode</a>" + 
// "    </div>" + 
// "    <script src=\"jquery.js\"></script>" + 
// "    <script language=\"JavaScript\" type=\"text/javascript\" src=\"Draw.js\"></script>" + 
// "</body>" + 
// "" + 
// "</html>");
//return ___flcrbckj8yjtyfpdf1568632ybtKJBKSDs1jlyj4568qg32156thtvtyyjq.ToString();
//}
//}

//style.css

//using System.Text;
//using System;
//using Microsoft.AspNetCore.Http;
//public class Program{
//public static string Main(object args_context) {
//HttpContext _context = args_context as HttpContext;
//var ___flcrbckj8yjtyfpdf1568632ybtKJBKSDs1jlyj4568qg32156thtvtyyjq = new StringBuilder("");
//___flcrbckj8yjtyfpdf1568632ybtKJBKSDs1jlyj4568qg32156thtvtyyjq.Append("body{" + 
// "    font-family: Courier, monospace serif;" + 
// "}" + 
// "" + 
// ".topbar {" + 
// "    background-color:#323436;" + 
// "    height:35px;" + 
// "    margin: -10px;" + 
// "    text-align: left;" + 
// "}" + 
// "" + 
// ".topbar a {" + 
// "    color: white;" + 
// "    text-decoration: none;" + 
// "    padding: 10px;" + 
// "    line-height: 35px;" + 
// "}" + 
// "" + 
// ".topbar .C2C-logo {" + 
// "    color: #C5FFFA;" + 
// "    line-height: 35px;" + 
// "    margin-left: 15px;" + 
// "    display: inline;" + 
// "    font-weight: bold;" + 
// "}" + 
// "" + 
// ".canvas {" + 
// "    text-align: center;" + 
// "}" + 
// "" + 
// ".canvas div {" + 
// "    margin-top:25px;" + 
// "}" + 
// "" + 
// "#canvas {" + 
// "    border:1px solid #323436; " + 
// "    margin-top:25px;" + 
// "    width: 60%;" + 
// "}" + 
// "" + 
// ".loginform {" + 
// "    text-align: center;" + 
// "    margin: 25px;" + 
// "    line-height: 45px;" + 
// "}" + 
// "" + 
// ".login-submit {" + 
// "    margin-top: 15px; " + 
// "    font-family: inherit;" + 
// "}" + 
// "" + 
// ".bottombar {" + 
// "    position: fixed;" + 
// "    height: 35px;" + 
// "    bottom: 0px;" + 
// "    left: 0px;" + 
// "    right: 0px;" + 
// "    margin-bottom: 0px;" + 
// "    color: #323436;" + 
// "    text-align: center;" + 
// "}" + 
// "" + 
// ".bottombar a {" + 
// "    color: #323436;" + 
// "    text-decoration: none;" + 
// "}" + 
// "" + 
// "");
//return ___flcrbckj8yjtyfpdf1568632ybtKJBKSDs1jlyj4568qg32156thtvtyyjq.ToString();
//}
//}

//Canvas

//using System.Text;
//using System;
//using Microsoft.AspNetCore.Http;
//public class Program{
//public static string Main(object args_context) {
//HttpContext _context = args_context as HttpContext;
//var ___flcrbckj8yjtyfpdf1568632ybtKJBKSDs1jlyj4568qg32156thtvtyyjq = new StringBuilder("");
//___flcrbckj8yjtyfpdf1568632ybtKJBKSDs1jlyj4568qg32156thtvtyyjq.Append("<!DOCTYPE>" + 
// "" + 
// "<html>" + 
// "" + 
// "<head>" + 
// "    <title>Coffee To Code</title>" + 
// "    <meta name=\"keywords\" content=\"start\">" + 
// "    <meta name=\"description\" content=\"ASP.NET - let's start\">" + 
// "    <meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\">" + 
// "    <meta content=\"utf-8\" http-equiv=\"encoding\">" + 
// "    <link rel=\"stylesheet\" type=\"text/css\" href=\"style.css\">" + 
// "</head>" + 
// "" + 
// "<body onload=\"getSavedData()\">" + 
// "    <div class=\"topbar\">" + 
// "        <a class=\"C2C-logo\" href=\"https://github.com/EkaSe/CoffeeToCode/\">CoffeeToCode</a>" + 
// "        ");
//string userName = _context.Request.Query["Login"].ToString();
//___flcrbckj8yjtyfpdf1568632ybtKJBKSDs1jlyj4568qg32156thtvtyyjq.Append("" + 
// "        <a href=\"?Login\">Logout</a>" + 
// "    </div>" + 
// "    <form class=\"canvas\" method=\"POST\" accept-charset=utf-8>" + 
// "        <div>Hello, ");
//___flcrbckj8yjtyfpdf1568632ybtKJBKSDs1jlyj4568qg32156thtvtyyjq.Append(userName.ToString());
//___flcrbckj8yjtyfpdf1568632ybtKJBKSDs1jlyj4568qg32156thtvtyyjq.Append("!</div>" + 
// "        <canvas id=\"canvas\"></canvas>" + 
// "        <input type=\"button\" value=\"Clear\" id=\"clear-button\">" + 
// "        <input type=\"button\" value=\"Save\" id=\"save-button\">" + 
// "        <input type=\"hidden\" name=\"imageData\" id=\"imageData\" />" + 
// "    </form>" + 
// "    <div class=\"bottombar\">" + 
// "        <a href=\"https://github.com/EkaSe/CoffeeToCode/\">CoffeeToCode</a>" + 
// "    </div>" + 
// "    <script src=\"jquery.js\"></script>" + 
// "    <script language=\"JavaScript\" type=\"text/javascript\" src=\"Draw.js\"></script>" + 
// "</body>" + 
// "" + 
// "</html>");
//return ___flcrbckj8yjtyfpdf1568632ybtKJBKSDs1jlyj4568qg32156thtvtyyjq.ToString();
//}
//}

//style.css

//using System.Text;
//using System;
//using Microsoft.AspNetCore.Http;
//public class Program{
//public static string Main(object args_context) {
//HttpContext _context = args_context as HttpContext;
//var ___flcrbckj8yjtyfpdf1568632ybtKJBKSDs1jlyj4568qg32156thtvtyyjq = new StringBuilder("");
//___flcrbckj8yjtyfpdf1568632ybtKJBKSDs1jlyj4568qg32156thtvtyyjq.Append("body{" + 
// "    font-family: Courier, monospace serif;" + 
// "}" + 
// "" + 
// ".topbar {" + 
// "    background-color:#323436;" + 
// "    height:35px;" + 
// "    margin: -10px;" + 
// "    text-align: left;" + 
// "}" + 
// "" + 
// ".topbar a {" + 
// "    color: white;" + 
// "    text-decoration: none;" + 
// "    padding: 10px;" + 
// "    line-height: 35px;" + 
// "}" + 
// "" + 
// ".topbar .C2C-logo {" + 
// "    color: #C5FFFA;" + 
// "    line-height: 35px;" + 
// "    margin-left: 15px;" + 
// "    display: inline;" + 
// "    font-weight: bold;" + 
// "}" + 
// "" + 
// ".canvas {" + 
// "    text-align: center;" + 
// "}" + 
// "" + 
// ".canvas div {" + 
// "    margin-top:25px;" + 
// "}" + 
// "" + 
// "#canvas {" + 
// "    border:1px solid #323436; " + 
// "    margin-top:25px;" + 
// "    width: 60%;" + 
// "}" + 
// "" + 
// ".loginform {" + 
// "    text-align: center;" + 
// "    margin: 25px;" + 
// "    line-height: 45px;" + 
// "}" + 
// "" + 
// ".login-submit {" + 
// "    margin-top: 15px; " + 
// "    font-family: inherit;" + 
// "}" + 
// "" + 
// ".bottombar {" + 
// "    position: fixed;" + 
// "    height: 35px;" + 
// "    bottom: 0px;" + 
// "    left: 0px;" + 
// "    right: 0px;" + 
// "    margin-bottom: 0px;" + 
// "    color: #323436;" + 
// "    text-align: center;" + 
// "}" + 
// "" + 
// ".bottombar a {" + 
// "    color: #323436;" + 
// "    text-decoration: none;" + 
// "}" + 
// "" + 
// "");
//return ___flcrbckj8yjtyfpdf1568632ybtKJBKSDs1jlyj4568qg32156thtvtyyjq.ToString();
//}
//}

//Index

//using System.Text;
//using System;
//using Microsoft.AspNetCore.Http;
//public class Program{
//public static string Main(object args_context) {
//HttpContext _context = args_context as HttpContext;
//var ___flcrbckj8yjtyfpdf1568632ybtKJBKSDs1jlyj4568qg32156thtvtyyjq = new StringBuilder("");
//___flcrbckj8yjtyfpdf1568632ybtKJBKSDs1jlyj4568qg32156thtvtyyjq.Append("");
//var currentDate = DateTime.Now;
//___flcrbckj8yjtyfpdf1568632ybtKJBKSDs1jlyj4568qg32156thtvtyyjq.Append("" + 
// "<!DOCTYPE>" + 
// "<html>" + 
// "    <head>" + 
// "        <title>Coffee To Code - Login</title>" + 
// "        <link rel=\"stylesheet\" type=\"text/css\" href=\"style.css\">" + 
// "        <META http-equiv=\"refresh\" content=\"0;URL=Login\">" + 
// "    </head>" + 
// "    <body>" + 
// "        <div class=\"topbar\">" + 
// "" + 
// "        </div>" + 
// "        <form action=\"Canvas\" class=\"loginform\">" + 
// "            <label for \"login\">Login</label><br>" + 
// "            <input type=\"text\" id=\"login\" name=\"Login\"><br>" + 
// "            <label for \"password\">Password</label><br>" + 
// "            <input type=\"password\" id=\"password\" name=\"pass\"><br>" + 
// "            <input type=\"submit\" value=\"Login\" class=\"login-submit\">" + 
// "        </form>" + 
// "    <div>Requested file is Index.html. ");
//___flcrbckj8yjtyfpdf1568632ybtKJBKSDs1jlyj4568qg32156thtvtyyjq.Append(currentDate.ToString());
//___flcrbckj8yjtyfpdf1568632ybtKJBKSDs1jlyj4568qg32156thtvtyyjq.Append(" </div></body>" + 
// "</html>");
//return ___flcrbckj8yjtyfpdf1568632ybtKJBKSDs1jlyj4568qg32156thtvtyyjq.ToString();
//}
//}

//style.css

//using System.Text;
//using System;
//using Microsoft.AspNetCore.Http;
//public class Program{
//public static string Main(object args_context) {
//HttpContext _context = args_context as HttpContext;
//var ___flcrbckj8yjtyfpdf1568632ybtKJBKSDs1jlyj4568qg32156thtvtyyjq = new StringBuilder("");
//___flcrbckj8yjtyfpdf1568632ybtKJBKSDs1jlyj4568qg32156thtvtyyjq.Append("body{" + 
// "    font-family: Courier, monospace serif;" + 
// "}" + 
// "" + 
// ".topbar {" + 
// "    background-color:#323436;" + 
// "    height:35px;" + 
// "    margin: -10px;" + 
// "    text-align: left;" + 
// "}" + 
// "" + 
// ".topbar a {" + 
// "    color: white;" + 
// "    text-decoration: none;" + 
// "    padding: 10px;" + 
// "    line-height: 35px;" + 
// "}" + 
// "" + 
// ".topbar .C2C-logo {" + 
// "    color: #C5FFFA;" + 
// "    line-height: 35px;" + 
// "    margin-left: 15px;" + 
// "    display: inline;" + 
// "    font-weight: bold;" + 
// "}" + 
// "" + 
// ".canvas {" + 
// "    text-align: center;" + 
// "}" + 
// "" + 
// ".canvas div {" + 
// "    margin-top:25px;" + 
// "}" + 
// "" + 
// "#canvas {" + 
// "    border:1px solid #323436; " + 
// "    margin-top:25px;" + 
// "    width: 60%;" + 
// "}" + 
// "" + 
// ".loginform {" + 
// "    text-align: center;" + 
// "    margin: 25px;" + 
// "    line-height: 45px;" + 
// "}" + 
// "" + 
// ".login-submit {" + 
// "    margin-top: 15px; " + 
// "    font-family: inherit;" + 
// "}" + 
// "" + 
// ".bottombar {" + 
// "    position: fixed;" + 
// "    height: 35px;" + 
// "    bottom: 0px;" + 
// "    left: 0px;" + 
// "    right: 0px;" + 
// "    margin-bottom: 0px;" + 
// "    color: #323436;" + 
// "    text-align: center;" + 
// "}" + 
// "" + 
// ".bottombar a {" + 
// "    color: #323436;" + 
// "    text-decoration: none;" + 
// "}" + 
// "" + 
// "");
//return ___flcrbckj8yjtyfpdf1568632ybtKJBKSDs1jlyj4568qg32156thtvtyyjq.ToString();
//}
//}

