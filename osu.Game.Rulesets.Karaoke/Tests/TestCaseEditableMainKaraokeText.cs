﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Game.Rulesets.Karaoke.Edit.Drawables;
using osu.Game.Rulesets.Karaoke.Helps;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Tests.Visual;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.Tests
{
    /// <summary>
    /// test
    /// </summary>
    /// [TestFixture]
    [Ignore("getting CI working")]
    [System.ComponentModel.Description("Test EditableMainKaraokeText class")]
    public class TestCaseEditableMainKaraokeText : OsuTestCase
    {
        [BackgroundDependencyLoader]
        private void load(RulesetStore rulesets)
        {
            Add(new DrawableEditableKaraokeObject(DemoKaraokeObject.GenerateWithStartAndDuration(0, 100000))
            {
                Position = new Vector2(100, 100),
                Template = new KaraokeTemplate(),
                //ProgressUpdateByTime = false,
            });
        }
    }
}