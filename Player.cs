using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static SaveTheKingdomDotDotDotPlease.Globals;

namespace SaveTheKingdomDotDotDotPlease
{
    internal class Player
    {
        private float movementSpeed = 60f;
        public Vector2 playerPos = new Vector2(0, 0);

        private Rectangle currSourceRect;

        // the hitBox recalculates this rectangle automatically every frame
        public Rectangle hitBox =>
            new Rectangle(
                (int)MathF.Round(playerPos.X),
                (int)MathF.Round(playerPos.Y),
                Globals.EntityTileSize, // 16
                Globals.EntityTileSize
             );

        public Player()
        {
        }

        public void Update(GameTime gt)
        {
            // TODO: Clean all this up! Redo all this tomorrow morning
            //bool isMoving = false;
  
            var keyState = Keyboard.GetState();
            int rawX = 0, rawY = 0;
            if (keyState.IsKeyDown(Keys.W)) {
                rawY -= 1;
            }
            if (keyState.IsKeyDown(Keys.A))
            {
                rawX -= 1;
            }
            if (keyState.IsKeyDown(Keys.S))
            {
                rawY += 1;
            }
            if (keyState.IsKeyDown(Keys.D)) {
                rawX += 1;
            }

            Vector2 inputDir = new(rawX, rawY);
            float dt = (float)gt.ElapsedGameTime.TotalSeconds;
            if (inputDir != Vector2.Zero) // meaning we are currently moving
            {
                inputDir.Normalize();

                // change position
                Vector2 delta = inputDir * movementSpeed * dt;
                playerPos += delta;
            }
            currSourceRect = new Rectangle(24, 0, 16, 16);
        }

        public void Draw(SpriteBatch sb)
        {
            Rectangle playerRectScaled = new((int)MathF.Round(playerPos.X * SCALE), (int)MathF.Round(playerPos.Y * SCALE), Globals.ScaledEntityTileSize, Globals.ScaledEntityTileSize);
            sb.Draw(Globals.SpriteSheet, playerRectScaled, currSourceRect, Color.White);
        }
    }
}
