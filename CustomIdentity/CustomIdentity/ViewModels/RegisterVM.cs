using System.ComponentModel.DataAnnotations;

namespace CustomIdentity.ViewModels;

public class RegisterVM
{
    [Required]
    public string? Name { get; set; }
    [Required]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
    
    [Compare("Password", ErrorMessage = "Password does not match.")]
    [Display(Name = "Confirm Password")]
    public string? ConfirmPassword { get; set; }
    
    [StringLength(100)]
    [MaxLength(100)]
    [Required]
    public string? RoleName { get; set; }
    
    [DataType(DataType.MultilineText)]
    public string? Address { get; set; }
}