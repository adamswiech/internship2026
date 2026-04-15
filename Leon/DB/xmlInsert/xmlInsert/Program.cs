using xmlInsert;



string filePS = Path.GetFullPath(@"..\..\..\DataSmall.xml");
string fileP = Path.GetFullPath(@"..\..\..\Data.xml");
string fileXXS = Path.GetFullPath(@"..\..\..\DataXXS.xml");
string file1M = Path.GetFullPath(@"..\..\..\Data1M.xml");
InsertXml xmlI = new InsertXml();

//xmlI.InsertLinq(fileXXS);
//xmlI.InsertLinqBulk(file1M);
//xmlI.InsertSerialize(fileXXS);
//xmlI.InsertSerializeBulk(fileXXS);



//xmlI.InsertLinq(fileXXS);
xmlI.InsertLinqBulk(file1M);
//xmlI.InsertSerialize(fileXXS);
//xmlI.InsertSerializeBulk(fileXXS);
//xmlI.InsertTest(file1M);
