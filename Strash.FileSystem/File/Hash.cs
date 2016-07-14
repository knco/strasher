using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Security.Cryptography;

namespace Strash.FileSystem.File
{
    public class Hash : IFileHash
    {
        private bool _isHashCodeUppercase = false;
        public bool IsHashCodeUppercase
        {
            get { return _isHashCodeUppercase; }
            set { _isHashCodeUppercase = value; }
        }

        public string HashName { get; set; }
        public string HashCode { get; set; }

        private string GetHashCode(FileStream fileStream, HashAlgorithm hashAlgorithm)
        {
            string hashCode = BitConverter.ToString(hashAlgorithm.ComputeHash(fileStream)).Replace("-", "");
            return (IsHashCodeUppercase) ? hashCode.ToUpper() : hashCode.ToLower();
        }
        public string GetHashCode(FileInfo fileInfo, HashAlgorithm hashAlgorithm)
        {
            using (FileStream fileStream = fileInfo.OpenRead())
            {
                return GetHashCode(fileStream, hashAlgorithm);
            }
        }
        public List<Hash> GetHashes(FileInfo fileInfo, List<HashAlgorithm> hashAlgorithms)
        {
            List<Hash> hashes = new List<Hash>();

            foreach (HashAlgorithm hashAlgorithm in hashAlgorithms)
            {
                using (var fileStream = fileInfo.OpenRead())
                {
                    hashes.Add(new Hash { HashName = hashAlgorithm.GetType().BaseType.Name, HashCode = GetHashCode(fileStream, hashAlgorithm) });
                }
            }
            return hashes;
        }
    }
}
