using System.Collections.Generic;
using Yatsyshyn.Models;

namespace Yatsyshyn.Auxiliary.DataStorage
{
    internal interface IDataStorage
    {
        bool PersonExists(Person person);

        void AddPerson(Person person);

        void RemovePerson(Person person);

        void ApplyChanges();

        List<Person> PersonsList { get; }
    }
}