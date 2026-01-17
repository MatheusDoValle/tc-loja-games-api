using System;
using TcLojaGames.Application.Validation;
using Xunit;

namespace TcLojaGames.Tests.Auth;

public class AuthValidationTests
{
    [Theory]
    [InlineData("abcdefg!")] // sem número
    [InlineData("1234567!")] // sem letra
    [InlineData("Abcdefg1")] // sem especial
    public void ValidateStrongPassword_DeveLancar_QuandoFaltarAlgumRequisito(string password)
    {
        var ex = Assert.Throws<ArgumentException>(() => AuthValidation.ValidateStrongPassword(password));
        Assert.Contains("Senha deve conter letras, números e caractere especial", ex.Message);
    }

    [Fact]
    public void ValidateStrongPassword_NaoDeveLancar_QuandoSenhaForForte()
    {
        AuthValidation.ValidateStrongPassword("Abc@1234"); // letra, número, especial
    }
}
