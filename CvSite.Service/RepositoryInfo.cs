using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvSite.Service
{
    public class RepositoryInfo
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Language { get; set; }
        public DateTime LastCommit { get; set; }
        public int Stars { get; set; }
        public int PullRequests { get; set; }
    }

}
