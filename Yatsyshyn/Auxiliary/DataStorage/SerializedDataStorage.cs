using System;
using System.Collections.Generic;
using System.IO;
using Yatsyshyn.Auxiliary.Managers;
using Yatsyshyn.Models;

namespace Yatsyshyn.Auxiliary.DataStorage
{
    internal class SerializedDataStorage : IDataStorage
    {
        private readonly List<Person> _list;

        internal SerializedDataStorage()
        {
            try
            {
                _list = SerializationManager.Deserialize<List<Person>>(FileManager.StorageFilePath);
            }
            catch (FileNotFoundException)
            {
                _list = new List<Person>();
                string[] firstNames =
                    {
                        "Esmeralda",
                        "Mcclain",
                        "Laverne",
                        "Nettie",
                        "Stokes",
                        "Joy",
                        "Stanton",
                        "Mercado",
                        "Townsend",
                        "Gretchen",
                        "Garrett",
                        "Leon",
                        "Trujillo",
                        "Augusta",
                        "Ford",
                        "Lana",
                        "Inez",
                        "Pickett",
                        "Dickerson",
                        "Effie",
                        "Hodge",
                        "Shelly",
                        "Bonnie",
                        "Wiley",
                        "Butler",
                        "Allyson",
                        "Antoinette",
                        "Sophie",
                        "Angelina",
                        "Lawson",
                        "Fran",
                        "Cora",
                        "Gill",
                        "Rosa",
                        "Ware",
                        "Gallegos",
                        "Vaughan",
                        "Washington",
                        "Annie",
                        "Esperanza",
                        "Nunez",
                        "Joyce",
                        "Elma",
                        "Crystal",
                        "Milagros",
                        "Deleon",
                        "Maxine",
                        "Oneal",
                        "Christian",
                        "Virgie"
                    },
                    lastNames =
                    {
                        "Lamb",
                        "Evans",
                        "Jackson",
                        "Mosley",
                        "Maxwell",
                        "Whitehead",
                        "Kent",
                        "Swanson",
                        "Torres",
                        "Trujillo",
                        "Crosby",
                        "Salas",
                        "Mcknight",
                        "Sosa",
                        "Kinney",
                        "Little",
                        "Bond",
                        "Cochran",
                        "Carver",
                        "Vega",
                        "Blackwell",
                        "Spence",
                        "Flowers",
                        "Landry",
                        "Armstrong",
                        "Rocha",
                        "Herman",
                        "Bartlett",
                        "Rios",
                        "Mathis",
                        "Wright",
                        "Slater",
                        "Berg",
                        "Perez",
                        "Petty",
                        "Oneal",
                        "Maddox",
                        "Ramos",
                        "Pacheco",
                        "Donaldson",
                        "Maynard",
                        "Vincent",
                        "Mccarthy",
                        "Goodman",
                        "Wynn",
                        "Lyons",
                        "Rodriquez",
                        "Curtis",
                        "Cooper",
                        "Parsons"
                    },
                    domainZones =
                    {
                        ".ca",
                        ".us",
                        ".tv",
                        ".me",
                        ".name",
                        ".net",
                        ".com",
                        ".org",
                        ".io",
                        ".biz",
                        ".info",
                        ".co.uk",
                        ".ca",
                        ".us",
                        ".tv",
                        ".me",
                        ".name",
                        ".net",
                        ".com",
                        ".org",
                        ".io",
                        ".biz",
                        ".info",
                        ".co.uk",
                        ".ca",
                        ".us",
                        ".tv",
                        ".me",
                        ".name",
                        ".net",
                        ".com",
                        ".org",
                        ".io",
                        ".biz",
                        ".info",
                        ".co.uk",
                        ".ca",
                        ".us",
                        ".tv",
                        ".me",
                        ".name",
                        ".net",
                        ".com",
                        ".org",
                        ".io",
                        ".biz",
                        ".info",
                        ".co.uk",
                        ".ca",
                        ".us"
                    };

                var random = new Random();
                for (var i = 0; i < 50; ++i)
                {
                    var birthday = new DateTime(random.Next(1970, 2019), random.Next(1, 12), random.Next(1, 28));
                    var email = firstNames[i].ToLower() + "." + lastNames[i].ToLower() + "@mail" + domainZones[i];
                    var tmp = new Person(firstNames[i], lastNames[i], email, birthday);
                    _list.Add(tmp);
                }

                SaveChanges();
            }
        }

        public bool PersonExists(Person person)
        {
            return _list.Contains(person);
        }

        public void AddPerson(Person person)
        {
            _list.Add(person);
            SaveChanges();
        }

        public void RemovePerson(Person person)
        {
            _list.Remove(person);
            SaveChanges();
        }

        public void ApplyChanges()
        {
            SaveChanges();
        }

        public List<Person> PersonsList => _list;

        private void SaveChanges()
        {
            SerializationManager.Serialize(_list, FileManager.StorageFilePath);
        }
    }
}