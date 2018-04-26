using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontPanic.TumblrSharp
{
	internal class TumblrErrors
	{
		[JsonProperty("errors")]
		public string[] Errors { get; set; }
	}
}














