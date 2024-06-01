using System.ComponentModel.DataAnnotations;

namespace ExerciseWebApplication1.Models
{
    public class Expense
    {
        public int ID { get; set; }
        public decimal Value { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
