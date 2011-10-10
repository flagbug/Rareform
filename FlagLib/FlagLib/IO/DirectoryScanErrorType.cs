using System;

namespace FlagLib.IO
{
    /// <summary>
    /// Specifies which error occured during the directory scan.
    /// </summary>
    [Serializable]
    public enum DirectoryScanErrorType
    {
        SecurityError,
        AccessError,
        DirectoryNotFoundError
    }
}