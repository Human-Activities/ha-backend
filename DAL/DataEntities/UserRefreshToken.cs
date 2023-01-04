#nullable disable

using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.DataEntities
{
    public class UserRefreshToken
    {
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public string Token { get; set; }

        public virtual User User { get; set; }
    }
}
