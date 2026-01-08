using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Linq1
{
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> people = new List<Person>
            {
                new Person { Name = "Иван", Age = 25, City = "Москва" },
                new Person { Name = "Мария", Age = 30, City = "Москва" },
                new Person { Name = "Алексей", Age = 22, City = "Москва" },
                new Person { Name = "Екатерина", Age = 28, City = "Новосибирск" },
                new Person { Name = "Дмитрий", Age = 35, City = "Екатеринбург" },
                new Person { Name = "Анна", Age = 17, City = "Красноярск" },
                new Person { Name = "Сергей", Age = 40, City = "Владивосток" },
                new Person { Name = "Ольга", Age = 31, City = "Сочи" },
                new Person { Name = "Павел", Age = 27, City = "Калининград" },
                new Person { Name = "Наталья", Age = 70, City = "Уфа" }
            };
            Older30(people);
            IsAnyUnder18(people);
            CityStatistic(people);
            MaxAge(people);
        }
        static void Older30(List<Person> people)
        {
            var peopleOlder30 = people.Where(p => p.Age >= 30).OrderBy(p => p.Age);
            Console.WriteLine("Люди старше 30 лет:");
            foreach (var person in peopleOlder30)
            {
                Console.WriteLine($"{person.Name}, {person.Age} лет, г. {person.City}");
            }
        }
        static void IsAnyUnder18(List<Person> people)
        {
            var under18 = people.Where(p => p.Age < 18);
            if (under18.Count() == 0) { Console.WriteLine("Людей меньше 18 нет"); }
            else { Console.WriteLine("Есть люди младше 18"); }

        }
        static void CityStatistic(List<Person> people)
        {
            var cityStatistic = people.GroupBy(p => p.City).Select(g => new {City = g.Key, Count = g.Count()});
            foreach (var city in cityStatistic)
            {
                Console.WriteLine($"{city.City} - {city.Count}");
            }
        }
        static void MaxAge(List<Person> people)
        {
            var maxAge = people.OrderByDescending(p => p.Age).FirstOrDefault();
            Console.WriteLine($"{maxAge.Name} {maxAge.Age} {maxAge.City}");
        }
    }
}
