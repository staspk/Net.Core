namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string path = Kozubenko.IO.File.GenerateConfigFile("TestApplication", "data.json");

            Console.WriteLine(path);
        }
    }
}
