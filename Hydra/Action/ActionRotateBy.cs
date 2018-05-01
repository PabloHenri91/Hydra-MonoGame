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
    class ActionRotateBy : SKAction
    {
        protected float radians;
        protected float speed;

        public ActionRotateBy(float radians, float duration)
        {
            this.radians = radians;
            this.duration = duration;
            if (this.duration <= 0)
            {
                this.duration = 0.001f;
            }
            speed = radians / duration;
        }

        internal override SKAction copy()
        {
            return new ActionRotateBy(radians, duration)
            {
                timingFunction = this.timingFunction
            };
        }

        internal override void evaluateWithNode(SKNode node, float dt)
        {
            if (elapsed + dt > duration)
            {
                dt = duration - elapsed;
            }

            elapsed += dt;

            float t1 = timingFunction(elapsed, 0, 1, 1);

            node.zRotation += speed * (t1 - t0);

            t0 = t1;
        }
    }
}
