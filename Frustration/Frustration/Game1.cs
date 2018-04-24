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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public Texture2D bulletTexture;
        public float attackSpeed = 0.5f,attackTimer;
        public Texture2D enemyTexture;
        Random rnd = new Random();
        List<PowerUp> powerUps;

        MenuState menu;

        public States curState;

        Stack<States> stateStack;

        public void ChangeState(States state)
        {
            curState = state;
            stateStack.Push(state);
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            stateStack = new Stack<States>();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            IsMouseVisible = true;

            menu = new MenuState(this, GraphicsDevice, Content);
            curState = menu;

            stateStack.Push(menu);
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            bulletTexture = Content.Load<Texture2D>("bullet");
            enemyTexture = Content.Load<Texture2D>("enemy");
        }


        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {

            if (stateStack.Peek().Update(gameTime) == false)
            {
                stateStack.Pop();
            }
            base.Update(gameTime);
            
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here

            stateStack.Peek().Draw(gameTime, spriteBatch);

            spriteBatch.Begin();

            /*foreach (PowerUp powerUp in powerUps)
            {
                powerUp.Draw(spriteBatch);
            }*/

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void PopStack()
        {
            stateStack.Pop();
        }
    }
}
