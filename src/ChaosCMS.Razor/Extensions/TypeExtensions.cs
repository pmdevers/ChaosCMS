﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ChaosCMS.Razor.Extensions
{
	public static class TypeExtensions
	{
		public static ExpandoObject ToExpando(this object anonymousObject)
		{
			IDictionary<string, object> expando = new ExpandoObject();
			foreach (var propertyDescriptor in anonymousObject.GetType().GetTypeInfo().GetProperties())
			{
				var obj = propertyDescriptor.GetValue(anonymousObject);
				expando.Add(propertyDescriptor.Name, obj);
			}

			return (ExpandoObject)expando;
		}

		public static bool IsAnonymousType(this Type type)
		{
			bool hasCompilerGeneratedAttribute = type.GetTypeInfo().GetCustomAttributes(typeof(CompilerGeneratedAttribute), false).Any();
			bool nameContainsAnonymousType = type.FullName.Contains("AnonymousType");
			bool isAnonymousType = hasCompilerGeneratedAttribute && nameContainsAnonymousType;

			return isAnonymousType;
		}

	}
}
