using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PracticaClase.Models;
using PracticaClase.DTOs;
using PracticaClase.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PracticaClase.Services.Implements;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
    {
        var existente = await _userManager.FindByEmailAsync(dto.Email);
        if (existente is not null)
            throw new InvalidOperationException("Ya existe un usuario con ese correo.");

        var rolesValidos = new[] { "Admin", "Empleado" };
        if (!rolesValidos.Contains(dto.Rol))
            throw new ArgumentException("El rol debe ser 'Admin' o 'Empleado'.");

        var usuario = new ApplicationUser
        {
            UserName = dto.Email,
            Email = dto.Email,
            NombreCompleto = dto.NombreCompleto
        };

        var resultado = await _userManager.CreateAsync(usuario, dto.Password);
        if (!resultado.Succeeded)
            throw new InvalidOperationException(string.Join("; ", resultado.Errors.Select(e => e.Description)));

        if (!await _roleManager.RoleExistsAsync(dto.Rol))
            await _roleManager.CreateAsync(new IdentityRole(dto.Rol));

        await _userManager.AddToRoleAsync(usuario, dto.Rol);

        return await GenerarTokenAsync(usuario, dto.Rol);
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        var usuario = await _userManager.FindByEmailAsync(dto.Email)
            ?? throw new UnauthorizedAccessException("Correo o contraseña incorrectos.");

        var passwordValido = await _userManager.CheckPasswordAsync(usuario, dto.Password);
        if (!passwordValido)
            throw new UnauthorizedAccessException("Correo o contraseña incorrectos.");

        var roles = await _userManager.GetRolesAsync(usuario);
        var rol = roles.FirstOrDefault() ?? "Empleado";

        return await GenerarTokenAsync(usuario, rol);
    }

    private Task<AuthResponseDto> GenerarTokenAsync(ApplicationUser usuario, string rol)
    {
        var claveTexto = _configuration["Jwt:Key"]!;
        var issuer = _configuration["Jwt:Issuer"]!;
        var audience = _configuration["Jwt:Audience"]!;
        var expira = DateTime.UtcNow.AddHours(8);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, usuario.Id),
            new(ClaimTypes.Email, usuario.Email!),
            new(ClaimTypes.Role, rol),
            new("nombreCompleto", usuario.NombreCompleto)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(claveTexto));
        var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expira,
            signingCredentials: credenciales);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return Task.FromResult(new AuthResponseDto(tokenString, usuario.Email!, rol, expira));
    }
}