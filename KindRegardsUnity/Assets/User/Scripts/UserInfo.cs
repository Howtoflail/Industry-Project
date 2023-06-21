using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Firebase.Firestore;

[StorageCollection("Users")]
[FirestoreData]
public class UserInfo : MonoBehaviour
{
	public UnityEvent onLoaded;
	private FirestoreManager mgr;

	[FirestoreDocumentId]
	public string uniqueId
	{
		get;
		private set;
	}

	private void Load()
	{
		mgr.GetObject(this, result => {
			if (result != null)
			{
				FirestoreDataTransferer.TransferTo(result as DocumentSnapshot, this);
			}
			
			onLoaded.Invoke();
		});
	}

	void Start()
	{
		#if UNITY_IOS
			uniqueId = Device.vendorIdentifier;
		#else
			uniqueId = SystemInfo.deviceUniqueIdentifier;
		#endif

		mgr = GameObject.FindWithTag("FirestoreManager").GetComponent<FirestoreManager>();

		if (mgr.ready == false)
		{
			mgr.onLoaded.AddListener(Load);
		}
		else
		{
			Load();
		}
	}
}


