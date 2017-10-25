namespace Spss
{
    /// <summary>
    /// Definitions of "type 7" records
    /// </summary>
    public enum Type7Record : int
    {
        /// <summary>
        /// Documents (actually type 6
        /// </summary>
        SPSS_T7_DOCUMENTS = 0,

        /// <summary>
        /// VAX Data Entry - dictionary version
        /// </summary>
        SPSS_T7_VAXDE_DICT = 1,

        /// <summary>
        /// VAX Data Entry - data
        /// </summary>
        SPSS_T7_VAXDE_DATA = 2,

        /// <summary>
        /// Source system characteristics
        /// </summary>
        SPSS_T7_SOURCE = 3,

        /// <summary>
        /// Source system floating pt constants
        /// </summary>
        SPSS_T7_HARDCONST = 4,

        /// <summary>
        /// Variable sets
        /// </summary>
        SPSS_T7_VARSETS = 5,

        /// <summary>
        /// Trends date information
        /// </summary>
        SPSS_T7_TRENDS = 6,

        /// <summary>
        /// Multiple response groups
        /// </summary>
        SPSS_T7_MULTRESP = 7,

        /// <summary>
        /// Windows Data Entry data
        /// </summary>
        SPSS_T7_DEW_DATA = 8,

        /// <summary>
        /// TextSmart data
        /// </summary>
        SPSS_T7_TEXTSMART = 10,

        /// <summary>
        /// Msmt level, col width, &amp; alignment
        /// </summary>
        SPSS_T7_MSMTLEVEL = 11,

        /// <summary>
        /// Windows Data Entry GUID
        /// </summary>
        SPSS_T7_DEW_GUID = 12,

        /// <summary>
        /// Extended variable names
        /// </summary>
        SPSS_T7_XVARNAMES = 13,
    }
}