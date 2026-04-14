using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace xmlInsert
{
    [XmlRoot("People")]
    public class People
    {
        [XmlElement("Person")]
        public List<Person> Persons { get; set; }
    }

    public class Person
    {
        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("Name")]
        public Name Name { get; set; }

        [XmlElement("Location")]
        public Location Location { get; set; }

        [XmlElement("Stats")]
        public Stats Stats { get; set; }
    }

    public class Name
    {
        [XmlElement("first_name")]
        public string FirstName { get; set; }

        [XmlElement("middle_name")]
        public string MiddleName { get; set; }

        [XmlElement("last_name")]
        public string LastName { get; set; }
    }

    public class Location
    {
        [XmlElement("city")]
        public string City { get; set; }

        [XmlElement("country")]
        public string Country { get; set; }
    }

    public class Stats
    {
        [XmlElement("age")]
        public int Age { get; set; }

        [XmlElement("height_cm")]
        public double HeightCm { get; set; }

        [XmlElement("weight_kg")]
        public double WeightKg { get; set; }

        [XmlElement("favorite_number")]
        public int FavoriteNumber { get; set; }
    }

}
