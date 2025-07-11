﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace The_Boss_Project
{
    internal class FallingObjects
    {
        protected Texture2D _objectTexture;
        protected float _objectX, _objectY;
        protected Random _rng;
        protected float _objectRotation;
        private float _objectRotationAmount;
        private bool _rotateLeft;
        protected float _objectSpeed;
        

        public FallingObjects(float objectX, float objectY, Texture2D objectTexture)
        {
            _objectX = objectX;
            _objectY = objectY;
            _objectTexture = objectTexture;
            _rng = new Random();
            _objectRotation = _rng.Next(0, 101) / 100f;
            _objectRotationAmount = (_rng.Next(1, 1000) / 10000f);
            _objectSpeed = 2f;

            if (_rng.Next(1, 101) < 50)
                _rotateLeft = true;
        }

        //Get the Y (for objects to despawn at a certain y)
        public float GetY() 
        { 
            return _objectY; 
        }

        //Falling object hitbox
        public Rectangle GetBounds() 
        { 
            return new Rectangle((int)_objectX - (int)(_objectTexture.Width) / 2, (int)_objectY - (int)(_objectTexture.Height) / 2, (int)(_objectTexture.Width), (int)(_objectTexture.Height)); 
        }

        public virtual void Interacted()
        {
            //Will be used later
        }

        //Make the object update itself
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
            _objectY += _objectSpeed;
        }

        //Make the object draw itself
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_objectTexture,
                     new Vector2(_objectX, _objectY),    //set the object position
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
