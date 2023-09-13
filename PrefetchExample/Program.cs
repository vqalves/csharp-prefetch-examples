using PrefetchExample;

internal class Program
{
    public static async Task Main(string[] args)
    {
        await DemoTaskChaining.Execute();
        await DemoIAsyncEnumerablePrefetch.Execute();

        Console.WriteLine("Completed");
    }    
}