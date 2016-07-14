using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Security.Cryptography;
using Strash;
using Strash.FileSystem.File;
using Strash.FileSystem.Ntfs.Ads;

namespace Strasher
{
    public class Strasher
    {
        public void StrashFile(string file, List<string> hashAlgorithmsNames)
        {
            FileInfo fileInfo = new FileInfo(file);

            List<HashAlgorithm> hashAlgorithms = new List<HashAlgorithm>();
            foreach (string hashAlgorthimName in hashAlgorithmsNames)
            {
                hashAlgorithms.Add(HashAlgorithm.Create(hashAlgorthimName));
            }

            Hash hash = new Hash();
            FileStrash fileStrash = new FileStrash();
            fileStrash.DateStrashed = DateTimeOffset.Now;
            fileStrash.fileHashes = hash.GetHashes(fileInfo, hashAlgorithms);

            string streamText;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                (new StrashFormatter()).Serialize(memoryStream, fileStrash);
                memoryStream.Position = 0;
                streamText = (new StreamReader(memoryStream)).ReadToEnd();
            }

            AdsWriter adsWriter = new AdsWriter();
            adsWriter.WriteUtf8(fileInfo, fileStrash.StrashName, streamText);
        }
        public List<FileStrash> GetFileStrashes(string file)
        {
            List<FileStrash> fileStrashes = new List<FileStrash>();
            foreach (KeyValuePair<string, string> strash in ReadFileStrashes(file))
            {
                StrashFormatter strashFormatter = new StrashFormatter();
                fileStrashes.Add((FileStrash)strashFormatter.Deserialize(strash.Value));
            }
            return fileStrashes;
        }
        public Dictionary<string, string> ReadFileStrashes(string file)
        {
            FileInfo fileInfo = new FileInfo(file);
            AdsReader adsReader = new AdsReader();
            return adsReader.ReadUtf8AllHavingPrefix(fileInfo, FileStrash.StrashNamePrefix);
        }
        public Dictionary<string, bool> VerifyLastestStrash(string file)
        {
            FileInfo fileInfo = new FileInfo(file);
            AdsReader adsReader = new AdsReader();

            List<long> strashIds = new List<long>();
            foreach (string adsName in adsReader.ListAlternateDataStreams(fileInfo))
            {
                if (adsName.StartsWith(FileStrash.StrashNamePrefix))
                {
                    strashIds.Add(Convert.ToInt64(adsName.Split('.')[1]));
                }
            }
            string strashText = adsReader.ReadUtf8(fileInfo, string.Concat(FileStrash.StrashNamePrefix, strashIds.Max().ToString()));
            FileStrash fileStrash = (FileStrash)(new StrashFormatter().Deserialize(strashText));

            Dictionary<string, bool> verifiedItems = new Dictionary<string, bool>();
            if (fileStrash.fileHashes != null)
            {
                foreach (IFileHash fileHash in fileStrash.fileHashes)
                {
                    verifiedItems.Add(fileHash.HashName, fileHash.HashCode == (new Hash()).GetHashCode(fileInfo, HashAlgorithm.Create(fileHash.HashName)));
                }
            }
            return verifiedItems;
        }
    }
}
