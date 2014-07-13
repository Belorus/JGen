using System.IO;

namespace JGen.Readers
{
	public struct JsonAddressReader
	{
		private readonly TextReader _textReader;

		public JsonAddressReader(TextReader textReader)
		{
			_textReader = textReader;
		}

		public Address Read()
		{
			ReadChar ();
			var obj = new Address();
			SkipSpaces ();
			if (PeekChar () == '}') {
				ReadChar ();
				return obj;
			}
			while (true) {
				SkipSpaces ();
				if (PeekChar () == '}')
					break;
				string name = JsonStringReader.Read(_textReader);
				SkipSpaces ();
				//Expect (':');
				ReadChar();

				SkipSpaces ();
				switch (name)
				{
					case "streetAddress":
						obj.streetAddress = JsonStringReader.Read(_textReader);
						break;
					case "city":
						obj.city = JsonStringReader.Read(_textReader);
						break;
					case "state":
						obj.state = JsonStringReader.Read(_textReader);
						break;
					case "postalCode":
						obj.postalCode = JsonStringReader.Read(_textReader);
						break;
					
					default:
						throw new JsonException("Not supported node: " + name);
				}
				SkipSpaces ();
				int c = ReadChar ();
				if (c == ',')
					continue;
				if (c == '}')
					break;
			}
			return obj;
		}

		private int PeekChar ()
		{
			return _textReader.Peek();
		}
		
		private int ReadChar ()
		{
			return  _textReader.Read();
		}

		private void SkipSpaces()
		{
			while (true)
			{
				switch (PeekChar())
				{
					case ' ':
					case '\t':
					case '\r':
					case '\n':
						ReadChar();
						continue;
					default:
						return;
				}
			}
		}




	}
}