namespace TcLojaGames.Application.Validation;

public static class AuthValidation
{
    public static void ValidateStrongPassword(string password)
    {
        var hasLetter = password.Any(char.IsLetter);
        var hasDigit = password.Any(char.IsDigit);
        var hasSpecial = password.Any(ch => !char.IsLetterOrDigit(ch));

        if (!hasLetter || !hasDigit || !hasSpecial)
            throw new ArgumentException("Senha deve conter letras, números e caractere especial.");
    }
}