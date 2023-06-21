public class StorageCollectionAttribute : System.Attribute
{
	public string collectionName;

	public StorageCollectionAttribute(string collectionName)
	{
		this.collectionName = collectionName;
	}
}
