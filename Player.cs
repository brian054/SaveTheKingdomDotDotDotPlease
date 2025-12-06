using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static SaveTheKingdomDotDotDotPlease.Globals;
using static SaveTheKingdomDotDotDotPlease.Enums;
using System.Security;

namespace SaveTheKingdomDotDotDotPlease
{
    internal class Player
    {
        private float movementSpeed = 60f;
        public Vector2 playerPos = new Vector2(0, 0);

        private Rectangle currSourceRect;

        private Boolean isMoving;
        private Vector2 moveDir;
        private Vector2 movingStartPos;
        private float distanceMoved = 0f;

        Overworld currLevel; // change to Level later

        // the hitBox recalculates this rectangle automatically every frame
        public Rectangle hitBox =>
            new Rectangle(
                (int)MathF.Round(playerPos.X),
                (int)MathF.Round(playerPos.Y),
                Globals.EntityTileSize, // 16
                Globals.EntityTileSize
             );

        // TODO: Make Level class to replace Overworld, or refactor once you wanna add more than one level.
        public Player(Overworld overworld) { // Now I'm thinking it should be 'Level overworld' cuz overworld will just be 'FirstVillage' or 'Forest' 
            this.currLevel = overworld;
        }

        public void Update(GameTime gt)
        {
            float dt = (float)gt.ElapsedGameTime.TotalSeconds;
            var keyState = Keyboard.GetState();

            // Move player

            /*
                I figured surely there was a way to reduce the code below, 
                plus...what happens when you want to add arrow keys, or 
                allow the user to customize their controls for movement.

                Could add an InputManager.cs to handle Input so then Player just asks
                the InputManager "am I moving any direction, and if so which way, idc what actual 
                button was pressed"

                For now I'll just add:

                var keyDirectionMap = new Dictionary<Keys, Direction>
                {
                    { Keys.W, Direction.up },
                    { Keys.A, Direction.left },
                    { Keys.S, Direction.down },
                    { Keys.D, Direction.right },
                };

                foreach (var pair in keyDirectionMap)
                {
                    if (keyState.IsKeyDown(pair.Key))
                    {
                        StartMove(pair.Value);
                        break;
                    }
                }

                into this class. 
            */

            if (!isMoving)
            {
                if (keyState.IsKeyDown(Keys.W))
                {
                    StartMove(Direction.up);
                }
                else if (keyState.IsKeyDown(Keys.A))
                {
                    StartMove(Direction.left);
                }
                else if (keyState.IsKeyDown(Keys.S))
                {
                    StartMove(Direction.down);
                }
                else if (keyState.IsKeyDown(Keys.D))
                {
                    StartMove(Direction.right);
                }
            }
            else
            {
                float moveStep = movementSpeed * dt; 
                playerPos += moveDir * moveStep; 
                distanceMoved += moveStep; 

                if (distanceMoved >= EntityTileSize)
                {
                    playerPos = movingStartPos + moveDir * EntityTileSize;
                    isMoving = false;
                    distanceMoved = 0f;
                }

            }

            currSourceRect = new Rectangle(16 * 3, 0, 16, 16);
        }

        public void Draw(SpriteBatch sb)
        {
            Rectangle playerRectScaled = new((int)MathF.Round(playerPos.X * SCALE), (int)MathF.Round(playerPos.Y * SCALE), Globals.ScaledEntityTileSize, Globals.ScaledEntityTileSize);
            sb.Draw(Globals.SpriteSheet, playerRectScaled, currSourceRect, Color.White);
        }

        private void StartMove(Direction dir)
        {
            // if the next move the player is trying to make will not run them into a solid tile, let them move.
            if (!IsPlayerNextMoveSolidTileOrOutOfBounds(dir)) 
            {
                isMoving = true;
                movingStartPos = playerPos;
                distanceMoved = 0f;

                switch (dir)
                {
                    case Direction.up:
                        moveDir = new Vector2(0, -1);
                        break;
                    case Direction.down:
                        moveDir = new Vector2(0, 1);
                        break;
                    case Direction.left:
                        moveDir = new Vector2(-1, 0);
                        break;
                    case Direction.right:
                        moveDir = new Vector2(1, 0);
                        break;
                }
            }

        }

        // Prevents player from going out of map bounds, also prevents player from moving into solid tiles
        private Boolean IsPlayerNextMoveSolidTileOrOutOfBounds(Direction dir)
        {
            //int currTileX = (int)Math.Floor(playerPos.X / WorldTileSize);
            //int currTileY = (int)Math.Floor(playerPos.Y / WorldTileSize);

            var (currTileX, currTileY) = GetCurrentTilePosition();

            int[] tilePos = [-1, -1]; // x,y

            switch (dir)
            {
                case Direction.up:
                    tilePos[0] = currTileX;
                    if (currTileY - 1 >= 0) { tilePos[1] = currTileY - 1; }
                    break;
                case Direction.down: 
                    tilePos[0] = currTileX;
                    if (currTileY + 1 < Globals.WindowHeight / Globals.EntityTileSize) { tilePos[1] = currTileY + 1; }
                    break;
                case Direction.left:
                    if (currTileX - 1 >= 0) { tilePos[0] = currTileX - 1; }
                    tilePos[1] = currTileY;
                    break;
                case Direction.right: 
                    if (currTileX + 1 + EntityTileSize < Globals.WindowWidth / Globals.EntityTileSize) { tilePos[0] = currTileX + 1; }
                    tilePos[1] = currTileY;
                    break;
            }

            if (tilePos[0] == -1 || tilePos[1] == -1) {
                return true; // treat out of bounds as solid
            } 
            return currLevel.IsSolid(tilePos[0], tilePos[1]); 
        }

        // might be unnecessary hold on
        private (int currTileX, int currTileY) GetCurrentTilePosition()
        {
            return ((int)Math.Floor(playerPos.X / WorldTileSize), (int)Math.Floor(playerPos.Y / WorldTileSize));
        }
    }
}
