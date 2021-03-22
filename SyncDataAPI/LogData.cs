namespace SyncDataAPI
{
    public class LogData
    {
        //Use string for Guids
        public string Id { get; set; }
        public int FormId { get; set; }
        public int SubApplicationId { get; set; }
        public int FieldId { get; set; }
        public int InventoryCountPrevious { get; set; }
        public int InventoryCountNew { get; set; }
        public string InventoryDescPrevious { get; set; }
        public string InventoryDescNew { get; set; }     
            
        //Would use an actual customer object here
        public string UpdatedBy { get; set; }
    }
}
