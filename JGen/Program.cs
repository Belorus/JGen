using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JGen.Generators;
using JGen.Readers;
using SF.Common.API.Model;

namespace JGen
{
	internal class Program
	{
		private static void Main()
		{
			var str = @"{""service"":{""errorCode"":0},""result"":{""spinResult"":{""winLines"":[],""viewReels"":[[4,2,3,4,8],[5,2,3,5,8],[12,2,3,5,8]],""winAmount"":0.0,""totalFreeSpins"":0,""bonusChoosenRoom"":-1,""freeSpinsAmount"":0.0,""freeSpins"":0,""newFreeSpinsCount"":0,""giftFreeSpins"":0,""freeSpinsMultiplier"":0,""regularSpinsMultiplier"":1,""freeSpinsBonusWin"":0.0,""lastScatterChoose"":0,""giftFreeSpinsWin"":0.0,""freeSpinsDuringGiftFreeSpinsWin"":0.0,""freeSpinsDuringGiftFreeSpinsCount"":0,""linesCount"":50,""bonus"":false,""scatter"":false,""randomFreeSpins"":false,""scatterType"":""FREE_SPINS"",""payingScatterType"":""FREE_SPINS"",""bet"":{""id"":8,""value"":10.0,""levelId"":0},""nextSpinWillBeGift"":false,""newGiftFreeSpins"":0,""randomWilds"":false,""increasingFreeSpinMultiplier"":0,""betMultiplier"":{""id"":205,""value"":30.0,""levelId"":0},""minigameId"":0,""minigamePoolSize"":0.0,""reSpinCount"":0,""freeSpinModeId"":-1,""collapsing"":false,""collapsingWinAmount"":0.0},""userStatus"":{""balanceInCoins"":3103828.7299999986,""balanceInDiamonds"":0.0,""experience"":4.9474188514999866E8},""userLevel"":{""level"":250,""levelUp"":false,""nextLevelReward"":161000.0,""nextLevelExperience"":4.9782E8,""prevLevelExperience"":4.9082E8,""prevLevelReward"":160000.0,""coinsGiftAmount"":0.0,""specialBonusAmount"":0},""isForcedInApp"":false,""isDONEnabled"":false,""isUserDON"":false,""bonusLevel"":0,""bonusWinSum"":0.0,""bonusGameCompleted"":false,""replayBonus"":false,""winType"":""NORMAL"",""biggerCoinPackageUnlocked"":false,""experienceMultiplier"":1.0,""needToRestoreReplayBonus"":false,""baseItems"":[]}}";
			var r = new StringReader(str);
			var response = JsonSlotsServerResponseDtoOfNreSpinResponseReader.Read(r);

			Generator gen = CreateGeneratorForType(typeof (SlotsServerResponseDto<SF.Common.API.Model.GameNRE.NreSpinResponse>));
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