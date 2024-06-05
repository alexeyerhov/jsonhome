using System;
using System.Text.Json;
using System.Xml;
using System.Xml.Linq;

class Program
{
    static void Main(string[] args)
    {
        // Пример JSON строки
        string jsonString = @"{
            ""name"": ""Alex"",
            ""age"": 30,
            ""isStudent"": false,
            ""subjects"": [""Math"", ""Science""]
        }";

        // Парсим JSON строку в JsonDocument
        using (JsonDocument document = JsonDocument.Parse(jsonString))
        {
            XElement xml = JsonToXml(document.RootElement, "Root");
            Console.WriteLine(xml);
        }
    }

    static XElement JsonToXml(JsonElement jsonElement, string elementName)
    {
        if (jsonElement.ValueKind == JsonValueKind.Array)
        {
            XElement arrayElement = new XElement(elementName);
            foreach (JsonElement item in jsonElement.EnumerateArray())
            {
                arrayElement.Add(JsonToXml(item, "Item"));
            }
            return arrayElement;
        }
        else if (jsonElement.ValueKind == JsonValueKind.Object)
        {
            XElement objectElement = new XElement(elementName);
            foreach (JsonProperty property in jsonElement.EnumerateObject())
            {
                objectElement.Add(JsonToXml(property.Value, property.Name));
            }
            return objectElement;
        }
        else
        {
            return new XElement(elementName, jsonElement.ToString());
        }
    }
}