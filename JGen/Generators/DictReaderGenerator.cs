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

			var name = string.Format("Json{0}Reader", ReflectionUtils.GetHumanName(_type));
			return new ReaderCode
			{
				ReaderName = name,
				Content = new JsonDictionaryReaderTemplate(name, keyReader.ReaderName, valReader.ReaderName, ReflectionUtils.GetCodeName(_type.GetGenericArguments()[0]), ReflectionUtils.GetCodeName(_type.GetGenericArguments()[1])).TransformText(),
				Dependend = new[] { keyReader, valReader }
			};
		}
	}
}