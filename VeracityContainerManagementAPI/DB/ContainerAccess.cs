using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;


namespace VeracityContainerManagementAPI.DB
{
    public partial class ContainerAccess
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ContainerAccess()
        {

        }

        [Key]
        public Guid ContainerAccessId { get; set; }

        [ForeignKey("UserGroup")]
        public Guid UserGroupId { get; set; }

        public UserGroups UserGroup { get; set; }

        [ForeignKey("ContainerGroups")]
        public Guid ContainerGroupId { get; set; }

        public ContainerGroups ContainerGroups { get; set; }

        public Guid AccessKeyId { get; set; }
        public DateTime DateTimeAdded { get; set; }

        [ForeignKey("User")]
        public Guid OwnerId { get; set; }

        public Users User { get; set; }

    }
}