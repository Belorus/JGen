using System;
using System.IO;

namespace JGen.Readers
{
	public static class JsonIntegerReader
	{
		public static Int32 Read(TextReader _textReader)
		{			
			bool negative = false;
			if (_textReader.Peek() == '-')
			{
				negative = true;
				_textReader.Read();
				if (_textReader.Peek() < 0)
					throw new JsonException("Invalid JSON numeric literal; extra negation");
			}

			Int32 val = 0;
			int x = 0;
			bool zeroStart = _textReader.Peek() == '0';
			for (;; x++)
			{
				int c = _textReader.Peek();
				if (c < '0' || '9' < c)
					break;
				val = val*10 + (c - '0');
				_textReader.Read();
				if (zeroStart && x == 1 && c == '0')
					throw new JsonException("leading multiple zeros are not allowed");
			}

			return (negative ? -val : val);
		}
	}
}