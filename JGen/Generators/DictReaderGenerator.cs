using System;
using System.Linq;
using System.Text.RegularExpressions;
using JGen.Templates;

namespace JGen.Generators
{
	internal class DictionaryReaderGenerator : Generator
	{
		private readonly Type _type;
		
		public Generator KeyGenerator { get; private set; }
		public Generator ValueGenerator { get; private set; }

		public DictionaryReaderGenerator(Type type, Generator keyGenerator, Generator valGenerator)
		{
			_type = type;

			KeyGenerator = keyGenerator;
			ValueGenerator = valGenerator;
		}

		public override ReaderCode GenerateReader()
		{
			var keyReader = KeyGenerator.GenerateReader();
			var valReader = ValueGenerator.GenerateReader();

			var name = string.Format("Json{0}Reader", GetHumanName(_type));
			return new ReaderCode
			{
				ReaderName = name,
				Content = new JsonDictionaryReaderTemplate(name, keyReader.ReaderName, valReader.ReaderName, GetCodeName(_type.GetGenericArguments()[0]), GetCodeName(_type.GetGenericArguments()[1])).TransformText(),
				Dependend = new[] { keyReader, valReader }
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

			return t.Name;
		}
	}
}