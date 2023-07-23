using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToCook.DataLayer.Entities.User
{
    public class Role
    {

        public Role()
        {

        }

        [Key]
        public int RoleId { get; set; }

        [Required]
        [MaxLength(200)]
        public string RoleTitle { get; set; }

        #region Navigation props
        public virtual List<UsersRole> UsersRole { get; set; }
        #endregion

    }
}
