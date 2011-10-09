namespace FlagLib.IO
{
    /// <summary>
    /// Specifies which error occured during the directory scan.
    /// </summary>
    public enum DirectoryScanErrorType
    {
        SecurityError,
        AccessError,
        DirectoryNotFoundError
    }
}