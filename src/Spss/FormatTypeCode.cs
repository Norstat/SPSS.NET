namespace Spss
{
    /// <summary>
    /// Format Type codes
    /// </summary>
    public enum FormatTypeCode : int
    {
        /// <summary>
        /// Alphanumeric
        /// </summary>
        SPSS_FMT_A = 1,

        /// <summary>
        /// Alphanumeric hexadecimal
        /// </summary>
        SPSS_FMT_AHEX = 2,

        /// <summary>
        /// F Format with commas
        /// </summary>
        SPSS_FMT_COMMA = 3,

        /// <summary>
        /// Commas and floating dollar sign
        /// </summary>
        SPSS_FMT_DOLLAR = 4,

        /// <summary>
        /// Default Numeric Format
        /// </summary>
        SPSS_FMT_F = 5,

        /// <summary>
        /// Int16 binary
        /// </summary>
        SPSS_FMT_IB = 6,

        /// <summary>
        /// Positive Int16 binary - hex
        /// </summary>
        SPSS_FMT_PIBHEX = 7,

        /// <summary>
        /// Packed decimal
        /// </summary>
        SPSS_FMT_P = 8,

        /// <summary>
        /// Positive Int16 binary unsigned
        /// </summary>
        SPSS_FMT_PIB = 9,

        /// <summary>
        /// Positive Int16 binary unsigned
        /// </summary>
        SPSS_FMT_PK = 10,

        /// <summary>
        /// Floating poInt32 binary
        /// </summary>
        SPSS_FMT_RB = 11,

        /// <summary>
        /// Floating poInt32 binary hex
        /// </summary>
        SPSS_FMT_RBHEX = 12,

        /// <summary>
        /// Zoned decimal
        /// </summary>
        SPSS_FMT_Z = 15,

        /// <summary>
        /// N Format- unsigned with leading 0s
        /// </summary>
        SPSS_FMT_N = 16,

        /// <summary>
        /// E Format- with explicit power of 10
        /// </summary>
        SPSS_FMT_E = 17,

        /// <summary>
        /// Date format dd-mmm-yyyy
        /// </summary>
        SPSS_FMT_DATE = 20,

        /// <summary>
        /// Time format hh:mm:ss.s
        /// </summary>
        SPSS_FMT_TIME = 21,

        /// <summary>
        /// Date and Time
        /// </summary>
        SPSS_FMT_DATE_TIME = 22,

        /// <summary>
        /// Date format dd-mmm-yyyy
        /// </summary>
        SPSS_FMT_ADATE = 23,

        /// <summary>
        /// Julian date - yyyyddd
        /// </summary>
        SPSS_FMT_JDATE = 24,

        /// <summary>
        /// Date-time dd hh:mm:ss.s
        /// </summary>
        SPSS_FMT_DTIME = 25,

        /// <summary>
        /// Day of the week
        /// </summary>
        SPSS_FMT_WKDAY = 26,

        /// <summary>
        /// Month
        /// </summary>
        SPSS_FMT_MONTH = 27,

        /// <summary>
        /// mmm yyyy
        /// </summary>
        SPSS_FMT_MOYR = 28,

        /// <summary>
        /// q Q yyyy
        /// </summary>
        SPSS_FMT_QYR = 29,

        /// <summary>
        /// ww WK yyyy
        /// </summary>
        SPSS_FMT_WKYR = 30,

        /// <summary>
        /// Percent - F followed by %
        /// </summary>
        SPSS_FMT_PCT = 31,

        /// <summary>
        /// Like COMMA, switching dot for comma
        /// </summary>
        SPSS_FMT_DOT = 32,

        /// <summary>
        /// User Programmable currency format
        /// </summary>
        SPSS_FMT_CCA = 33,

        /// <summary>
        /// User Programmable currency format
        /// </summary>
        SPSS_FMT_CCB = 34,

        /// <summary>
        /// User Programmable currency format
        /// </summary>
        SPSS_FMT_CCC = 35,

        /// <summary>
        /// User Programmable currency format
        /// </summary>
        SPSS_FMT_CCD = 36,

        /// <summary>
        /// User Programmable currency format
        /// </summary>
        SPSS_FMT_CCE = 37,

        /// <summary>
        /// Date in dd/mm/yyyy style
        /// </summary>
        SPSS_FMT_EDATE = 38,

        /// <summary>
        /// Date in yyyy/mm/dd style
        /// </summary>
        SPSS_FMT_SDATE = 39,
    }
}