using System.ComponentModel.DataAnnotations;

namespace WebApplicationRevision.OptionPatternsClasses
{
    public class WeatherOptions
    {
        public const string SectionName = "Weather";

        //[Required(AllowEmptyStrings = false)]
        public string City { get; set; }

        //[Required(AllowEmptyStrings = false)]
        public string State { get; set; }

        //[Range(0, 100)]
        public int Teampreature { get; set; }
        public string Summury { get; set; }
    }
}
