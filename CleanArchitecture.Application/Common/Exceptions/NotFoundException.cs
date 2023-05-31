namespace CleanArchitecture.Application.Common.Exceptions
{
    public sealed class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) =>
            Errors = Array.Empty<string>();

        public NotFoundException(string[] errors) : base("Inúmeros erros foram encontrados. Verifique os erros para mais detalhes.") =>
            Errors = errors;


        public string[] Errors { get; private set; }
    }
}
