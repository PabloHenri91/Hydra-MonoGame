﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Design;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Hydra
{
    class ActionScaleTo : ActionScaleBy
    {
        public ActionScaleTo(Vector2 scale, float duration) : base(scale, duration)
        {
        }

        internal override Action copy()
        {
            return new ActionScaleTo(scale, duration);
        }

        internal override void runOnNode(Node node)
        {
            SpriteNode spriteNode = (SpriteNode)node;
            speed = (scale - spriteNode.scale) / duration;
        }
    }
}
