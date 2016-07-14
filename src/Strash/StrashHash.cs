using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strash
{
    public class StrashHash : IFileHash
    {
        public StrashHash()
        {
            IsHashCodeUppercase = false;
        }
        public string HashName { get; set; }
        public string HashCode { get; set; }
        public bool IsHashCodeUppercase { get; set; }
    }
}
