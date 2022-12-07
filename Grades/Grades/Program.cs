// See https://aka.ms/new-console-template for more information

using Csv;


Console.WriteLine("Hello, World!");

CSV GradesTable = new CSV();

//Get data from txt file
var file = File.OpenRead("semOneGrades.txt");
var reader = new StreamReader(file);
var gradesString = "";

while (!reader.EndOfStream)
{
    var line = reader.ReadLine()!;
    gradesString += line;
}

var assingments = new List<string>();

for (int i = 0; i < assingments.Count; i++)
{
    Row row = new Row()
    {
        { "Classes", "" }
        {"Category", ""}
    };
}