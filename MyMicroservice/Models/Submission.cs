using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMicroservice.Models
{
    public class Submission
    {
        [Key]
        public int SubmissionId { get; set; }

        // Foreign key for Homework
        [ForeignKey("Homework")]
        public int HomeworkId { get; set; }

        // Foreign key for User (Student)
        [ForeignKey("User")]
        public int StudentId { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime SubmittedAt { get; set; }

        [Required]
        public decimal Grade { get; set; }

        public string Feedback { get; set; }

        [Required]
        public int GradedBy { get; set; }

        public DateTime GradedAt { get; set; }

        // Navigation properties
        public Homework Homework { get; set; }
        public User Student { get; set; }
        public User Teacher { get; set; }
    }
}
