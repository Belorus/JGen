using JGen.Templates;

namespace JGen.Generators
{
	internal class BoolReaderGenerator : Generator
	{
		public override ReaderCode GenerateReader()
		{
			return new ReaderCode
			{
				ReaderName = "JsonBoolReader",
				Content = new JsonBoolReaderTemplate().TransformText(),
			};
		}
	}
}