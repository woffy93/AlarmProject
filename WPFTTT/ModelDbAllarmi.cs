namespace WPFTTT
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelDbAllarmi : DbContext
    {
        public ModelDbAllarmi()
            : base("name=ModelDbAllarmi")
        {
        }

        public virtual DbSet<AnaOperatori> AnaOperatori { get; set; }
        public virtual DbSet<AnaSensori> AnaSensori { get; set; }
        public virtual DbSet<Luoghi> Luoghi { get; set; }
        public virtual DbSet<StoricoAllarmi> StoricoAllarmi { get; set; }
        public virtual DbSet<TipiSensori> TipiSensori { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnaOperatori>()
                .Property(e => e.Login)
                .IsUnicode(false);

            modelBuilder.Entity<AnaOperatori>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<AnaOperatori>()
                .HasMany(e => e.StoricoAllarmi)
                .WithOptional(e => e.AnaOperatori)
                .HasForeignKey(e => e.IdOperatore);

            modelBuilder.Entity<AnaSensori>()
                .HasMany(e => e.StoricoAllarmi)
                .WithOptional(e => e.AnaSensori)
                .HasForeignKey(e => e.IdSensore);

            modelBuilder.Entity<Luoghi>()
                .Property(e => e.nome)
                .IsUnicode(false);

            modelBuilder.Entity<TipiSensori>()
                .Property(e => e.TipoSensore)
                .IsUnicode(false);
        }
    }
}
