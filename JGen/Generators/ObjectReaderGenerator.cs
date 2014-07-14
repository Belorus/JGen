using System;
using System.Collections.Generic;
using System.Linq;
using JGen.Templates;

namespace JGen.Generators
{
	internal class ObjectReaderGenerator : Generator
	{
		private readonly Type _type;
		private readonly Dictionary<string, Generator> _innerGenerators;

		public ObjectReaderGenerator(Type type, Dictionary<string, Generator> inner )
		{
			_innerGenerators = inner;
			_type = type;
		}

		public override ReaderCode GenerateReader()
		{			
			Dictionary<string, ReaderCode> inner = _innerGenerators.ToDictionary(kv => kv.Key, kv => kv.Value.GenerateReader());

			var name = string.Format("Json{0}Reader", ReflectionUtils.GetHumanName(_type));
			return
				new ReaderCode()
				{
					ReaderName = name,
					Content = new JsonObjectReaderTemplate(ReflectionUtils.GetCodeName(_type), inner.Select(kv => new PropertyReaderReference()
					{
						ReaderName = kv.Value.ReaderName,
						PropertyName = kv.Key
					}).ToArray(), name).TransformText(),
					Dependend = inner.Values.ToArray()

				};
		}
	}
}