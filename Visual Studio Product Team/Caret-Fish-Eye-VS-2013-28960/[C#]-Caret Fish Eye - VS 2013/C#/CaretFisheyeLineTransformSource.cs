//***************************************************************************
//
//    Copyright (c) Microsoft Corporation. All rights reserved.
//    This code is licensed under the Visual Studio SDK license terms.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//***************************************************************************

﻿namespace CaretFisheye
{
    using System;
    using Microsoft.VisualStudio.Text;
    using Microsoft.VisualStudio.Text.Editor;
    using Microsoft.VisualStudio.Text.Formatting;

    public class CaretFisheyeLineTransformSource : ILineTransformSource
    {
        #region private members
        IWpfTextView _textView;

        private CaretFisheyeLineTransformSource(IWpfTextView textView)
        {
            _textView = textView;

            //Sync to changing the caret position.
            _textView.Caret.PositionChanged += OnCaretChanged;
        }

        private void OnCaretChanged(object sender, CaretPositionChangedEventArgs args)
        {
            //Did the caret line number change?
            SnapshotPoint oldPosition = args.OldPosition.BufferPosition;
            SnapshotPoint newPosition = args.NewPosition.BufferPosition;

            if (_textView.TextSnapshot.GetLineNumberFromPosition(newPosition) != _textView.TextSnapshot.GetLineNumberFromPosition(oldPosition))
            {
                //Yes. Is the caret on a line that has been formatted by the view?
                ITextViewLine line = _textView.Caret.ContainingTextViewLine;
                if (line.VisibilityState != VisibilityState.Unattached)
                {
                    //Yes. Force the view to redraw so that (top of) the caret line has exactly the same position.
                    _textView.DisplayTextLineContainingBufferPosition(line.Start, line.Top, ViewRelativePosition.Top);
                }
            }
        }
        #endregion

        /// <summary>
        /// Static class factory that ensures a single instance of the line transform source/view.
        /// </summary>
        public static CaretFisheyeLineTransformSource Create(IWpfTextView view)
        {
            return view.Properties.GetOrCreateSingletonProperty<CaretFisheyeLineTransformSource>(delegate { return new CaretFisheyeLineTransformSource(view); });
        }

        #region ILineTransformSource Members
        public LineTransform GetLineTransform(ITextViewLine line, double yPosition, ViewRelativePosition placement)
        {
            //Vertically compress lines that are far from the caret (based on buffer lines, not view lines).
            int caretLineNumber = _textView.TextSnapshot.GetLineNumberFromPosition(_textView.Caret.Position.BufferPosition);
            int lineNumber = _textView.TextSnapshot.GetLineNumberFromPosition(line.Start);
            int delta = Math.Abs(caretLineNumber - lineNumber);

            double scale;
            if (delta <= 3)
                scale = 1.0;
            else if (delta <= 8)
                scale = 1.0 - ((double)(delta - 3)) * 0.05;
            else if (delta <= 18)
                scale = 0.75 - ((double)(delta - 8)) * 0.025;
            else
                scale = 0.5;

            return new LineTransform(0.0, 0.0, scale);
        }
        #endregion
    }
}
