using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Trinet.Core.IO.Ntfs;

namespace Strash.FileSystem.Ntfs.Ads
{
    public class AdsWriter
    {
        public AdsWriter()
        {
            PermitOverwrite = false;
        }
        public bool PermitOverwrite { get; set; }
        public void WriteUtf8(FileInfo fileInfo, string adsName, string adsUtf8Text)
        {
            if (PermitOverwrite || !fileInfo.AlternateDataStreamExists(adsName))
            {
                using (FileStream fileStream = fileInfo.GetAlternateDataStream(adsName).OpenWrite())
                {
                    Byte[] adsBytes = new UTF8Encoding(true).GetBytes(adsUtf8Text);
                    fileStream.Write(adsBytes, 0, adsBytes.Length);
                }
            }
        }
    }
}
