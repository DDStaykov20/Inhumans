using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace CustomIdentity.Models;

public class AssignmentModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    [MaxLength(100)]
    public string? Class { get; set; }
        
    [StringLength(450)]
    [MaxLength(450)]
    [DataType(DataType.MultilineText)]
    public string? AssignmentDescription { get; set; }
    
    [NotMapped]
    public IFormFile? File { get; set; }
    
    public string? FilePath { get; set; }
    
    [Range(2, 6)]
    public decimal Grade { get; set; }
}