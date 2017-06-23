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
		Posted = 0,
		Reblog = 1,
		Like = 2,
		Reply = 3
	}
}
