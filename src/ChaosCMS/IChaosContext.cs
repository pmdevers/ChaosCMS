namespace ChaosCMS
{
    /// <summary>
    /// 
    /// </summary>
    public interface IChaosContext<TPage>
    {
        /// <summary>
        /// 
        /// </summary>
        TPage CurrentPage { get; }
    }
}