namespace Spss
{
    /// <summary>
    /// Diagnostics regarding var names
    /// </summary>
    public enum VarNameDiagnostic : int
    {
        /// <summary>
        /// Valid standard name
        /// </summary>
        SPSS_NAME_OK = 0,

        /// <summary>
        /// Valid scratch var name
        /// </summary>
        SPSS_NAME_SCRATCH = 1,

        /// <summary>
        /// Valid system var name
        /// </summary>
        SPSS_NAME_SYSTEM = 2,

        /// <summary>
        /// Empty or longer than SPSS_MAX_VARNAME
        /// </summary>
        SPSS_NAME_BADLTH = 3,

        /// <summary>
        /// Invalid character or imbedded blank
        /// </summary>
        SPSS_NAME_BADCHAR = 4,

        /// <summary>
        /// Name is a reserved word
        /// </summary>
        SPSS_NAME_RESERVED = 5,

        /// <summary>
        /// Invalid initial character
        /// </summary>
        SPSS_NAME_BADFIRST = 6,
    }
}