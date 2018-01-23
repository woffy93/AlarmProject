namespace WPFTTT
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StoricoAllarmi")]
    public partial class StoricoAllarmi
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int? IdSensore { get; set; }

        public DateTime? OraScatto { get; set; }

        public DateTime? OraDisatt { get; set; }

        public int? IdOperatore { get; set; }

        public virtual AnaOperatori AnaOperatori { get; set; }

        public virtual AnaSensori AnaSensori { get; set; }
    }
}
