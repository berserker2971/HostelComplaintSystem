using System.ComponentModel.DataAnnotations;


namespace HostelComplaintSystem.Models
{
    public class Complaint
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Location is required")]
        [StringLength(100)]
        public string Location { get; set; }


        [Required(ErrorMessage = "Select an Issue Type")]
        [StringLength(100)]
        public string IssueType { get; set; }

        [Required(ErrorMessage = "Please provide a description")]
        [StringLength(100)]
        public string Description { get; set; }

        public DateTime RegisteredTime { get; set; } = DateTime.Now;

        public string? Status { get; set; }

        public string? ImagePath { get; set; }

    }
}