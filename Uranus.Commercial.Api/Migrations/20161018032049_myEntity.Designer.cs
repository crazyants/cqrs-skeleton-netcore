using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Uranus.Commercial.Infrastructure.Persistance.Context;

namespace Uranus.Commercial.Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20161018032049_myEntity")]
    partial class myEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Uranus.Commercial.CommandStack.Domain.Model.MyEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("PropertyOne");

                    b.Property<string>("PropertyTwo");

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.ToTable("MyEntities");
                });
        }
    }
}
