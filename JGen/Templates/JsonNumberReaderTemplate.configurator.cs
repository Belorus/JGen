using System;

namespace JGen.Templates
{
	public partial class JsonNumberReaderTemplate
	{
		private readonly string ReaderName;
		public string Type { get; private set; }

		public JsonNumberReaderTemplate(Type type, string readerName)
		{
			ReaderName = readerName;
			Type = type.FullName;
		}
	}
}
