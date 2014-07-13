using System;
using JGen.Templates;

namespace JGen.Generators
{
	internal class FloatReaderGenerator : Generator
	{
		private readonly Type _type;

		public FloatReaderGenerator(Type type)
		{
			_type = type;
		}

		public override ReaderCode GenerateReader()
		{
			string name = string.Format("Json{0}Reader", _type.Name);
			return new ReaderCode
				{
					ReaderName = name,
					Content = new JsonFloatReaderTemplate(_type, name).TransformText(),
				};

		}
	}
}