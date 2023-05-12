using Firebase.Firestore;

[FirestoreData]
public struct FireObj
{
	[FirestoreProperty]
	public string name { get; set; }
}
