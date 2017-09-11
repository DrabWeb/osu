﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Linq;
using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Beatmaps;
using osu.Game.Graphics;
using osu.Game.Graphics.Containers;
using osu.Game.Overlays.OnlineBeatmapSet;

namespace osu.Game.Overlays
{
    public class OnlineBeatmapSetOverlay : WaveOverlayContainer
    {
        public const float X_PADDING = 40;
        public const float RIGHT_WIDTH = 275;

        private readonly ReverseChildIDFillFlowContainer<Drawable> scrollContent;

        public OnlineBeatmapSetOverlay()
        {
            FirstWaveColour = OsuColour.Gray(0.4f);
            SecondWaveColour = OsuColour.Gray(0.3f);
            ThirdWaveColour = OsuColour.Gray(0.2f);
            FourthWaveColour = OsuColour.Gray(0.1f);

            Anchor = Anchor.TopCentre;
            Origin = Anchor.TopCentre;
            RelativeSizeAxes = Axes.Both;
            Width = 0.85f;

            Masking = true;
            EdgeEffect = new EdgeEffectParameters
            {
                Colour = Color4.Black.Opacity(0),
                Type = EdgeEffectType.Shadow,
                Radius = 3,
                Offset = new Vector2(0f, 1f),
            };

            Child = new ScrollContainer
            {
                RelativeSizeAxes = Axes.Both,
                ScrollbarVisible = false,
                Child = scrollContent = new ReverseChildIDFillFlowContainer<Drawable>
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Direction = FillDirection.Vertical,
                },
            };
        }

        protected override void PopIn()
        {
            base.PopIn();
            FadeEdgeEffectTo(0.25f, APPEAR_DURATION, Easing.In);
        }

        protected override void PopOut()
        {
            base.PopOut();
            FadeEdgeEffectTo(0, DISAPPEAR_DURATION, Easing.Out);
        }

        public void ShowBeatmapSet(BeatmapSetInfo set)
        {
            Header header;
            Info info;
            scrollContent.Children = new Drawable[]
            {
                header = new Header(set),
                info = new Info(set),
            };

            header.Picker.Beatmap.ValueChanged += b => info.Beatmap = b;

            Show();
        }
    }
}