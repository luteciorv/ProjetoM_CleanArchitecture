namespace CleanArchitecture.Application.Common.Exceptions
{
    public sealed class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) =>
             Errors = Array.Empty<string>();

        public BadRequestException(string[] errors) : base("Inúmeros erros foram encontrados. Verifique os erros para mais detalhes.") =>
            Errors = errors;


        public string[] Errors { get; private set; } 
    }
}
