namespace PenguinCatcher.Areas.User.Models.ViewModels
{
    public class DistroViewModel
    {
        public string ReleaseCycle { get; set; }
        public string Developer { get; set; }
        public string DistroUrl { get; set; }
        public string DistroName { get; set; } = string.Empty;
    }
}
