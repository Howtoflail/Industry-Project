using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;

[StorageCollection("examples")]
[FirestoreData]
public class DataExample : MonoBehaviour
{
	//quick fix to set initial property values on unity
	[SerializeField]
	private string _key;
	[SerializeField]
	private string _name;
	
	[FirestoreDocumentId] //lets the manager know which document this is
	public string key { get { return _key; } set { _key = value; } }
	[FirestoreProperty] //marked to be saved
	public string name { get { return _name; } set { _name = value; } }

	public string ToString()
	{
		return $"example {key} {name}";
	}
}
