using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomIdentity.Models;

public class GroupModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    [MaxLength(100)]
    public string? ClassName { get; set; }
    
    [StringLength(100)]
    [MaxLength(100)]
    [Required]
    public string? TeacherName { get; set; }
    
    [ForeignKey("TeacherName")]
    public AppUser? Teacher { get; set; }
    
    
    [StringLength(100)]
    [MaxLength(100)]
    [Required]
    public string? StudentName { get; set; }
    
    [ForeignKey("StudentName")]
    public AppUser? Student { get; set; }
}