using System.Linq;
using System.Reflection;
using Firebase.Firestore;

using UnityEngine;

public static class FirestoreDataTransferer
{
	private static string GetPropName(PropertyInfo prop)
	{
		var attr = prop.GetCustomAttributes(typeof(FirestorePropertyAttribute)).FirstOrDefault() as FirestorePropertyAttribute;
		return (attr != null && attr.Name != null) ? attr.Name : prop.Name;
	}

	public static void TransferTo(DocumentSnapshot source, object target)
	{
		foreach (var prop in target.GetType().GetProperties())
		{
			var name = GetPropName(prop);
			
			if (source.TryGetValue<object>(name, out object val))
			{
				switch(prop.GetValue(target))
				{
					case bool:
						prop.SetValue(target, (bool)val);
						break;
					case decimal:
						prop.SetValue(target, (decimal)val);
						break;
					case double:
						prop.SetValue(target, (double)val);
						break;
					case float:
						prop.SetValue(target, (float)val);
						break;
					case int:
						prop.SetValue(target, (int)val);
						break;
					case string:
						prop.SetValue(target, (string)val);
						break;
				}
			}
		}
	}
	
}
