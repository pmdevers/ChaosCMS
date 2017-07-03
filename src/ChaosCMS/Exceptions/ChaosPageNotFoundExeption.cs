using Microsoft.AspNetCore.Http;

namespace ChaosCMS
{
    /// <summary>
    ///
    /// </summary>
    public class ChaosHttpExeption : ChaosException
    {
        private ChaosHttpExeption()
        {
        }

        private ChaosHttpExeption(string message) : base(message)
        {
        }

        /// <summary>
        /// The <see cref="StatusCodes"/> that occourd.
        /// </summary>
        public int HttpStatusCode { get; private set; }

        /// <summary>
        /// Initializes a new <see cref="ChaosHttpExeption"/> with status code 404
        /// </summary>
        /// <returns></returns>
        public static ChaosException PageNotFound(string requestPath)
        {
            var message = Resources.FormatPageNotFound(requestPath);
            return new ChaosHttpExeption(message) { HttpStatusCode = StatusCodes.Status404NotFound };
        }
    }
}