using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellCheck
{
    public class FlaggedTokens
    {
        public int offset { get; set; }
        public string token { get; set; }
        public string type { get; set; }
        public List<Suggestions> suggestions { get; set; }
    }

    public class Suggestions
    {
        public string suggestion { get; set; }
        public double score { get; set; }
    }

    public class ResponseModel
    {
        public string _type { get; set; }
        public List<FlaggedTokens> flaggedTokens { get; set; }
    }    
}
