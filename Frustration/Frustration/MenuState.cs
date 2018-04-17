using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Frustration
{
    public class MenuState : States
    {
        private List<Components> components;

        public MenuState(Game1 Game, GraphicsDevice graphicsDevice, ContentManager content) : base(Game, graphicsDevice, content)
        {
            Texture2D buttonTexture = content.Load<Texture2D>("button");
            SpriteFont buttonFont = content.Load<SpriteFont>("font");

            Button startEzButton = new Button(buttonTexture, buttonFont)
            {
                Pos = new Vector2(300, 250),
                Text = "Baby Mode",
            };
            startEzButton.Click += StartButton_Click;

            Button startHardButton = new Button(buttonTexture, buttonFont)
            {
                Pos = new Vector2(300, 200),
                Text = "Start Game",
            };
            startHardButton.Click += StartHardButton_Click;

            Button quitButton = new Button(buttonTexture, buttonFont)
            {
                Pos = new Vector2(300, 300),
                Text = "Quit",
            };
            quitButton.Click += QuitButton_Click;


            components = new List<Components>()
            {
                startEzButton,
                quitButton,
                startHardButton,
            };
        }

        private void StartHardButton_Click(object sender, EventArgs e)
        {
            game.ChangeState(new GameState(game, graphDevice, contentManager));
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            game.Exit();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            game.ChangeState(new GameState(game, graphDevice, contentManager));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (var component in components)
            {
                component.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            // Remove unused sprites.
        }

        public override bool Update(GameTime gameTime)
        {
            foreach(var component in components)
            {
                component.Update(gameTime);
            }
            return true;
        }
    }
}
