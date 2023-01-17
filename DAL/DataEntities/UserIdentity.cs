using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.DataEntities
{
    public class UserIdentity
    {
        [ForeignKey("User")]
        public int UserId { get; set; }

        public Guid UserGuid { get; set; }

        public string Salt { get; set; }


        public virtual User User { get; set; }
    }
}
