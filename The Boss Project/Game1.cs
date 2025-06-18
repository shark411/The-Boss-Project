using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Formats.Asn1.AsnWriter;

namespace The_Boss_Project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Random _rng;
        private List<FallingObjects> _fallingObjects;
        private int _numFallingObjects;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _rng = new Random();
            _numFallingObjects = 1;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _fallingObjects = new List<FallingObjects>();
            
            for (int i = 0; i < _numFallingObjects; i++)
            {
                int odds = _rng.Next(1, 3);
                if (odds == 1)
                {
                    _fallingObjects.Add(new Candy(_rng.Next(0, 801), (_rng.Next(-200, -90)), Content.Load<Texture2D>("Candy")));
                }
                else if (odds == 2)
                {
                    _fallingObjects.Add(new Axe(_rng.Next(0, 801), (_rng.Next(-200, -90)), Content.Load<Texture2D>("Toothy Hammer")));
                }


            }

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            foreach (FallingObjects o in _fallingObjects)
            {
                o.Update();
            }

            for (int i = 0; i < _fallingObjects.Count; i++)
            {
                if (_fallingObjects[i].IsDestroyed() || _fallingObjects[i].GetY() > 500)
                {
                    _fallingObjects.RemoveAt(i);
                }

               
            }

            if (_fallingObjects.Count <= 0)
            {
                _numFallingObjects++;
                for (int i = 0; i < _numFallingObjects; i++)
                {
                    int odds = _rng.Next(1, 3);
                    if (odds == 1)
                    {
                        _fallingObjects.Add(new Candy(_rng.Next(0, 801), (_rng.Next(-200, -90)), Content.Load<Texture2D>("Candy")));
                    }
                    else if (odds == 2)
                    {
                        _fallingObjects.Add(new Axe(_rng.Next(0, 801), (_rng.Next(-200, -90)), Content.Load<Texture2D>("Toothy Hammer")));
                    }
                }

            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            foreach (FallingObjects o in _fallingObjects)
            {
                o.Draw(_spriteBatch);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
