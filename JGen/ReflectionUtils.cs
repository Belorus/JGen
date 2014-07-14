using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace JGen
{
	internal static class ReflectionUtils
	{
		public static bool IsCollection(Type type)
		{
			var hasEmtpyPublicCtor = type.GetConstructors().Any(c => c.GetParameters().Length == 0) || type.IsArray;
			return type.IsClass && hasEmtpyPublicCtor && typeof(IEnumerable).IsAssignableFrom(type);
		}

		public static Type GetCollectionType(Type type)
		{
			if (type.IsArray)
			{
				return type.GetElementType();
			}
			return type.GetGenericArguments()[0];
		}

		public static ObjectPropertyInfo[] GetAllSettableMembers(Type type)
		{
			return
				type.GetTypeInfo()
					.GetRuntimeFields().Where(f => (f.Attributes | FieldAttributes.Public) == FieldAttributes.Public)
					.Select(m => new ObjectPropertyInfo(m.FieldType, m.Name))
					.Union(type.GetTypeInfo().GetRuntimeProperties()
					.Select(m => new ObjectPropertyInfo(m.PropertyType, m.Name)))
					.ToArray();
		}

		public static string GetCodeName(Type t)
		{
			if (t.IsGenericType)
			{
				return Regex.Replace(t.Name, @"`\d+", string.Empty) + "<" +
					   string.Join(",", t.GetGenericArguments().Select(GetCodeName)) + ">";
			}

			return t.FullName;
		}

		public static string GetHumanName(Type t)
		{
			if (t.IsGenericType)
			{
				string result = Regex.Replace(t.Name, @"`\d+", string.Empty) + "Of" +
					   string.Join("And", t.GetGenericArguments().Select(GetHumanName));

				return result;
			}
			else if (t.IsArray)
			{
				return "ArrayOf" + GetHumanName(t.GetElementType());
			}

			return t.Name;
		}
	}
}