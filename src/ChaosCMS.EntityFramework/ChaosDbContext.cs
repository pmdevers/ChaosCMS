using ChaosCMS.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChaosCMS.EntityFramework
{
    ///// <summary>
    ///// Base class for the Entity Framework database context used for chaos.
    ///// </summary>
    //public class ChaosDbContext : ChaosDbContext<ChaosPage, ChaosContent, string>
    //{
    //    /// <summary>
    //    /// Initializes a new instance of <see cref="ChaosDbContext"/>
    //    /// </summary>
    //    /// <param name="options">The options to be used by a <see cref="DbContext"/></param>
    //    public ChaosDbContext(DbContextOptions options) : base(options)
    //    { }

    //    /// <summary>
    //    /// Initializes a new instance of the <see cref="ChaosDbContext"/> class.
    //    /// </summary>
    //    protected ChaosDbContext() { }
    //}

    ///// <summary>
    ///// Base class for the Entity Framework database context used for chaos.
    ///// </summary>
    ///// <typeparam name="TPage">The type of the page objects.</typeparam>
    ///// <typeparam name="TContent">The type of the content objects.</typeparam>
    //public class ChaosDbContext<TPage, TContent> : ChaosDbContext<TPage, TContent, string> 
    //    where TPage : ChaosPage
    //    where TContent : ChaosContent
    //{
    //    /// <summary>
    //    /// Initializes a new instance of <see cref="ChaosDbContext"/>
    //    /// </summary>
    //    /// <param name="options">The options to be used by a <see cref="DbContext"/></param>
    //    public ChaosDbContext(DbContextOptions options) : base(options)
    //    { }

    //    /// <summary>
    //    /// Initializes a new instance of the <see cref="ChaosDbContext"/> class.
    //    /// </summary>
    //    protected ChaosDbContext() { }
    //}

    /// <summary>
    /// Base class for the Entity Framework database context used for ChaosCMS.
    /// </summary>
    /// <typeparam name="TPage">The type of the page objects.</typeparam>
    /// <typeparam name="TContent">The type of the content objects.</typeparam>
    /// <typeparam name="TKey">The type of the primary key.</typeparam>
    public abstract class ChaosDbContext<TPage, TContent, TKey> : DbContext
        where TPage : ChaosPage<TKey>
        where TContent : ChaosContent<TContent, TKey>
        where TKey : struct, IEquatable<TKey>
    {
        /// <summary>
        /// Initialies a new instance of <see cref="ChaosDbContext{TPage, TContent, TKey}"/>
        /// </summary>
        /// <param name="options"></param>
        public ChaosDbContext(DbContextOptions options) :base(options)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChaosDbContext{TPage, TContent, TKey}"/> class.
        /// </summary>
        protected ChaosDbContext() { }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> of Pages.
        /// </summary>
        public DbSet<TPage> Pages { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> of Pages.
        /// </summary>
        public DbSet<TContent> Contents { get; set; }

        /// <summary>
        /// Configures the schema needed for the chaos framework.
        /// </summary>
        /// <param name="builder">
        /// The builder being used to construct the model for this context.
        /// </param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TPage>(b =>
            {
                b.HasKey(p => p.Id);

                b.HasIndex(p => p.Url).HasName("PageUrlIndex").IsUnique();

                b.ToTable("ChaosPages");

                b.Property(p => p.Name).HasMaxLength(256).IsRequired();
                b.Property(p => p.Url).HasMaxLength(256).IsRequired();
                b.Property(p => p.Template).HasMaxLength(256).IsRequired();

            });

            builder.Entity<TContent>(c =>
            {
                c.HasKey(r => r.Id);

                c.ToTable("ChaosContent");

                c.Property(r => r.Name).HasMaxLength(256);
                c.Property(r => r.Type).HasMaxLength(256);

                //c.HasOne(x => x.Parent).WithMany(x => x.Children).HasForeignKey(x => x.ParentId);
            });
        }
    }
}
