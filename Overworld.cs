using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using static SaveTheKingdomDotDotDotPlease.Enums;

namespace SaveTheKingdomDotDotDotPlease
{
    internal class Overworld
    {
        int tileSize = Globals.WorldTileSize;
        int scaledTileSize = Globals.ScaledWorldTileSize;

        private Texture2D mapTexture;
        //private Texture2D SpriteSheet;

        Color[] mapData;

        //Vector2 playerPos;
        public Overworld(Texture2D mapTexture)
        {
            this.mapTexture = mapTexture;
            mapData = new Color[mapTexture.Width * mapTexture.Height];
            mapTexture.GetData(mapData);

            //this.SpriteSheet = SpriteSheet;
           // playerPos.X = 0;
            //playerPos.Y = 0;
        }

        private Rectangle GetSourceRectangleForTile(TileType tileType)
        {
            switch (tileType)
            {
                case TileType.Grass:
                    return new Rectangle(0, 0, tileSize, tileSize);
                case TileType.Water:
                    return new Rectangle(8, 0, tileSize, tileSize);
                case TileType.Stone:
                    return new Rectangle(16, 0, tileSize, tileSize);
                default:
                    return new Rectangle(2, 8, tileSize, tileSize);
            }
        }

        public TileType GetTileTypeFromColor(Color color)
        {
            // Define RGB values for specific tile colors
            if (color.R == 22 && color.G == 217 && color.B == 33)  // Green for grass 
                return Enums.TileType.Grass;
            else if (color.R == 5 && color.G == 107 && color.B == 194)  // Blue for water
                return TileType.Water;
            else if (color.R == 77 && color.G == 91 && color.B == 103)  // Gray for dirt
                return TileType.Stone;
            else
                return TileType.Unknown;  // For any other colors
        }

        public void Update(GameTime gameTime) {
            //var keyState = Keyboard.GetState();
            //if (keyState.IsKeyDown(Keys.D))
            //{

            //} 
        }

        public void Draw(SpriteBatch sb) // might pass player in?
        {
            for (int x = 0; x < mapTexture.Width; x++) 
            {
                for (int y = 0; y < mapTexture.Height; y++)
                {
                    Color pixelColor = mapData[y * 40 + x]; 

                    TileType tileType = GetTileTypeFromColor(pixelColor);

                    Rectangle sourceRectangle = GetSourceRectangleForTile(tileType);

                    Rectangle destinationRectangle = new Rectangle(x * scaledTileSize, y * scaledTileSize, scaledTileSize, scaledTileSize);

                    sb.Draw(Globals.SpriteSheet, destinationRectangle, sourceRectangle, Color.White);
                }
            }
        }

    }
}
