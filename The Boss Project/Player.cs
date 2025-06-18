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
        private Texture2D _playerTexture;
        private float _playerX, _playerY, _playerSpeed;
        private int _playerLives;
        private bool _playerIsDead, _playerMovingRight;


        public Player(Texture2D playerTexture)
        {
            _playerTexture = playerTexture;
            _playerX = 370;
            _playerY = 380;
            _playerSpeed = 5;
            _playerLives = 3;
            _playerIsDead = false;
            _playerMovingRight = false;
        }

        //Player hitbox
        public Rectangle GetBounds() 
        {
            return new Rectangle((int)_playerX, (int)_playerY, _playerTexture.Width, _playerTexture.Height); 
        }

        public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.A) || (keyboardState.IsKeyDown(Keys.Left))) //LEFT
            {
                _playerX -= _playerSpeed;
                _playerMovingRight = false;
            }
            if (keyboardState.IsKeyDown(Keys.D) || (keyboardState.IsKeyDown(Keys.Right))) //RIGHT
            {
                _playerX += _playerSpeed;
                _playerMovingRight = true;
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
            if (!_playerMovingRight) //Face the right way!
            {
                PlayerEffects = SpriteEffects.FlipHorizontally;
            }
            spriteBatch.Draw(_playerTexture, new Vector2(_playerX, _playerY), null, Color.White, 0, new Vector2(_playerTexture.Width / 2, _playerTexture.Height / 2), 1f, PlayerEffects, 0);


        }

    }
}
