using System.Collections.Generic;
using System.IO;

namespace JGen.Readers
{
	public static class JsonArrayReader
	{
		public static List<string> Read(TextReader _textReader)
		{
			int c = _textReader.Peek();
			if (c < 0 || c != '[')
				throw new JsonException("Incomplete JSON input");

			_textReader.Read();
			var list = new List<string>();
			SkipSpaces(_textReader);
			if (_textReader.Peek() == ']')
			{
				_textReader.Read();
				return list;
			}
			
			while (true)
			{
				list.Add(JsonStringReader.Read(_textReader));
				SkipSpaces(_textReader);
				c = _textReader.Peek();
				if (c != ',')
					break;
				_textReader.Read();
				SkipSpaces(_textReader);
			}
			if (_textReader.Read() != ']')
				throw new JsonException("JSON array must end with ']'");
			return list;
		}

		private static void SkipSpaces(TextReader _textReader)
		{
			while (true)
			{
				switch (_textReader.Peek())
				{
					case ' ':
					case '\t':
					case '\r':
					case '\n':
						_textReader.Read();
						continue;
					default:
						return;
				}
			}
		}




	}
}