using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Trinet.Core.IO.Ntfs;

namespace Strash.FileSystem.Ntfs.Ads
{
    public class AdsReader
    {
        public List<string> ListAlternateDataStreams(FileInfo fileInfo)
        {
            List<string> alternateDataStreams = new List<string>();
            foreach (AlternateDataStreamInfo fileAlternateDataStream in fileInfo.ListAlternateDataStreams())
            {
                alternateDataStreams.Add(fileAlternateDataStream.Name);
            }
            return alternateDataStreams;
        }
        public string ReadUtf8(FileInfo fileInfo, string adsName)
        {
            using (FileStream fileStream = fileInfo.GetAlternateDataStream(adsName).OpenRead())
            {
                using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }
        public Dictionary<string, string> ReadUtf8AllHavingPrefix(FileInfo fileInfo, string adsNamePrefix)
        {
            Dictionary<string, string> fileStreams = new Dictionary<string, string>();
            foreach (AlternateDataStreamInfo fileAlternateDataStream in fileInfo.ListAlternateDataStreams())
            {
                if (fileAlternateDataStream.Name.StartsWith(adsNamePrefix))
                {
                    fileStreams.Add(fileAlternateDataStream.Name, ReadUtf8(fileInfo, fileAlternateDataStream.Name));
                }
            }
            return fileStreams;
        }
    }
}
