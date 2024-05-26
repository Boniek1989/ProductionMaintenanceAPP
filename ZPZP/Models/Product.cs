namespace ZPZP.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string? SerialNumber { get; set; }
        public string? Name { get; set; }
        public string? DescriptionProduction { get; set; }
        public string? DescriptionQuality { get; set; }
        public string? DescriptionLogistics { get; set; }
        public string? Author { get; set; }
        public string? ProductionWorkerAssigned1 { get; set; }
        public string? ProductionWorkerAssigned2 { get; set; }
        public string? ProductionWorkerAssigned3 { get; set; }
        public string? QualityWorkerAssigned1 { get; set; }
        public string? QualityWorkerAssigned2 { get; set; }
        public string? QualityWorkerAssigned3 { get; set; }
        public string? LogisticsWorkersAssigned1 { get; set; }
        public string? LogisticsWorkersAssigned2 { get; set; }
        public string? LogisticsWorkersAssigned3 { get; set; }
        public byte[]? ProductionDocumentation { get; set; }
        public byte[]? QualityDocumentation { get; set; }
        public byte[]? LogisticsDocumentation { get; set; }   
        public string? Status { get; set; }

    }
}
