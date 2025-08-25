using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForms_FileAnalyzer.Models
{
    internal class AnalysisResult
    {
        public List<WordCount> TopWords { get; set; } = new List<WordCount>();
        public Dictionary<char, int> Punctuation { get; set; } = new Dictionary<char, int>();
        public int UniqueWordCount { get; set; } = 0;
    }
}
