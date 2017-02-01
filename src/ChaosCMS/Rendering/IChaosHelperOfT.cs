using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChaosCMS.Rendering
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TPage"></typeparam>
    public interface IChaosHelper<TModel, TPage> : IChaosHelper<TPage>
    {
    }
}
