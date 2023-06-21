using System;
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
				var t = prop.PropertyType;
				
				if (t == typeof(bool))
				{
					prop.SetValue(target, (bool)val);
				}
				else if(t == typeof(decimal))
				{
					prop.SetValue(target, (decimal)val);
				}
				else if(t == typeof(double))
				{
					prop.SetValue(target, (double)val);
				}
				else if(t == typeof(float))
				{
					prop.SetValue(target, (float)val);
				}
				else if(t == typeof(int))
				{
					prop.SetValue(target, Convert.ToInt32(val));
				}
				else if(t == typeof(string))
				{
					prop.SetValue(target, (string)val);
				}
			}
			else
			{
				var attr = prop.GetCustomAttributes(typeof(FirestoreDocumentIdAttribute)).FirstOrDefault() as FirestoreDocumentIdAttribute;
				
				if (attr != null)
				{
					prop.SetValue(target, source.Id);
				}
			}
		}
	}
	
}
