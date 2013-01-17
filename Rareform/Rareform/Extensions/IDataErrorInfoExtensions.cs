using System.ComponentModel;
using System.Linq;

namespace Rareform.Extensions
{
    public static class IDataErrorInfoExtensions
    {
        public static bool HasErrors(this IDataErrorInfo info)
        {
            bool hasErrors = info.GetType().GetProperties().Any(p => info[p.Name] != null);
        }
    }
}