using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Uprajnenie_2___CsharpDisc
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //typically the current directory is where the code is compiled,
            //so its in bin/debug
            string currentDirectory = Directory.GetCurrentDirectory();
            
            //Folderpaths for starting point
            string filePathInput1 = currentDirectory + @"\task1\input-01.txt";
            string outputFilePath1 = currentDirectory + @"\task1\output.json";           

            string filePathInput2 = currentDirectory + @"\task2\input-02.txt";
            string outputFilePath2 = currentDirectory + @"\task2\contacts.xml";

            string filePathInput3 = currentDirectory + @"\task3\input-03.dae";
            string outputFilePath3 = currentDirectory + @"\task3\output.json";



            //Executing the code for each task
            //--------------------- Task 1 ---------------------
            List<Coordinate> coordinates = ParseCoordinatesFromFile(filePathInput1);
            string jsonOutput = System.Text.Json.JsonSerializer.Serialize(coordinates, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(outputFilePath1, jsonOutput);
            Console.WriteLine($"Coordinates have been written to {outputFilePath1}");
            //--------------------- Task 2 ---------------------
            var contacts = ParseContactsFromFile(@filePathInput2);

            WriteContactsToXml(contacts, outputFilePath2);

            Console.WriteLine($"Contacts have been written to {outputFilePath2}");

            //--------------------- Task 3 ----------------------


            var tags = ParseTagsWithConnections(filePathInput3);

            var jsonOutput2 = System.Text.Json.JsonSerializer.Serialize(tags, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(outputFilePath3, jsonOutput2);

            Console.WriteLine($"Tag structure with connections has been written to {outputFilePath3}");  
    }


        //TASK 1 METHOD
        static List<Coordinate> ParseCoordinatesFromFile(string filePath)
        {
            var coordinates = new List<Coordinate>();
            try
            {
                string[] pairs = File.ReadAllText(filePath).Split(';');

                foreach (string pair in pairs)
                {
                    if (string.IsNullOrWhiteSpace(pair)) continue;

                    string[] parts = pair.Split(',');

                    if (parts.Length == 2 &&
                        float.TryParse(parts[0], NumberStyles.Float, CultureInfo.InvariantCulture, out float latitude) &&
                        float.TryParse(parts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out float longitude))
                    {
                        coordinates.Add(new Coordinate(latitude, longitude));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading or parsing the file: {ex.Message}");
            }

            return coordinates;
        }

        //TASK 2 METHOD
        static List<Contact> ParseContactsFromFile(string filePath)
        {
            var contacts = new List<Contact>();
            try
            {
                // Regular expressions to identify the patterns in the file
                var namePattern = new Regex(@"\b[А-Яа-я]+\b", RegexOptions.Compiled);
                var idPattern = new Regex(@"\b\d{6}\b", RegexOptions.Compiled);
                var phonePattern = new Regex(@"\+395\s?\d{3}\s?\d{2}\s?\d{2}", RegexOptions.Compiled);

                var lines = File.ReadAllLines(filePath);
                Contact currentContact = new Contact();

                foreach (var line in lines)
                {
                    // Check for name
                    var nameMatch = namePattern.Match(line);
                    if (nameMatch.Success && string.IsNullOrEmpty(currentContact.Name))
                    {
                        currentContact.Name = nameMatch.Value;
                    }

                    // Check for ID
                    var idMatch = idPattern.Match(line);
                    if (idMatch.Success && string.IsNullOrEmpty(currentContact.ID))
                    {
                        currentContact.ID = idMatch.Value;
                    }

                    // Check for phone number
                    var phoneMatch = phonePattern.Match(line);
                    if (phoneMatch.Success && string.IsNullOrEmpty(currentContact.PhoneNumber))
                    {
                        currentContact.PhoneNumber = phoneMatch.Value.Replace(" ", "");
                    }

                    // If a contact is complete, add it to the list
                    if (!string.IsNullOrEmpty(currentContact.Name) &&
                        !string.IsNullOrEmpty(currentContact.ID) &&
                        !string.IsNullOrEmpty(currentContact.PhoneNumber))
                    {
                        contacts.Add(currentContact);
                        currentContact = new Contact(); // Reset for next contact
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading or parsing the file: {ex.Message}");
            }

            return contacts;
        }
        static void WriteContactsToXml(List<Contact> contacts, string filePath)
        {
            var xmlContacts = new XElement("Contacts",
                contacts.Select(contact => new XElement("Contact",
                    new XElement("Name", contact.Name),
                    new XElement("ID", contact.ID),
                    new XElement("PhoneNumber", contact.PhoneNumber)
                ))
            );

            xmlContacts.Save(filePath);
        }


        //Task 3 METHOD
        static List<Tag> ParseTagsWithConnections(string filePath)
        {
            var tags = new List<Tag>();
            var tagDictionary = new Dictionary<string, Tag>();

            XDocument doc = XDocument.Load(filePath);

            foreach (var element in doc.Descendants())
            {
                var tag = new Tag { Id = element.Attribute("id")?.Value };

                foreach (var attr in element.Attributes())
                {
                    tag.Attributes[attr.Name.LocalName] = attr.Value;
                }

                if (!string.IsNullOrEmpty(tag.Id))
                {
                    tagDictionary[tag.Id] = tag;
                }

                tags.Add(tag);
            }

            foreach (var tag in tags)
            {
                foreach (var attr in tag.Attributes)
                {
                    if (attr.Value.StartsWith("#"))
                    {
                        string refId = attr.Value.Substring(1);

                        if (tagDictionary.TryGetValue(refId, out Tag referencedTag))
                        {
                            tag.Connection = referencedTag;
                        }
                    }
                }
            }

            return tags;
        }
    }



}
