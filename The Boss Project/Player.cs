using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Media;

namespace The_Boss_Project
{
    internal class Player
    {
        private Texture2D _playerTexture;
        private float _playerX, _playerY, _playerSpeed;
        private int _playerLives;
        private bool _playerIsDead;

        public Player(Texture2D playerTexture)
        {
            _playerTexture = playerTexture;
            _playerX = 370;
            _playerY = 380;
            _playerSpeed = 500;
            _playerLives = 3;
            _playerIsDead = false;
        }
        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_playerTexture, new Vector2(_playerX,_playerY), Color.White);
        }

    }
}
