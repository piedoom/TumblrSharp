using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontPanic.TumblrSharp
{
	/// <summary>
	/// Defines the type of note on a post
	/// </summary>
	public enum NoteType
	{
        /// <summary>
        /// posted
        /// </summary>
        Posted = 0,
        /// <summary>
        /// reblog
        /// </summary>
		Reblog = 1,
        /// <summary>
        /// like
        /// </summary>
		Like = 2,
        /// <summary>
        /// reply
        /// </summary>
		Reply = 3,
        /// <summary>
        /// post attribution
        /// </summary>
        Post_attribution = 4
    }
}
