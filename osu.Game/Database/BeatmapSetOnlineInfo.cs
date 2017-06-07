﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using Newtonsoft.Json;
using System.Collections.Generic;

namespace osu.Game.Database
{
    /// <summary>
    /// Beatmap set info retrieved for previewing locally without having the set downloaded.
    /// </summary>
    public class BeatmapSetOnlineInfo
    {
        /// <summary>
        /// The different sizes of cover art for this beatmap.
        /// </summary>
        [JsonProperty(@"covers")]
        public BeatmapSetOnlineCovers Covers { get; set; }

        /// <summary>
        /// A small sample clip of this beatmap's song.
        /// </summary>
        [JsonProperty(@"previewUrl")]
        public string Preview { get; set; }

        /// <summary>
        /// The amount of plays this set has.
        /// </summary>
        [JsonProperty(@"play_count")]
        public int PlayCount { get; set; }

        /// <summary>
        /// The amount of people who have favourited this map.
        /// </summary>
        [JsonProperty(@"favourite_count")]
        public int FavouriteCount { get; set; }
    }

    public class BeatmapSetOnlineCovers
    {
        public string Cover { get; set; }

        [JsonProperty(@"cover@2x")]
        public string Cover2x { get; set; }

        public string Card { get; set; }

        [JsonProperty(@"card@2x")]
        public string Card2x { get; set; }

        public string List { get; set; }

        [JsonProperty(@"list@2x")]
        public string List2x { get; set; }
    }
}