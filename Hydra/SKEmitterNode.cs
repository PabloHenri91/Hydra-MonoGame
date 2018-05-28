﻿using System;
using System.Collections.Generic;
using System.Text;

using FarseerPhysics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hydra
{
    public class SKEmitterNode : SKSpriteNode
    {
        List<Particle> particles = new List<Particle>();

        //Determining When Particles Are Created
        internal float particleBirthRate; // The rate at which new particles are created.
        internal int numParticlesToEmit = -1; // The number of particles the emitter should emit before stopping.
        //ParticleRenderOrder particleRenderOrder; // The order in which the emitter’s particles are rendered.

        // Defining Which Node Emits Particles
        internal SKNode targetNode; // The target node which renders the emitter’s particles.

        // Determining a Particle Lifetime
        internal float particleLifetime; // The average lifetime of a particle, in seconds.
        internal float particleLifetimeRange; // The range of allowed random values for a particle’s lifetime.

        // Determining a Particle’s Initial Position
        internal Vector2 particlePosition; // The average starting position for a particle.
        internal Vector2 particlePositionRange; // The range of allowed random values for a particle’s position.
        internal float particleZPosition; // The average starting depth of a particle.

        // Determining a Particle’s Velocity and Acceleration
        internal float particleSpeed; // The average initial speed of a new particle in points per second.
        internal float particleSpeedRange; // The range of allowed random values for a particle’s initial speed.
        internal float emissionAngle; // The average initial direction of a particle, expressed as an angle in radians.
        internal float emissionAngleRange; // The range of allowed random values for a particle’s initial direction, expressed as an angle in radians.
        internal float xAcceleration; // The acceleration to apply to a particle’s horizontal velocity.
        internal float yAcceleration; // The acceleration to apply to a particle’s vertical velocity.

        // Determining a Particle’s Rotation
        internal float particleRotation; //The average initial rotation of a particle, expressed as an angle in radians.
        internal float particleRotationRange; // The range of allowed random values for a particle’s initial rotation, expressed as an angle in radians.
        internal float particleRotationSpeed; // The speed at which a particle rotates, expressed in radians per second.

        // Determining a Particle’s Scale Factor
        //particleScaleSequence: KeyframeSequence? // The sequence used to specify the scale factor of a particle over its lifetime.
        internal float particleScale = 1; // The average initial scale factor of a particle.
        internal float particleScaleRange; // The range of allowed random values for a particle’s initial scale.
        internal float particleScaleSpeed; // The rate at which a particle’s scale factor changes per second.

        // Setting a Particle’s Texture and Size
        //particleTexture: Texture? // The texture to use to render a particle.
        //particleSize: CGSize // The starting size of each particle.

        // Configuring Particle Color
        //particleColorSequence: KeyframeSequence? // The sequence used to specify the color components of a particle over its lifetime.
        //particleColor: UIColor // The average initial color for a particle.
        internal float particleColorAlphaRange; // The range of allowed random values for the alpha component of a particle’s initial color.
        internal float particleColorBlueRange; // The range of allowed random values for the blue component of a particle’s initial color.
        internal float particleColorGreenRange; // The range of allowed random values for the green component of a particle’s initial color.
        internal float particleColorRedRange; // The range of allowed random values for the red component of a particle’s initial color.
        internal float particleColorAlphaSpeed; // The rate at which the alpha component of a particle’s color changes per second.
        internal float particleColorBlueSpeed; // The rate at which the blue component of a particle’s color changes per second.
        internal float particleColorGreenSpeed; // The rate at which the green component of a particle’s color changes per second.
        internal float particleColorRedSpeed; // The rate at which the red component of a particle’s color changes per second.

        // Determining How the Particle Texture Is Blended with the Particle Color
        //particleColorBlendFactorSequence: KeyframeSequence? // The sequence used to specify the color blend factor of a particle over its lifetime.
        internal float particleColorBlendFactor; // The average starting value for the color blend factor.
        internal float particleColorBlendFactorRange; // The range of allowed random values for a particle’s starting color blend factor.
        internal float particleColorBlendFactorSpeed; // The rate at which the color blend factor changes per second.

        // Blending Particles with the Framebuffer
        //particleBlendMode: BlendMode // The blending mode used to blend particles into the framebuffer.
        //particleAlphaSequence: KeyframeSequence? // The sequence used to specify the alpha value of a particle over its lifetime.
        internal float particleAlpha = 1; // The average starting alpha value for a particle.
        internal float particleAlphaRange; // The range of allowed random values for a particle’s starting alpha value.
        internal float particleAlphaSpeed; // The rate at which the alpha value of a particle changes per second.

        // Adding an Action to Particles
        //particleAction: Action? // Specifies an action executed by new particles.

        // Applying Physics Fields to the Particles
        //fieldBitMask: UInt32 //A mask that defines which categories of physics fields can exert forces on the particles.

        float particleCounter;

        public SKEmitterNode() : base("spark")
        {
            SKScene.current.emitterNodeList.Add(this);
        }

        internal void update(float currentTime, float elapsedTime)
        {
            if (numParticlesToEmit != 0)
            {
                particleCounter += particleBirthRate * elapsedTime;

                while ((int)particleCounter > 0 && numParticlesToEmit > 0)
                {
                    numParticlesToEmit--;
                    particleCounter--;

                    Particle particle = new Particle();
                    particle.birthTime = currentTime;

                    particle.position = position;

                    float randomAngle = (float)(random.NextDouble() * emissionAngleRange);
                    float randomSpeed = (float)(random.NextDouble() * particleSpeedRange);
                    particle.speedX = (float)(Math.Sin(emissionAngle - emissionAngleRange / 2 + randomAngle) * (particleSpeed - particleSpeedRange / 2 + randomSpeed));
                    particle.speedY = (float)(-Math.Cos(emissionAngle - emissionAngleRange / 2 + randomAngle) * (particleSpeed - particleSpeedRange / 2 + randomSpeed));

                    float randomAlpha = (float)(random.NextDouble() * particleAlphaRange);
                    particle.alpha = particleAlpha - particleAlphaRange / 2 + randomAlpha;

                    float randomScale = (float)(random.NextDouble() * particleScaleRange);
                    particle.scale = particleScale - particleScaleRange / 2 + randomScale;

                    float randomLifeTime = (float)(random.NextDouble() * particleLifetimeRange);
                    particle.lifetime = particleLifetime - particleLifetimeRange / 2 + randomLifeTime;

                    particles.Add(particle);
                }
            }

            for (int i = particles.Count - 1; i >= 0; i--)
            {
                var particle = particles[i];

                if (currentTime - particle.birthTime > particle.lifetime)
                {
                    particles.RemoveAt(i);
                }
                else
                {
                    particle.update(elapsedTime, xAcceleration, yAcceleration, particleAlphaSpeed, particleScaleSpeed);
                }
            }
        }

		internal override void draw(Vector2 currentPosition, float currentAlpha, Vector2 currentScale)
        {
            if (isHidden || currentAlpha <= 0.0f)
            {
                return;
            }

            beforeDraw();
            
            foreach (var particle in particles)
            {
				Game1.spriteBatch.Draw(texture2D, currentPosition + particle.position, sourceRectangle, color * currentAlpha * particle.alpha, zRotation, origin, currentScale * scale * particle.scale, effects, layerDepth);
            }

			drawChildren(currentPosition, currentAlpha, currentScale);
        }
    }
}
