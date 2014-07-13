using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JGen.Generators;

namespace JGen
{
	internal class Program
	{
		private static void Main()
		{
			Generator gen = CreateGeneratorForType(typeof (SF.Common.API.Model.GameNRE.NreSpinResponse));
			ReaderCode reader = gen.GenerateReader();

			Save(reader, @"C:\Users\GrigoryP\Documents\visual studio 2012\Projects\JGen\JGen\Test\");

			Console.ReadLine();
		}

		private static void Save(ReaderCode reader, string path)
		{
			File.WriteAllText(Path.Combine(path, reader.ReaderName + ".cs"), reader.Content);
			if (reader.Dependend != null)
			{
				foreach (var c in reader.Dependend)
				{
					Save(c, path);
				}
			}
		}

		private static Generator CreateGeneratorForType(Type type)
		{
			Console.WriteLine("Processing type: "+type.FullName);

			if (type == typeof (object))
			{
				throw new ArgumentException("Object field type is prohibited");
			}

			if (type == typeof (string))
				return new StringReaderGenerator();
			if (type == typeof (int) || type == typeof(long))
				return new NumberReaderGenerator(type);
			if (type == typeof(double) || type == typeof(float) || type == typeof(Decimal))
				return new FloatReaderGenerator(type);
			if (type == typeof (bool))
				return new BoolReaderGenerator();
			if (typeof(IDictionary).IsAssignableFrom(type))
				return new DictionaryReaderGenerator(type, CreateGeneratorForType(type.GetGenericArguments()[0]), CreateGeneratorForType(type.GetGenericArguments()[1]));

			if (ReflectionUtils.IsCollection(type))
			{
				Type itemType = ReflectionUtils.GetCollectionType(type);
				Generator itemGenerator = CreateGeneratorForType(itemType);
				return new CollectionReaderGenerator(type, itemGenerator);
			}

			ObjectPropertyInfo[] publicMembers = ReflectionUtils.GetAllSettableMembers(type);
			var dict = publicMembers.ToDictionary(m => m.Name, m => CreateGeneratorForType(m.Type));
			
			return new ObjectReaderGenerator(type, dict);
		}
	}

	public class TestType
	{
		public Dictionary<string, Dictionary<int, List<int>>> data;
	}
}