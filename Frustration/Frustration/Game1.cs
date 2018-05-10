using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace Frustration
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        MenuState menu;
        public States curState;
        Stack<States> stateStack;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            stateStack = new Stack<States>();
            graphics.PreferredBackBufferHeight = 480;
            graphics.PreferredBackBufferWidth = 800;
        }

        protected override void Initialize()
        {
            base.Initialize();
            IsMouseVisible = true;
            menu = new MenuState(this, GraphicsDevice, Content);
            curState = menu;
            stateStack.Push(menu);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent() { }

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
            stateStack.Peek().Draw(gameTime, spriteBatch);

            base.Draw(gameTime);
        }

        // Used to remove a state that is unused.
        public void PopStack()
        {
            stateStack.Pop();
        }
        
        // Used to change a state.
        public void ChangeState(States state)
        {
            curState = state;
            stateStack.Push(state);
        }
    }
}
