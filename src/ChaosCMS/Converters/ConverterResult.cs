using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosCMS.Converters
{
    /// <summary>
    /// Result of the Convertion
    /// </summary>
    /// <typeparam name="TDestination"></typeparam>
    public class ConverterResult<TDestination> : ChaosResult
    {
        /// <summary>
        /// 
        /// </summary>
        public TDestination Destination { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="destination"></param>
        /// <returns></returns>
        public new static ConverterResult<TDestination> Success(TDestination destination)
        {
            return new ConverterResult<TDestination>() { Destination = destination, Succeeded = true };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        public static ConverterResult<TDestination> Failed(TDestination destination, params ChaosError[] errors)
        {
            var result = new ConverterResult<TDestination>() { Destination = destination, Succeeded = false };
            if (errors != null)
            {
                result.errors.AddRange(errors);
            }
            return result;
        }
    }
    
}
