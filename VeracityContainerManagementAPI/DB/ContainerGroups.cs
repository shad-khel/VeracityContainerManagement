namespace VeracityContainerManagementAPI.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ContainerGroups
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ContainerGroups()
        {
            Containers = new HashSet<Containers>();
            //UserGroups = new HashSet<UserGroups>();
        }

        [Key]
        public Guid ContainerGroupId { get; set; }

        [ForeignKey("User")]
        public Guid OwnerId { get; set; }
 
        public virtual Users User { get; set; }

        [Required]
        [StringLength(50)]
        public string ContainerGroupName { get; set; }

        [Required]
        public Guid DefaultKeyTemplateId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Containers> Containers { get; set; }

      
    }
}
