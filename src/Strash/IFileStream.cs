using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strash
{
    public interface IFileStream
    {
        string ForkName { get; set; }
        byte[] ForkData { get; set; }
    }
}
