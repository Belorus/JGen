using System;

namespace JGen.Templates
{
	public partial class JsonFloatReaderTemplate
	{
		private readonly string ReaderName;
		public string Type { get; private set; }

		public JsonFloatReaderTemplate(Type type, string readerName)
		{
			ReaderName = readerName;
			Type = type.FullName;
		}
	}
}
