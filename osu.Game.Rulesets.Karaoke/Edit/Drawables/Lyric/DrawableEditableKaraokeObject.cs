﻿// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Linq;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input;
using osu.Game.Graphics.UserInterface;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Edit.Drawables.Pieces;
using osu.Game.Rulesets.Karaoke.Edit.Drawables.Thumbnail;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Lyric;
using osu.Game.Rulesets.Karaoke.Objects.Text;
using osu.Game.Rulesets.Karaoke.Objects.TimeLine;
using osu.Game.Rulesets.Karaoke.Objects.Translate;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.Edit.Drawables.Lyric
{
    /// <summary>
    ///     Editable karaoke Drawable Object
    ///     Right click :
    ///     Translate >> Add
    /// </summary>
    public class DrawableEditableKaraokeObject : DrawableLyric, IHasContextMenu
    {
        public MenuItem[] ContextMenuItems => new MenuItem[]
        {
            new OsuMenuItem(@"Some option"),
            new OsuMenuItem(@"Highlighted option", MenuItemType.Highlighted),
            new OsuMenuItem(@"Another option"),
            new OsuMenuItem(@"Choose me please"),
            new OsuMenuItem(@"And me too"),
            new OsuMenuItem(@"Trying to fill"),
            new OsuMenuItem(@"Destructive option", MenuItemType.Destructive)
        };

        protected bool IsDrag;
        protected DrawableKaraokeThumbnail DrawableKaraokeThumbnail { get; set; }
        protected EditableLyricText EditableLyricText { get; set; } = new EditableLyricText(null, null);

        public DrawableEditableKaraokeObject(BaseLyric hitObject)
            : base(hitObject)
        {
            DrawableKaraokeThumbnail = new DrawableKaraokeThumbnail(Lyric)
            {
                Position = new Vector2(0, -100),
                Width = 300,
                Height = 100
            };
            AddInternal(EditableLyricText);
            AddInternal(DrawableKaraokeThumbnail);
        }

        public void AddPoint(TimeLineIndex index)
        {
            var previousPoint = Lyric.TimeLines.GetFirstProgressPointByIndex(index);
            var nextPoint = Lyric.TimeLines.GetLastProgressPointByIndex(index);
            var deltaTime = ((previousPoint.Value?.RelativeTime ?? 0) + (nextPoint.Value?.RelativeTime ?? (previousPoint.Value?.RelativeTime ?? 0) + 500)) / 2;
            var point = new TimeLine(deltaTime);
            Lyric.TimeLines.Add(index, point);
            DrawableKaraokeThumbnail.UpdateView();
        }

        public void AddTranslate(TranslateCode code, string translateResult)
        {
            //Add it into Karaoke object
            Lyric.Translates.Add(code, new LyricTranslate(translateResult));
        }

        protected override void UpdateDrawable()
        {
            base.UpdateDrawable();
            EditableLyricText.MainTextObject = Lyric.Lyric.ToDictionary(k => k.Key, v => (TextComponent)v.Value);
            EditableLyricText.TextObject = Template?.Value?.MainText;
            EditableLyricText.Alpha = 1f;
        }

        #region Input

        protected override bool OnMouseDown(InputState state, MouseDownEventArgs args)
        {
            IsDrag = true;
            var index = GetPointedText(state);
            EditableLyricText.StartSelectIndex = index;

            return base.OnMouseDown(state, args);
        }

        //detect whith text is picked
        protected override bool OnMouseMove(InputState state)
        {
            if (!IsDrag)
            {
                var index = GetPointedText(state);
                EditableLyricText.HoverIndex = index;
            }
            else
            {
                var index = GetPointedText(state);
                EditableLyricText.EndSelectIndex = index;
            }

            return base.OnMouseMove(state);
        }

        protected override bool OnMouseUp(InputState state, MouseUpEventArgs args)
        {
            IsDrag = false;
            var index = new TimeLineIndex(GetPointedText(state));
            AddPoint(index);
            EditableLyricText.StartSelectIndex = null;
            EditableLyricText.EndSelectIndex = null;

            return base.OnMouseUp(state, args);
        }

        protected override void OnHoverLost(InputState state)
        {
            base.OnHoverLost(state);
            EditableLyricText.HoverIndex = null;
        }

        protected int GetPointedText(InputState state)
        {
            var mousePosition = ToLocalSpace(state.Mouse.NativeState.Position);
            return EditableLyricText.GetIndexByPosition(mousePosition.X);
        }

        #endregion
    }
}