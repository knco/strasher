using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;
using System.IO;

namespace Strash
{
    public class StrashFormatter : IFormatter
    {
        private string _labelDateStrashed = "DateStrashed";
        private string _labelFileHash = "Hash";
        public string LabelDateStrashed
        {
            get
            {
                return _labelDateStrashed;
            }
        }
        public string LabelFileHash
        {
            get
            {
                return _labelFileHash;
            }
        }
        public SerializationBinder Binder { get; set; }
        public StreamingContext Context { get; set; }
        public ISurrogateSelector SurrogateSelector { get; set; }

        public object Deserialize(Stream serializationStream)
        {
            using (StreamReader streamReader = new StreamReader(serializationStream))
            {
                return Deserialize(streamReader.ReadToEnd());
            }
        }
        public object Deserialize(string serializationString)
        {
            FileStrash fileStrash = new FileStrash();
            List<IFileHash> filesHashes = new List<IFileHash>();
            using (StringReader stringReader = new StringReader(serializationString))
            {
                string line;
                while ((line = stringReader.ReadLine()) != null)
                {
                    List<string> lineData = line.Split(':').ToList();
                    if (lineData.Count == 2 && !string.IsNullOrEmpty(System.Security.Cryptography.CryptoConfig.MapNameToOID(lineData[0])))
                    {
                        filesHashes.Add(new StrashHash { HashName = lineData[0], HashCode = lineData[1] });
                    }
                    if (lineData.Count == 3 && lineData[0] == LabelDateStrashed)
                    {
                        fileStrash.DateStrashed = new DateTimeOffset(Convert.ToInt64(lineData[1]), TimeSpan.Zero);
                    }
                }
            }
            fileStrash.fileHashes = filesHashes;
            return fileStrash;
        }
        public void Serialize(Stream serializationStream, object graph)
        {
            FileStrash fileStrash = (FileStrash)graph;
            StringBuilder stringBuilder = new StringBuilder();
            foreach (IFileHash fileHash in fileStrash.fileHashes)
            {
                stringBuilder.Append(fileHash.HashName);
                stringBuilder.Append(":");
                stringBuilder.Append(fileHash.HashCode);
                stringBuilder.Append(Environment.NewLine);
            }
            stringBuilder.Append(LabelDateStrashed);
            stringBuilder.Append(":");
            stringBuilder.Append(fileStrash.DateStrashed.UtcTicks);
            stringBuilder.Append(":");
            stringBuilder.Append(fileStrash.DateStrashed.ToString());
            stringBuilder.Append(Environment.NewLine);

            StreamWriter streamWriter = new StreamWriter(serializationStream);
            streamWriter.Write(stringBuilder.ToString());
            streamWriter.Flush();
        }
    }
}
