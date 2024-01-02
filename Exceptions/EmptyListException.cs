namespace DataStructuresDemo.Exceptions;

public class EmptyListException:Exception
{
    public EmptyListException()
    {
        
    }

    public EmptyListException(string message):base(message)
    {
        
    }
}