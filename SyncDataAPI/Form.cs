using System.Collections.Generic;

namespace SyncDataAPI
{
    public class Form
    {
        //Use string for Guids
        public string FormId { get; set; }

        public string FormName { get; set; }

        //Would use an actual customer object here
        public string UpdatedBy { get; set; }
        public Sync Sync { get; set; }        

    }

    public class Sync
    {
        public List<Field> Fields { get; set; }
    }

    public class Field
    {
        //Use string for Guids
        public string FieldId { get; set; }
        public int InventoryCount { get; set; }
        public string InventoryDesc { get; set; }       

    }
}
