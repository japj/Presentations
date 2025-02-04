<Query Kind="Statements">
  <Output>DataGrids</Output>
  <NuGetReference>Microsoft.CodeAnalysis.Compilers</NuGetReference>
  <Namespace>Microsoft.CodeAnalysis</Namespace>
  <Namespace>Microsoft.CodeAnalysis.CSharp</Namespace>
</Query>

var syntaxTree = CSharpSyntaxTree.ParseText(@"
	class MyClass
	{
		void MyFunc(int x)
		{
		}
	}
");

var mscorlib = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
var options = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);
var compilation = CSharpCompilation.Create("MyAssembly").AddSyntaxTrees(syntaxTree).AddReferences(mscorlib).WithOptions(options);
using(var stream = new MemoryStream())
{
	var result = compilation.Emit(stream);
	result.Dump();
}