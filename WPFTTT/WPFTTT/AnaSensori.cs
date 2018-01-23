namespace WPFTTT
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AnaSensori")]
    public partial class AnaSensori
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AnaSensori()
        {
            StoricoAllarmi = new HashSet<StoricoAllarmi>();
        }

        public int Id { get; set; }

        public int? IdTipo { get; set; }

        public bool? IsAbilitato { get; set; }

        public int? IdLuogo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoricoAllarmi> StoricoAllarmi { get; set; }
    }
}
