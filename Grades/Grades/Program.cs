
// See https://aka.ms/new-console-template for more information

using Csv;

var classAndCates = new Dictionary<string, string[]>()
{
    {"Math", new [] {"All Work"}},
    {"English", new [] {"Assessments", "Participation"}},
    {"History", new [] {"Section Assessments", "Annotated Bibliography", "Engagement"}},
    {"Chem", new [] {"Exams", "Labs", "Graded Homework"}},
    {"Spanish", new [] {"Summative Assessments", "Projects/Quizzes", "Daily Practice"}}
    
};

var cateWeights = new Dictionary<string, int>()
{
    { "Math-All Work", 100 },
    { "English-Assessments", 85 },
    { "English-Participation", 15 },
    { "History-Section Assessments", 75 },
    { "History-Annotated Bibliography", 15 },
    { "History-Engagement", 10 },
    { "Chem-Exams", 60 },
    { "Chem-Labs", 30 },
    { "Chem-Graded Homework", 10 },
    { "Spanish-Summative Assessments", 55 },
    { "Spanish-Projects/Quizzes", 25 },
    { "Spanish-Daily Practice", 20 }
};
var dataTable = new CSV();

var path = "dataTable.csv";

/*try
{
     // Open a file given a path to a CSV.
     dataTable = new CSV(File.OpenRead(path));
}
catch (Exception ex)
{
     Console.WriteLine(ex.Message);
}*/

var quit = false;
    
while (!quit){
    Console.WriteLine("What do you want to do? " +
                      "\n 0 quit " +
                      "\n 1 add one assignment " +
                      "\n 2 add many assignments " +
                      "\n 3 delete an assignment " +
                      "\n 4 grade for one category " +
                      "\n 5 grade for one class" +
                      "\n 6 clear all grades");
    try
    {
        var choice = int.Parse(Console.ReadLine());
        
        if (choice == 0)
            quit = true;
        else if (choice == 1)
            addOneAssignment();
        else if (choice == 2)
            addManyAssignments();
        else if (choice == 3) 
            deleteOneAssignment();
        else if (choice == 4)
            gradeForOneCate();
        else if (choice == 5)
            gradeForOneClass();
        else if (choice == 6)
            clearTable();
    }
    catch(Exception ex){Console.WriteLine(ex.Message);}
    
    Console.Clear();
}

//_______METHODS_______

string getClass()
{
    var classString = "What class? (";
    var num = 0;
    var first = true;
    foreach (var element in classAndCates.Keys)
    {
        if (!(first))
            classString += ", ";
        classString += $"{num}:{element}";
        num++;
        first = false;
    }
    classString += ") ";
    Console.WriteLine(classString);
    var classNum = int.Parse(Console.ReadLine());
    return classAndCates.Keys.ToList()[classNum];
}

string getClassAndCate()
{
    //get class
    var theClass = getClass();
    
    //writes cate string
    Console.Write("What category? (");
    var categoryList = classAndCates[theClass];
    var first1 = true;
    for (int i = 0; i < categoryList.Count(); i++)
    {
        if (!first1)
            Console.Write(", ");
        Console.Write($"{categoryList[i]}:{i}");
        first1 = false;
    }
    Console.Write(") ");
    
    //gets cate data
    var category = categoryList[int.Parse(Console.ReadLine())];

    return $"{theClass}-{category}";
}

void addOneAssignment()
{
    var classAndCate = getClassAndCate();

    //get assignment name
    Console.WriteLine("what is the name of the assignment?");
    var name = Console.ReadLine();
    
    //get score
    Console.WriteLine("Write score in this format: pointsGotten/totalPoints");
    var scoreString = new string[2];
   
    try{scoreString = Console.ReadLine().Split("/");}
    catch
    {
        Console.WriteLine("wrong format try again");
        addOneAssignment();
    }
    
    double pointsGotten = double.Parse(scoreString[0]);
    double totalPoints = double.Parse(scoreString[1]);
    double percent = Math.Round(pointsGotten / totalPoints, 4) * 100;
    
    //check that its correct
    Console.WriteLine($"Is this correct? \n {classAndCate[0]}:{classAndCate[1]}:{name}:{pointsGotten}:{totalPoints}:{percent}% \n [y or n]");
    var answer = Console.ReadLine();
    if (!(answer == "y"))
        addOneAssignment();
    
    //add to table
    Row row = new Row()
    {
        {"Class and Category", classAndCate},
        {"Category Weight", cateWeights[classAndCate].ToString()},
        {"Name", name},
        {"Points Gotten", $"{pointsGotten}"},
        {"Total Points", $"{totalPoints}"},
        {"Percentage", $"{percent}"},
    };
    dataTable.Add(row);
    dataTable.Save(File.OpenWrite(path));
    Console.WriteLine("Assignment Successfully Added!");
}

void addManyAssignments()
{
    var classAndCate = getClassAndCate();
    
    //get rest of assignment
    Console.WriteLine("Put assignments in this format: \n nameOfAssignment:pointsGotten:totalPoints-nameOfAssignment:pointsGotten:totalPoints...ect");
    var assignmentsString = Console.ReadLine()!.Split("-");
    
    //check if its right
    Console.WriteLine("Is this correct?");
    foreach (var element in assignmentsString)
    {
        Console.WriteLine(element);
    }
    Console.WriteLine("[y or n]");
    
    var answer = Console.ReadLine();
    if (!(answer == "y"))
        addManyAssignments();

    //add assignments to csv
    foreach (var element in assignmentsString)
    {
        var partsOfIt = element.Split(":");
        var percent = Math.Round(double.Parse(partsOfIt[1]) / double.Parse(partsOfIt[2]), 4) * 100;
        Row row = new Row()
        {
            {"Class and Category", classAndCate},
            {"Category Weight", cateWeights[classAndCate].ToString()},
            {"Name", partsOfIt[0]},
            {"Points Gotten", $"{partsOfIt[1]}"},
            {"Total Points", $"{partsOfIt[2]}"},
            {"Percentage", $"{percent}"},
        };
        dataTable.Add(row);
        dataTable.Save(File.OpenWrite(path));
        Console.WriteLine("Assignment Successfully Added!");
    }
}

void deleteOneAssignment()
{
    var classAndCate = getClassAndCate();
    
    //get name
    Console.WriteLine("Which assignment is it?");
    Console.WriteLine(dataTable.AllKeys);
}

void gradeForOneCate()
{
    var classAndCate = getClassAndCate();
    var assignments = dataTable.GetRow("Math-All Work", "Chapter 1 Test");
    Console.WriteLine(assignments.ToString());
}

void gradeForOneClass()
{
    var theClass = getClass();
}

void clearTable()
{
    CSV emptyTable = new CSV();

    for (int i = 0; i < 20; i++)
    {
        Row row = new Row()
        {
            {"Class and Category", ""},
            {"Category Weight", ""},
            {"Name", ""},
            {"Points Gotten", ""},
            {"Total Points", ""},
            {"Percentage", ""},
        };
        emptyTable.Add(row);
    }
    
    Console.WriteLine("Are you sure you want to clear the entire table? [y or n]");
    var answer = Console.ReadLine();
    if (answer == "y")
    {
        Console.WriteLine("But are you really sure because you can't undo this? [y or n]");
        var answer2 = Console.ReadLine();
        if (answer2 == "y")
        {
            emptyTable.Save(File.OpenWrite("dataTable.csv"));
        }
    }

}






