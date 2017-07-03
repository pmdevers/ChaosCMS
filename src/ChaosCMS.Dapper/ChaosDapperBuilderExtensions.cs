using System;

namespace ChaosCMS.Dapper
{
    public static class ChaosDapperBuilderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name=""></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static ChaosBuilder AddDapperStores<TContext>(this ChaosBuilder builder, Action<ChaosDapperStoreOptions> options = null)
        {


            return builder;
        }
    }
}
