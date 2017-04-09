using System;

namespace ChaosCMS
{
    /// <summary>
    /// The base exception type of the chaos system
    /// </summary>
    public class ChaosException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChaosException"/>
        /// </summary>
        public ChaosException() : base("Unkown Exception!")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChaosException"/>
        /// </summary>
        /// <param name="message">The message of the <see cref="ChaosException"/>.</param>
        public ChaosException(string message) : base(message)
        {
        }
    }
}