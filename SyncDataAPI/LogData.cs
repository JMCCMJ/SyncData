using System;

namespace SyncDataAPI
{
    public class LogData
    {
        //Use string for Guids
        public string Id { get; set; }
        public int FormId { get; set; }
        public string SubApplicationId { get; set; }
        public int FieldId { get; set; }
        public int InventoryCount { get; set; }
        public string InventoryDesc { get; set; }     
            
        //Would use an actual customer object here
        public string UpdatedBy { get; set; }

        public LogData(int FormId, string SubApplicationId, int FieldId, int InventoryCount, string InventoryDesc, string UpdatedBy)
        {
            this.Id = Guid.NewGuid().ToString();
            this.FormId = FormId;
            this.SubApplicationId = SubApplicationId;
            this.FieldId = FieldId;
            this.InventoryCount = InventoryCount;
            this.InventoryDesc = InventoryDesc;
            this.UpdatedBy = UpdatedBy;
        }
    }
}
