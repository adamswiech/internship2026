using Insert1000000;

Inserter inserter = new Inserter();

Selector s = new Selector();



string query = "select * from users u where u.Miasto like 'N%'";

var time = DateTime.Now;
//s.Select(query);
//Console.WriteLine(DateTime.Now - time);

query = "select * from users u where u.Nazwisko like 'N%'";

var timeAll = DateTime.Now - DateTime.Now;
var timeAllSelect = DateTime.Now - DateTime.Now;
int max = 10;
for (int i = 0; i < max; i++)
{
    time = DateTime.Now;
    inserter.Insert();
    timeAll += DateTime.Now - time;
    //Console.WriteLine("insert time: " + $"{DateTime.Now - time}");

    //time = DateTime.Now;
    //s.Select2("select * from users u where u.Imie = 'Piotr'");
    //timeAllSelect += DateTime.Now - time;
    //Console.WriteLine("select time: " + $"{DateTime.Now - time}");
}
//Console.WriteLine("\ninsert time avg:" + $"{timeAll / max}");
//Console.WriteLine("\nselect time avg:" + $"{timeAllSelect / max}");
//s.Select(query);





















//for( int  i = 0; i < 1; i++)

//inserter.InsertFromXml();




//s.Select();
//var normalTime = DateTime.Now - DateTime.Now;

//int testCount = 1;
//for (int i = 0; i < testCount; i++)
//{
//    var start = DateTime.Now;
//    multyTime +=DateTime.Now - start;

//    start = DateTime.Now;
//s.SelectToXML();

//    normalTime += DateTime.Now - start;
//    Console.WriteLine(multyTime + " " + normalTime);
//}
//Console.WriteLine("multi 10 thread time: " + (multyTime / testCount));
//Console.WriteLine("multi 5 thread time: " + (normalTime / testCount));

