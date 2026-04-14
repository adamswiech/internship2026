using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace IndexApi.Models
{


    public class Person
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public int age { get; set; }
        public decimal height_cm { get; set; }
        public decimal weight_kg { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public int favorite_number { get; set; }
    }

}
