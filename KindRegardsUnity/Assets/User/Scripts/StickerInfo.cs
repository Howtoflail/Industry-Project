using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;

public class StickerInfo : MonoBehaviour
{
	private UserInfo user;
	private FirestoreManager mgr;
	private GameObject stickerRes;

	void Start()
	{
		mgr = GameObject.FindWithTag("FirestoreManager").GetComponent<FirestoreManager>();
		user = GameObject.FindWithTag("UserInfo").GetComponent<UserInfo>();
		stickerRes = Resources.Load<GameObject>("Sticker");
	}

	public void LoadInfo()
	{
		if (mgr.ready == true)
		{
			var query = mgr.store.Collection("stickers").WhereEqualTo("user", user.uniqueId);
			
			query.GetSnapshotAsync().ContinueWithOnMainThread((querySnapshotTask) => {
				foreach (DocumentSnapshot doc in querySnapshotTask.Result.Documents)
				{
					var sticker = Instantiate(stickerRes, transform);
					var sInfo = sticker.GetComponent<Sticker>();
					FirestoreDataTransferer.TransferTo(doc, sInfo);
				}
			});
		}
	}
	
	public void Add(int number)
	{
		if (mgr.ready == true)
		{
			var sticker = Instantiate(stickerRes, transform);
			var sInfo = sticker.GetComponent<Sticker>();
			sInfo.user = user.uniqueId;
			sInfo.stickerNumber = number;

			mgr.SetObject(sInfo, id => {
				sInfo.id = id;
			});
		}
	}
}
