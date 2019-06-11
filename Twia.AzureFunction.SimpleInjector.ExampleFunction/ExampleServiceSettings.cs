public class ExampleServiceSettings : IExampleServiceSettings
{
    public static string SectionName = "ExampleService";

    public string ExampleGreeting { get; set; } = "Hello World!";

    public string ExampleGreetingFromEnvironment { get; set; }

}