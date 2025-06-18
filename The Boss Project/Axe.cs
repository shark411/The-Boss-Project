using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Media;

namespace The_Boss_Project
{
    internal class Axe : FallingObjects
    {
        public Axe(float x, float y, Texture2D axeTexture) : base(x, y, axeTexture)
        {
            _objectSpeed = _rng.Next(2, 6);
        }
    }
}
