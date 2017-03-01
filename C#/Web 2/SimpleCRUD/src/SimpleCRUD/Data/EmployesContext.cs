using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleCRUD.Models;

namespace SimpleCRUD.Data
{
    public class EmployesContext : DbContext
    {
        public EmployesContext(DbContextOptions<EmployesContext> options) : base(options)
        { 
        }

        public DbSet<Employe> Employes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employe>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("no_emp");
                entity.Property(e => e.Nom)
                    .HasColumnName("nom_emp")
                    .IsRequired()
                    .HasMaxLength(20);
                entity.Property(e => e.Prenom)
                    .HasColumnName("prenom_emp")
                    .IsRequired()
                    .HasMaxLength(20);
                entity.Property(e => e.Fonction).HasMaxLength(20);
                entity.Property(e => e.Pays).HasMaxLength(20);
                entity.Property(e => e.DateEmbauche).HasColumnName("date_embauche");
                entity.Property(e => e.DateNaissance).HasColumnName("date_naissance");

                entity.ToTable("Employes");
            });
        }
    }
}
