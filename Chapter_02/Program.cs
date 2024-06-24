

//Console.WriteLine(":" + System.Environment.CurrentManagedThreadId);
//Console.WriteLine(":" + AppDomain.GetCurrentThreadId());
//Console.WriteLine(":" + Thread.CurrentThread.ManagedThreadId);

//Thread thread = new((o) =>
//{
//    for (int i = 0; i < 30; i++)
//    {
//        Console.WriteLine($"Thread: {i}");
//    }
//});

//thread.IsBackground = true;
//thread.Start();

//for (int i = 0; i < 30; i++)
//{
//    Console.WriteLine($"Main: {i}");
//}

//System.Threading.ThreadState threadState = System.Threading.ThreadState.WaitSleepJoin;
//System.Threading.ThreadState threadState2 = System.Threading.ThreadState.Stopped;
//System.Threading.ThreadState threadState3 = System.Threading.ThreadState.Unstarted;
//System.Threading.ThreadState threadState4 = System.Threading.ThreadState.SuspendRequested;


// Lock,locking ile ksıtlanan bölgeye tek bir thread erişirken,diğer bölgeler kritik bölgenin kilidin açılmasını bekler.

object _locking = new();
int i = 1;

Thread thread1 = new(() =>
{
    lock (_locking)
    {
        while (i <= 10)
        {
            Console.WriteLine(i++);
        }
    }
});

Thread thread2 = new(() =>
{
    lock (_locking)
    {
        while (i > 1)
        {
            Console.WriteLine(i--);
        }
    }
});

thread1.Start();
thread2.Start();

// Join, bir threadin diğer bir threadin işlemini bitirmesi için kullanılşan metottur.
// Anlayaçağın üzere threadler arasında senkron bir davranış sergilenmekte.

Thread thread3 = new(() =>
{
    for (int i = 0; i < 10; i++)
    {
        Console.WriteLine($"Thread3 - {i}");
    }
});

Thread thread4 = new(() =>
{
    for (int i = 0; i < 10; i++)
    {
        Console.WriteLine($"Thread4 - {i}");
    }
});

thread3.Start();
thread3.Join();
thread4.Start();

// Thread İptal Etme ,cancellationTokenSource

bool shouldStop = true;

Thread thread5 = new(() =>
{
    while (shouldStop)
    {
        Console.WriteLine("Thread is working!!");
        Thread.Sleep(452);
    }
    Console.WriteLine("thread is done");

});
thread5.Start();
Thread.Sleep(5000);
shouldStop = false;

Thread thread6 = new((cancellationTokenSource) =>
{
    var _cancellationTokenSource = (cancellationTokenSource as CancellationTokenSource);

    while (!_cancellationTokenSource.IsCancellationRequested)
    {
        Console.WriteLine("Thread working!!!");
        Thread.Sleep(845);
    }
    Console.WriteLine("thread is done");


});

CancellationTokenSource cancellationTokenSource = new();
thread6.Start(cancellationTokenSource);
Thread.Sleep(5000);
cancellationTokenSource.Cancel();


// Interrupt, threadi bekleyen durumdan uyandırmak ve çalışma durumunu kesintiye uğrastmak için kullanlır
Console.WriteLine("*****************************"); 
Thread thread7 = new(() =>
{
    try
    {
        Console.WriteLine("Thread beklemye geçti.");
        Thread.Sleep(Timeout.Infinite);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Thread uyarıldı.");
        throw;
    }
});

thread7.Start();    
thread7.Interrupt();