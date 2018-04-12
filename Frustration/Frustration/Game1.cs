﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Frustration
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        Vector2 scale;
        float speed;
        Bullet bullet;
        Texture2D player;
        Rectangle playerRec;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Vector2 playerPos;
        Vector2 dir;
        Texture2D bulletTexture;
        
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <Initialize>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            bullet = new Bullet(10f, new Vector2(1,0),bulletTexture,new Vector2(10,100));
            player = Content.Load<Texture2D>("ball");

            playerPos = new Vector2(1, 1);
            scale = new Vector2(0.33f, 0.33f);
            playerRec = new Rectangle(playerPos.ToPoint(),((player.Bounds.Size).ToVector2() * scale).ToPoint());
            speed = 5;
            IsMouseVisible = true;
        }

        /// <LoadContent>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            bulletTexture = Content.Load<Texture2D>("bullet");

            // TODO: use this.Content to load your game content here
        }

        /// <Unload>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <Update>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            bullet.Update();
            // TODO: Add your update logic here
            MouseState mouse = Mouse.GetState();
            KeyboardState keyState = Keyboard.GetState();
            Vector2 dir = new Vector2();
            

            if(keyState.IsKeyDown(Keys.A))
            {
                dir.X = -1;
            }
            else if (keyState.IsKeyDown(Keys.D))
            {
                dir.X = 1;
            }
            else if (keyState.IsKeyDown(Keys.W))
            {
                dir.Y = -1;
            }
            else if (keyState.IsKeyDown(Keys.S))
            {
                dir.Y = 1;
            }
            if (mouse.LeftButton == ButtonState.Pressed)
            {
                bullet.GetDir(mouse.Position.ToVector2(),bullet.position);
            }

            if (dir.X > 1f || dir.Y > 1f)
            {
                dir.Normalize();
            }
            playerRec.Location += (dir * speed).ToPoint();

            base.Update(gameTime);
        }

        /// <Draw>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            spriteBatch.Draw(player, playerRec, Color.Black);
            bullet.DrawBullet(spriteBatch);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
