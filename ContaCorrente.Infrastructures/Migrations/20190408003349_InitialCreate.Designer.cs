﻿// <auto-generated />
using System;
using ContaCorrente.Infrastructures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ContaCorrente.Infrastructures.Migrations
{
    [DbContext(typeof(ContaCorrenteContext))]
    [Migration("20190408003349_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity("ContaCorrente.Domain.Entities.Conta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Numero");

                    b.HasKey("Id");

                    b.ToTable("Contas");
                });

            modelBuilder.Entity("ContaCorrente.Domain.Entities.Lancamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ContaDestinoId");

                    b.Property<int?>("ContaOrigemId");

                    b.Property<decimal>("Valor");

                    b.HasKey("Id");

                    b.HasIndex("ContaDestinoId");

                    b.HasIndex("ContaOrigemId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("ContaCorrente.Domain.Entities.Lancamento", b =>
                {
                    b.HasOne("ContaCorrente.Domain.Entities.Conta", "ContaDestino")
                        .WithMany()
                        .HasForeignKey("ContaDestinoId");

                    b.HasOne("ContaCorrente.Domain.Entities.Conta", "ContaOrigem")
                        .WithMany()
                        .HasForeignKey("ContaOrigemId");
                });
#pragma warning restore 612, 618
        }
    }
}
