﻿using Newtonsoft.Json;

#if NETSTANDARD2_0
using System.Drawing;
#endif

namespace DontPanic.TumblrSharp.Client
{
    /// <summary>
    /// theme of a blog see <see cref="TrailBlog.Theme"/>
    /// </summary>
    public class TrailTheme
    {
        /// <summary>
        /// full width of the header
        /// </summary>
        [JsonProperty(PropertyName = "header_full_width")]
        public int HeaderFullWidth { get; set; }

        /// <summary>
        /// full height of the header
        /// </summary>
        [JsonProperty(PropertyName = "header_full_height")]
        public int HeaderFullHeight { get; set; }

        /// <summary>
        /// focus width of the header
        /// </summary>
        [JsonProperty(PropertyName = "header_focus_width")]
        public int HeaderFocusWidth { get; set; }

        /// <summary>
        /// focus height of the header
        /// </summary>
        [JsonProperty(PropertyName = "header_focus_height")]
        public int HeaderFocusHeight { get; set; }

        /// <summary>
        /// avatarshape
        /// </summary>
        [JsonProperty(PropertyName = "avatar_shape")]
        public AvatarShape AvatarShape { get; set; }

#if NETSTANDARD2_0
        /// <summary>
        /// Backgroundcolor
        /// </summary>
        [JsonProperty(PropertyName = "background_color")]
        public Color BackgroundColor { get; set; }
#else
        /// <summary>
        /// Backgroundcolor
        /// </summary>
        [JsonProperty(PropertyName = "background_color")]
        public string BackgroundColor { get; set; }
#endif
        /// <summary>
        /// font of the body
        /// </summary>
        [JsonProperty(PropertyName = "body_font")]
        public string BodyFont { get; set; }

        /// <summary>
        /// bounds of the header
        /// </summary>
        [JsonProperty(PropertyName = "header_bounds")]
        public string HeaderBounds { get; set; }

        /// <summary>
        /// image from the header
        /// </summary>
        [JsonProperty(PropertyName = "header_image")]
        public string HeaderImage { get; set; }

        /// <summary>
        /// focused image from the header
        /// </summary>
        [JsonProperty(PropertyName = "header_image_focused")]
        public string HeaderImageFocused { get; set; }

        /// <summary>
        /// scaled image from the header
        /// </summary>
        [JsonProperty(PropertyName = "header_image_scaled")]
        public string HeaderImageScaled { get; set; }

        /// <summary>
        /// is the header stretched
        /// </summary>
        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty(PropertyName = "header_stretch")]
        public bool HeaderStretch { get; set; }

#if NETSTANDARD2_0
        /// <summary>
        /// color of link
        /// </summary>
        [JsonProperty(PropertyName = "link_color")]
        public Color LinkColor { get; set; }
#else
        /// <summary>
        /// color of link
        /// </summary>
        [JsonProperty(PropertyName = "link_color")]
        public string LinkColor { get; set; }
#endif

        /// <summary>
        /// show the avatar
        /// </summary>
        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty(PropertyName = "show_avatar")]
        public bool ShowAvatar { get; set; }

        /// <summary>
        /// show the description
        /// </summary>
        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty(PropertyName = "show_description")]
        public bool ShowDescription { get; set; }

        /// <summary>
        /// show headerimage
        /// </summary>
        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty(PropertyName = "show_header_image")]
        public bool ShowHeaderImage { get; set; }

        /// <summary>
        /// show title
        /// </summary>
        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty(PropertyName = "show_title")]
        public bool ShowTitle { get; set; }

#if NETSTANDARD2_0
        /// <summary>
        /// color of the title
        /// </summary>
        [JsonProperty(PropertyName = "title_color")]
        public Color TitleColor { get; set; }
#else
        /// <summary>
        /// color of the title
        /// </summary>
        [JsonProperty(PropertyName = "title_color")]
        public string TitleColor { get; set; }
#endif

        /// <summary>
        /// font of the title
        /// </summary>
        [JsonProperty(PropertyName = "title_font")]
        public string TitleFont { get; set; }

        /// <summary>
        /// weight of the font from title
        /// </summary>
        [JsonProperty(PropertyName = "title_font_weight")]
        public string TitleFontWeight { get; set; }
    }
}