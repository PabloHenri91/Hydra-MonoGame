﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Hydra
{
    public class ActionSetTexture : SKAction
    {
        Texture2D texture2D;
        bool resize;

        public ActionSetTexture(Texture2D texture2D, bool resize)
        {
            this.texture2D = texture2D;
            this.resize = resize;
        }

		internal override SKAction copy()
		{
            return new ActionSetTexture(texture2D, resize);
		}

		internal override void runOnNode(SKNode node)
		{
            if (resize)
            {
                SKSpriteNode spriteNode = (SKSpriteNode)node;
                //spriteNode.size = new Vector2(texture2D.Width, texture2D.Height);
            }
        }

		internal override void evaluateWithNode(SKNode node, float dt)
        {
            if (elapsed + dt > duration)
            {
                dt = duration - elapsed;
            }

            elapsed += dt;

            SKSpriteNode spriteNode = (SKSpriteNode)node;
            spriteNode.texture2D = texture2D;
        }
	}
}
