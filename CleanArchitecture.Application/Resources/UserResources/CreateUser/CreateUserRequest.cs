using CleanArchitecture.Application.Commands;
using Flunt.Validations;

namespace CleanArchitecture.Application.Resources.UserResources.CreateUser
{
    public sealed class CreateUserRequest : BaseRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public override void Validate()
        {
            AddNotifications(new Contract<CreateUserRequest>().Requires()
               .IsLowerOrEqualsThan(Email, 50, nameof(Email), "Este campo deve conter no máximo 50 caracteres")
               .IsEmail(Email, nameof(Email), "Este campo precisa ser um endereço de e-mail válido")
               .IsNotNullOrEmpty(Email, nameof(Email), "Este campo não pode ser nulo ou vazio.")

               .IsLowerOrEqualsThan(Name, 30, nameof(Name), "Este campo deve conter ao menos 30 caracteres")
               .IsNotNullOrEmpty(Name, nameof(Name), "Este campo não pode ser nulo ou vazio.")
           );
        }
    }
}
