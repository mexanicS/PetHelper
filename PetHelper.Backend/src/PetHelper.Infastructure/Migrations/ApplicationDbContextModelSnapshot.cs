﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PetHelper.Infastructure;

#nullable disable

namespace PetHelper.Infastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PetHelper.Domain.Models.Breed.Breed", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid?>("species_id")
                        .HasColumnType("uuid")
                        .HasColumnName("species_id");

                    b.ComplexProperty<Dictionary<string, object>>("Name", "PetHelper.Domain.Models.Breed.Breed.Name#Name", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("name");
                        });

                    b.HasKey("Id")
                        .HasName("pk_breed");

                    b.HasIndex("species_id")
                        .HasDatabaseName("ix_breed_species_id");

                    b.ToTable("breed", (string)null);
                });

            modelBuilder.Entity("PetHelper.Domain.Models.Pet", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

                    b.Property<DateOnly?>("DateOfBirth")
                        .IsRequired()
                        .HasColumnType("date")
                        .HasColumnName("date_of_birth");

                    b.Property<bool>("IsNeutered")
                        .HasColumnType("boolean")
                        .HasColumnName("is_neutered");

                    b.Property<bool>("IsVaccinated")
                        .HasColumnType("boolean")
                        .HasColumnName("is_vaccinated");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<Guid?>("volunteer_id")
                        .HasColumnType("uuid")
                        .HasColumnName("volunteer_id");

                    b.ComplexProperty<Dictionary<string, object>>("Address", "PetHelper.Domain.Models.Pet.Address#Address", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("city");

                            b1.Property<string>("HouseNumber")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("house_number");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("street");

                            b1.Property<string>("ZipCode")
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("zip_code");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Breed", "PetHelper.Domain.Models.Pet.Breed#Breed", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("breed");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Color", "PetHelper.Domain.Models.Pet.Color#Color", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("color");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Description", "PetHelper.Domain.Models.Pet.Description#Description", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(4000)
                                .HasColumnType("character varying(4000)")
                                .HasColumnName("description");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("HealthInformation", "PetHelper.Domain.Models.Pet.HealthInformation#HealthInformation", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("health_information");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Height", "PetHelper.Domain.Models.Pet.Height#Height", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<double>("Value")
                                .HasMaxLength(1000)
                                .HasColumnType("double precision")
                                .HasColumnName("height");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Name", "PetHelper.Domain.Models.Pet.Name#Name", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("name");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("PhoneNumber", "PetHelper.Domain.Models.Pet.PhoneNumber#PhoneNumber", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(12)
                                .HasColumnType("character varying(12)")
                                .HasColumnName("phone_number");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("SpeciesBreed", "PetHelper.Domain.Models.Pet.SpeciesBreed#SpeciesBreed", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<Guid>("BreedId")
                                .HasColumnType("uuid")
                                .HasColumnName("breed_id");

                            b1.Property<Guid>("SpeciesId")
                                .HasColumnType("uuid")
                                .HasColumnName("species_id");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("TypePet", "PetHelper.Domain.Models.Pet.TypePet#TypePet", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("type_pet");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Weight", "PetHelper.Domain.Models.Pet.Weight#Weight", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<double>("Value")
                                .HasMaxLength(1000)
                                .HasColumnType("double precision")
                                .HasColumnName("weight");
                        });

                    b.HasKey("Id")
                        .HasName("pk_pet");

                    b.HasIndex("volunteer_id")
                        .HasDatabaseName("ix_pet_volunteer_id");

                    b.ToTable("pet", (string)null);
                });

            modelBuilder.Entity("PetHelper.Domain.Models.PetPhoto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("file_path");

                    b.Property<bool>("IsMain")
                        .HasColumnType("boolean")
                        .HasColumnName("is_main");

                    b.Property<Guid>("pet_id")
                        .HasColumnType("uuid")
                        .HasColumnName("pet_id");

                    b.HasKey("Id")
                        .HasName("pk_pet_photo");

                    b.HasIndex("pet_id")
                        .HasDatabaseName("ix_pet_photo_pet_id");

                    b.ToTable("pet_photo", (string)null);
                });

            modelBuilder.Entity("PetHelper.Domain.Models.Species", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.ComplexProperty<Dictionary<string, object>>("Name", "PetHelper.Domain.Models.Species.Name#Name", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("name");
                        });

                    b.HasKey("Id")
                        .HasName("pk_species");

                    b.ToTable("species", (string)null);
                });

            modelBuilder.Entity("PetHelper.Domain.Models.Volunteer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.ComplexProperty<Dictionary<string, object>>("Description", "PetHelper.Domain.Models.Volunteer.Description#Description", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(4000)
                                .HasColumnType("character varying(4000)")
                                .HasColumnName("description");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Email", "PetHelper.Domain.Models.Volunteer.Email#Email", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(4000)
                                .HasColumnType("character varying(4000)")
                                .HasColumnName("email");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("ExperienceInYears", "PetHelper.Domain.Models.Volunteer.ExperienceInYears#ExperienceInYears", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int>("Value")
                                .HasMaxLength(4000)
                                .HasColumnType("integer")
                                .HasColumnName("experience");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Name", "PetHelper.Domain.Models.Volunteer.Name#FullName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("first_name");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("last_name");

                            b1.Property<string>("MiddleName")
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("middle_name");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("PhoneNumber", "PetHelper.Domain.Models.Volunteer.PhoneNumber#PhoneNumber", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(4000)
                                .HasColumnType("character varying(4000)")
                                .HasColumnName("phone_number");
                        });

                    b.HasKey("Id")
                        .HasName("pk_volunteer");

                    b.ToTable("volunteer", (string)null);
                });

            modelBuilder.Entity("PetHelper.Domain.Models.Breed.Breed", b =>
                {
                    b.HasOne("PetHelper.Domain.Models.Species", null)
                        .WithMany("Breeds")
                        .HasForeignKey("species_id")
                        .HasConstraintName("fk_breed_species_species_id");
                });

            modelBuilder.Entity("PetHelper.Domain.Models.Pet", b =>
                {
                    b.HasOne("PetHelper.Domain.Models.Volunteer", null)
                        .WithMany("Pets")
                        .HasForeignKey("volunteer_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_pet_volunteer_volunteer_id");

                    b.OwnsOne("PetHelper.Domain.Models.PetDetails", "PetDetails", b1 =>
                        {
                            b1.Property<Guid>("PetId")
                                .HasColumnType("uuid");

                            b1.HasKey("PetId");

                            b1.ToTable("pet");

                            b1.ToJson("pet_details");

                            b1.WithOwner()
                                .HasForeignKey("PetId")
                                .HasConstraintName("fk_pet_pet_id");

                            b1.OwnsMany("PetHelper.Domain.ValueObjects.DetailsForAssistance", "DetailsForAssistances", b2 =>
                                {
                                    b2.Property<Guid>("PetDetailsPetId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Description")
                                        .IsRequired()
                                        .HasMaxLength(4000)
                                        .HasColumnType("character varying(4000)");

                                    b2.Property<string>("Name")
                                        .IsRequired()
                                        .HasMaxLength(100)
                                        .HasColumnType("character varying(100)");

                                    b2.HasKey("PetDetailsPetId", "Id");

                                    b2.ToTable("pet");

                                    b2.ToJson("pet_details");

                                    b2.WithOwner()
                                        .HasForeignKey("PetDetailsPetId")
                                        .HasConstraintName("fk_pet_pet_pet_details_pet_id");
                                });

                            b1.Navigation("DetailsForAssistances");
                        });

                    b.Navigation("PetDetails")
                        .IsRequired();
                });

            modelBuilder.Entity("PetHelper.Domain.Models.PetPhoto", b =>
                {
                    b.HasOne("PetHelper.Domain.Models.Pet", null)
                        .WithMany("PetPhotos")
                        .HasForeignKey("pet_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_pet_photo_pet_pet_id");
                });

            modelBuilder.Entity("PetHelper.Domain.Models.Volunteer", b =>
                {
                    b.OwnsOne("PetHelper.Domain.ValueObjects.DetailsForAssistanceList", "DetailsForAssistance", b1 =>
                        {
                            b1.Property<Guid>("VolunteerId")
                                .HasColumnType("uuid");

                            b1.HasKey("VolunteerId");

                            b1.ToTable("volunteer");

                            b1.ToJson("details_for_assistance");

                            b1.WithOwner()
                                .HasForeignKey("VolunteerId")
                                .HasConstraintName("fk_volunteer_volunteer_id");

                            b1.OwnsMany("PetHelper.Domain.ValueObjects.DetailsForAssistance", "DetailsForAssistance", b2 =>
                                {
                                    b2.Property<Guid>("DetailsForAssistanceListVolunteerId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Description")
                                        .IsRequired()
                                        .HasMaxLength(1000)
                                        .HasColumnType("character varying(1000)")
                                        .HasColumnName("details_for_assistance_description");

                                    b2.Property<string>("Name")
                                        .IsRequired()
                                        .HasMaxLength(100)
                                        .HasColumnType("character varying(100)")
                                        .HasColumnName("details_for_assistance_name");

                                    b2.HasKey("DetailsForAssistanceListVolunteerId", "Id");

                                    b2.ToTable("volunteer");

                                    b2.ToJson("details_for_assistance");

                                    b2.WithOwner()
                                        .HasForeignKey("DetailsForAssistanceListVolunteerId")
                                        .HasConstraintName("fk_volunteer_volunteer_details_for_assistance_list_volunteer_id");
                                });

                            b1.Navigation("DetailsForAssistance");
                        });

                    b.OwnsOne("PetHelper.Domain.ValueObjects.SocialNetworkList", "SocialNetwork", b1 =>
                        {
                            b1.Property<Guid>("VolunteerId")
                                .HasColumnType("uuid");

                            b1.HasKey("VolunteerId");

                            b1.ToTable("volunteer");

                            b1.ToJson("social_network");

                            b1.WithOwner()
                                .HasForeignKey("VolunteerId")
                                .HasConstraintName("fk_volunteer_volunteer_id");

                            b1.OwnsMany("PetHelper.Domain.ValueObjects.SocialNetwork", "SocialNetwork", b2 =>
                                {
                                    b2.Property<Guid>("SocialNetworkListVolunteerId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Name")
                                        .IsRequired()
                                        .HasMaxLength(100)
                                        .HasColumnType("character varying(100)")
                                        .HasColumnName("social_network_name");

                                    b2.Property<string>("Url")
                                        .IsRequired()
                                        .HasMaxLength(1000)
                                        .HasColumnType("character varying(1000)")
                                        .HasColumnName("social_network_link");

                                    b2.HasKey("SocialNetworkListVolunteerId", "Id");

                                    b2.ToTable("volunteer");

                                    b2.ToJson("social_network");

                                    b2.WithOwner()
                                        .HasForeignKey("SocialNetworkListVolunteerId")
                                        .HasConstraintName("fk_volunteer_volunteer_social_network_list_volunteer_id");
                                });

                            b1.Navigation("SocialNetwork");
                        });

                    b.Navigation("DetailsForAssistance")
                        .IsRequired();

                    b.Navigation("SocialNetwork")
                        .IsRequired();
                });

            modelBuilder.Entity("PetHelper.Domain.Models.Pet", b =>
                {
                    b.Navigation("PetPhotos");
                });

            modelBuilder.Entity("PetHelper.Domain.Models.Species", b =>
                {
                    b.Navigation("Breeds");
                });

            modelBuilder.Entity("PetHelper.Domain.Models.Volunteer", b =>
                {
                    b.Navigation("Pets");
                });
#pragma warning restore 612, 618
        }
    }
}
