using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WriteExample : MonoBehaviour
{
	private FirestoreManager mgr;

	private void RunExample()
	{
		var data = GetComponent<DataExample>();
		//provide with an annotated object and a function that is called on receive
		mgr.SetObject(data, result => {
			print($"data id is: {result}");
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
