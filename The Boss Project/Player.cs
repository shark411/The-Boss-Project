using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Media;
using System.Xml.Linq;


namespace The_Boss_Project
{
    internal class Player
    {
        private Texture2D _playerTexture, _playerTextureMovingL, _playerTextureMovingR;
        private float _playerX, _playerY, _playerSpeed;
        private bool _playerFacingRight, _playerMovingL, _playerMovingR;


        public Player(Texture2D playerTexture, Texture2D playerTextureMovingL, Texture2D playerTextureMovingR)
        {
            _playerTexture = playerTexture;
            _playerTextureMovingL = playerTextureMovingL;
            _playerTextureMovingR = playerTextureMovingR;
            _playerX = 370;
            _playerY = 380;
            _playerSpeed = 7;
            _playerFacingRight = false;
            _playerMovingL = false;
            _playerMovingR = false;
        }

        //Player hitbox
        public Rectangle GetBounds() 
        {
           return new Rectangle((int)_playerX - (int)(_playerTexture.Width) / 2, (int)_playerY - (int)(_playerTexture.Height) / 2, (int)( _playerTexture.Width), (int)(_playerTexture.Height)); ; 
        }

        //Player will update itself
        public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.A) || (keyboardState.IsKeyDown(Keys.Left))) //LEFT
            {
                _playerX -= _playerSpeed;
                _playerFacingRight = false;
                _playerMovingL = true;
                _playerMovingR = false;
            }
            else if (keyboardState.IsKeyDown(Keys.D) || (keyboardState.IsKeyDown(Keys.Right))) //RIGHT
            {
                _playerX += _playerSpeed;
                _playerFacingRight = true;
                _playerMovingL = false;
                _playerMovingR = true;
            }
            else
            {
                _playerMovingL = false;
                _playerMovingR = false;
            }

            //Keep the player on the screen!
            if (_playerX < 0)
            {
                _playerX = 0;
            }
            if (_playerX > 800)
            {
                _playerX = 800;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw the PLAYER
            SpriteEffects PlayerEffects = SpriteEffects.None;
            if (!_playerFacingRight) //Face the right way!
            {
                PlayerEffects = SpriteEffects.FlipHorizontally;
            }
            if (_playerMovingL) //If the player is moving LEFT
            {
                spriteBatch.Draw(_playerTextureMovingL, new Vector2(_playerX, _playerY), null, Color.White, 0, new Vector2(_playerTexture.Width / 2, _playerTexture.Height / 2), 1f, SpriteEffects.None, 0);
            }
            else if (_playerMovingR) //If the player is moving RIGHT, yes its a different sprite than LEFT
            {
                spriteBatch.Draw(_playerTextureMovingR, new Vector2(_playerX, _playerY), null, Color.White, 0, new Vector2(_playerTexture.Width / 2, _playerTexture.Height / 2), 1f, SpriteEffects.None, 0);
            }
            else //Draw idle!
            {
                spriteBatch.Draw(_playerTexture, new Vector2(_playerX, _playerY), null, Color.White, 0, new Vector2(_playerTexture.Width / 2, _playerTexture.Height / 2), 1f, PlayerEffects, 0);
            }

        }

    }
}
