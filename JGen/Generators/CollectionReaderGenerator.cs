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

			var name = string.Format("Json{0}Reader", ReflectionUtils.GetHumanName(_type));
			return new ReaderCode
			{
				ReaderName = name,
				Content = new JsonArrayReaderTemplate(ReflectionUtils.GetCodeName(_type), childReader.ReaderName, name, ReflectionUtils.GetCodeName(ReflectionUtils.GetCollectionType(_type)), _type.IsArray).TransformText(),
				Dependend = new []{ childReader}
			};
		}

		
	}
}