using xmlInsert;



string filePS = Path.GetFullPath(@"..\..\..\DataSmall.xml");
string fileP = Path.GetFullPath(@"..\..\..\Data.xml");
string fileXXS = Path.GetFullPath(@"..\..\..\DataXXS.xml");
InsertXml xmlI = new InsertXml();

//xmlI.InsertLinq(fileXXS);
//xmlI.InsertLinqBatch(fileXXS);
//xmlI.InsertSerialize(fileXXS);
xmlI.InsertSerializeBulk(fileXXS);

