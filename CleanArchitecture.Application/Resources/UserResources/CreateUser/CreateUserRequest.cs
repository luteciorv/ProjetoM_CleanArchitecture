using CleanArchitecture.Application.Commands;
using Flunt.Validations;

namespace CleanArchitecture.Application.Resources.UserResources.CreateUser
{
    public sealed class CreateUserRequest : BaseRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;

        public override void Validate()
        {
            AddNotifications(new Contract<CreateUserRequest>().Requires()
               .IsLowerOrEqualsThan(Email, 50, nameof(Email), "Este campo deve conter no máximo 50 caracteres.")
               .IsEmail(Email, nameof(Email), "Este campo precisa ser um endereço de e-mail válido.")
               .IsNotNullOrEmpty(Email, nameof(Email), "Este campo não pode ser nulo ou vazio.")

               .IsLowerOrEqualsThan(Username, 30, nameof(Username), "Este campo deve conter ao menos 30 caracteres.")
               .IsNotNullOrEmpty(Username, nameof(Username), "Este campo não pode ser nulo ou vazio.")

               .IsLowerOrEqualsThan(Password, 30, nameof(Password), "Este campo deve conter ao menos 30 caracteres.")
               .IsNotNullOrEmpty(Password, nameof(Password), "Este campo não pode ser nulo ou vazio.")

               .IsLowerOrEqualsThan(ConfirmPassword, 30, nameof(ConfirmPassword), "Este campo deve conter ao menos 30 caracteres.")
               .IsNotNullOrEmpty(ConfirmPassword, nameof(ConfirmPassword), "Este campo não pode ser nulo ou vazio.")
               .AreEquals(ConfirmPassword, Password, nameof(ConfirmPassword), "As senhas não coincidem.")
           );
        }
    }
}
