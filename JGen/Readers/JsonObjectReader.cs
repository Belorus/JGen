using System.IO;

namespace JGen.Readers
{
	public static class JsonObjectReader
	{
		public static Person Read(TextReader _textReader)
		{
			_textReader.Read();
			var obj = new Person();
			SkipSpaces(_textReader);
			if (_textReader.Peek() == '}')
			{
				_textReader.Read();
				return obj;
			}
			while (true)
			{
				SkipSpaces(_textReader);
				string name = JsonStringReader.Read(_textReader);
				SkipSpaces(_textReader);
				//Expect (':');
				_textReader.Read();

				SkipSpaces(_textReader);
				switch (name)
				{
					case "firstName":
						obj.firstName = JsonStringReader.Read(_textReader);
						break;
					case "lastName":
						obj.lastName = JsonStringReader.Read(_textReader);
						break;
					case "isAlive":
						obj.isAlive = JsonBoolReader.Read(_textReader);
						break;
					case "age":
						obj.age = JsonIntegerReader.Read(_textReader);
						break;
					case "height_cm":
						obj.height_cm = JsonFloatReader.Read(_textReader);
						break;
					case "address":
						obj.address = new JsonAddressReader(_textReader).Read();
						break;
					default:
						throw new JsonException("Not supported node: " + name);
				}
				SkipSpaces(_textReader);
				int c = _textReader.Read();
				if (c == ',')
					continue;
				if (c == '}')
					break;
			}
			return obj;
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