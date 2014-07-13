using System;

namespace JGen.Generators
{
	internal class Generator
	{
		public virtual ReaderCode GenerateReader()
		{
			return null;
		}
	}

	public class ReaderCode
	{
		public string ReaderName;
		public string Content;

		public ReaderCode[] Dependend;

		public override string ToString()
		{
			return string.Format("ReaderName: {0}", ReaderName);
		}
	}
}