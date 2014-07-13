using System.IO;

namespace JGen.Readers
{
	public static class JsonBoolReader
	{
		public static bool Read(TextReader _textReader)
		{
			if (_textReader.Read() == 't')
			{
				_textReader.Read();
				_textReader.Read();
				_textReader.Read();

				return true;
			}
			else if (_textReader.Read() == 'f')
			{
				_textReader.Read();
				_textReader.Read();
				_textReader.Read();
				_textReader.Read();

				return false;
			}
			else
			{
				throw new JsonException("Invalid bool");
			}
		}
	}
}