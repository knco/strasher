using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strash
{
    public interface IFileHash
    {
        string HashName { get; set; }
        string HashCode { get; set; }
        bool IsHashCodeUppercase { get; set; }
    }
}
