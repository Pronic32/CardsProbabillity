using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardsProbability
{
    public class Experience
    {
        /// <summary>
        /// Количество вынутых карт
        /// </summary>
        public byte ExtractedCount;
        /// <summary>
        /// Количество карт в колоде
        /// </summary>
        public byte TotalCount;

        
        public Experience(byte extracted, byte total)
        {
            ExtractedCount = extracted;
            TotalCount = total;
        }
      
    }
}
