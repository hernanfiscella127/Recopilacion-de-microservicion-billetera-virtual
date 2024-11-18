namespace Application.Exceptions
{
    public class ObjectNotFoundException : Exception
    {
        public string message;
        public ObjectNotFoundException(string message) : base(message)
        {
            this.message = message;
        }
    }
}
