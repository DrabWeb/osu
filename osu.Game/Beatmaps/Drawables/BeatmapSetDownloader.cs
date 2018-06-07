﻿// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;

namespace osu.Game.Beatmaps.Drawables
{
    public class BeatmapSetDownloader : Component
    {
        private readonly BeatmapSetInfo set;
        private readonly bool noVideo;

        private BeatmapManager beatmaps;

        public readonly BindableBool Downloaded = new BindableBool();

        public BeatmapSetDownloader(BeatmapSetInfo set, bool noVideo = false)
        {
            this.set = set;
            this.noVideo = noVideo;
        }

        [BackgroundDependencyLoader]
        private void load(BeatmapManager beatmaps)
        {
            this.beatmaps = beatmaps;

            beatmaps.ItemAdded += setAdded;
            beatmaps.ItemRemoved += setRemoved;

            // initial value
            if (set.OnlineBeatmapSetID != null)
                Downloaded.Value = beatmaps.QueryBeatmapSets(s => s.OnlineBeatmapSetID == set.OnlineBeatmapSetID && !s.DeletePending).Count() != 0;
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            if (beatmaps != null)
            {
                beatmaps.ItemAdded -= setAdded;
                beatmaps.ItemRemoved -= setRemoved;
            }
        }

        public bool Download()
        {
            if (Downloaded.Value)
                return false;

            if (beatmaps.GetExistingDownload(set) != null)
                return false;

            beatmaps.Download(set, noVideo);
            return true;
        }

        private void setAdded(BeatmapSetInfo s)
        {
            if (s.OnlineBeatmapSetID == set.OnlineBeatmapSetID)
                Downloaded.Value = true;
        }

        private void setRemoved(BeatmapSetInfo s)
        {
            if (s.OnlineBeatmapSetID == set.OnlineBeatmapSetID)
                Downloaded.Value = false;
        }
    }
}