namespace GeminiSample.Model
{
    [GeminiOptions(typeof(ParentsOptions), "Info1", "Info2")]
    public class SubOptions
    {

        public int Age { get; set; }

        public string Name { get; set; }

    }
}
