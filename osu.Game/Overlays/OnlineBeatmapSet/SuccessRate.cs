﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;
using osu.Game.Graphics.UserInterface;
using osu.Game.Beatmaps;
using osu.Game.Screens.Select.Details;
using System;

namespace osu.Game.Overlays.OnlineBeatmapSet
{
    public class SuccessRate : Container
    {
        private readonly FillFlowContainer header;
        private readonly OsuSpriteText successRateLabel, successPercent, graphLabel;
        private readonly Bar successRate;
        private readonly Container percentContainer;
        private readonly FailRetryGraph graph;

        private BeatmapInfo beatmap;
        public BeatmapInfo Beatmap
        {
            get { return beatmap; }
            set
            {
                if (value == beatmap) return;
                beatmap = value;

                var rate = (float)beatmap.OnlineInfo.PassCount / beatmap.OnlineInfo.PlayCount;
                successPercent.Text = $"{Math.Round(rate * 100)}%";
                successRate.Length = rate;

                graph.Metrics = Beatmap.Metrics;
            }
        }

        public SuccessRate()
        {
            Children = new Drawable[]
            {
                header = new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Direction = FillDirection.Vertical,
                    Children = new Drawable[]
                    {
                        successRateLabel = new OsuSpriteText
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            Text = "Success Rate",
                            TextSize = 13,
                        },
                        successRate = new Bar
                        {
                            RelativeSizeAxes = Axes.X,
                            Height = 5,
                            Margin = new MarginPadding { Top = 5 },
                        },
                        percentContainer = new Container
                        {
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y,
                            Child = successPercent = new OsuSpriteText
                            {
                                Anchor = Anchor.TopRight,
                                Origin = Anchor.TopCentre,
                                TextSize = 13,
                            },
                        },
                        graphLabel = new OsuSpriteText
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            Text = "Points of Failure",
                            TextSize = 13,
                            Margin = new MarginPadding { Vertical = 20 },
                        },
                    },
                },
                graph = new FailRetryGraph
                {
                    Anchor = Anchor.BottomLeft,
                    Origin = Anchor.BottomLeft,
                    RelativeSizeAxes = Axes.Both,
                },
            };
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            successRateLabel.Colour = successPercent.Colour = graphLabel.Colour = colours.Gray5;
            successRate.AccentColour = colours.Green;
            successRate.BackgroundColour = colours.GrayD;
        }

        protected override void UpdateAfterChildren()
        {
            base.UpdateAfterChildren();

            graph.Padding = new MarginPadding { Top = header.DrawHeight };
        }

        protected override void Update()
        {
            base.Update();

            percentContainer.Width = successRate.Length;
        }
    }
}