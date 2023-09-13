using System.Diagnostics;

namespace PrefetchExample
{
    public class DemoIAsyncEnumerablePrefetch
    {
        public static async Task Execute()
        {
            Stopwatch st = new Stopwatch();
            st.Start();
            Console.WriteLine($"Beginning in {st.ElapsedMilliseconds}");
            await foreach(var item in EnumerateAsync().WithPrefetch())
            {
                Console.WriteLine($"Loop start in {st.ElapsedMilliseconds}");
                await Task.Delay(1_000);
                Console.WriteLine($"Loop end in {st.ElapsedMilliseconds}");
            }
            Console.WriteLine($"Finishing in {st.ElapsedMilliseconds}");
            st.Stop();
        }

        private static async IAsyncEnumerable<int> EnumerateAsync()
        {
            for(var i = 0; i < 10; i++)
            {
                await Task.Delay(1_000);
                yield return i;
            }
        }
    }
}