
namespace JGen.Templates
{
	public partial class JsonObjectReaderTemplate
	{
		public string Type { get; private set; }
		public string ReaderName;

		public PropertyReaderReference[] PropertyReaders { get; private set; }
		
		public JsonObjectReaderTemplate(System.Type type, PropertyReaderReference[] propertyReaders, string readerName)
		{
			PropertyReaders = propertyReaders;
			Type = type.FullName;
			ReaderName = readerName;
		}
	}

	public class PropertyReaderReference
	{
		public string PropertyName;
		public string ReaderName;
	}
}
