using System;

namespace JGen
{
	internal class ObjectPropertyInfo
	{
		public string Name;
		public Type Type;

		public ObjectPropertyInfo(Type fieldType, string name)
		{
			Name = name;
			Type = fieldType;
		}
	}
}