namespace dotmockator.core;

public class ScopeTest
{
    public string MyStringVariable;

    public void TestMyStringVariable()
    {
        var myIntVariable = 0;
        MyStringVariable = "This is my super dupa string Variable";
    }
    
    public void TestMyIntVariable()
    {
        MyStringVariable = "";
    }
}