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


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //Finish setting up random number generator
            _rng = new Random();
            
            //For falling objects
            _fallingObjects = new List<FallingObjects>();
            _numFallingObjects = 5;

            //For player
            _playerLives = 3;
            _playerScore = 0;

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
                    _fallingObjects.Add(new Candy(_rng.Next(0, 801), (_rng.Next(-200, -90)), Content.Load<Texture2D>("Candy"), Content.Load<SoundEffect>("630502__jimbo555__soap-dispenser")));
                }
                else if (odds == 2)
                {
                    _fallingObjects.Add(new Axe(_rng.Next(0, 801), (_rng.Next(-200, -90)), Content.Load<Texture2D>("Toothy Hammer"), Content.Load<SoundEffect>("978__rhumphries__rbh-glass_break-02")));
                }
            }

            //For the player
            _player = new Player(Content.Load<Texture2D>("DrKB_Front"));

            //For the game font
            _GameFont = Content.Load<SpriteFont>("GameFont");

            //For the song
            //Songs and sounds are from https://freesound.org/browse/, I originally used them for Game Engine
            _Music = Content.Load<Song>("650965__betabeats__beat-tune-abysses");
            MediaPlayer.Play(_Music);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

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
                    _fallingObjects.RemoveAt(i);
                   }
                   else if (_fallingObjects[i].GetBounds().Intersects(_player.GetBounds()))
                   {
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
                        _fallingObjects.Add(new Candy(_rng.Next(0, 801), (_rng.Next(-200, -90)), Content.Load<Texture2D>("Candy"), Content.Load<SoundEffect>("630502__jimbo555__soap-dispenser")));
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

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            //Each falling object draws itself!
            foreach (FallingObjects o in _fallingObjects)
            {
                o.Draw(_spriteBatch);
            }

            //The player will draw itself
            _player.Draw(_spriteBatch);

            //Document lives and score
            _spriteBatch.DrawString(_GameFont, "Lives: " + _playerLives, new Vector2(5, 0), Color.White);
            _spriteBatch.DrawString(_GameFont, "Candies: " + _playerScore, new Vector2(650, 0), Color.White);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
