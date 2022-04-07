namespace dotmockator.core;

public static class Test
{
    public static void Main(string[] args)
    {
        var fullName = Faker.Name.FullName();
        Console.WriteLine(fullName);
    }
}