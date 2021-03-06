﻿using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace Hydra
{
    class Label : SKLabelNode
    {
        internal static Color defaultColor = GameColors.fontWhite;
        internal static FontName defaultFontName = FontName.SpriteFont;
        internal static FontSize defaultFontSize = FontSize.size32;

        internal Vector2 sketchPosition;

        HorizontalAlignment horizontalAlignment = HorizontalAlignment.left;
        VerticalAlignment verticalAlignment = VerticalAlignment.top;

        public Label(string text, float x = 0, float y = 0,
                     HorizontalAlignment horizontalAlignment = HorizontalAlignment.left,
                     VerticalAlignment verticalAlignment = VerticalAlignment.top,
                     FontName fontName = FontName.Default,
                     FontSize fontSize = FontSize.Default) : base((fontName == FontName.Default ?
                                                                   defaultFontName :
                                                                   fontName).ToString() +
                                                                  (fontSize == FontSize.Default ?
                                                                   defaultFontSize :
                                                                   fontSize).GetHashCode(), text)
        {
            sketchPosition = new Vector2(x, y);
            setAlignment(horizontalAlignment, verticalAlignment);
            color = defaultColor;
            SKScene.current.labelList.Add(this);
        }

        internal void resetPosition()
        {
            position = Control.positionWith(sketchPosition, horizontalAlignment, verticalAlignment);
        }

        internal void setAlignment(HorizontalAlignment someHorizontalAlignment, VerticalAlignment someVerticalAlignment)
        {
            horizontalAlignment = someHorizontalAlignment;
            verticalAlignment = someVerticalAlignment;
            resetPosition();
        }
    }
}
