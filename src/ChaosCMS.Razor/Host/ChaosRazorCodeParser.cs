// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Diagnostics;
using Microsoft.AspNetCore.Razor;
using Microsoft.AspNetCore.Razor.Chunks.Generators;
using Microsoft.AspNetCore.Razor.Parser;
using Microsoft.AspNetCore.Razor.Parser.SyntaxTree;
using Microsoft.AspNetCore.Razor.Tokenizer.Symbols;

namespace ChaosCMS.Razor.Host
{
	public class ChaosRazorCodeParser : CSharpCodeParser
	{
		private const string ModelKeyword = "model";
		private const string InjectKeyword = "inject";
		private SourceLocation? _endInheritsLocation;
		private bool _modelStatementFound;

		public ChaosRazorCodeParser()
		{
			MapDirectives(ModelDirective, ModelKeyword);
			MapDirectives(InjectDirective, InjectKeyword);
		}

		protected override void InheritsDirective()
		{
			// Verify we're on the right keyword and accept
			AssertDirective(SyntaxConstants.CSharp.InheritsKeyword);
			AcceptAndMoveNext();
			_endInheritsLocation = CurrentLocation;

			InheritsDirectiveCore();
			CheckForInheritsAndModelStatements();
		}

		private void CheckForInheritsAndModelStatements()
		{
			if (_modelStatementFound && _endInheritsLocation.HasValue)
			{
				Context.OnError(
					_endInheritsLocation.Value,
					Resources.MvcRazorCodeParser_CannotHaveModelAndInheritsKeyword,
					SyntaxConstants.CSharp.InheritsKeyword.Length);
			}
		}

		protected virtual void ModelDirective()
		{
			// Verify we're on the right keyword and accept
			AssertDirective(ModelKeyword);
			var startModelLocation = CurrentLocation;
			AcceptAndMoveNext();


			BaseTypeDirective("Keyword must be followed with the type name",
							  CreateModelChunkGenerator);

			if (_modelStatementFound)
			{
				Context.OnError(
					startModelLocation,
					Resources.MvcRazorCodeParser_OnlyOneModelStatementIsAllowed,
					ModelKeyword.Length);
			}

			_modelStatementFound = true;

			CheckForInheritsAndModelStatements();
		}

		protected virtual void InjectDirective()
		{
			// @inject MyApp.MyService MyServicePropertyName
			AssertDirective(InjectKeyword);
			var startLocation = CurrentLocation;
			AcceptAndMoveNext();

			Context.CurrentBlock.Type = BlockType.Directive;

			// Accept whitespace
			var remainingWhitespace = AcceptSingleWhiteSpaceCharacter();
			var keywordwithSingleWhitespaceLength = Span.GetContent().Value.Length;
			if (Span.Symbols.Count > 1)
			{
				Span.EditHandler.AcceptedCharacters = AcceptedCharacters.None;
			}
			Output(SpanKind.MetaCode);

			if (remainingWhitespace != null)
			{
				Accept(remainingWhitespace);
			}
			var remainingWhitespaceLength = Span.GetContent().Value.Length;

			// Consume any other whitespace tokens.
			AcceptWhile(IsSpacingToken(includeNewLines: false, includeComments: true));

			var hasTypeError = !At(CSharpSymbolType.Identifier);
			if (hasTypeError)
			{
				Context.OnError(
					startLocation, 
					"Inject keyword must be followed by type name",
					InjectKeyword.Length);
			}

			// Accept 'MyApp.MyService'
			NamespaceOrTypeName();

			// typeName now contains the token 'MyApp.MyService'
			var typeName = Span.GetContent().Value;

			AcceptWhile(IsSpacingToken(includeNewLines: false, includeComments: true));

			if (!hasTypeError && (EndOfFile || At(CSharpSymbolType.NewLine)))
			{
				// Add an error for the property name only if we successfully read the type name
				Context.OnError(
					startLocation, 
					"Property name is required",
					keywordwithSingleWhitespaceLength + remainingWhitespaceLength + typeName.Length);
			}

			// Read until end of line. Span now contains 'MyApp.MyService MyServiceName'.
			AcceptUntil(CSharpSymbolType.NewLine);
			if (!Context.DesignTimeMode)
			{
				// We want the newline to be treated as code, but it causes issues at design-time.
				Optional(CSharpSymbolType.NewLine);
			}

			// Parse out 'MyServicePropertyName' from the Span.
			var propertyName = Span.GetContent()
				.Value
				.Substring(typeName.Length);

			// ';' is optional
			propertyName = RemoveWhitespaceAndTrailingSemicolons(propertyName);
			Span.ChunkGenerator = new InjectParameterGenerator(typeName.Trim(), propertyName);

			// Output the span and finish the block
			CompleteBlock();
			Output(SpanKind.Code, AcceptedCharacters.AnyExceptNewline);
		}

		private SpanChunkGenerator CreateModelChunkGenerator(string model)
		{
			return new ModelChunkGenerator(model);
		}

		// Internal for unit testing
		internal static string RemoveWhitespaceAndTrailingSemicolons(string value)
		{
			Debug.Assert(value != null);
			value = value.TrimStart();

			for (var index = value.Length - 1; index >= 0; index--)
			{
				var currentChar = value[index];
				if (!char.IsWhiteSpace(currentChar) && currentChar != ';')
				{
					return value.Substring(0, index + 1);
				}
			}

			return string.Empty;
		}
	}
}
