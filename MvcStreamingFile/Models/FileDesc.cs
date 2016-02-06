using System.Runtime.Serialization;

namespace MvcStreamingFile.Controllers
{
    [DataContract]
    public class FileDesc
    {
        public FileDesc()
        {
        }

        public FileDesc(string n, string p, long s)
        {
            name = n;
            path = p;
            size = s;
        }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string path { get; set; }

        [DataMember]
        public long size { get; set; }
    }
}