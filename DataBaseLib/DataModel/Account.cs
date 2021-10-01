namespace DataBaseLib.DataModel
{
    public class Account : BaseDataModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public Role Role { get; set; }
    }
}