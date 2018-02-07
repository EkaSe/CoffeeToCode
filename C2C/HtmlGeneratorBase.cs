using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;

namespace C2C
{
    public abstract class HtmlGeneratorBase
    {
        protected string Request(HttpContext context) => context.Request.QueryString.ToString().Substring(1);

        protected string CompileCode(string code, string dllName = null)
        {
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code);

            string assemblyName = dllName ?? Path.GetRandomFileName();
            MetadataReference[] references = new MetadataReference[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Console).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(System.Runtime.AssemblyTargetedPatchBandAttribute).Assembly.Location)
            };

            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: new[] { syntaxTree },
                references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            Assembly assembly = null;

            using (var ms = new MemoryStream())
            {
                EmitResult result = compilation.Emit(ms);

                if (!result.Success)
                {
                    IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic => 
                        diagnostic.IsWarningAsError || 
                        diagnostic.Severity == DiagnosticSeverity.Error);

                    foreach (Diagnostic diagnostic in failures)
                    {
                        Console.Error.WriteLine("{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
                        assemblyName = null;
                    }
                }
                else
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    assembly = Assembly.Load(ms.ToArray());
                }
            }
            var type = assembly?.GetType("Program");
            return type?.InvokeMember("Main", BindingFlags.Default | BindingFlags.InvokeMethod, null, null, null).ToString();

            // EmitResult result = compilation.Emit(assemblyName);

            // if (!result.Success)
            // {
            //     IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic => 
            //         diagnostic.IsWarningAsError || 
            //         diagnostic.Severity == DiagnosticSeverity.Error);

            //     foreach (Diagnostic diagnostic in failures)
            //     {
            //         Console.Error.WriteLine("{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
            //         assemblyName = null;
            //     }
            // }
        }

        protected Process GetProcess(string dllName, string workingDirectory, bool captureConsoleOutput) =>
            new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    WorkingDirectory = workingDirectory,
                    FileName = "dotnet",
                    Arguments = dllName,
                    UseShellExecute = !captureConsoleOutput,
                    RedirectStandardOutput = captureConsoleOutput,
                    RedirectStandardError = false,
                    CreateNoWindow = true
                }
            };

        protected string ReadFromFile(string fileName, string directoryName)
        {
            string code = "";

            string path = directoryName == ""
                ? fileName
                : $"{directoryName}/{fileName}";

            var file = new FileInfo(path);
            
            if (file.Exists)
            {
                code = File.ReadAllText(file.FullName);
            }

            return code;
        }

        protected string ReadProcessConsoleOutput(Process process)
        {
            process.Start();

            string result = process.StandardOutput.ReadToEnd();

            process.WaitForExit();
            process.Close();

            return result;
        }

        protected void RunProcess(Process process)
        {
            process.Start();
            process.WaitForExit();
            process.Close();
        }

        protected async Task SendResponse(HttpContext context, string responseText, string contentType)
        {
            context.Response.ContentType = contentType;

            byte[] buffer = Encoding.UTF8.GetBytes(responseText);

            context.Response.StatusCode = (int)HttpStatusCode.OK;

            using (var stream = context.Response.Body)
            {
                await stream.WriteAsync(buffer, 0, buffer.Length);
                await stream.FlushAsync();
            }

            context.Response.ContentLength = buffer.Length;
        }
    }
}