using System.Collections.Generic;

namespace DataBaseLib.DataModel
{
    public class User : BaseDataModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> ListOfEmail { get; set; }
    }
}