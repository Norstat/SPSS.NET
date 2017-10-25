namespace Spss
{
    /// <summary>
    /// Missing Value Type codes
    /// </summary>
    public enum MissingValueFormatCode : int
    {
        /// <summary>
        /// Indicates that no discrete missing values will be defined.
        /// </summary>
        SPSS_NO_MISSVAL = 0,

        /// <summary>
        /// Indicates that 1 discrete missing values will be defined.
        /// </summary>
        SPSS_ONE_MISSVAL = 1,

        /// <summary>
        /// Indicates that 2 discrete missing values will be defined.
        /// </summary>
        SPSS_TWO_MISSVAL = 2,

        /// <summary>
        /// Indicates that 3 discrete missing values will be defined.
        /// </summary>
        SPSS_THREE_MISSVAL = 3,

        /// <summary>
        /// missingVal1 and missingVal2 are taken as the upper and lower limits, 
        /// respectively, of the range, and missingVal3 is ignored
        /// </summary>
        SPSS_MISS_RANGE = -2,

        /// <summary>
        /// missingval1 and missingVal2 are taken as limits of the range and missingVal3 is taken
        /// as the discrete missing value.
        /// </summary>
        SPSS_MISS_RANGEANDVAL = -3,
    }
}