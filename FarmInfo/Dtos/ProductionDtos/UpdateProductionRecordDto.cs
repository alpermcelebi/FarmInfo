namespace FarmInfo.Dtos.ProductionDtos
{
    public class UpdateProductionRecordDto
    {
        public int CowId { get; set; }
        public float Quantity { get; set; }
        public DateTime Date { get; set; }
    }
}
