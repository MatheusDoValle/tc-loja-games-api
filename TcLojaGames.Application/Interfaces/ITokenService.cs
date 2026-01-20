using TcLojaGames.Application.DTOs.Auth;
using TcLojaGames.Domain.Entities;

namespace TcLojaGames.Application.Interfaces;

public interface ITokenService
{
    AuthResponse Generate(User user);
}