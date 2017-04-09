namespace ChaosCMS.Hal
{
    /// <summary>
    ///
    /// </summary>
    public class HalModelConfig : IHalModelConfig
    {
        /// <inheritdoc cref="IHalModelConfig" />
        public bool ForceHal { get; set; }

        /// <inheritdoc cref="IHalModelConfig" />
        public string LinkBase { get; set; }
    }
}