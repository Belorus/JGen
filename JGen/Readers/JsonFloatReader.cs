using System;
using System.IO;

namespace JGen.Readers
{
	public static class JsonFloatReader
	{
		public static double Read(TextReader _textReader)
		{
			bool negative = false;
			if (_textReader.Peek() == '-')
			{
				negative = true;
				_textReader.Read();
				if (_textReader.Peek() < 0)
					throw new JsonException("Invalid JSON numeric literal; extra negation");
			}

			int c;
			decimal val = 0;
			int x = 0;
			bool zeroStart = _textReader.Peek() == '0';
			for (; ; x++)
			{
				c = _textReader.Peek();
				if (c < '0' || '9' < c)
					break;
				val = val * 10 + (c - '0');
				_textReader.Read();
				if (zeroStart && x == 1 && c == '0')
					throw new JsonException("leading multiple zeros are not allowed");
			}

			// fraction

			bool hasFrac = false;
			decimal frac = 0;
			int fdigits = 0;
			if (_textReader.Peek() == '.')
			{
				hasFrac = true;
				_textReader.Read();
				if (_textReader.Peek() < 0)
					throw new JsonException("Invalid JSON numeric literal; extra dot");
				decimal d = 10;
				while (true)
				{
					c = _textReader.Peek();
					if (c < '0' || '9' < c)
						break;
					_textReader.Read();
					frac += (c - '0') / d;
					d *= 10;
					fdigits++;
				}
				if (fdigits == 0)
					throw new JsonException("Invalid JSON numeric literal; extra dot");
			}
			frac = Decimal.Round(frac, fdigits);

			c = _textReader.Peek();
			if (c != 'e' && c != 'E')
			{
				if (!hasFrac)
				{

						return (double)(negative ? -val : val);					
				}
				var v = val + frac;
				return (double)(negative ? -v : v);
			}

			// exponent
			_textReader.Read();

			int exp = 0;
			if (_textReader.Peek() < 0)
				throw new JsonException("Invalid JSON numeric literal; incomplete exponent");

			bool negexp = false;
			c = _textReader.Peek();
			if (c == '-')
			{
				_textReader.Read();
				negexp = true;
			}
			else if (c == '+')
				_textReader.Read();

			if (_textReader.Peek() < 0)
				throw new ArgumentException("Invalid JSON numeric literal; incomplete exponent");
			while (true)
			{
				c = _textReader.Peek();
				if (c < '0' || '9' < c)
					break;
				exp = exp * 10 + (c - '0');
				_textReader.Read();
			}

			if (negexp)
				return (double)(val + frac) / Math.Pow(10, exp);
			else
				return (double)(val + frac) * Math.Pow(10, exp);
		}
	}
}