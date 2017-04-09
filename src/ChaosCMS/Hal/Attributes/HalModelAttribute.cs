using System;

namespace ChaosCMS.Hal.Attributes
{
    /// <summary>
    ///
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class HalModelAttribute : Attribute
    {
        /// <summary>
        ///
        /// </summary>
        public string LinkBase { get; }

        /// <summary>
        ///
        /// </summary>
        public bool? ForceHal { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="linkBase"></param>
        public HalModelAttribute(string linkBase = null)
        {
            LinkBase = linkBase;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="linkBase"></param>
        /// <param name="forceHAL"></param>
        public HalModelAttribute(string linkBase, bool forceHAL)
        {
            LinkBase = linkBase;
            ForceHal = forceHAL;
        }
    }
}