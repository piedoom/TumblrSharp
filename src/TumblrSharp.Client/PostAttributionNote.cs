using System;
using System.Collections.Generic;
using System.Text;

namespace DontPanic.TumblrSharp.Client
{
    /// <summary>
    /// PostAttributionNote
    /// </summary>
    public class PostAttributionNote : BaseNote
    {
        /// <summary>
        /// post_attribution_type
        /// </summary>
        public string Post_attribution_type { get; set; }
        /// <summary>
        /// post_attribution_type_name
        /// </summary>
        public string Post_attribution_type_name { get; set; }
        /// <summary>
        /// Photo_url
        /// </summary>
        public string Photo_url { get; set; }
        /// <summary>
        /// Photo_width
        /// </summary>
        public int Photo_width { get; set; }
        /// <summary>
        /// Photo_height
        /// </summary>
        public int Photo_height { get; set; }
    }
}
