using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Firebase.Firestore;

[StorageCollection("pets")]
[FirestoreData]
public class PetInfo : MonoBehaviour
{
	public UnityEvent onLoaded;
	private UserInfo userInfo;

	public void LoadInfo()
	{
		userInfo = GameObject.FindWithTag("UserInfo").GetComponent<UserInfo>();
		id = userInfo.uniqueId;
		
		var mgr = GameObject.FindWithTag("FirestoreManager").GetComponent<FirestoreManager>();
		
		mgr.GetObject(this, result => {
			if (result != null)
			{
				//FirestoreDataTransferer.TransferTo(result as DocumentSnapshot, this);
				print(petName);
			}
			
			onLoaded.Invoke();
		});
	}
	
	[FirestoreDocumentId]
	public string id
	{
		get;
		private set;
	}

	[FirestoreProperty]
	public string petName
	{
		get;
		set;
	}

	[FirestoreProperty]
	public string petType
	{
		get;
		set;
	}

	[FirestoreProperty]
	public string petColor
	{
		get;
		set;
	}
}
