namespace KindRegardsApi.Entity
{
    public class DeviceEntity
    {
        public string Id {get; set;} = "";

        // Default constructor
        public DeviceEntity(){}

        public DeviceEntity(string id)
        {
            this.Id = id;
        }
    }
}
