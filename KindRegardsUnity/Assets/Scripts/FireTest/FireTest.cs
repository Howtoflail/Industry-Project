using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;
using Firebase.Analytics;

public class FireTest : MonoBehaviour
{
	FirebaseFirestore store;

    void Start()
    {
		FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
			FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);

			store = FirebaseFirestore.DefaultInstance;
			Run();
		});
	}

	void Run()
	{
		FireObj obj = new FireObj { name = "user" };

		DocumentReference objRef = store.Collection("users").Document("user");
		objRef.SetAsync(obj).ContinueWithOnMainThread(t => {
			Debug.Log(t.IsCompleted);
		});
	}
}
