using CleanArchitecture.Application.DTOs.Email;

namespace CleanArchitecture.Application.Resources.Emails
{
    public static class EmailsTemplates
    {
        public static CreateEmailDto CreateEmailRegister(string email, string token)
        {
            var to = new List<string>() { email };
            string subject = "🎉 Bem vindo ao ProjetoM 🎉 | Confirmação de e-mail";
            string body = @$"<h2>Confirme o seu e-mail para concluir o cadastro</h2> 
                            <br/>
                            <a href='https://localhost:7108/api/users/confirm-email/{token}'>Clique aqui para confirmar o seu e-mail!</a>";

            return new CreateEmailDto(to, subject, body);
        }
    }
}
