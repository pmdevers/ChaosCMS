namespace ChaosCMS.Razor.Compilation
{
	public interface ICompilerService
	{
		CompilationResult Compile(CompilationContext context);
	}
}
