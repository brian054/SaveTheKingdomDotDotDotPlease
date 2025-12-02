using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static SaveTheKingdomDotDotDotPlease.Enums;

namespace SaveTheKingdomDotDotDotPlease
{
    internal class Map
    {
        int tileSize = Globals.TileSize;
        int scaledTileSize = Globals.ScaledTileSize;

        private Texture2D mapTexture;
        private Texture2D spriteSheet;

        Color[] mapData;

        public Map(int width, int height, Texture2D mapTexture, Texture2D spriteSheet)
        {
            this.mapTexture = mapTexture;
            mapData = new Color[mapTexture.Width * mapTexture.Height];
            mapTexture.GetData(mapData);

            this.spriteSheet = spriteSheet;
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

        public void Draw(SpriteBatch sb)
        {
            for (int x = 0; x < mapTexture.Width; x++) 
            {
                for (int y = 0; y < mapTexture.Height; y++)
                {
                    Color pixelColor = mapData[y * 40 + x]; 

                    TileType tileType = GetTileTypeFromColor(pixelColor);

                    Rectangle sourceRectangle = GetSourceRectangleForTile(tileType);

                    Rectangle destinationRectangle = new Rectangle(x * scaledTileSize, y * scaledTileSize, scaledTileSize, scaledTileSize);

                    sb.Draw(spriteSheet, destinationRectangle, sourceRectangle, Color.White);
                }
            }
        }

    }
}
