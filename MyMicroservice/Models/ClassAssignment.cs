using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMicroservice.Models
{
    public enum UserRole
    {
        Student,
        Teacher
    }

    public class ClassAssignment
    {
        [Key]
        public int ClassAssignmentId { get; set; }

        // Foreign key for SchoolClass
        [ForeignKey("SchoolClass")]
        public int ClassId { get; set; }

        // Foreign key for User
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        public UserRole Role { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public SchoolClass SchoolClass { get; set; }
        public User User { get; set; }
    }
}