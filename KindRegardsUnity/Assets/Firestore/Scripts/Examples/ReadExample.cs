using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;

public class ReadExample : MonoBehaviour
{
	private FirestoreManager mgr;

	private void RunExample()
	{
		var data = GetComponent<DataExample>();
		//provide with an annotated object and a function that is called on receive
		mgr.GetObject(data, result => {
			FirestoreDataTransferer.TransferTo(result as DocumentSnapshot, data);
			print($"data name: {data.name}");
		});
	}

	void Start()
	{
		mgr = GameObject.FindWithTag("FirestoreManager").GetComponent<FirestoreManager>();

		if (mgr.ready == false)
		{
			//wait if not ready
			mgr.onLoaded.AddListener(RunExample);
		}
		else
		{
			RunExample();
		}
	}
}
