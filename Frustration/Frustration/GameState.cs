﻿using System;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Frustration
{
    public class GameState : States
    {

        float pressTimer = 0f;
        Player player;
        Texture2D bullet;
        Texture2D backSpace;
        List<Bullet> bullets;
        bool manualSpawning = true;
        List<int> posList = new List<int> { 9, 10, 11, 0, 8, 12, 0, 8, 12, 0, 8, 12, 0, 8, 12, 14, 6, 15, 5, 16, 4, 0, 8, 12, 7, 13, 17, 3, 9, 10, 11, 0, 16, 4, 15, 5, 14, 6, 0 };
        List<int> order = new List<int>();
        float delay = 1.5f;
        float remainingDelay = 1.5f;
        List<Enemy> enemies;
        float enemyCount = 3;
        float enemiesPerLine = 1;
        float speed = 1;
        Enemy enemy;
        Random rnd = new Random();
        
        List<PowerUp> powerUps;



        // Contructor that makes a gamestate work with all variables and working funktions.
        public GameState(Game1 Game, GraphicsDevice graphicsDevice, ContentManager content, bool easyMode) : base(Game, graphicsDevice, content)
        {
            player = new Player(game.Content.Load<Texture2D>("spaceship.1"))
            {
                difficulty = easyMode
            };
            
            enemies = new List<Enemy>();
            backSpace = content.Load<Texture2D>("stars");
            bullet = game.bulletTexture;
            
            bullets = new List<Bullet>();
            powerUps.Add(new PowerUp(10,game.Content.Load<Texture2D>("ball"),new Vector2(800,rnd.Next(0,400)),rnd.Next(0,3),player,game));

        }
        public void ReadPosition()
        {
            if (posList.Count == 0)
            {
                manualSpawning = false;
            }
            for (int i = 0; i < posList.Count;)
            {
                if (posList[i] == 0)
                {
                    posList.RemoveAt(i);
                    break;
                }
                else
                {
                    order.Add(posList[i]);
                    posList.RemoveAt(i);
                }
            }
        }
        public void GiveValues(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                int x = rnd.Next(0, 20);
                if (!order.Contains(x))
                {
                    order.Add(x);
                }
                else i--;
            }
        }

        public float GivePosition(int num)
        {
            return num * 22 - 20;
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Begin();

            spriteBatch.Draw(backSpace, new Rectangle(0, 0, 800, 480), Color.White);
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].DrawEnemy(spriteBatch);
            }

            player.Draw(spriteBatch);

            for (int i = 0; i < bullets.Count; ++i)
            {
                bullets[i].DrawBullet(spriteBatch);
                if (bullets[i].rectangle.Intersects(player.rectangle))
                {
                   // player.position = new Vector2(1000,1000);
                }
            }
            foreach (PowerUp powerUp in powerUps)
            {
                powerUp.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
        public void EnemyShoot()
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].FindIQ(i, enemies))
                {
                    Console.WriteLine("hey");
                    bullets.Add(new Bullet(10f,GetDir(player.position, enemies[i].FindPos(i, enemies)), bullet, enemies[i].FindPos(i, enemies) + enemies[i].FindOffset(i)));
                }
                //bullets.Add(new Bullet(10f, new Vector2(-1, 0), bullet, enemies[i].FindPos(i, enemies) + enemies[i].FindOffset(i)));
            }
        }

        public override bool Update(GameTime gameTime)
        {
            #region enemies
            for (int i = 0; i < order.Count;)
            {
                enemies.Add(new Enemy(game.enemyTexture, new Vector2(1000, GivePosition(order[i])), speed, new Vector2(0.1f, 0.1f), 0, Color.White, true));
                order.RemoveAt(i);
            }

            player.Update();

            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].FindPos(i,enemies).X < 0)
                {
                    enemies.RemoveAt(i);
                }
                enemies[i].Update();
            }
            KeyboardState pause = Keyboard.GetState();
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
            remainingDelay -= timer;
            if (remainingDelay <= 0)
            {
                if (enemiesPerLine < 3) enemiesPerLine += 0.02f;
                if (speed < 4) speed *= 1.02f;
                if (delay > 0.75f) delay -= 0.02f;

                if (manualSpawning) ReadPosition();
                else GiveValues((int)enemyCount);

                for (int i = 0; i < enemies.Count; i++)
                {
                    enemyCount = enemiesPerLine;
                    EnemyShoot();
                }
                remainingDelay = delay;
            }
            #endregion
            
            #region For Shooting Bullets

            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Update();
            }

            MouseState mouse = Mouse.GetState();
            if (mouse.LeftButton == ButtonState.Pressed)
            {
                if (game.attackTimer <= 0)
                {
                    Shoot();
                    game.attackTimer = game.attackSpeed;
                }
            }
            game.attackTimer -= deltaTime;

            #endregion

            #region Swap To Pause Menu

            pressTimer += deltaTime;
            if (pause.IsKeyDown(Keys.Escape) && pressTimer > 1f)
            {
                game.ChangeState(new PauseState(game, graphDevice, contentManager));
                pressTimer = 0f;
            }

            return true;

            #endregion
        }


        // Shoots when you have ammo and press left mouse.
        public void Shoot()
        {
            MouseState mouse = Mouse.GetState();
            if (player.ammo > 0)
            {
                bullets.Add(new Bullet(10f, GetDir(mouse.Position.ToVector2(), player.position+player.offset), bullet, (player.position+player.offset)));
                player.ammo--;
            }

        }

        // Gets the direction of the bullets.
        public Vector2 GetDir(Vector2 to, Vector2 from)
        {
            Vector2 dir = to - from;
            dir.Normalize();

            return dir;
        }

    }
}
