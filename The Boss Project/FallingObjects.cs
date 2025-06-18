using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Media;

namespace The_Boss_Project
{
    internal class FallingObjects
    {
        private Texture2D _objectTexture;
        private float _objectX, _objectY;
        private Random _rng;
        private float _objectRotation;
        private float _objectRotationAmount;
        private bool _rotateLeft;
        private bool _isDestroyed;

        public FallingObjects(float objectX, float objectY, Texture2D objectTexture)
        {
            _objectX = objectX;
            _objectY = objectY;
            _objectTexture = objectTexture;
            _rng = new Random();
            _objectRotation = _rng.Next(0, 101) / 100f;
            _objectRotationAmount = (_rng.Next(1, 1000) / 10000f);
            _isDestroyed = false;

            if (_rng.Next(1, 101) < 50)
                _rotateLeft = true;
        }

        public float GetY() 
        { 
            return _objectY; 
        }

        public bool IsDestroyed() 
        { 
            return _isDestroyed; 
        }
        public void Update()
        {
            if (_rotateLeft)
            {
                _objectRotation -= _objectRotationAmount;
            }
            else
            {
                _objectRotation += _objectRotationAmount;
            }
            _objectY++;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_objectTexture,
                     new Vector2(_objectX, _objectY),    //set the star position
                     null,                                   //ignore this
                     Color.White * 1,         //set colour and transparency
                     _objectRotation,                          //set rotation
                     new Vector2(_objectTexture.Width / 2, _objectTexture.Height / 2), //ignore this
                     new Vector2(1, 1),    //set scale (same number 2x)
                     SpriteEffects.None,                     //ignore this
                     0f);
        }
    }
}
