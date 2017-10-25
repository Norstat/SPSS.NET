namespace Spss
{
    /// <summary>
    /// Measurement Level codes
    /// </summary>
    public enum MeasurementLevelCode : int
    {
        /// <summary>
        /// Unknown
        /// </summary>
        SPSS_MLVL_UNK = 0,

        /// <summary>
        /// Nominal 
        /// </summary>
        SPSS_MLVL_NOM = 1,

        /// <summary>
        /// Ordinal
        /// </summary>
        SPSS_MLVL_ORD = 2,

        /// <summary>
        /// Scale (Ratio)
        /// </summary>
        SPSS_MLVL_RAT = 3,
    }
}