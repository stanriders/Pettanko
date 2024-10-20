
namespace Pettanko.Performance
{
    public class TaikoPerformanceAttributes : PerformanceAttributes
    {
        public double Difficulty { get; set; }
        
        public double Accuracy { get; set; }
        
        public double EffectiveMissCount { get; set; }
        
        public double? EstimatedUnstableRate { get; set; }
    }
}
