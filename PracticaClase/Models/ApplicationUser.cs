using Microsoft.AspNetCore.Identity;

namespace PracticaClase.Models;

public class ApplicationUser : IdentityUser
{
    public string NombreCompleto { get; set; } = string.Empty;
}