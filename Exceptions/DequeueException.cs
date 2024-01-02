namespace DataStructuresDemo.Exceptions;

public class DequeueException:Exception
{
    public DequeueException()
    {
        
    }

    public DequeueException(string message):base(message)
    {
        
    }

    public DequeueException(string message,Exception innerException):base(message,innerException.InnerException)
    {
        
    }
}