using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimassUHFUploadVideo.Vpass.Object.ObjectNhanDienBienSo
{
    public class Result
    {
        public Box box { get; set; }
        public string plate { get; set; }
        public Region region { get; set; }
        public double score { get; set; }
        public List<Candidate> candidates { get; set; }
        public double dscore { get; set; }
        public Vehicle vehicle { get; set; }
    }
}
