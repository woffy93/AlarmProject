namespace WPFTTT
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TipiSensori")]
    public partial class TipiSensori
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string TipoSensore { get; set; }
    }
}
