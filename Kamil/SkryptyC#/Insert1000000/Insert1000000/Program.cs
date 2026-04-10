using Insert1000000;

Inserter inserter = new Inserter();

Selector s = new Selector();

//for( int  i = 0; i < 1; i++)

var time = DateTime.Now;
inserter.InsertFromXml();
Console.WriteLine(DateTime.Now - time);

time = DateTime.Now;
inserter.Insert();
Console.WriteLine(DateTime.Now - time);



//var normalTime = DateTime.Now - DateTime.Now;

//int testCount = 1;
//for (int i = 0; i < testCount; i++)
//{
//    var start = DateTime.Now;
//    s.SelectMultiThread(10);
//    multyTime +=DateTime.Now - start;

//    start = DateTime.Now;
//s.SelectToXML();

//    normalTime += DateTime.Now - start;
//    Console.WriteLine(multyTime + " " + normalTime);
//}
//Console.WriteLine("multi 10 thread time: " + (multyTime / testCount));
//Console.WriteLine("multi 5 thread time: " + (normalTime / testCount));

