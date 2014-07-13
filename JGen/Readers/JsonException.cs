using System;

namespace JGen.Readers
{
	public class JsonException : Exception
	{
		public JsonException(string message) : base(message)
		{
		}
	}
}