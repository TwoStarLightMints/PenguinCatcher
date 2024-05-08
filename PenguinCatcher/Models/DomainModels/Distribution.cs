namespace PenguinCatcher.Models.DomainModels
{
    public class Distribution
    {
        public int DistributionID { get; set; }
        public string DistroName { get; set; } = string.Empty;
        public string ReleaseCycle { get; set; }
        public string Developer { get; set; }
        public string DistroURL { get; set; }
    }
}
