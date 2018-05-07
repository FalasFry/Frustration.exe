using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Frustration
{
    public class MenuState : States
    {
        private List<Components> buttons;
        Texture2D menu;
        Vector2 overPos = new Vector2(325, 190);
        Vector2 centerPos = new Vector2(325, 240);
        Vector2 underPos = new Vector2(325, 290);
        Song song;

        // Creates a menustate that have the buttons ready for you.
        public MenuState(Game1 Game, GraphicsDevice graphicsDevice, ContentManager content) : base(Game, graphicsDevice, content)
        {
            Texture2D buttonTexture = content.Load<Texture2D>("button");
            SpriteFont buttonFont = content.Load<SpriteFont>("font");
            menu = content.Load<Texture2D>("menu");
            song = content.Load<Song>("menuMusic");
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;

            Button startHardButton = new Button(buttonTexture, buttonFont)
            {
                Pos = overPos,
                Text = "Normal Mode",
            };
            startHardButton.Click += StartHardButton_Click;

            Button startEzButton = new Button(buttonTexture, buttonFont)
            {
                Pos = centerPos,
                Text = "Baby Mode",
            };
            startEzButton.Click += StartButton_Click;

            Button quitButton = new Button(buttonTexture, buttonFont)
            {
                Pos = underPos,
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
            spriteBatch.Draw(menu, new Rectangle(0, 0, 800, 480), Color.White);
            foreach (Button component in buttons)
            {
                component.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();
        }

        public override bool Update(GameTime gameTime)
        {
            foreach(Button component in buttons)
            {
                component.Update(gameTime);
            }
            return true;
        }
    }
}
