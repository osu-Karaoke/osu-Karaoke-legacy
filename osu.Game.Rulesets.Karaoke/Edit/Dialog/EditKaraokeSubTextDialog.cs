﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE
using osu.Game.Rulesets.Karaoke.Objects;


namespace osu.Game.Rulesets.Karaoke.Edit.Dialog
{
    /// <summary>
    /// right click karaoke and select add/Edit subText object, will pop-up this dialog
    /// show a textbox and Add/Edit Button
    /// </summary>
    public class EditKaraokeSubTextDialog : DialogContainer
    {
        public override string Title => "SubText";

        protected KaraokeObject KaraokeObject{get;set;}
        
        public EditKaraokeSubTextDialog(KaraokeObject karaokeObject)
        {
            KaraokeObject = karaokeObject;
            Width =200;
            Height=100;
        }
    }
}