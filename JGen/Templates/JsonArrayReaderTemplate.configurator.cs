using System;

namespace JGen.Templates
{
	public partial class JsonArrayReaderTemplate
	{
		public string ItemTypeReaderName;
		public string Type;
		public string ReaderName;

		public JsonArrayReaderTemplate(string collectionType, string itemTypeReaderName, string readerName, string itemType, bool isArray)
		{
			ItemTypeReaderName = itemTypeReaderName;
			Type = collectionType;
			ReaderName = readerName;

			ItemType = itemType;
			IsArray = isArray;

		}

		public string ItemType { get; set; }

		public bool IsArray { get; set; }
	}
}
