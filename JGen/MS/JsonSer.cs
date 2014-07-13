using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace JGen.MS
{
	public class JSONTypeHelper
	{
		public Type Type;
		// All properties and fields of a type with their inherited custom attributes
		public KeyValuePair<MemberInfo, Attribute[]>[] Props;

		public JSONTypeHelper(Type type)
		{
			Type = type;
			List<MemberInfo> members = new List<MemberInfo>();
			members.AddRange(type.GetRuntimeProperties());
			members.AddRange(type.GetRuntimeFields());
			Props = new KeyValuePair<MemberInfo, Attribute[]>[members.Count];
			for (int i = 0; i < members.Count; ++i)
			{
				MemberInfo m = members[i];
				Props[i] = new KeyValuePair<MemberInfo, Attribute[]>(m, Attribute.GetCustomAttributes(m));
			}
		}
	}

	public class JSONSerializer
	{
		static Dictionary<Type, JSONTypeHelper> typeCache = new Dictionary<Type, JSONTypeHelper>();

		private delegate void Func(object obj, Dictionary<string, object> dict, string key);

		public Type Type
		{ get; set; }

		public JSONSerializer()
			: this(null)
		{
		}

		public JSONSerializer(Type type)
		{
			this.Type = type;
		}

		public object Deserialize(object obj)
		{
			return Deserialize(obj, null);
		}

		public object Deserialize(object obj, string key)
		{
			return Deserialize(obj, this.Type, key, null);
		}

		private object Deserialize(object obj, Type objType, string key, object inObj)
		{
			object result = null;
			if (isDictionary(objType) || isList(objType) || objType.IsArray || isPrimitiveType(objType) || objType.GetTypeInfo().IsEnum)
			{
				result = CreateObjectOfType(objType, obj, null);
			}
			else
			{
				Dictionary<string, object> dictObj = obj as Dictionary<string, object>;
				result = inObj ?? Activator.CreateInstance(objType);

				/* Get properties and fields */
				JSONTypeHelper mProps = null;
				lock (typeCache)
				{
					if (!typeCache.TryGetValue(objType, out mProps))
					{
						mProps = new JSONTypeHelper(objType);
						typeCache[objType] = mProps;
					}
				}
				for (int i = 0; i < mProps.Props.Length; ++i)
				{
					var pair = mProps.Props[i];
					for (int j = 0; j < pair.Value.Length; ++j)
					{
						Attribute attribute = pair.Value[j];
						if (attribute is JSONKeyAttribute)
						{
							SetMemberValue(pair.Key, result, key);
						}
						else if (attribute is JSONValueAttribute)
						{
							JSONValueAttribute jsonAttribute = attribute as JSONValueAttribute;
							PropertyInfo pInfo = pair.Key as PropertyInfo;
							FieldInfo fInfo = pair.Key as FieldInfo;
							object member = (pInfo != null) ? pInfo.GetValue(result, null) : fInfo.GetValue(result);

							/* Retrive object from json part by specified path or name */
							Type t = (pInfo != null) ? pInfo.PropertyType : fInfo.FieldType;
							object valueObj = null;
							if (jsonAttribute.Path != null)
								valueObj = GetObjectByPath(dictObj, jsonAttribute.Path);
							else
								dictObj.TryGetValue(pair.Key.Name, out valueObj);
							if (valueObj != null)
							{
								if (t == typeof(object))
									SetMemberValue(pair.Key, result, valueObj);
								else
								{
									member = System.Convert.ChangeType(CreateObjectOfType(t, valueObj, member), t, CultureInfo.InvariantCulture.NumberFormat);
									SetMemberValue(pair.Key, result, member);
								}
							}
							else if (jsonAttribute.IsRequired)
							{
								throw new System.Runtime.Serialization.SerializationException("JSON error. Required field \"" + pair.Key.Name + "\" for " + t.ToString() + " not specified");
							}
						}
					}
				}
			}

			return result;
		}

		/* Function for setting member value */
		private static void SetMemberValue(MemberInfo member, object target, object value)
		{
			var asFieldInfo = member as FieldInfo;
			if (asFieldInfo != null)
			{
				asFieldInfo.SetValue(target, value);
			}
			else
			{
				var asPropertyInfo = member as PropertyInfo;
				if (asPropertyInfo != null)
				{
					asPropertyInfo.SetValue(target, value, null);
				}
			}
		}

		private object CreateObjectOfType(Type t, object obj, object inObj)
		{
			/* Primitive type */
			if (isPrimitiveType(t))
			{
				inObj = obj;
			}
			/* Enum */
			else if (t.GetTypeInfo().IsEnum)
			{
				inObj = Enum.Parse(t, (string)obj);
			}
			else if (t.IsArray)
			{
				Type itemType = t.GetElementType();
				if (obj is Object[])
				{
					Object[] arrayObj = obj as Object[];
					int num = arrayObj.Length;
					Array destArray = Array.CreateInstance(itemType, num);

					for (int i = 0; i < arrayObj.Length; ++i)
					{
						object val = Deserialize(arrayObj[i], itemType, null, null);
						destArray.SetValue(val, i);
					}
					inObj = destArray;

				}
				else if (obj is Dictionary<string, object>)
				{
					Dictionary<string, object> dictObj = obj as Dictionary<string, object>;
					int num = dictObj.Keys.Count;
					Array destArray = Array.CreateInstance(itemType, num);

					int i = 0;
					foreach (string key in dictObj.Keys)
					{
						object item = dictObj[key];
						object val = Deserialize(item, itemType, key, null);
						destArray.SetValue(val, i);
						i++;
					}
					inObj = destArray;
				}
			}
			/* Array or List */
			else if (isList(t))
			{
				Type itemType = t.GetTypeInfo().GenericTypeArguments[0];
				if (obj is Object[])
				{
					Object[] arrayObj = obj as Object[];
					IList destlist = Activator.CreateInstance(t) as IList;

					for (int i = 0; i < arrayObj.Length; ++i)
					{
						object val = Deserialize(arrayObj[i], itemType, null, null);
						destlist.Add(val);
					}
					inObj = destlist;

				}
				else if (obj is Dictionary<string, object>)
				{
					Dictionary<string, object> dictObj = obj as Dictionary<string, object>;
					IList destList = Activator.CreateInstance(t) as IList;

					foreach (string key in dictObj.Keys)
					{
						object item = dictObj[key];
						object val = Deserialize(item, itemType, key, null);
						destList.Add(val);
					}
					inObj = destList;
				}
			}
			/* Dictionary */
			else if (isDictionary(t))
			{
				Type[] arguments = t.GetTypeInfo().GenericTypeArguments;
				Type keyType = arguments[0];
				Type valuesType = arguments[1];

				if (keyType == typeof(string) && valuesType == typeof(object))
				{
					inObj = obj;
				}
				else
				{
					IDictionary dict = obj as IDictionary;
					IDictionary res = Activator.CreateInstance(t) as IDictionary;
					foreach (string key in dict.Keys)
					{
						object val = CreateObjectOfType(valuesType, dict[key], null);
						res.Add(System.Convert.ChangeType(key, keyType, CultureInfo.InvariantCulture.NumberFormat), val);
					}
					inObj = res;
				}
			}
			/* Class or Struct */
			else if (t.GetTypeInfo().IsClass || t.GetTypeInfo().IsValueType)
			{
				inObj = Deserialize(obj, t, null, inObj);
			}
			return inObj;
		}

		#region - Checking types

		private static bool isPrimitiveType(Type t)
		{
			return t.GetTypeInfo().IsPrimitive || t == typeof(Decimal) || t == typeof(String);
		}

		private static bool isDictionary(Type t)
		{
			return t.GetTypeInfo().ImplementedInterfaces.Contains(typeof(IDictionary));
		}

		private static bool isList(Type t)
		{
			return t.GetTypeInfo().ImplementedInterfaces.Contains(typeof(IList));
		}

		#endregion

		#region - Dictionary utils

		private object GetObjectByPath(Dictionary<string, object> dict, string[] path)
		{
			object obj = dict;
			foreach (string subpath in path)
			{
				(obj as Dictionary<string, object>).TryGetValue(subpath, out obj);
				if (obj == null)
					break;
			}
			return obj;
		}

		#endregion
	}
}
