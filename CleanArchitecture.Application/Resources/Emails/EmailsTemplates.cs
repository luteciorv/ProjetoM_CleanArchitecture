using CleanArchitecture.Application.DTOs.Email;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Resources.Emails
{
    public static class EmailsTemplates
    {
        public static CreateEmailDto CreateEmailRegister(User user)
        {
            var to = new List<string>() { user.Email.Address };
            string subject = "🎉 Bem vindo ao ProjetoM 🎉 | Confirmação de e-mail";
            string body = @$"<h2>Confirme o seu e-mail para concluir o cadastro</h2> 
                            <br/>
                            <a href='https://localhost:7108/api/users/confirm-register/{user.Id}'>Clique aqui para confirmar o seu e-mail!</a>";

            return new CreateEmailDto(to, subject, body);
        }
    }
}
