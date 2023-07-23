using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToCook.DataLayer.Entities.User
{
    public class UsersRole
    {

        public UsersRole()
        {

        }

        [Key]
        public int UR_Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        #region Navigation props
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
        #endregion

    }
}
