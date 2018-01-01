﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using System.Linq;
using osu.Framework.Input;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Karaoke.Edit.Dialog;
using osu.Game.Rulesets.Karaoke.Edit.Drawables;
using osu.Game.Rulesets.Karaoke.UI;
using osu.Game.Rulesets.Objects.Drawables;
using OpenTK.Input;

namespace osu.Game.Rulesets.Karaoke.Edit
{
    public class KaraokeEditPlayfield : KaraokeBasePlayfield
    {
        /// <summary>
        /// Selected karaoke Object
        /// </summary>
        public DrawableEditableKaraokeObject NowSelectedKaraokeObject { get; set; }


        public KaraokeEditPlayfield(Ruleset ruleset, WorkingBeatmap beatmap, KaraokeEditRulesetContainer container)
            : base(ruleset, beatmap, container)
        {
        }

        /// <summary>
        /// Add : Add to editList
        /// 
        /// </summary>
        /// <param name="drawable"></param>
        public override void Add(DrawableHitObject h)
        {
            if (h is DrawableEditableKaraokeObject drawableEditableKaraokeObject)
            {
            }

            base.Add(h);
        }

        /// <summary>
        /// using hotkay to open dialog
        /// </summary>
        /// <param name="state"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        protected override bool OnKeyDown(InputState state, KeyDownEventArgs args)
        {
            foreach (var single in state.Keyboard.Keys)
            {
                if (single == Key.L)
                {
                    //Open Lyrics dialog
                    OpenListKaraokeLyricsDialog();
                    break;
                }
                else if (single == Key.T)
                {
                    //Open Translate dialog
                    OpenListKaraokeTranslateDialog();
                    break;
                }
            }
            return base.OnKeyDown(state, args);
        }

        #region Dialog

        public void OpenListKaraokeLyricsDialog()
        {
            if (!dialogLayer.Children.OfType<ListKaraokeLyricsDialog>().Any())
            {
                dialogLayer.Add(new ListKaraokeLyricsDialog(this));
            }
        }

        public void OpenListKaraokeTranslateDialog()
        {
            if (!dialogLayer.Children.OfType<ListKaraokeLyricsDialog>().Any())
            {
                dialogLayer.Add(new ListKaraokeTranslateDialog(this));
            }
        }

        #endregion
    }
}