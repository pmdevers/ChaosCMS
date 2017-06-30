using System.Collections.Generic;
using System.Linq;

namespace ChaosCMS
{
    /// <summary>
    ///
    /// </summary>
    public class ChaosResult
    {
        private static readonly ChaosResult success = new ChaosResult() { Succeeded = true };
        /// <summary>
        /// 
        /// </summary>
        protected List<ChaosError> errors = new List<ChaosError>();

        /// <summary>
        ///
        /// </summary>
        public bool Succeeded { get; protected set; }

        /// <summary>
        ///
        /// </summary>
        public IEnumerable<ChaosError> Errors => this.errors;

        /// <summary>
        ///
        /// </summary>
        public static ChaosResult Success => success;

        /// <summary>
        ///
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        public static ChaosResult Failed(params ChaosError[] errors)
        {
            var result = new ChaosResult() { Succeeded = false };
            if (errors != null)
            {
                result.errors.AddRange(errors);
            }
            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Succeeded
                       ? "Succeeded"
                       : $"Failed : {string.Join(",", this.Errors.Select(x => x.Code).ToList())}";
        }
    }
}