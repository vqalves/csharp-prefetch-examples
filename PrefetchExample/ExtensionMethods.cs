namespace System.Collections.Generic
{
    public static class ExtensionMethods
    {
        public static async IAsyncEnumerable<T> WithPrefetch<T>(this IAsyncEnumerable<T> enumerable)
        {
            await using(var enumerator = enumerable.GetAsyncEnumerator())
            {
                ValueTask<bool> hasNextTask = enumerator.MoveNextAsync();

                while(await hasNextTask)
                {
                    T data = enumerator.Current;
                    hasNextTask = enumerator.MoveNextAsync();
                    yield return data;
                }
            }
        }
    }
}