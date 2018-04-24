using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;


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

        MenuState menu;

        public States curState;

        Stack<States> stateStack;


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
            bulletTexture = Content.Load<Texture2D>("bullet.2");
            //enemyTexture = Content.Load<Texture2D>("enemy");
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

            stateStack.Peek().Draw(gameTime, spriteBatch);

            spriteBatch.Begin();



            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void PopStack()
        {
            stateStack.Pop();
        }
        
        public void ChangeState(States state)
        {
            curState = state;
            stateStack.Push(state);
        }
    }
}
