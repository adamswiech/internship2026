using Insert1000000;

Inserter inserter = new Inserter();

Selector s = new Selector();

//for( int  i = 0; i < 1; i++)
    //inserter.Insert();

//var multyTime = DateTime.Now  - DateTime.Now;
//var normalTime = DateTime.Now - DateTime.Now;

//int testCount = 1;
//for (int i = 0; i < testCount; i++)
//{
//    var start = DateTime.Now;
//    s.SelectMultiThread(10);
//    multyTime +=DateTime.Now - start;

//    start = DateTime.Now;
    s.SelectToXML();
//    normalTime += DateTime.Now - start;
//    Console.WriteLine(multyTime + " " + normalTime);
//}
//Console.WriteLine("multi 10 thread time: " + (multyTime / testCount));
//Console.WriteLine("multi 5 thread time: " + (normalTime / testCount));

