using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Firebase;
using Firebase.Extensions;
using Firebase.Firestore;
using Firebase.Analytics;

public class FirestoreManager : MonoBehaviour
{
	public UnityEvent onLoaded;

	public bool ready
	{
		get;
		private set;
	}

	private FirebaseFirestore store;

	private string GetCollectionName(object obj)
	{
		var colAttr = Attribute.GetCustomAttribute(
			obj.GetType(),
			typeof(StorageCollectionAttribute)
		) as StorageCollectionAttribute;

		if (colAttr == null) return null;

		return colAttr.collectionName;
	}

	private string GetDocumentKey(object obj)
	{
		foreach (var info in obj.GetType().GetProperties())
		{
			foreach (var attr in info.CustomAttributes)
			{
				if (attr.AttributeType == typeof(FirestoreDocumentIdAttribute))
				{
					return (string)info.GetValue(obj);
				}
			}
		}

		return null;
	}
	
	private void CheckForErrors(System.Threading.Tasks.Task task)
	{
		if (task.Exception != null)
		{
			Debug.LogError(task.Exception);
		}
	}

	public void SetObject(object obj, Action<string> callback)
	{
		var collection = GetCollectionName(obj);
		var key = GetDocumentKey(obj);

		if (collection == null) return;

		if (String.IsNullOrEmpty(key))
		{
			store.Collection(collection).AddAsync(obj)
				.ContinueWithOnMainThread(task => {
					if (callback != null) callback(task.Result.Id);
				}).ContinueWithOnMainThread(CheckForErrors);
		}
		else
		{
			store.Collection(collection).Document(key)
				.SetAsync(obj).ContinueWithOnMainThread(task => {
					if (callback != null) callback(key);
				})
				.ContinueWithOnMainThread(CheckForErrors);
		}
	}

	public void GetObject(object obj, Action<object> callback)
	{
		var collection = GetCollectionName(obj);
		var key = GetDocumentKey(obj);

		if (collection == null || key == null || callback == null) return;

		store.Collection(collection).Document(key)
			.GetSnapshotAsync().ContinueWithOnMainThread(task => {
				callback(task.Result);
			})
			.ContinueWithOnMainThread(CheckForErrors);
	}

	void Start()
	{
		FirebaseApp.CheckAndFixDependenciesAsync()
		.ContinueWithOnMainThread(task => {
			FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
			store = FirebaseFirestore.DefaultInstance;
			ready = true;
			onLoaded.Invoke();
		});
	}
}
