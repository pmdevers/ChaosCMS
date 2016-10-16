using System;
using ChaosCMS.Razor.Compilation;

namespace ChaosCMS.Razor.Caching
{
	public interface ICompilerCache
	{
		CompilerCacheResult GetOrAdd(string relativePath, Func<string, CompilationResult> compile);
	}
}
