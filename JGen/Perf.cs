using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using JGen.MS;
using JGen.Readers;
using Newtonsoft.Json;

namespace JGen
{
	public static class Perf
	{
		private static string sample = @"
  {
    ""firstName"": ""John"",
    ""lastName"": ""Smith"",
    ""isAlive"": true,
    ""age"": 25,
    ""height_cm"": 167.6,
    ""address"": {
        ""streetAddress"": ""21 2nd Street"",
        ""city"": ""New York"",
        ""state"": ""NY"",
        ""postalCode"": ""10021-3100""
    },
    ""phoneNumbers"": [
        { ""type"": ""home"", ""number"": ""212 555-1234"" },
        { ""type"": ""office"",  ""number"": ""646 555-4567"" }
    ]
}";

	//	static string sample = @"{""firstName"":""John"",""lastName"":""Smith"",""isAlive"":true,""age"":25,""height_cm"":167.6,""address"":{""streetAddress"":""21 2nd Street"",""city"":""New York"",""state"":""NY"",""postalCode"":""10021-3100""}}";


		public static void Foo()
		{
			Console.WriteLine("Press Enter...");
			Console.ReadLine();

			Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;
			Thread.CurrentThread.Priority = ThreadPriority.Highest;
			JsonDotNet();

			var sw = Stopwatch.StartNew();
			for (int i = 0; i < 100*1000; i++)
			{
				JsonDotNet();
			}
			sw.Stop();
			Console.WriteLine(sw.ElapsedMilliseconds);

		}
		private static void MS()
		{
			var textReader2 = new StringReader(sample);
			var readJson = new JSONReader().Read(textReader2);
			var result3 = new JSONSerializer(typeof (Person)).Deserialize(readJson);
		}

		private static void JGen()
		{
			var textReader = new StringReader(sample);
			//var res = JsonPersonReader.Read(textReader);
		}

		private static void JsonDotNet()
		{
			var result = JsonConvert.DeserializeObject<Person>(sample);
		}
	}

}
