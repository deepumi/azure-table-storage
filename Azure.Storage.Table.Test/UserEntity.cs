namespace Azure.Storage.Table.Test
{
    public class UserEntity : TableEntity
    {
        public string Email { get; set; }

        public string Phone { get; set; }

        public UserEntity() : base("Users") { }
    }
}
