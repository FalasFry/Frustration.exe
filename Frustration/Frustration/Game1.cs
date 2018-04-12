using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Frustration
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        Bullet bullet;
        Player player;
        Texture2D playerTexture;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D bulletTexture,floorTexture;
        Rectangle floorRect;
        Random rnd = new Random();
        List<Bullet> bullets;
        List<Components> gameComponents;

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
            bullets = new List<Bullet>();

            
            playerTexture = Content.Load<Texture2D>("ball");
            player = new Player(playerTexture);
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

            // Button script.
            Button quit = new Button(Content.Load<Texture2D>("button"), Content.Load<SpriteFont>("font"))
            {
                Pos = new Vector2(300, 300),
                Text = "Quit",
                Paint = Color.Black,
            };

            quit.Click += Quit_Click;

            gameComponents = new List<Components>()
            {
                quit,
            };
        }

        private void Quit_Click(object sender, EventArgs e)
        {
            this.Exit();
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
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Update();
                if (bullets[i].rectangle.Intersects(player.rectangle))
                {
                    player.position = new Vector2(10000, 10000);
                }
            }

            player.Update();
            //// TODO: Add your update logic here
            MouseState mouse = Mouse.GetState();
            //KeyboardState keyState = Keyboard.GetState();
            //Vector2 dir = new Vector2();
            if (mouse.LeftButton == ButtonState.Pressed)
            {
                Shoot();

            }

            // For Buttons.
            foreach (var components in gameComponents)
            {
                components.Update(gameTime);
            }

            //if (dir.X > 1f || dir.Y > 1f)
            //{
            //    dir.Normalize();
            //}
            //playerRec.Location += (dir * speed).ToPoint();
            base.Update(gameTime);
        }

        /// <Draw>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 
        public void Shoot()
        {
            MouseState mouse = Mouse.GetState();
            if (player.ammo > 0)
            {
                bullets.Add(new Bullet(10f, GetDir(mouse.Position.ToVector2(), player.position), bulletTexture, player.position));
                player.ammo--;
            }
            
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            player.Draw(spriteBatch);
            for (int i =0;i<bullets.Count;i++)
            {
                bullets[i].DrawBullet(spriteBatch);
            }

            // For Buttons.
            foreach (var components in gameComponents)
            {
                components.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
        public Vector2 GetDir(Vector2 to, Vector2 from)
        {
            Vector2 dir = to - from;
            dir.Normalize();

            return dir;
        }
    }
}
