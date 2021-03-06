﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosCMS.Converters
{
    /// <summary>
    /// A Converter thats converts one instance to another
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    public interface IConverter<TSource, TDestination>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        Task<ConverterResult<TDestination>> Convert(TSource source);
    }
}
