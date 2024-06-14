using System.Collections;

#region Senkron
//IEnumerable<int> GetNumbers()
//{
//    for (int i = 0; i < 30; i++)
//    {
//        Thread.Sleep(1000);
//        yield return i;
//    }
//}
//foreach (var item in GetNumbers())
//    Console.WriteLine(item);

#endregion

#region Asenkron

//async IAsyncEnumerable<int> GetNumbersAsync()
//{
//    for (int j = 0; j < 30; j++)
//    {
//        await Task.Delay(1000);
//        yield return j;
//    }
//}

//await foreach (var item in GetNumbersAsync())
//    Console.WriteLine(item);
#endregion

#region Custom

// await foreach ifadesi, üzerinde çalışacağı nesnenin IAsyncEnumerable<T> arayüzünü uyguladığını varsayar.
// Bu arayüz, GetAsyncEnumerator metodunu içerir ve bu metod bir IAsyncEnumerator<T> döner.
NumberList numberlist = new NumberList();

await foreach (var number in numberlist)
    Console.WriteLine(number);

await using (var enumarator = numberlist.GetAsyncEnumerator())
{
    while (await enumarator.MoveNextAsync())
    {
        var number = enumarator.Current;
        Console.WriteLine(number);
    }
}


class NumberList : IAsyncEnumerable<int>
{
    List<int> numbers = new() { 1, 3, 5, 7, 9 };

    public async IAsyncEnumerator<int> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        foreach (var number in numbers)
        {
            await Task.Delay(500);
            yield return number;
        }

    }

    public async IAsyncEnumerator<int> GetAsyncEnum()
    {
        foreach (var number in numbers)
        {
            await Task.Delay(500);
            yield return number;
        }

    }

}

#endregion