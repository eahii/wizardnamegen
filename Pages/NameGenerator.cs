using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class NameGenerator
{
    private List<string> FirstPart { get; set; }
    private List<string> SecondPart { get; set; }
    private static readonly Random Random = new Random();

    public NameGenerator(string filePath)
    {
        LoadNamesFromJson(filePath);
    }

    private void LoadNamesFromJson(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"JSON file not found at path: {filePath}");
        }

        var json = File.ReadAllText(filePath);
        var nameParts = JsonConvert.DeserializeObject<NameParts>(json);

        FirstPart = nameParts.FirstPart ?? new List<string>();
        SecondPart = nameParts.SecondPart ?? new List<string>();
    }

    public string GenerateName()
    {
        var first = FirstPart[Random.Next(FirstPart.Count)];
        var second = SecondPart[Random.Next(SecondPart.Count)];
        return $"{first}{second}";
    }

    public void SaveNameToJson(string name, string filePath)
    {
        var names = new List<string>();

        if (File.Exists(filePath))
        {
            var json = File.ReadAllText(filePath);
            names = JsonConvert.DeserializeObject<List<string>>(json) ?? new List<string>();
        }

        names.Add(name);

        var serializedNames = JsonConvert.SerializeObject(names, Formatting.Indented);
        File.WriteAllText(filePath, serializedNames);
    }

    private class NameParts
    {
        public List<string> FirstPart { get; set; }
        public List<string> SecondPart { get; set; }
    }
}
