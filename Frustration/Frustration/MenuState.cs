﻿using System;
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

        Player player;

        public MenuState(Game1 Game, GraphicsDevice graphicsDevice, ContentManager content, Player aPlayer) : base(Game, graphicsDevice, content)
        {
            player = aPlayer;
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
                Text = "Normal Mode",
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
            player.difficulty = false;
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            game.Exit();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            game.ChangeState(new GameState(game, graphDevice, contentManager));
            player.difficulty = true;

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

        public override void Update(GameTime gameTime)
        {
            foreach(var component in components)
            {
                component.Update(gameTime);
            }        }
    }
}
