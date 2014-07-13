using System;

namespace JGen.Templates
{
	public partial class JsonArrayReaderTemplate
	{
		public string ItemTypeReaderName;
		public string Type;
		public string ReaderName;

		public JsonArrayReaderTemplate(string collectionType, string itemTypeReaderName, string readerName)
		{
			ItemTypeReaderName = itemTypeReaderName;
			Type = collectionType;
			ReaderName = readerName;
		}
	}
}
