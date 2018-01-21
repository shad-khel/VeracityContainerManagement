namespace VeracityContainerManagementAPI.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Containers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Containers()
        {
            ContainerGroups = new HashSet<ContainerGroups>();
        }

        [Key]
        public Guid ContainerId { get; set; }

        [ForeignKey("User")]
        public Guid OwnerId { get; set; }

     
        public virtual Users User { get; set; }
         

        [Required]
        [StringLength(50)]
        public string ContainerName { get; set; }




        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ContainerGroups> ContainerGroups { get; set; }
    }
}
