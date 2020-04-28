namespace BattleCards.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Card
    {
        public Card()
        {
            this.userCards = new HashSet<UserCard>();
        }

        [Key]
        public int Id { get; set; }

        [MinLength(5)]
        [MaxLength(15)]
        [Required]
        public string Name { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string Keyword { get; set; }

        [Required]
        public int Attack { get; set; }

        [Required]
        public int Health { get; set; }

        [MaxLength(200)]
        [Required]
        public string Description { get; set; }

        public ICollection<UserCard> userCards { get; set; }
    }
}
