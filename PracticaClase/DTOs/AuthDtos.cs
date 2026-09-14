namespace PracticaClase.DTOs;

public record RegisterDto(string Email, string Password, string NombreCompleto, string Rol);
public record LoginDto(string Email, string Password);
public record AuthResponseDto(string Token, string Email, string Rol, DateTime Expira);