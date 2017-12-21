using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VeracityContainerManagementAPI.DB
{
    public class ContainerManagementUsers
    {
      

        [Key]
        public Guid CMUserId { get; set; }

    
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual Users User { get; set; }
        
    }
}