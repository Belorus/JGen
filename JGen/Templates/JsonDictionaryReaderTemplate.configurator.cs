
namespace JGen.Templates
{
	public partial class JsonDictionaryReaderTemplate
	{
		public string ReaderName { get; private set; }
		public string KeyReaderName;
		public string ValueReaderName;

		public string ValType;
		public string KeyType;
		public JsonDictionaryReaderTemplate(string readerName, string keyReaderName, string valReaderName, string keyType, string valType)
		{
			ReaderName = readerName;
			KeyReaderName = keyReaderName;
			ValueReaderName = valReaderName;
			KeyType = keyType;
			ValType = valType;
		}
	}
}
