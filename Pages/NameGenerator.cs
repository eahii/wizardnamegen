using System;                               // Imports the System namespace, which includes basic classes like String and Console.
using System.Collections.Generic;           // Imports the namespace for collections like List.
using System.IO;                            // Imports the namespace for file input/output operations.
using Newtonsoft.Json;                      // Imports the Newtonsoft.Json namespace for working with JSON data.

public class NameGenerator
{
    // Properties for storing parts of the names.
    private List<string> FirstPart { get; set; }  // A list to store the first part of the name.
    private List<string> SecondPart { get; set; } // A list to store the second part of the name.

    // Static Random object for generating random numbers.
    private static readonly Random Random = new Random();

    // Constructor that initializes the name generator by loading names from a JSON file.
    public NameGenerator(string filePath)
    {
        LoadNamesFromJson(filePath);  // Loads the names from the specified JSON file.
    }

    // Method to load the name parts from a JSON file.
    private void LoadNamesFromJson(string filePath)
    {
        // Check if the file exists; if not, throw an exception.
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"JSON file not found at path: {filePath}");
        }

        // Read the JSON content from the file.
        var json = File.ReadAllText(filePath);

        // Deserialize the JSON content into a NameParts object.
        var nameParts = JsonConvert.DeserializeObject<NameParts>(json);

        // Assign the deserialized parts to the respective lists, or initialize empty lists if null.
        FirstPart = nameParts.FirstPart ?? new List<string>();
        SecondPart = nameParts.SecondPart ?? new List<string>();
    }

    // Method to generate a random name by combining a random first and second part.
    public string GenerateName()
    {
        // Select a random element from the FirstPart list.
        var first = FirstPart[Random.Next(FirstPart.Count)];

        // Select a random element from the SecondPart list.
        var second = SecondPart[Random.Next(SecondPart.Count)];

        // Return the combined name.
        return $"{first}{second}";
    }

    // Method to save a generated name to a JSON file.
    public void SaveNameToJson(string name, string filePath)
    {
        // Initialize an empty list to store names.
        var names = new List<string>();

        // If the file exists, read the existing names from it.
        if (File.Exists(filePath))
        {
            var json = File.ReadAllText(filePath);
            names = JsonConvert.DeserializeObject<List<string>>(json) ?? new List<string>();
        }

        // Add the new name to the list.
        names.Add(name);

        // Serialize the list of names back to JSON.
        var serializedNames = JsonConvert.SerializeObject(names, Formatting.Indented);

        // Write the serialized JSON back to the file.
        File.WriteAllText(filePath, serializedNames);
    }

    // Private class to hold the parts of names as lists.
    private class NameParts
    {
        public List<string> FirstPart { get; set; }  // List for the first part of the name.
        public List<string> SecondPart { get; set; } // List for the second part of the name.
    }
}
