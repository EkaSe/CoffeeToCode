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
using Microsoft.Extensions.Primitives;

namespace C2C
{
    public static class HttpOperationUtils
    {
        public static Process GetProcess(string dllName, string workingDirectory, bool captureConsoleOutput) =>
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
        
        public static string CompileCode(string code, string dllName = null, HttpContext context = null)
        {
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code);

            string assemblyName = dllName ?? Path.GetRandomFileName();

            var coreDir = Path.GetDirectoryName(typeof(object).GetTypeInfo().Assembly.Location);

            List<MetadataReference> references = new List<MetadataReference>
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Console).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(System.Runtime.AssemblyTargetedPatchBandAttribute).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(StringBuilder).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(DateTime).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(HttpContext).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(HttpRequest).Assembly.Location),
                MetadataReference.CreateFromFile(Path.Combine(coreDir, "mscorlib.dll")),
                MetadataReference.CreateFromFile(typeof(IQueryCollection).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(StringValues).Assembly.Location)

            };

            foreach (var reference in CollectReferences().ToArray())
            {  
                references.Add(reference);
            }

            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: new[] { syntaxTree },
                references: references.ToArray(),
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

            object[] args = context is null ? null : new[] {context};
            return type?.InvokeMember
                ("Main", BindingFlags.Default | BindingFlags.InvokeMethod, null, null, args)
                .ToString();
        }

        public static string ReadFromFile(string fileName, string directoryName)
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

        public static string ReadProcessConsoleOutput(Process process)
        {
            process.Start();

            string result = process.StandardOutput.ReadToEnd();

            process.WaitForExit();
            process.Close();

            return result;
        }

        public static void RunProcess(Process process)
        {
            process.Start();
            process.WaitForExit();
            process.Close();
        }

        public static async Task SendResponse(HttpContext context, string responseText, string contentType)
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

        private static List<MetadataReference> CollectReferences()
        {
            // first, collect all assemblies
            var assemblies = new HashSet<Assembly>();

            Collect(Assembly.Load(new AssemblyName("netstandard")));

            //// add extra assemblies which are not part of netstandard.dll, for example:
            //Collect(typeof(Uri).Assembly);

            // second, build metadata references for these assemblies
            var result = new List<MetadataReference>(assemblies.Count);
            foreach (var assembly in assemblies)
            {
                result.Add(MetadataReference.CreateFromFile(assembly.Location));
            }

            return result;

            // helper local function - add assembly and its referenced assemblies
            void Collect(Assembly assembly)
            {
                if (!assemblies.Add(assembly))
                {
                    // already added
                    return;
                }

                var referencedAssemblyNames = assembly.GetReferencedAssemblies();

                foreach (var assemblyName in referencedAssemblyNames)
                {
                    var loadedAssembly = Assembly.Load(assemblyName);
                    assemblies.Add(loadedAssembly);
                }
            }
        }
    }
}