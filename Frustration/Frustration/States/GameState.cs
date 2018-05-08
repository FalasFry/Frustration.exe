using System;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;

namespace Frustration
{
    public class GameState : States
    {
        #region Structs

        float attackTimer = 10;
        float moveTimer = 15;
        float pressTimer, timeElapsed, smartPercent;
        float delay = 2f;
        float remainingDelay = 1.5f;
        float enemyCount = 3;
        float enemiesPerLine = 1;
        float speed = 1;
        float timer;
        float health;
        public float score;
        float invTimer = 1;
        bool manualSpawning = true;
        bool inv;
        int wait = 0;
        string powerupTimer = "0";

        #endregion

        #region Classes

        Player player;
        Texture2D bullet;
        Texture2D backSpace;
        Random rnd = new Random();
        SpriteFont font;
        Texture2D powerupsTexture;
        Texture2D enemyTexture;
        Song song;
        Color color = Color.White;

        #endregion

        #region Lists

        List<int> posList = new List<int>();
        List<int> form1 = new List<int> { 5, 0, 6, 4, 0, 7, 3, 0, 8, 2, 0, 9, 1, 0 };
        List<int> form2 = new List<int> { 1, 2, 3, 4, 8, 9, 0, 0, 0, 0, 0, 1, 5, 6, 7, 8, 9, 0, 0 };
        List<int> form3 = new List<int> { 1, 9, 0, 2, 8, 0, 3, 7, 0, 4, 6, 0, 5, 0 };
        List<int> form4 = new List<int> { 1, 6, 0, 2, 7, 0, 3, 8, 0, 4, 9, 0, 4, 9, 0, 4, 9, 0, 3, 8, 0, 2, 7, 0, 1, 6, 0, 1, 6, 0, 2, 7, 0, 3, 8, 0, 4, 9, 0, 4, 9, 0, 4, 9, 0, 3, 8, 0, 2, 7, 0, 1, 6, 0 };
        List<int> form5 = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 0, 5, 0, 5, 0, 1, 2, 3, 4, 5, 6, 7, 8, 0, 0, 1, 2, 3, 4, 5, 6, 7, 8, 0, 1, 5, 8, 0, 1, 8, 1, 8, 0, 1, 2, 3, 4, 5, 6, 7, 8, 0, 8, 0, 8, 0, 1, 2, 3, 4, 5, 6, 7, 8, 0, 8, 0, 8, 0 };
        List<int> form6 = new List<int> { 6, 7, 8, 0, 5, 6, 0, 2, 4, 5, 6, 7, 8, 0, 3, 4, 6, 7, 9, 0, 4, 5, 6, 7, 9, 0, 4, 5, 6, 7, 0, 4, 5, 6, 7, 9, 0, 3, 4, 6, 7, 9, 0, 2, 4, 5, 6, 7, 8, 0, 5, 6, 0, 6, 7, 8, 0 };
        List<int> form7 = new List<int> { 5, 0, 4, 5, 6, 0, 4, 5, 6, 0, 3, 4, 5, 6, 7, 0, 5, 0, 5, 0, 5, 0, 5, 0, 5, 0, 5, 0, 5, 0, 5, 0, 4, 5, 6, 0, 3, 4, 6, 7, 0, 7, 3, 0 };
        List<int> form8 = new List<int> { 2, 3, 5, 6, 0, 1, 4, 7, 0, 2, 6, 0, 3, 5, 0, 4, 0, 0, 0, 0, 3, 4, 6, 7, 0, 2, 5, 8, 0, 3, 7, 0, 4, 6, 0, 5, 0 };
        List<List<int>> forms = new List<List<int>>();
        List<int> order = new List<int>();
        List<Enemy> enemies;
        List<PowerUp> powerUps;
        List<Bullet> bullets;

        #endregion

        // Contructor that makes a gamestate work with all variables and working funktions.
        public GameState(Game1 Game, GraphicsDevice graphicsDevice, ContentManager content, bool easyMode) : base(Game, graphicsDevice, content)
        {
            #region Adding enemyspawns

            forms.Add(form1);
            forms.Add(form2);
            forms.Add(form3);
            forms.Add(form4);
            forms.Add(form5);
            forms.Add(form6);
            forms.Add(form7);
            forms.Add(form8);

            #endregion

            manualSpawning = true;

            #region Load content

            powerupsTexture = content.Load<Texture2D>("powerup");
            font = content.Load<SpriteFont>("ammo");
            backSpace = content.Load<Texture2D>("stars");
            bullet = content.Load<Texture2D>("bullet.2");
            enemyTexture = content.Load<Texture2D>("enemy");
            song = content.Load<Song>("inGameMusic");

            #endregion

            #region SetHP

            if (easyMode)
            {
                health = 5;
            }
            if(!easyMode)
            {
                health = 1;
            }

            #endregion

            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;

            #region Making lists

            player = new Player(game.Content.Load<Texture2D>("squareship"), game)
            {
                normalDiff = easyMode,
                hp = health,
            };

            bullets = new List<Bullet>
            {
                new Bullet(2, new Vector2(1, -1), bullet, new Vector2(3000, 3000), 1, Color.Black)
            };

            enemies = new List<Enemy>
            (
            );
            enemies.Add(new Enemy(enemyTexture, new Vector2(10000, 10000), speed, Vector2.Zero, 0, false, Color.White));
            powerUps = new List<PowerUp>();

            #endregion
        }

        #region Methods
        //om en form ska användas så läser programmet av den listan som man skickar in
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
        //om man ska spawna fiender randomly så ser den till att dem inte har samma position
        public void GiveValues(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                int x = rnd.Next(1, 10);
                if (!order.Contains(x))
                {
                    order.Add(x);
                }
                else i--;
            }
        }

        // Enemies cant spawn outside of the sceeen.
        public float GivePosition(int num)
        {
            return num * 48 - 20;
        }

        // Makes enemies smarf randomly.
        public bool IsSmart()
        {
            if (rnd.Next(0, (int)smartPercent) > rnd.Next(0, 100))
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        // Shoots when you have ammo and press left mouse.
        public void Shoot()
        {
            MouseState mouse = Mouse.GetState();
            if (player.ammo > 0)
            {
                bullets.Add(new Bullet(10f, GetDir(mouse.Position.ToVector2(), player.position+player.offset), bullet, (player.position+player.offset),1, Color.White));
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

        // Enemies will shoot straight unless they are smart, then they shoot as the player.
        public void EnemyShoot()
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].FindIQ(i, enemies))
                {
                    bullets.Add(new Bullet(5f,GetDir(player.position, enemies[i].FindPos(i, enemies)), bullet, enemies[i].FindPos(i, enemies) + enemies[i].FindOffset(i),2, Color.Cyan));
                }
                else bullets.Add(new Bullet(5f, new Vector2(-1, 0), bullet, enemies[i].FindPos(i, enemies) + enemies[i].FindOffset(i),2, Color.Cyan));
            }
        }

        // Makes Powerups spawn between 15 and 29 seconds.
        public void PowerUpSpawn(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Timer start as 0, thats why you always get a powerup at start.
            if (timer > 0)
            {
                timer -= deltaTime;
            }
            if(timer <= 0)
            {
                powerUps.Add(new PowerUp(2, powerupsTexture, new Vector2(800, rnd.Next(3 , 430)), rnd.Next(1, 4), player, game));
                timer = rnd.Next(15, 30);
            }
        }

        // Sets it so that powerps only work for a specific amount of time.
        public bool CuntDown(GameTime gameTime, bool speed)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (speed && moveTimer > 0)
            {
                moveTimer -= deltaTime;
            }
            if (!speed && attackTimer > 0)
            {
                attackTimer -= deltaTime;
            }

            if (attackTimer <= 0 || moveTimer <= 0)
            {
                return false;
            }

            return true;
        }

        // Once you take damage you can not take damage again in a second.
        public void InvTimer(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (inv == true)
            {
                invTimer -= deltaTime;
            }

            if (invTimer <= 0)
            {
                inv = false;
            }

        }

        #endregion

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(backSpace, new Rectangle(0, 0, 800, 480), Color.White);

            spriteBatch.DrawString(font, "ammo = " + player.ammo.ToString(), new Vector2(700, 10), Color.White);
            spriteBatch.DrawString(font, "HP = " + player.hp.ToString(), new Vector2(10, 10), Color.White);

            player.Draw(spriteBatch);

            #region Enemy

            for (int i = 0; i < enemies.Count; ++i)
            {
                enemies[i].DrawEnemy(spriteBatch);
            }

            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].DrawBullet(spriteBatch);
            }

            #endregion

            #region PowerUps

            foreach (PowerUp powerUp in powerUps)
            {
                powerUp.Draw(spriteBatch);
            }

            spriteBatch.DrawString(font, "Powerup Timer: " + powerupTimer, new Vector2(350, 10), Color.White);

            #endregion

            spriteBatch.DrawString(font, "score = " + score.ToString(), new Vector2(10, 30), Color.White);

            spriteBatch.End();
        }

        public override bool Update(GameTime gameTime)
        {
            InvTimer(gameTime);
            KeyboardState pause = Keyboard.GetState();
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            MouseState mouse = Mouse.GetState();
            timeElapsed += deltaTime;

            #region Damage

            for (int i = 0; i < bullets.Count; ++i)
            {
                if (bullets[i].rectangle.Intersects(player.rectangle) && bullets[i].owner != 1)
                {
                    if (inv == false)
                    {
                        player.hp -= bullets[i].damage;
                        invTimer = 1;
                        inv = true;
                    }
                    bullets.RemoveAt(i);
                }

             }
            for (int i = 0; i < bullets.Count; ++i)
            {
                for (int k = 0; k < enemies.Count; ++k)
                {
                    if (bullets[i].rectangle.Intersects(enemies[k].rectangle) && bullets[i].owner != 2)
                    {
                        bullets.RemoveAt(i);
                        if (i == bullets.Count && bullets.Count != 0)
                        {
                            --i;
                        }
                        enemies.RemoveAt(k);
                        ++score;

                    }
                }
            }


            #endregion

            #region PowerUps

            if(wait == 1)
            {
                if (CuntDown(gameTime, false))
                {
                    player.attackSpeed = 0.1f;
                }
                if (!CuntDown(gameTime, false))
                {
                    player.attackSpeed = 0.5f;
                }
                if (attackTimer >= 0)
                {
                    powerupTimer = ((int)attackTimer).ToString();
                }
                if (attackTimer < 0)
                {
                    powerupTimer = "0";
                }
            }
            if(wait == 2)
            {
                if (CuntDown(gameTime, true))
                {
                    player.speed = 7;

                }
                if (!CuntDown(gameTime, true))
                {
                    player.speed = 5;
                }
                if (moveTimer >= 0)
                {
                    powerupTimer = ((int)moveTimer).ToString();
                }
                if (moveTimer <0)
                {
                    powerupTimer = "0";
                }
            }

            PowerUpSpawn(gameTime);

            for (int j = 0; j < powerUps.Count; j++)
            {
                powerUps[j].Update(gameTime);

                if (powerUps[j].rectangle.Intersects(player.rectangle))
                {
                    
                    if(powerUps[j].powerType == 1)
                    {
                        wait = 1;
                        attackTimer = 10;
                        moveTimer = 15;
                    }
                    if (powerUps[j].powerType == 2)
                    {
                        wait = 2;
                        moveTimer = 15;
                        attackTimer = 10;
                    }
                    powerUps.RemoveAt(j);
                }
            }

            #endregion

            #region Game Over Screen

            if (player.dead == true)
            {
                game.ChangeState(new GameOverState(game, graphDevice, contentManager, score, (int)timeElapsed));
            }

            #endregion

            #region enemies

            for (int i = 0; i < order.Count;)
            {
                enemies.Add(new Enemy(enemyTexture, new Vector2(1000, GivePosition(order[i])), speed, new Vector2(0.2f, 0.2f), 0, IsSmart(), color));
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

            remainingDelay -= deltaTime;
            if (remainingDelay <= 0)
            {
                if (enemiesPerLine < 3) enemiesPerLine += 0.02f;
                if (speed < 4) speed *= 1.02f;
                if (delay > 1f) delay -= 0.02f;
                if (smartPercent < 40) smartPercent += 0.4f;
                int index = rnd.Next(0, 100);
                if (index < forms.Count && !manualSpawning) posList = forms[index];
                if (posList.Count > 0) manualSpawning = true;
                if (posList.Count <= 0) manualSpawning = false;

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

            if (mouse.LeftButton == ButtonState.Pressed)
            {
                if (player.attackTimer <= 0)
                {
                    Shoot();
                    player.attackTimer = player.attackSpeed;
                }
            }
            player.attackTimer -= deltaTime;

            #endregion

            #region Swap To Pause Menu

            pressTimer += deltaTime;
            if (pause.IsKeyDown(Keys.Escape) && pressTimer > 1f)
            {
                game.ChangeState(new PauseState(game, graphDevice, contentManager));
                pressTimer = 0f;
                MediaPlayer.Pause();
            }

            return true;

            #endregion

        }

    }
}
