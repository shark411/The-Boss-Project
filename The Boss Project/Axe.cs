using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace The_Boss_Project
{
    internal class Axe : FallingObjects
    {
        private bool _hasHit;
        SoundEffect _glassBreakSFX;

        public Axe(float x, float y, Texture2D axeTexture, SoundEffect glassBreakSFX) : base(x, y, axeTexture)
        {
            _objectSpeed = _rng.Next(2, 6);
            _hasHit = false;
            _glassBreakSFX = glassBreakSFX;
        }

        public override void Interacted()
        {
            _hasHit = true;
            _glassBreakSFX.Play();
        }

        public bool HasHit()
        {
            return _hasHit;
        }
    }
}
