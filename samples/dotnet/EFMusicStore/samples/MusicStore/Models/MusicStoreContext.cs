using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Storage.Internal;

namespace MusicStore.Models
{
    public class ApplicationUser : IdentityUser { }

    public class ApplicationRole : IdentityRole<string>
    {
        public string Email { get; internal set; }
        [Key]
        public override string Id { get; set; }
        public string PasswordHash { get; internal set; }
        public string UserName { get; internal set; }
    }

    public class IdentityRole
    {
        [Key]
        public string Id { get; internal set; }
    }

    //DbContext
    public class MusicStoreContext : IdentityDbContext<ApplicationUser, IdentityRole<String>, string>
    {
        public MusicStoreContext(DbContextOptions<MusicStoreContext> options)
            : base(options)
        {
            // TODO: #639
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        //public new DbSet<IdentityRole> Roles { get; set; }
        //public DbSet<ApplicationRole> Aspnetroles { get; set; }
        //public new DbSet<ApplicationUser> Users { get; set; }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}