namespace VeracityContainerManagementAPI.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserGroups
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserGroups()
        {
            //ContainerGroups = new HashSet<ContainerGroups>();
            Users = new HashSet<Users>();
        }

        [Key]
        public Guid UserGroupId { get; set; }

        [ForeignKey("User")]
        public Guid OwnerId { get; set; }

       
        public virtual Users User { get; set; }

        [Required]
        [StringLength(50)]
        public string UserGroupName { get; set; }
 

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Users> Users { get; set; }
    }
}
