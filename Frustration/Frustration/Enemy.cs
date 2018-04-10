using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Frustration
{
    public class Enemy
    {
        Texture2D texture;
        Rectangle rectangle;
        Vector2 moveDir;
        Vector2 position;
        Vector2 scale;
        Vector2 offset;
        Color color;
        float speed;
        float rotation;
        List<Enemy> enemyList;

        public void AddEnemy(Enemy enemy)
        {
            enemyList.Add(enemy);
        }

        public Enemy(Texture2D enemyTexture, Vector2 enemyStartPos, float enemySpeed, Vector2 enemyScale, float enemyRotation, Color enemyColor)
        {
            texture = enemyTexture;
            position = enemyStartPos;
            speed = enemySpeed;
            moveDir = Vector2.Zero;
            scale = enemyScale;
            offset = (enemyTexture.Bounds.Size.ToVector2() / 2) * scale;
            rectangle = new Rectangle((enemyStartPos - offset).ToPoint(), (enemyTexture.Bounds.Size.ToVector2() * enemyScale).ToPoint());
            color = enemyColor;
            rotation = enemyRotation;
        }
        public void Destroy (Enemy enemy)
        {

        }
        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float pixelsToMove = speed * deltaTime;
            for (int i = 0; i < enemyList.Count; i++)
            {
                    
            }
        }
    }
}