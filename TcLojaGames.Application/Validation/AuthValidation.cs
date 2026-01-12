namespace TcLojaGames.Application.Validation;

public static class AuthValidation
{
    public static void ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("E-mail é obrigatório.");

        try
        {
            var addr = new System.Net.Mail.MailAddress(email.Trim());
            if (addr.Address != email.Trim())
                throw new ArgumentException("E-mail inválido.");
        }
        catch
        {
            throw new ArgumentException("E-mail inválido.");
        }
    }

    public static void ValidateStrongPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Senha é obrigatória.");

        if (password.Length < 8)
            throw new ArgumentException("Senha deve ter no mínimo 8 caracteres.");

        var hasLetter = password.Any(char.IsLetter);
        var hasDigit = password.Any(char.IsDigit);
        var hasSpecial = password.Any(ch => !char.IsLetterOrDigit(ch));

        if (!hasLetter || !hasDigit || !hasSpecial)
            throw new ArgumentException("Senha deve conter letras, números e caractere especial.");
    }
}