using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChaosCMS.Entityframework
{
    /// <summary>
    /// Base class for the Entity Framework database context used for chaos.
    /// </summary>
    public class ChaosDbContext : ChaosDbContext<ChaosPage, string>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ChaosDbContext"/>
        /// </summary>
        /// <param name="options">The options to be used by a <see cref="DbContext"/></param>
        public ChaosDbContext(DbContextOptions options) : base(options)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChaosDbContext"/> class.
        /// </summary>
        protected ChaosDbContext() { }
    }

    /// <summary>
    /// Base class for the Entity Framework database context used for chaos.
    /// </summary>
    /// <typeparam name="TPage">The type of the page objects.</typeparam>
    public class ChaosDbContext<TPage> : ChaosDbContext<TPage, string> where TPage : ChaosPage
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ChaosDbContext"/>
        /// </summary>
        /// <param name="options">The options to be used by a <see cref="DbContext"/></param>
        public ChaosDbContext(DbContextOptions options) : base(options)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChaosDbContext"/> class.
        /// </summary>
        protected ChaosDbContext() { }
    }

    /// <summary>
    /// Base class for the Entity Framework database context used for ChaosCMS.
    /// </summary>
    /// <typeparam name="TPage">The type of the page objects.</typeparam>
    /// <typeparam name="TKey">The type of the primary key.</typeparam>
    public abstract class ChaosDbContext<TPage, TKey> : DbContext
        where TPage : ChaosPage<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Initialies a new instance of <see cref="ChaosDbContext{TPage, TKey}"/>
        /// </summary>
        /// <param name="options"></param>
        public ChaosDbContext(DbContextOptions options) :base(options)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChaosDbContext{TPage, TKey}"/> class.
        /// </summary>
        protected ChaosDbContext() { }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> of Pages.
        /// </summary>
        public DbSet<TPage> Pages { get; set; }

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

                b.Property(p => p.Name).HasMaxLength(255);
                b.Property(p => p.Url).HasMaxLength(255);
            });
        }
    }
}
