using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Domain.ValueObjects
{
    public record Document
    {
        public Document(string number, EDocumentType type)
        {
            Number = number;
            Type = type;
        }

        public string Number { get; private set; }
        public EDocumentType Type { get; private set; }
    }
}
