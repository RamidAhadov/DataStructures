namespace DataStructuresDemo.Exceptions;

public class NegativeIndexException:Exception
{
    public NegativeIndexException()
    {
        
    }

    public NegativeIndexException(string message):base(message)
    {
        
    }

    public NegativeIndexException(string message, Exception innerException):base(message,innerException)
    {
        
    }
}