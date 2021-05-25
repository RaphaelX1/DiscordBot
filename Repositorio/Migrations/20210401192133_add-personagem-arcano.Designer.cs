﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repositorio;

namespace Repositorio.Migrations
{
    [DbContext(typeof(AplicacaoContexto))]
    [Migration("20210401192133_add-personagem-arcano")]
    partial class addpersonagemarcano
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Modelos.Arcano", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipoArcano")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Arcanos");
                });

            modelBuilder.Entity("Modelos.Atributo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Atributos");
                });

            modelBuilder.Entity("Modelos.Efeito", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipoEfeito")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Efeitos");
                });

            modelBuilder.Entity("Modelos.EfeitoCaracteristica", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EfeitoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TipoCaracteristica")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EfeitoId");

                    b.ToTable("EfeitoCaracteristicas");
                });

            modelBuilder.Entity("Modelos.Formacao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Formacoes");
                });

            modelBuilder.Entity("Modelos.Nacao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Nacoes");
                });

            modelBuilder.Entity("Modelos.Pericia", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Pericias");
                });

            modelBuilder.Entity("Modelos.Personagem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Ferimentos")
                        .HasColumnType("int");

                    b.Property<int>("FerimentosDramaticos")
                        .HasColumnType("int");

                    b.Property<int>("Fortuna")
                        .HasColumnType("int");

                    b.Property<Guid>("NacaoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ReligiaoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("NacaoId");

                    b.HasIndex("ReligiaoId");

                    b.ToTable("Personagens");
                });

            modelBuilder.Entity("Modelos.PersonagemArcano", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ArcanoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PersonagemId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ArcanoId");

                    b.HasIndex("PersonagemId");

                    b.ToTable("PersonagemArcanos");
                });

            modelBuilder.Entity("Modelos.PersonagemAtributo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AtributoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PersonagemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Valor")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AtributoId");

                    b.HasIndex("PersonagemId");

                    b.ToTable("PersonagemAtributos");
                });

            modelBuilder.Entity("Modelos.PersonagemEfeito", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("EfeitoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PersonagemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TempoAtividade")
                        .HasColumnType("int");

                    b.Property<int>("Valor")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EfeitoId");

                    b.HasIndex("PersonagemId");

                    b.ToTable("PersonagemEfeitos");
                });

            modelBuilder.Entity("Modelos.PersonagemFormacao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FormacaoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PersonagemId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("FormacaoId");

                    b.HasIndex("PersonagemId");

                    b.ToTable("PersonagemFormacoes");
                });

            modelBuilder.Entity("Modelos.PersonagemPericia", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PericiaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PersonagemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Valor")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PericiaId");

                    b.HasIndex("PersonagemId");

                    b.ToTable("PersonagemPericias");
                });

            modelBuilder.Entity("Modelos.PersonagemVantagem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PersonagemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Valor")
                        .HasColumnType("int");

                    b.Property<Guid>("VantagemId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PersonagemId");

                    b.HasIndex("VantagemId");

                    b.ToTable("PersonagemVantagens");
                });

            modelBuilder.Entity("Modelos.Religiao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Religioes");
                });

            modelBuilder.Entity("Modelos.Vantagem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Vantagens");
                });

            modelBuilder.Entity("Modelos.EfeitoCaracteristica", b =>
                {
                    b.HasOne("Modelos.Efeito", "Efeito")
                        .WithMany("CaracteristicasAfetadas")
                        .HasForeignKey("EfeitoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Modelos.Personagem", b =>
                {
                    b.HasOne("Modelos.Nacao", "Nacao")
                        .WithMany("Habitantes")
                        .HasForeignKey("NacaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Modelos.Religiao", "Religiao")
                        .WithMany("Fieis")
                        .HasForeignKey("ReligiaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Modelos.PersonagemArcano", b =>
                {
                    b.HasOne("Modelos.Arcano", "Arcano")
                        .WithMany()
                        .HasForeignKey("ArcanoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Modelos.Personagem", "Personagem")
                        .WithMany("Arcanos")
                        .HasForeignKey("PersonagemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Modelos.PersonagemAtributo", b =>
                {
                    b.HasOne("Modelos.Atributo", "Atributo")
                        .WithMany()
                        .HasForeignKey("AtributoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Modelos.Personagem", "Personagem")
                        .WithMany("Atributos")
                        .HasForeignKey("PersonagemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Modelos.PersonagemEfeito", b =>
                {
                    b.HasOne("Modelos.Efeito", "Efeito")
                        .WithMany()
                        .HasForeignKey("EfeitoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Modelos.Personagem", "Personagem")
                        .WithMany("Efeitos")
                        .HasForeignKey("PersonagemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Modelos.PersonagemFormacao", b =>
                {
                    b.HasOne("Modelos.Formacao", "Formacao")
                        .WithMany()
                        .HasForeignKey("FormacaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Modelos.Personagem", "Personagem")
                        .WithMany("Formacoes")
                        .HasForeignKey("PersonagemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Modelos.PersonagemPericia", b =>
                {
                    b.HasOne("Modelos.Pericia", "Pericia")
                        .WithMany()
                        .HasForeignKey("PericiaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Modelos.Personagem", "Personagem")
                        .WithMany("Periciais")
                        .HasForeignKey("PersonagemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Modelos.PersonagemVantagem", b =>
                {
                    b.HasOne("Modelos.Personagem", "Personagem")
                        .WithMany("Vantagens")
                        .HasForeignKey("PersonagemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Modelos.Vantagem", "vantagem")
                        .WithMany()
                        .HasForeignKey("VantagemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}