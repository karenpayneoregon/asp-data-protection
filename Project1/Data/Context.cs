﻿#nullable disable
using Microsoft.EntityFrameworkCore;
using Project1.Classes;
using Project1.Models;
using static Project1.Classes.Utilities;


namespace Project1.Data;

public partial class Context : DbContext
{
    public Context() { }

    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    public virtual DbSet<UserLogin> UserLogin { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserLogin>(entity =>
        {
            entity.Property(e => e.EmailAddress).IsRequired();
            entity.Property(e => e.Pin)
                .IsRequired()
                .HasMaxLength(5)
                .IsFixedLength();
        });

        if (CurrentEnvironment() == AspNetCoreEnvironment.Development)
        {
            /*
             * Ensure there are five records available
             */
            modelBuilder.Entity<UserLogin>().HasData(
                new UserLogin() { Id = 1, EmailAddress = "payne@comcast.net", Pin = "12345" },
                new UserLogin() { Id = 2, EmailAddress = "billybob@gmail.com", Pin = "55555" },
                new UserLogin() { Id = 3, EmailAddress = "mary@yahoo.com", Pin = "97865" },
                new UserLogin() { Id = 4, EmailAddress = "jimj@gmail.com", Pin = "37179" },
                new UserLogin() { Id = 5, EmailAddress = "adam@comcast.net", Pin = "66666" }
            );
        }


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}