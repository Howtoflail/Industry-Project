using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;

[FirestoreData]
[StorageCollection("stickers")]
public class Sticker : MonoBehaviour
{
	[FirestoreDocumentId]
	public string id
	{
		get;
		set;
	}
	
	[FirestoreProperty]
	public string user
	{
		get;
		set;
	}
	
	[FirestoreProperty]
	public int stickerNumber
	{
		get;
		set;
	}
}
