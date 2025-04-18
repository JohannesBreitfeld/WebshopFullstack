using System.ComponentModel.DataAnnotations;

namespace Webshop.Presentation.Models;

public class RegisterModel
{
    [Required(ErrorMessage = "Förnamn är obligatoriskt")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Efternamn är obligatoriskt")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "E-post är obligatoriskt")]
    [EmailAddress(ErrorMessage = "Ogiltig e-postadress")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Gatuadress är obligatoriskt")]
    public string StreetAdress { get; set; } = string.Empty;

    [Required(ErrorMessage = "Postnummer är obligatoriskt")]
    [Range(10000, 99999, ErrorMessage = "Postnummer måste vara 5 siffror")]
    public int PostalCode { get; set; }

    [Required(ErrorMessage = "Stad är obligatoriskt")]
    public string City { get; set; } = string.Empty;

    [Required(ErrorMessage = "Lösenord är obligatoriskt")]
    [MinLength(6, ErrorMessage = "Lösenordet måste vara minst 6 tecken")]
    public string Password { get; set; } = string.Empty;

    [Compare(nameof(Password), ErrorMessage = "Lösenorden matchar inte")]
    public string ConfirmPassword { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;
}

