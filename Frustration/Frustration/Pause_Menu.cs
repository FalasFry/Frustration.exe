using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Frustration
{
    public class Pause_Menu : States
    {
        KeyboardState unPause = Keyboard.GetState();
        bool paused;
        List<Components> components;

        public Pause_Menu(Game1 Game, GraphicsDevice graphicsDevice, ContentManager content) : base(Game, graphicsDevice, content)
        {
            Texture2D buttonText = content.Load<Texture2D>("button");
            SpriteFont buttonFont = content.Load<SpriteFont>("font");

            Button resumeButton = new Button(buttonText, buttonFont)
            {

            };
            resumeButton.Click += ResumeButton_Click;

            Button menuButton = new Button(buttonText, buttonFont)
            {

            };
            menuButton.Click += MenuButton_Click;

            components = new List<Components>()
            {
                resumeButton,
                menuButton,
            };
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 2; i++)
            {
                game.PopStack();
            }
        }

        private void ResumeButton_Click(object sender, EventArgs e)
        {
            paused = false;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach(var component in components)
            {
                component.Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override bool Update(GameTime gameTime)
        {
            foreach(var component in components)
            {
                component.Update(gameTime);
            }

            if(unPause.IsKeyDown(Keys.Escape))
            {
                return false;
            }
            if(paused == false)
            {
                return false;
            }
            return true;
        }

    }
}