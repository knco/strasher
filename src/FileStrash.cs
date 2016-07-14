using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strash
{
    [Serializable]
    public class FileStrash
    {
        private static string _strashNamePrefix = "Strash.";

        public static string StrashNamePrefix
        {
            get
            {
                return _strashNamePrefix;
            }
        }

        public DateTimeOffset DateStrashed { get; set; }
        public IEnumerable<IFileHash> fileHashes { get; set; }
        public string StrashName
        {
            get
            {
                return string.Concat(_strashNamePrefix, DateStrashed.UtcTicks);
            }
        }
    }
}
