using SIS.MvcFramework;
using System.Collections.Generic;

namespace BattleCards.Models
{

    public class User : IdentityUser<string>
    {
        public User()
        {
            this.userCards = new HashSet<UserCard>();
        }

        public ICollection<UserCard> userCards { get; set; }
    }

}
