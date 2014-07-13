using System;
using System.Linq;
using System.Text.RegularExpressions;
using JGen.Templates;

namespace JGen.Generators
{
	internal class CollectionReaderGenerator : Generator
	{
		private readonly Type _type;
		public Generator ItemGenerator { get; private set; }

		public CollectionReaderGenerator(Type type, Generator itemGenerator)
		{
			_type = type;
			ItemGenerator = itemGenerator;
		}

		public override ReaderCode GenerateReader()
		{
			var childReader = ItemGenerator.GenerateReader();

			var name = string.Format("Json{0}Reader", GetHumanName(_type));
			return new ReaderCode
			{
				ReaderName = name,
				Content = new JsonArrayReaderTemplate(GetCodeName(_type), childReader.ReaderName, name).TransformText(),
				Dependend = new []{ childReader}
			};
		}

		private static string GetCodeName(Type t)
		{
			if (t.IsGenericType)
			{
				return Regex.Replace(t.Name, @"`\d+", string.Empty) + "<" +
				       string.Join(",", t.GetGenericArguments().Select(GetCodeName)) + ">";
			}

			return t.FullName;
		}

		private string GetHumanName(Type t)
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