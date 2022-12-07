// See https://aka.ms/new-console-template for more information

using Csv;

Console.WriteLine("Hello, World!");

CSV GradesTable = new CSV();

var classes = new List<string> () {"chem" ,"histroy","english", "math", "spanish"}
var assingments = new List<string>();

for (int i = 0; i < assingments.Count; i++)
{
    Row row = new Row()
    {
        { "Classes", "" }
    };
}