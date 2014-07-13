using JGen.Templates;

namespace JGen.Generators
{
	internal class StringReaderGenerator : Generator
	{
		public override ReaderCode GenerateReader()
		{
			return
				new ReaderCode
				{
					ReaderName = "JsonStringReader",
					Content = new JsonStringReaderTemplate().TransformText(),
				};
		}
	}
}