using Newtonsoft.Json;

#if NETSTANDARD2_0
using System.Drawing;
#endif

namespace DontPanic.TumblrSharp.Client
{
    public class TrialTheme
    {
        [JsonProperty(PropertyName = "header_full_width")]
        public int HeaderFullWidth { get; set; }

        [JsonProperty(PropertyName = "header_full_height")]
        public int HeaderFullHeight { get; set; }

        [JsonProperty(PropertyName = "header_focus_width")]
        public int HeaderFocusWidth { get; set; }

        [JsonProperty(PropertyName = "header_focus_height")]
        public int HeaderFocusHeight { get; set; }

        [JsonConverter(typeof(EnumConverter))]
        [JsonProperty(PropertyName = "avatar_shape")]
        public AvatarShape AvatarShape { get; set; }

#if NETSTANDARD2_0
        [JsonProperty(PropertyName = "background_color")]
        public Color BackgroundColor { get; set; }
#else
        [JsonProperty(PropertyName = "background_color")]
        public string BackgroundColor { get; set; }
#endif

        [JsonProperty(PropertyName = "body_font")]
        public string BodyFont { get; set; }

        [JsonProperty(PropertyName = "header_bounds")]
        public string HeaderBounds { get; set; }

        [JsonProperty(PropertyName = "header_image")]
        public string HeaderImage { get; set; }

        [JsonProperty(PropertyName = "header_image_focused")]
        public string HeaderImageFocused { get; set; }

        [JsonProperty(PropertyName = "header_image_scaled")]
        public string HeaderImageScaled { get; set; }

        [JsonProperty(PropertyName = "header_stretch")]
        public bool HeaderStretch { get; set; }

#if NETSTANDARD2_0
        [JsonProperty(PropertyName = "link_color")]
        public Color LinkColor { get; set; }
#else
        [JsonProperty(PropertyName = "link_color")]
        public string LinkColor { get; set; }
#endif

        [JsonProperty(PropertyName = "show_avatar")]
        public bool ShowAvatar { get; set; }

        [JsonProperty(PropertyName = "show_description")]
        public bool ShowDescription { get; set; }

        [JsonProperty(PropertyName = "show_header_image")]
        public bool ShowHeaderImage { get; set; }

        [JsonProperty(PropertyName = "show_title")]
        public bool ShowTitle { get; set; }

#if NETSTANDARD2_0
        [JsonProperty(PropertyName = "title_color")]
        public Color TitleColor { get; set; }
#else
        [JsonProperty(PropertyName = "title_color")]
        public string TitleColor { get; set; }
#endif

        [JsonProperty(PropertyName = "title_font")]
        public string TitleFont { get; set; }

        [JsonProperty(PropertyName = "title_font_weight")]
        public string TitleFontWeight { get; set; }
    }
}