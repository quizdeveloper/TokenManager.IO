using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenManager.Entities.Condition
{
    public class TokenSearchCondition
    {
        /// <summary>
        /// Search by Name or Symbol
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// Current page index 
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// Total item per page
        /// </summary>
        public int PageItem { get; set; }
    }
}
