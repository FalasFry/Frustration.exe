﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frustration
{
    public class Enemy
    {
        Texture2D texture;
        public Rectangle rectangle;
        Vector2 moveDir;
        Vector2 position;
        Vector2 scale;
        Vector2 offset;
        bool enemySmart;
        Color color;
        float speed;
        float rotation;
        int index = 0;
        List<Enemy> enemyList = new List<Enemy>();

        public void AddEnemy(Enemy enemy)
        {
            enemyList.Add(enemy);
        }

        public Enemy(Texture2D enemyTexture, Vector2 enemyStartPos, float enemySpeed, Vector2 enemyScale, float enemyRotation, bool smart, Color enemyColor)
        {
            texture = enemyTexture;
            position = enemyStartPos;
            speed = enemySpeed;
            enemySmart = smart;
            moveDir = new Vector2(-1, 0);
            scale = enemyScale;
            offset = (enemyTexture.Bounds.Size.ToVector2() / 2) * scale;
            rectangle = new Rectangle((enemyStartPos - offset).ToPoint(), (enemyTexture.Bounds.Size.ToVector2() * enemyScale).ToPoint());
            color = enemyColor;
            rotation = enemyRotation;
            index = enemyList.Count + 1;
        }

        // Detsroys enemy when dead.
        public void Destroy(Enemy enemy)
        {
            enemyList.RemoveAt(enemy.index);
        }

        // Gets if it is smart or nah.
        public bool FindIQ(int indexx, List<Enemy> list)
        {
            return list[indexx].enemySmart;
        }

        // Gets position.
        public Vector2 FindPos(int indexx, List<Enemy> list)
        {
            enemyList = list;
            return enemyList[indexx].position;
        }

        // Gets offset.
        public Vector2 FindOffset(int indexx)
        {
            return enemyList[indexx].offset;
        }

        public void DrawEnemy(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, color, rotation, offset, scale, SpriteEffects.None, 0);
        }

        public void Update()
        {
            position += (moveDir * speed);
            rectangle.Location = (position - offset).ToPoint();
            rotation = (float)Math.Atan2(moveDir.X, moveDir.Y) * -1;
            if (enemySmart)
            {
                color = Color.HotPink;
            }
        }
    }
}