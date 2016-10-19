using Microsoft.AspNetCore.Razor.Chunks;
using Microsoft.AspNetCore.Razor.CodeGenerators;
using Microsoft.AspNetCore.Razor.CodeGenerators.Visitors;
using System;

namespace ChaosCMS.Razor.Host
{
	public abstract class ChaosRazorCSharpCodeVisitor : ChoasRazorCSharpChunkVisitor
    {
		public ChaosRazorCSharpCodeVisitor(
			CSharpCodeWriter writer,
			CodeGeneratorContext context)
			: base(writer, context)
		{
			if (writer == null)
			{
				throw new ArgumentNullException(nameof(writer));
			}
			if (context == null)
			{
				throw new ArgumentNullException(nameof(context));
			}
		}

		protected override void Visit(InjectChunk chunk)
        {

        }
	}
}
