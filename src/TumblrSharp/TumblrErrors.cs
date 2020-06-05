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

        /// <summary>
        /// compare this object with another
        /// </summary>
        /// <param name="obj">Object to be equals</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return obj is TumblrError error &&
                   Title == error.Title &&
                   Code == error.Code &&
                   Detail == error.Detail;
        }

        /// <summary>
        /// returns a hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
#if NETSTANDARD2_1
            return HashCode.Combine(Title, Code, Detail);
#else
            var hashString = Title + Code.ToString() + Detail;
            return hashString.GetHashCode();
#endif
        }
    }
}