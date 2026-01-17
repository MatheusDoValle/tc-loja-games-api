using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using TcLojaGames.Application.DTOs.Auth;
using TcLojaGames.Application.Interfaces;
using TcLojaGames.Application.Services;
using TcLojaGames.Domain.Entities;
using TcLojaGames.Domain.Enums;
using Xunit;

namespace TcLojaGames.Tests.Auth;

public class AuthServiceTests
{
    private static AuthService CreateSut(
        Mock<IUserRepository> users,
        Mock<ITokenService> tokens,
        Mock<ILogger<AuthService>> logger)
        => new AuthService(users.Object, tokens.Object, logger.Object);

    [Fact]
    public async Task RegisterAsync_DeveLancar_QuandoEmailJaCadastrado()
    {
        var users = new Mock<IUserRepository>();
        var tokens = new Mock<ITokenService>();
        var logger = new Mock<ILogger<AuthService>>();

        users.Setup(r => r.GetByEmailAsync("teste@email.com", It.IsAny<CancellationToken>()))
             .ReturnsAsync(new User("X", "teste@email.com", "hash", UserRole.User));

        var sut = CreateSut(users, tokens, logger);

        var req = new RegisterRequest
        {
            Name = " Pedro ",
            Email = "TESTE@EMAIL.COM",
            Password = "Abc@1234"
        };

        var ex = await Assert.ThrowsAsync<ArgumentException>(() => sut.RegisterAsync(req, CancellationToken.None));
        Assert.Equal("E-mail já cadastrado.", ex.Message);

        users.Verify(r => r.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task RegisterAsync_DeveCadastrarUsuario_ComEmailNormalizado_EHashDeSenha()
    {
        var users = new Mock<IUserRepository>();
        var tokens = new Mock<ITokenService>();
        var logger = new Mock<ILogger<AuthService>>();

        users.Setup(r => r.GetByEmailAsync("user@email.com", It.IsAny<CancellationToken>()))
             .ReturnsAsync((User?)null);

        User? savedUser = null;
        users.Setup(r => r.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
             .Callback<User, CancellationToken>((u, _) => savedUser = u)
             .Returns(Task.CompletedTask);

        var sut = CreateSut(users, tokens, logger);

        var req = new RegisterRequest
        {
            Name = "  Pedro  ",
            Email = "USER@EMAIL.COM ",
            Password = "Abc@1234"
        };

        await sut.RegisterAsync(req, CancellationToken.None);

        Assert.NotNull(savedUser);
        Assert.Equal("Pedro", savedUser!.Name);
        Assert.Equal("user@email.com", savedUser.Email);
        Assert.Equal(UserRole.User, savedUser.Role);

        // garante que não salvou senha em texto puro
        Assert.NotEqual("Abc@1234", savedUser.PasswordHash);
        Assert.True(BCrypt.Net.BCrypt.Verify("Abc@1234", savedUser.PasswordHash));

        users.Verify(r => r.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task LoginAsync_DeveLancar_QuandoUsuarioNaoEncontrado()
    {
        var users = new Mock<IUserRepository>();
        var tokens = new Mock<ITokenService>();
        var logger = new Mock<ILogger<AuthService>>();

        users.Setup(r => r.GetByEmailAsync("x@email.com", It.IsAny<CancellationToken>()))
             .ReturnsAsync((User?)null);

        var sut = CreateSut(users, tokens, logger);

        var req = new LoginRequest { Email = " X@EMAIL.COM ", Password = "qualquer" };

        var ex = await Assert.ThrowsAsync<ArgumentException>(() => sut.LoginAsync(req, CancellationToken.None));
        Assert.Equal("Credenciais inválidas.", ex.Message);

        tokens.Verify(t => t.Generate(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public async Task LoginAsync_DeveLancar_QuandoSenhaIncorreta()
    {
        var users = new Mock<IUserRepository>();
        var tokens = new Mock<ITokenService>();
        var logger = new Mock<ILogger<AuthService>>();

        var user = new User("Pedro", "x@email.com", BCrypt.Net.BCrypt.HashPassword("SenhaCerta@123"), UserRole.User);

        users.Setup(r => r.GetByEmailAsync("x@email.com", It.IsAny<CancellationToken>()))
             .ReturnsAsync(user);

        var sut = CreateSut(users, tokens, logger);

        var req = new LoginRequest { Email = "x@email.com", Password = "SenhaErrada@123" };

        var ex = await Assert.ThrowsAsync<ArgumentException>(() => sut.LoginAsync(req, CancellationToken.None));
        Assert.Equal("Credenciais inválidas.", ex.Message);

        tokens.Verify(t => t.Generate(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public async Task LoginAsync_DeveRetornarToken_QuandoCredenciaisValidas()
    {
        var users = new Mock<IUserRepository>();
        var tokens = new Mock<ITokenService>();
        var logger = new Mock<ILogger<AuthService>>();

        var user = new User("Pedro", "x@email.com", BCrypt.Net.BCrypt.HashPassword("SenhaCerta@123"), UserRole.Admin);

        users.Setup(r => r.GetByEmailAsync("x@email.com", It.IsAny<CancellationToken>()))
             .ReturnsAsync(user);

        tokens.Setup(t => t.Generate(user))
              .Returns(new AuthResponse
              {
                  AccessToken = "fake-jwt",
                  ExpiresAtUtc = DateTime.UtcNow.AddHours(1)
              });

        var sut = CreateSut(users, tokens, logger);

        var req = new LoginRequest { Email = " X@EMAIL.COM ", Password = "SenhaCerta@123" };

        var res = await sut.LoginAsync(req, CancellationToken.None);

        Assert.Equal("fake-jwt", res.AccessToken);
        Assert.True(res.ExpiresAtUtc > DateTime.UtcNow);

        tokens.Verify(t => t.Generate(user), Times.Once);
    }
}
