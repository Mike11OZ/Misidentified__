using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Misidentified
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        FirstPersonCamera camera;
        Player player;
        TiledLoader map;
        Texture2D tileset;
        SpriteBatch spriteBatch;
        SpriteFont debugFont;
        string debugText = "";
        BasicEffect effect;
        CubePrimitive cube;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        // ....................
        // I N I T I A L I Z E
        // ....................
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            camera = new FirstPersonCamera(GraphicsDevice);
            IsMouseVisible = false;
            player = new Player();
            player.Position = new Vector3(0, 1.8f, 0); // Start slightly above the ground
            base.Initialize();
        }


        // ........................
        // L O A D   C O N T E N T
        // ........................
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            debugFont = Content.Load<SpriteFont>("Fonts/DebugFont");
            effect = new BasicEffect(GraphicsDevice);
            effect.TextureEnabled = true;
            effect.Texture = tileset;
            effect.LightingEnabled = false;

            cube = new CubePrimitive(1f);
            map = new TiledLoader();
            map.Load("Content/Maps/map.tmx");

            // Load tileset texture (export from Tiled)
            tileset = Content.Load<Texture2D>("Graphics/Tilesets/TextureAtlas01");
        }

        // ............
        // U P D A T E
        // ............
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            Input.Update();
            Mouse.SetPosition(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            camera.Update();
            player.Update(gameTime, camera);
            debugText = "Layers: " + map.Layers.Count;

            if (map.Layers.ContainsKey("Floor"))
            {
                debugText += "\nFloor tiles: " + map.Layers["Floor"].Length;
            }

            base.Update(gameTime);
        }

        // .........
        // D R A W
        // .........
        protected override void Draw(GameTime gameTime)
        {
            effect.View = camera.View;
            effect.Projection = camera.Projection;
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.DrawString(debugFont, debugText, new Vector2(10, 10), Color.White);
            // Draw layers in order
            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    int index = y * map.Width + x;
                    int tile = map.Layers["Walls"][index];

                    if (tile == 0) continue;

                    Matrix world =
                        Matrix.CreateScale(1f) *
                        Matrix.CreateTranslation(x, 0, y);

                    cube.Draw(GraphicsDevice, effect, world);
                }
            }
            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    Matrix world =
                        Matrix.CreateScale(1f, 0.1f, 1f) *
                        Matrix.CreateTranslation(x, -1f, y);

                    cube.Draw(GraphicsDevice, effect, world);
                }
            }


            spriteBatch.End();
            

            base.Draw(gameTime);
        }
    }
}
