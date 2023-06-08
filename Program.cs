using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

var currentDirectory = Directory.GetCurrentDirectory();
var storesDirectory = Path.Combine(currentDirectory, "stores");

var salesTotalDir = Path.Combine(currentDirectory, "salesTotalDir");
Directory.CreateDirectory(salesTotalDir);
var salesFiles = FindFiles(storesDirectory);

var salesTotal = CalculateSalesTotal(salesFiles);

File.WriteAllText(Path.Combine(salesTotalDir, "total.txt"), string.Empty);

IEnumerable<string> FindFiles(string folderName)
{
    List<string> salesFiles = new List<string>();

    var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);

    foreach(var file in foundFiles)
    {
        // if(file.EndsWith("sales.json"))
        // {
        //     salesFiles.Add(file);
        // }

        var extension = Path.GetExtension(file);
        if(extension == ".json")
        {
            salesFiles.Add(file);
        }
    }

    return salesFiles;
}


var salesJson = File.ReadAllText($"stores{Path.DirectorySeparatorChar}201{Path.DirectorySeparatorChar}sales.json");
var salesData = JsonConvert.DeserializeObject<SalesTotal>(salesJson);

Console.WriteLine(salesData != null ? salesData.Total : 0);

var data = JsonConvert.DeserializeObject<SalesTotal>(salesJson);
File.WriteAllText($"salesTotalDir{Path.DirectorySeparatorChar}total.txt", data.Total.ToString());

var data1 = JsonConvert.DeserializeObject<SalesTotal>(salesJson);
//File.AppendAllText($"salesTotalDir{Path.DirectorySeparatorChar}totals.txt", $"{data1.Total}{Environment.NewLine}");
File.AppendAllText($"salesTotalDir{Path.DirectorySeparatorChar}totals.txt", $"{salesTotal}{Environment.NewLine}");



double CalculateSalesTotal(IEnumerable<string> salesFiles)
{
    double salesTotal = 0;

    // Loop over each file path in salesFiles
    foreach (var file in salesFiles)
    {      
        // Read the contents of the file
        string salesJson = File.ReadAllText(file);

        // Parse the contents as JSON
        SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);

        // Add the amount found in the Total field to the salesTotal variable
        salesTotal += data?.Total ?? 0;
    }
    return salesTotal;
}

record SalesData (double Total);

class SalesTotal
{
    public double Total {get;set;}
}

/*var currentDirectory = Directory.GetCurrentDirectory();
var storesDirectory = Path.Combine(currentDirectory, "stores");
var salesFiles = FindFiles(storesDirectory);
foreach(var file in salesFiles)
{
    Console.WriteLine(file);
}

IEnumerable<string> FindFiles(string folderName)
{
    List<string> salesFiles = new List<string>();

    var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);

    foreach(var file in foundFiles)
    {
        // if(file.EndsWith("sales.json"))
        // {
        //     salesFiles.Add(file);
        // }

        var extension = Path.GetExtension(file);
        if(extension == ".json")
        {
            salesFiles.Add(file);
        }
    }

    return salesFiles;
}*/

//Console.WriteLine($"stores{Path.DirectorySeparatorChar}201");
//Console.WriteLine(Path.Combine("stores", "201"));
//Console.WriteLine(Path.GetExtension("sales.json"));

/*string fileName = $"stores{Path.DirectorySeparatorChar}201{Path.DirectorySeparatorChar}sales{Path.DirectorySeparatorChar}sales.json";

FileInfo info = new FileInfo(fileName);

Console.WriteLine($"Full Name:{info.FullName}{Environment.NewLine}Directory: {info.Directory}{Environment.NewLine}Extension:{info.Extension}{Environment.NewLine}Create Date:{info.CreationTime}");*/

/*
Console.WriteLine(Directory.GetCurrentDirectory());

string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
Console.WriteLine(docPath);


var salesFiles = FindFiles("stores");


*/