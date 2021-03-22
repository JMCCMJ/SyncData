using System.Collections.Generic;

namespace SyncDataAPI
{
    public class Form
    {
        //Use string for Guids
        public int FormId { get; set; }

        public string FormName { get; set; }

        //Would use an actual customer object here
        public string UpdatedBy { get; set; }
        public List<Field> Fields { get; set; } = new List<Field>();

    }

    public class Field
    {
        //Use string for Guids
        public int FieldId { get; set; }
        public int InventoryCount { get; set; }
        public string InventoryDesc { get; set; }

        public int FormId { get; set; }

    }
}
