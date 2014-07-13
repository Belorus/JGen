using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JGen.Generators;
using JGen.MS;

namespace JGen
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			//Perf.Foo();

			Generator gen = CreateGeneratorForType(typeof (SF.Common.API.Model.GameNRE.NreSpinResponse));
			ReaderCode reader = gen.GenerateReader();

			Save(reader);

			Console.ReadLine();
		}

		private static void Save(ReaderCode reader)
		{
			File.WriteAllText(@"C:\Users\GrigoryP\Documents\visual studio 2012\Projects\JGen\JGen\Test\"+reader.ReaderName+".cs", reader.Content);
			if (reader.Dependend != null)
			{
				foreach (var c in reader.Dependend)
				{
					Save(c);
				}
			}
		}

		private static Generator CreateGeneratorForType(Type type)
		{
			if (type == typeof (string))
				return new StringReaderGenerator();
			if (type == typeof (int))
				return new NumberReaderGenerator(type);
			if (type == typeof(double) || type == typeof(float))
				return new FloatReaderGenerator(type);
			if (type == typeof (bool))
				return new BoolReaderGenerator();
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

	internal class TestType
	{
		public List<List<int>> data;
	}

	public class Address
	{
		[JSONValue(Name = "streetAddress")]
		public string streetAddress { get; set; }
		[JSONValue(Name = "city")]
		public string city { get; set; }
		[JSONValue(Name = "state")]
		public string state { get; set; }
		[JSONValue(Name = "postalCode")]
		public string postalCode { get; set; }
	}

	public class PhoneNumber
	{
		public string type { get; set; }
		public string number { get; set; }
	}

	public class Person
	{
		[JSONValue(Name = "firstName")]
		public string firstName { get; set; }

		[JSONValue(Name = "lastName")]
		public string lastName { get; set; }

		[JSONValue(Name = "isAlive")]
		public bool isAlive { get; set; }

		[JSONValue(Name = "age")]
		public int age { get; set; }

		[JSONValue(Name = "height_cm")]
		public double height_cm { get; set; }

		[JSONValue(Name = "address")]
		public Address address { get; set; }

		[JSONValue(Name = "phoneNumbers")]
		public List<PhoneNumber> phoneNumbers { get; set; }
	}
}