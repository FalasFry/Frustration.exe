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
    public class PauseState : States
    {
        KeyboardState keys = Keyboard.GetState();
        bool paused;
        List<Components> buttons;

        public PauseState(Game1 Game, GraphicsDevice graphicsDevice, ContentManager content) : base(Game, graphicsDevice, content)
        {
            Texture2D buttonText = content.Load<Texture2D>("button");
            SpriteFont buttonFont = content.Load<SpriteFont>("font");

            Button resumeButton = new Button(buttonText, buttonFont)
            {
                Pos = new Vector2(300, 250),
                Text = "Resume",
            };
            resumeButton.Click += ResumeButton_Click;

            Button menuButton = new Button(buttonText, buttonFont)
            {
                Pos = new Vector2(300, 300),
                Text = "Main Menu",
            };
            menuButton.Click += MenuButton_Click;

            buttons = new List<Components>()
            {
                resumeButton,
                menuButton,
            };
        }

        #region Button Clicks

        private void MenuButton_Click(object sender, EventArgs e)
        {
            game.PopStack();
            game.PopStack();
        }

        private void ResumeButton_Click(object sender, EventArgs e)
        {
            paused = false;
        }

        #endregion

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach(var component in buttons)
            {
                component.Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();
        }


        public override bool Update(GameTime gameTime)
        {
            foreach(var component in buttons)
            {
                component.Update(gameTime);
            }

            keys = Keyboard.GetState();
            if (keys.IsKeyDown(Keys.Escape))
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