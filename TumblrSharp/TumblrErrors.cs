using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontPanic.TumblrSharp
{
    /// <summary>
    /// TumblrError
    /// </summary>
	public class TumblrError
	{
        /// <summary>
        /// title of error
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        /// <summary>
        /// codenumber of error
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public int Code { get; set; }

        /// <summary>
        /// details of error
        /// </summary>
        [JsonProperty(PropertyName = "detail")]
        public string Detail { get; set; }
    }
}