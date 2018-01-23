namespace WPFTTT
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Luoghi")]
    public partial class Luoghi
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string CoorX { get; set; }

        [StringLength(50)]
        public string CoorY { get; set; }

        [StringLength(50)]
        public string nome { get; set; }
    }
}
