using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Frustration
{
    public class MenuState : States
    {
        private List<Components> buttons;
        Texture2D menu;

        // Creates a menustate that have the buttons ready for you.
        public MenuState(Game1 Game, GraphicsDevice graphicsDevice, ContentManager content) : base(Game, graphicsDevice, content)
        {
            Texture2D buttonTexture = content.Load<Texture2D>("button");
            SpriteFont buttonFont = content.Load<SpriteFont>("font");
            menu = content.Load<Texture2D>("menu");

            Button startEzButton = new Button(buttonTexture, buttonFont)
            {
                Pos = new Vector2(300, 250),
                Text = "Baby Mode",
            };
            startEzButton.Click += StartButton_Click;

            Button startHardButton = new Button(buttonTexture, buttonFont)
            {
                Pos = new Vector2(300, 200),
                Text = "Normal Mode",
            };
            startHardButton.Click += StartHardButton_Click;

            Button quitButton = new Button(buttonTexture, buttonFont)
            {
                Pos = new Vector2(300, 300),
                Text = "Quit",
            };
            quitButton.Click += QuitButton_Click;

            buttons = new List<Components>()
            {
                startEzButton,
                quitButton,
                startHardButton,
            };
        }

        #region Button clicking

        private void StartHardButton_Click(object sender, EventArgs e)
        {
            game.ChangeState(new GameState(game, graphDevice, contentManager, false));
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            game.Exit();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            game.ChangeState(new GameState(game, graphDevice, contentManager, true));
        }

        #endregion

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(menu, new Rectangle(0, 0, 800, 440), Color.White);
            foreach (var component in buttons)
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
            return true;
        }
    }
}
