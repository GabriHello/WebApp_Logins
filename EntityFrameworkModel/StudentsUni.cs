namespace Nome.EntityFrameworkModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StudentsUni")]
    public partial class StudentsUni
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StudentsUni()
        {
            Subject = new HashSet<Subject>();
        }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        [StringLength(10)]
        public string IdNumber { get; set; }

        public int Id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Subject> Subject { get; set; }
    }
}
