namespace ChaosCMS.Hal
{
    /// <summary>
    ///
    /// </summary>
    public class PagedHalResponse<T> where T : class
    {
        /// <summary>
        ///
        /// </summary>
        public int TotalPages { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public int TotalResults { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public int Page { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="model"></param>
        public PagedHalResponse(ChaosPaged<T> model)
        {
            this.TotalPages = model.TotalPages;
            this.TotalResults = model.TotalItems;
            this.Page = model.CurrentPage;
        }
    }
}