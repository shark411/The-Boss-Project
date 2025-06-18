using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using static System.Formats.Asn1.AsnWriter;

namespace The_Boss_Project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //Random Number Generator
        private Random _rng;

        //Falling Objects!
        private List<FallingObjects> _fallingObjects;
        private int _numFallingObjects;

        //Player
        private Player _player;
        private int _playerLives, _playerScore;

        //Font
        private SpriteFont _GameFont;

        //For the music
        private Song _Music;
        private SoundEffect _gameOverSFX;
        private bool _isPlayingSound;

        //Bounding box
        private Texture2D _boundingBoxTexture;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //Finish setting up random number generator
            _rng = new Random();
            
            //For falling objects
            _fallingObjects = new List<FallingObjects>();
            _numFallingObjects = 5;

            //For player
            _playerLives = 3;
            _playerScore = 0;

            //Bounding box
            _boundingBoxTexture = new Texture2D(GraphicsDevice, 1, 1);
            _boundingBoxTexture.SetData(new Color[] { Color.White });

            //For music
            _isPlayingSound = false;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
           //For falling objects
            for (int i = 0; i < _numFallingObjects; i++)
            {
                int odds = _rng.Next(1, 3);
                if (odds == 1)
                {
                    _fallingObjects.Add(new Candy(_rng.Next(0, 801), (_rng.Next(-200, -90)), Content.Load<Texture2D>("Candy"), Content.Load<SoundEffect>("630502__jimbo555__soap-dispenser"), Content.Load<SoundEffect>("762781__sess8it__bubblepopping")));
                }
                else if (odds == 2)
                {
                    _fallingObjects.Add(new Axe(_rng.Next(0, 801), (_rng.Next(-200, -90)), Content.Load<Texture2D>("Toothy Hammer"), Content.Load<SoundEffect>("978__rhumphries__rbh-glass_break-02")));
                }
            }

            //For the player
            _player = new Player(Content.Load<Texture2D>("DrKB_Front"), (Content.Load<Texture2D>("DrKB_ProfileL")), (Content.Load<Texture2D>("DrKB_ProfileR")));

            //For the game font
            _GameFont = Content.Load<SpriteFont>("GameFont");

            //For the song
            //Songs and sounds are from https://freesound.org/browse/, I originally used them for Game Engine
            _Music = Content.Load<Song>("650965__betabeats__beat-tune-abysses");
            MediaPlayer.Play(_Music);
            _gameOverSFX = Content.Load<SoundEffect>("169317__scorpion67890__demonic-laugh");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            //While player lives, play the game
            if (_playerLives > 0)
            {
                //Each falling object updates itself
                foreach (FallingObjects o in _fallingObjects)
                {
                    o.Update();
                }

                //Destroy falling objects if they need to be destroyed
                for (int i = 0; i < _fallingObjects.Count; i++)
                {
                  
                    if (_fallingObjects[i].GetY() > 500)
                    {
                        //If they reach past the ground, destroy them!
                        _fallingObjects.RemoveAt(i);
                    }
                    else if (_fallingObjects[i].GetBounds().Intersects(_player.GetBounds()))
                    {
                        //If they touch the player, do their thing!
                        _fallingObjects[i].Interacted();

                        if (_fallingObjects[i].GetType() == typeof(Candy))
                        {
                            ((Candy)_fallingObjects[i]).HasScored();
                            _playerScore++;
                        }

                        if (_fallingObjects[i].GetType() == typeof(Axe))
                        {
                            ((Axe)_fallingObjects[i]).HasHit();
                            _playerLives--;
                        }
                        //Then die lol
                        _fallingObjects.RemoveAt(i);
                    }
                }

                //Fill out the list once it empties
                if (_fallingObjects.Count <= 0)
                {
                    _numFallingObjects++;
                    for (int i = 0; i < _numFallingObjects; i++)
                    {
                        int odds = _rng.Next(1, 3);
                        if (odds == 1)
                        {
                            _fallingObjects.Add(new Candy(_rng.Next(0, 801), (_rng.Next(-200, -90)), Content.Load<Texture2D>("Candy"), Content.Load<SoundEffect>("630502__jimbo555__soap-dispenser"), Content.Load<SoundEffect>("762781__sess8it__bubblepopping")));
                        }
                        else if (odds == 2)
                        {
                            _fallingObjects.Add(new Axe(_rng.Next(0, 801), (_rng.Next(-200, -90)), Content.Load<Texture2D>("Toothy Hammer"), Content.Load<SoundEffect>("978__rhumphries__rbh-glass_break-02")));
                        }
                    }
                }

                //The player will update itself
                _player.Update();

                //For music to loop
                MediaPlayer.IsRepeating = true;
            }
            else //When the player dies!
            {
                MediaPlayer.Stop(); //Stop the music when you die
                _fallingObjects.Clear();
                if (_isPlayingSound == false) //So the sound only plays once
                {
                    _gameOverSFX.Play();
                }
                _isPlayingSound = true;
                //The ability to restart the game!
                if (Keyboard.GetState().IsKeyDown(Keys.R))
                {
                    Initialize();
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (_playerLives > 0) //While the player lives!
            {
                GraphicsDevice.Clear(Color.Gray);

                _spriteBatch.Begin();

                //Each falling object draws itself!
                foreach (FallingObjects o in _fallingObjects)
                {
                    o.Draw(_spriteBatch);
                }

                //The player will draw itself
                _player.Draw(_spriteBatch);

                //Bounding boxes
                //_spriteBatch.Draw(_boundingBoxTexture, _player.GetBounds(), Color.Red * 0.25f);
                // for (int i = 0; i < _fallingObjects.Count; i++)
                // _spriteBatch.Draw(_boundingBoxTexture, _fallingObjects[i].GetBounds(), Color.Red * 0.25f);

                //Document lives and score
                _spriteBatch.DrawString(_GameFont, "Lives: " + _playerLives, new Vector2(5, 0), Color.White);
                _spriteBatch.DrawString(_GameFont, "Candies: " + _playerScore, new Vector2(650, 0), Color.White);

                _spriteBatch.End();
            }
            else //While the player is dead!
            {
                GraphicsDevice.Clear(Color.Black);

                _spriteBatch.Begin();

                //Each falling object draws itself! IT SHOULD BE NONE!!!!
                foreach (FallingObjects o in _fallingObjects)
                {
                    o.Draw(_spriteBatch);
                }

                //The player will draw itself. IT SHOULD BE STATIONARY!!!
                _player.Draw(_spriteBatch);

                //Bounding boxes
                //_spriteBatch.Draw(_boundingBoxTexture, _player.GetBounds(), Color.Red * 0.25f);
                // for (int i = 0; i < _fallingObjects.Count; i++)
                // _spriteBatch.Draw(_boundingBoxTexture, _fallingObjects[i].GetBounds(), Color.Red * 0.25f);

                //Calculating the center of the screen
                int screenCenterX = _graphics.PreferredBackBufferWidth / 2;
                int screenCenterY = _graphics.PreferredBackBufferHeight / 2;

                //Tell the player how well they did
                string gameOverText = "You died. You've collected " + _playerScore + " candies!";
                int textHalfWidth = (int)_GameFont.MeasureString(gameOverText).X / 2;
                int textHalfHeight = (int)_GameFont.MeasureString(gameOverText).Y / 2;
                _spriteBatch.DrawString(_GameFont, gameOverText, new Vector2(screenCenterX - textHalfWidth, screenCenterY - textHalfHeight - 100), Color.Red);

                //Tell the player how to restart!
                string gameOverText2 = "Press R to restart.";
                int textHalfWidth2 = (int)_GameFont.MeasureString(gameOverText2).X / 2;
                int textHalfHeight2 = (int)_GameFont.MeasureString(gameOverText2).Y / 2;
                _spriteBatch.DrawString(_GameFont, gameOverText2, new Vector2(screenCenterX - textHalfWidth2, screenCenterY - textHalfHeight2 + 100), Color.Red);

                _spriteBatch.End();
            }
            base.Draw(gameTime);
        }
    }
}
