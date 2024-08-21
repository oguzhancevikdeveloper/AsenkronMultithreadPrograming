
/*
   Thread senkronizasyonu ve volatile anahtar kelimesi gibi konular, 
   çok iş parçacıklı (multi-threaded) programlama ile ilgilidir. 
   Bu konularda bilgi sahibi olmak, özellikle performansın önemli olduğu uygulamalarda, 
   veri tutarlılığını ve iş parçacıkları arasındaki iletişimi doğru bir şekilde yönetmek açısından önemlidir.

 */

MyClass my = new();

my.X();


class MyClass
{
    int counter = 0;

    public void X()
    {
        Thread thread1 = new(() =>
        {
            Random rnd = new();

            while (true)
            {
                Volatile.Write(ref counter, rnd.Next());
            }

        });
        Thread thread2 = new(() =>
        {
            while (true)
                Console.WriteLine(Volatile.Read(ref counter));
        });

        thread1.Start();
        thread2.Start();
    }

}
