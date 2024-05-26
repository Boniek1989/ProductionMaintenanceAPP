namespace ZPZP.Models
{
    public class Products
    {
        public int Id { get; set; }
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
        public DateTime ProjectDate  {get; set;}
        public DateTime ProjectDateStart { get; set;}
        public DateTime ProductionDateStart { get; set;}
        public DateTime ProductionDateEnd { get; set;}
        public DateTime QualityDateAssigned { get; set;}
        public DateTime QualityWorkerDateStart { get; set;}
        public DateTime QualityWorkerDateEnd { get; set;}   
        public DateTime LogisticsWorkerDateAssigned { get; set;}
        public DateTime LogisticsWorkerDateStart { get; set;}
        public DateTime LogisticsWorkerDateEnd { get; set;}
        public string? Status { get; set; }

    }
}
