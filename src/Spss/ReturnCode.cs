namespace Spss
{
    /// <summary>
    /// Error/warning codes that calls to SPSS methods can return.
    /// </summary>
    public enum ReturnCode : int
    {
        /// <summary>
        /// No error
        /// </summary>
        SPSS_OK = 0,

        #region Error codes that calls to SPSS methods can return.
        /// <summary>
        /// Error opening file
        /// </summary>
        SPSS_FILE_OERROR = 1,
        /// <summary>
        /// File write error
        /// </summary>
        SPSS_FILE_WERROR = 2,
        /// <summary>
        /// Error reading the file
        /// </summary>
        SPSS_FILE_RERROR = 3,
        /// <summary>
        /// File table full (too many open SPSS data files)
        /// </summary>
        SPSS_FITAB_FULL = 4,
        /// <summary>
        /// The file handle is not valid
        /// </summary>
        SPSS_INVALID_HANDLE = 5,
        /// <summary>
        /// Data file contains no variables
        /// </summary>
        SPSS_INVALID_FILE = 6,
        /// <summary>
        /// Insufficient memory
        /// </summary>
        SPSS_NO_MEMORY = 7,

        /// <summary>
        /// File is open for reading, not writing
        /// </summary>
        SPSS_OPEN_RDMODE = 8,
        /// <summary>
        /// The file is open for writing, not reading
        /// </summary>
        SPSS_OPEN_WRMODE = 9,

        /// <summary>
        /// The variable name is not valid
        /// </summary>
        SPSS_INVALID_VARNAME = 10,
        /// <summary>
        /// No variables defined in the dictionary
        /// </summary>
        SPSS_DICT_EMPTY = 11,
        /// <summary>
        /// A variable with the given name does not exist
        /// </summary>
        SPSS_VAR_NOTFOUND = 12,
        /// <summary>
        /// There is already a variable with the same name
        /// </summary>
        SPSS_DUP_VAR = 13,
        /// <summary>
        /// The variable is not numeric
        /// OR
        /// At least one variable in the list is not numeric
        /// OR
        /// The specified variable has string values
        /// </summary>
        SPSS_NUME_EXP = 14,
        /// <summary>
        /// The variable is numeric
        /// </summary>
        SPSS_STR_EXP = 15,
        /// <summary>
        /// The variable is a long string (length > 8)
        /// </summary>
        SPSS_SHORTSTR_EXP = 16,
        /// <summary>
        /// Invalid length code (varLength is negative or
        /// exceeds 255)
        /// </summary>
        SPSS_INVALID_VARTYPE = 17,

        /// <summary>
        /// Invalid missing values specification ( missingFormat
        /// is invalid or the lower limit of range is greater than the
        /// upper limit)
        /// </summary>
        SPSS_INVALID_MISSFOR = 18,
        /// <summary>
        /// Invalid compression switch (other than 0 or 1)
        /// </summary>
        SPSS_INVALID_COMPSW = 19,
        /// <summary>
        /// The print format specification is invalid or is
        /// incompatible with the variable type
        /// </summary>
        SPSS_INVALID_PRFOR = 20,
        /// <summary>
        /// The write format specification is invalid or is
        /// incompatible with the variable type
        /// </summary>
        SPSS_INVALID_WRFOR = 21,
        /// <summary>
        /// Invalid date 
        /// OR
        /// The date value (spssDate) is negative
        /// </summary>
        SPSS_INVALID_DATE = 22,
        /// <summary>
        /// Invalid time
        /// </summary>
        SPSS_INVALID_TIME = 23,

        /// <summary>
        /// Fewer than two variables in list 
        /// OR
        /// Number of variables ( numVars) is zero or negative
        /// </summary>
        SPSS_NO_VARIABLES = 24,
        /// <summary>
        /// The list of values contains duplicates
        /// </summary>
        SPSS_DUP_VALUE = 27,

        /// <summary>
        /// The given case weight variable is invalid. This error
        /// signals an internal problem in the implementation
        /// of the DLL and should never occur.
        /// </summary>
        SPSS_INVALID_CASEWGT = 28,
        /// <summary>
        /// Dictionary has already been written with
        /// spssCommitHeader
        /// </summary>
        SPSS_DICT_COMMIT = 30,
        /// <summary>
        /// Dictionary of the output file has not yet been written
        /// with <see cref="SpssThinWrapper.spssCommitHeaderDelegate"/>.
        /// </summary>
        SPSS_DICT_NOTCOMMIT = 31,

        /// <summary>
        /// File is not a valid SPSS data file (no type 2 record)
        /// </summary>
        SPSS_NO_TYPE2 = 33,
        /// <summary>
        /// There is no type7, subtype3 record present. This
        /// code should be regarded as a warning even though
        /// it is positive. Files without this record are valid.
        /// </summary>
        SPSS_NO_TYPE73 = 41,
        /// <summary>
        /// The date variable information is invalid
        /// </summary>
        SPSS_INVALID_DATEINFO = 45,
        /// <summary>
        /// File is not a valid SPSS data file (missing type 999
        /// record)
        /// </summary>
        SPSS_NO_TYPE999 = 46,
        /// <summary>
        /// The value is longer than the length of the variable
        /// </summary>
        SPSS_EXC_STRVALUE = 47,
        /// <summary>
        /// Unable to free because arguments are illegal or
        /// inconsistent (for example, negative numLabels)
        /// </summary>
        SPSS_CANNOT_FREE = 48,
        /// <summary>
        /// Buffer value is too short to hold the value
        /// </summary>
        SPSS_BUFFER_SHORT = 49,
        /// <summary>
        /// Current case is not valid. This may be because no
        /// spssReadCaseRecord calls have been made yet or
        /// because the most recent call failed with error or encountered
        /// the end of file.
        /// </summary>
        SPSS_INVALID_CASE = 50,
        /// <summary>
        /// Internal data structures of the DLL are invalid. This
        /// signals an error in the DLL.
        /// </summary>
        SPSS_INTERNAL_VLABS = 51,
        /// <summary>
        /// File created on an incompatible system.
        /// </summary>
        SPSS_INCOMPAT_APPEND = 52,
        /// <summary>
        /// Undocumented by SPSS.
        /// </summary>
        SPSS_INTERNAL_D_A = 53,
        /// <summary>
        /// Error accessing the temporary file
        /// </summary>
        SPSS_FILE_BADTEMP = 54,
        /// <summary>
        /// spssGetDEWFirst was never called
        /// </summary>
        SPSS_DEW_NOFIRST = 55,
        /// <summary>
        /// measureLevel is not in the legal range, or is
        /// SPSS_MLVL_RAT and the variable is a string variable.
        /// </summary>
        SPSS_INVALID_MEASURELEVEL = 56,
        /// <summary>
        /// Parameter subtype not between 1 and
        /// MAX7SUBTYPE
        /// </summary>
        SPSS_INVALID_7SUBTYPE = 57,
        /// <summary>
        /// Invalid encoding
        /// </summary>
        SPSS_INVALID_ENCODING = 59, // not confirmed

        /// <summary>
        /// Files are open, which prevents encoding from changing
        /// </summary>
        SPSS_FILES_OPEN = 60, // not confirmed

        /// <summary>
        /// Existing multiple response set definitions are invalid
        /// </summary>
        SPSS_INVALID_MRSETDEF = 70,
        /// <summary>
        /// The multiple response set name is invalid
        /// </summary>
        SPSS_INVALID_MRSETNAME = 71,
        /// <summary>
        /// The multiple response set name is a duplicate
        /// </summary>
        SPSS_DUP_MRSETNAME = 72,
        /// <summary>
        /// Undocumented by SPSS.
        /// </summary>
        SPSS_BAD_EXTENSION = 73,
        #endregion

        #region Warning codes returned by functions

        /// <summary>
        /// Label length exceeds 64, truncated and used (warning)
        /// </summary>
        SPSS_EXC_LEN64 = -1,

        /// <summary>
        /// Variable label’s length exceeds 120, truncated and
        /// used (warning)
        /// </summary>
        SPSS_EXC_LEN120 = -2,

        /// <summary>
        /// ... (warning)
        /// </summary>
        SPSS_EXC_VARLABEL = -2,

        /// <summary>
        /// Label length exceeds 60, truncated and used (warning)
        /// </summary>
        SPSS_EXC_LEN60 = -4,

        /// <summary>
        /// ... (warning)
        /// </summary>
        SPSS_EXC_VALLABEL = -4,

        /// <summary>
        /// End of the file reached, no more cases (warning)
        /// </summary>
        SPSS_FILE_END = -5,

        /// <summary>
        /// There is no variable sets information in the file (warning)
        /// </summary>
        SPSS_NO_VARSETS = -6,

        /// <summary>
        /// The variable sets information is empty (warning)
        /// </summary>
        SPSS_EMPTY_VARSETS = -7,

        /// <summary>
        /// The variable has no labels (warning) (warning)
        /// </summary>
        SPSS_NO_LABELS = -8,

        /// <summary>
        /// There is no label for the given value (warning)
        /// </summary>
        SPSS_NO_LABEL = -9,

        /// <summary>
        /// A case weight variable has not been defined for this file (warning)
        /// </summary>
        SPSS_NO_CASEWGT = -10,

        /// <summary>
        /// There is no TRENDS date variable information in the file (warning)
        /// </summary>
        SPSS_NO_DATEINFO = -11,

        /// <summary>
        /// No definitions on the file (warning)
        /// </summary>
        SPSS_NO_MULTRESP = -12,

        /// <summary>
        /// The string contains no definitions (warning)
        /// </summary>
        SPSS_EMPTY_MULTRESP = -13,

        /// <summary>
        /// File contains no DEW information (warning)
        /// </summary>
        SPSS_NO_DEW = -14,

        /// <summary>
        /// Zero bytes to be written (warning)
        /// </summary>
        SPSS_EMPTY_DEW = -15,
        #endregion
    }
}