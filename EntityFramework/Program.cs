using EntityFramework.Data;

namespace EntityFramework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            new LibraryContext();
            Console.ReadLine();
        }
    }
}
