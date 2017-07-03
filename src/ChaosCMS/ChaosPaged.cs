using System.Collections.Generic;

namespace ChaosCMS
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class ChaosPaged<TEntity>
        where TEntity : class
    {
        /// <summary>
        ///
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int ItemsPerPage { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int TotalPages => TotalItems / ItemsPerPage;

        /// <summary>
        ///
        /// </summary>
        public IEnumerable<TEntity> Items { get; set; } = new List<TEntity>();
    }
}