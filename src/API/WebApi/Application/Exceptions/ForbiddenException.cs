namespace WebApi.Application.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException() : base("Acceso prohibido.") { }
        public ForbiddenException(string message) : base(message) { }
        public ForbiddenException(string message, Exception innerException) : base(message, innerException) { }
    }
}