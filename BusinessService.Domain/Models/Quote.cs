using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessService.Domain.Models
{
    public class Quote
    {
        [Key]
        public int QuoteID { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ContributionAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal MaturityAmount { get; set; }

        [Required]
        public int CustomerID { get; set; }
        public Boolean IsDeleted { get; set; }

    }
}
