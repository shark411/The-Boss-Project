using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Media;
using System.Xml.Linq;
using Microsoft.Xna.Framework.Audio;

namespace The_Boss_Project
{
    
    internal class Candy : FallingObjects
    {
        private Color _candyColor;
        private bool _hasScored;
        SoundEffect _candyCollectSFX;

        public Candy(float x, float y, Texture2D candyTexture, SoundEffect candyCollectSFX) : base (x, y, candyTexture)
        {
            _candyColor = new Color(128 + _rng.Next(0, 129), 128 + _rng.Next(0, 129), 128 + _rng.Next(0, 129));
            _hasScored = false;
            _candyCollectSFX = candyCollectSFX;
        }
        public override void Interacted()
        {
            _hasScored = true;
            _candyCollectSFX.Play();
        }
        public bool HasScored()
        { 
            return _hasScored; 
        }

        //Make the candy draw itself (added colour)
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_objectTexture,
                     new Vector2(_objectX, _objectY),    //set the candy position
                     null,                                   //ignore this
                     _candyColor * 1,         //set colour and transparency
                     _objectRotation,                          //set rotation
                     new Vector2(_objectTexture.Width / 2, _objectTexture.Height / 2), //ignore this
                     new Vector2(1, 1),    //set scale (same number 2x)
                     SpriteEffects.None,                     //ignore this
                     0f);
        }
    }
    
}
