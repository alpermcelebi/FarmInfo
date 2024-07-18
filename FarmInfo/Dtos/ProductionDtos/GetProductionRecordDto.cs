namespace FarmInfo.Dtos.ProductionDtos
{
    public class GetProductionRecordDto
    {
        public int Id { get; set; }

        public int CowId { get; set; }
        public float Quantity { get; set; }
        public DateTime Date { get; set; }
    }
}
