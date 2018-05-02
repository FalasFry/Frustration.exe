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
        float timer = 0f;
        bool paused = true;
        List<Components> buttons;
        Texture2D screen;
        Color white = Color.White;
        Vector2 overPos = new Vector2(325, 190);
        Vector2 centerPos = new Vector2(325, 240);
        Vector2 underPos = new Vector2(325, 290);

        public PauseState(Game1 Game, GraphicsDevice graphicsDevice, ContentManager content) : base(Game, graphicsDevice, content)
        {
            Texture2D buttonText = content.Load<Texture2D>("button");
            SpriteFont buttonFont = content.Load<SpriteFont>("font");

            screen = content.Load<Texture2D>("paused");

            Button resumeButton = new Button(buttonText, buttonFont)
            {
                Pos = overPos,
                Text = "Resume",
            };
            resumeButton.Click += ResumeButton_Click;

            Button menuButton = new Button(buttonText, buttonFont)
            {
                Pos = centerPos,
                Text = "Main Menu",
            };
            menuButton.Click += MenuButton_Click;

            Button quitButton = new Button(buttonText, buttonFont)
            {
                Pos = underPos,
                Text = "Quit",
            };
            quitButton.Click += QuitButton_Click;

            buttons = new List<Components>()
            {
                resumeButton,
                menuButton,
                quitButton,
            };
        }

        #region Button Clicks

        private void QuitButton_Click(object sender, EventArgs e)
        {
            game.Exit();
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

        #endregion

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(screen, new Rectangle(0,0, 800, 480), white);
            foreach(Button component in buttons)
            {
                component.Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();
        }

        public override bool Update(GameTime gameTime)
        {
            KeyboardState keys = Keyboard.GetState();
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer += deltaTime;

            foreach(Button component in buttons)
            {
                component.Update(gameTime);
            }

            if(keys.IsKeyDown(Keys.Escape) && timer > 1)
            {
                timer = 0f;
                paused = false;
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