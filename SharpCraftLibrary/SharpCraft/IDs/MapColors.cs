using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    /// <summary>
    /// Holds all the different map colors
    /// </summary>
    public static class MapColors
    {
        private static readonly MapColor[] mapColors = new MapColor[]
        {
            new MapColor(1, ID.Block.grass_block, 127, 178, 56),
            new MapColor(2, ID.Block.sandstone, 247, 233, 163),
            new MapColor(3, ID.Block.mushroom_stem, 199, 199, 199),
            new MapColor(4, ID.Block.redstone_block, 255, 0, 0),
            new MapColor(5, ID.Block.blue_ice, 160, 160, 255),
            new MapColor(6, ID.Block.iron_block, 167, 167, 167),
            new MapColor(7, ID.Block.grass, 0, 124, 0, 0, ID.Block.grass_block),
            new MapColor(8, ID.Block.snow_block, 255, 255, 255),
            new MapColor(9, ID.Block.clay, 164, 168, 184),
            new MapColor(10, ID.Block.dirt, 151, 109, 77),
            new MapColor(11, ID.Block.stone, 112, 112, 112),
            new MapColor(13, ID.Block.oak_wood, 143, 119, 72),
            new MapColor(14, ID.Block.quartz_block, 255, 252, 245),
            new MapColor(15, ID.Block.orange_wool, 216, 127, 51),
            new MapColor(16, ID.Block.magenta_wool, 178, 76, 216),
            new MapColor(17, ID.Block.light_blue_wool, 102, 153, 216),
            new MapColor(18, ID.Block.yellow_wool, 229, 229, 51),
            new MapColor(19, ID.Block.lime_wool, 127, 204, 25),
            new MapColor(20, ID.Block.pink_wool, 242, 127, 165),
            new MapColor(21, ID.Block.gray_wool, 76, 76, 76),
            new MapColor(22, ID.Block.light_gray_wool, 153, 153, 153),
            new MapColor(23, ID.Block.cyan_wool, 76, 127, 153),
            new MapColor(24, ID.Block.purple_wool, 127, 63, 178),
            new MapColor(25, ID.Block.blue_wool, 51, 76, 178),
            new MapColor(26, ID.Block.brown_wool, 102, 76, 51),
            new MapColor(27, ID.Block.green_wool, 102, 127, 51),
            new MapColor(28, ID.Block.red_wool, 153, 51, 51),
            new MapColor(29, ID.Block.black_wool, 25, 25, 25),
            new MapColor(30, ID.Block.gold_block, 250, 238, 77),
            new MapColor(31, ID.Block.diamond_block, 92, 219, 213),
            new MapColor(32, ID.Block.lapis_block, 74, 128, 255),
            new MapColor(33, ID.Block.emerald_block, 0, 217, 58),
            new MapColor(34, ID.Block.podzol, 129, 86, 49),
            new MapColor(35, ID.Block.netherrack, 112, 2, 0),
            new MapColor(36, ID.Block.white_terracotta, 209, 177, 161),
            new MapColor(37, ID.Block.orange_terracotta, 159, 82, 36),
            new MapColor(38, ID.Block.magenta_terracotta, 149, 87, 108),
            new MapColor(39, ID.Block.light_blue_terracotta, 112, 108, 138),
            new MapColor(40, ID.Block.yellow_terracotta, 186, 133, 36),
            new MapColor(41, ID.Block.lime_terracotta, 103, 117, 53),
            new MapColor(42, ID.Block.pink_terracotta, 160, 77, 78),
            new MapColor(43, ID.Block.gray_terracotta, 57, 41, 35),
            new MapColor(44, ID.Block.light_gray_terracotta, 135, 107, 98),
            new MapColor(45, ID.Block.cyan_terracotta ,87, 92, 92),
            new MapColor(46, ID.Block.purple_terracotta, 122, 73, 88),
            new MapColor(47, ID.Block.blue_terracotta, 76, 62, 92),
            new MapColor(48, ID.Block.brown_terracotta, 76, 50, 35),
            new MapColor(49, ID.Block.green_terracotta, 76, 82, 42),
            new MapColor(50, ID.Block.red_terracotta, 142, 60, 46),
            new MapColor(51, ID.Block.black_terracotta, 37, 22, 16),
            new MapColor(52, ID.Block.crimson_nylium, 189, 48, 49),
            new MapColor(53, ID.Block.crimson_stem, 148, 63, 97),
            new MapColor(54, ID.Block.crimson_hyphae ,92, 25, 29),
            new MapColor(55, ID.Block.warped_nylium, 22, 126, 134),
            new MapColor(56, ID.Block.warped_stem, 58, 142, 140),
            new MapColor(57, ID.Block.warped_hyphae, 86, 44, 62),
            new MapColor(58, ID.Block.warped_wart_block, 20, 180, 133),
        };

        private static MapColor[]? fullMapColors = null; 

        /// <summary>
        /// Returns a list of all the map colors
        /// </summary>
        /// <returns>A list of all map colors</returns>
        public static MapColor[] GetMapColors()
        {
            if (fullMapColors is null)
            {
                List<MapColor> allColors = new List<MapColor>();

                byte multiplyColor(int color, int value)
                {
                    return Convert.ToByte(color * value / 255);
                }

                mapColors.ToList().ForEach(color =>
                {
                    allColors.Add(new MapColor(color.Id * 4, color.Block, multiplyColor(color.Color.Red, 180), multiplyColor(color.Color.Green, 180), multiplyColor(color.Color.Blue, 180), -1, color.SupportBlock));
                    allColors.Add(new MapColor(color.Id * 4 + 1, color.Block, multiplyColor(color.Color.Red, 220), multiplyColor(color.Color.Green, 220), multiplyColor(color.Color.Blue, 220), 0, color.SupportBlock));
                    allColors.Add(new MapColor(color.Id * 4 + 2, color.Block, multiplyColor(color.Color.Red, 255), multiplyColor(color.Color.Green, 255), multiplyColor(color.Color.Blue, 255), 1, color.SupportBlock));
                });

                fullMapColors = allColors.ToArray();
            }
            return fullMapColors.ToList().ToArray();
        }

        /// <summary>
        /// Finds the closest map color to the given color
        /// </summary>
        /// <param name="findColor">The color to find</param>
        /// <param name="colorList">An array of map colors to search in</param>
        /// <returns>The closest color</returns>
        public static MapColor GetClosestColor(RGBColor findColor, MapColor[] colorList)
        {
            if (colorList.Length == 0)
            {
                throw new ArgumentException("Colorlist may not be empty", nameof(colorList));
            }

            int closestColorDistance = int.MaxValue;
            MapColor? closestColor = null;
            foreach(MapColor color in colorList)
            {
                int difference = 0;
                difference += Math.Abs(findColor.Red - color.Color.Red);
                difference += Math.Abs(findColor.Green - color.Color.Green);
                difference += Math.Abs(findColor.Blue - color.Color.Blue);
                if (difference < closestColorDistance)
                {
                    closestColorDistance = difference;
                    closestColor = color;
                }
            }

            return closestColor!;
        }

        /// <summary>
        /// Class for map colors
        /// </summary>
        public class MapColor
        {
            /// <summary>
            /// Intializes a new map color
            /// </summary>
            /// <param name="blue">The blue color part</param>
            /// <param name="green">The green color part</param>
            /// <param name="red">The red color part</param>
            /// <param name="block">The block which has the color</param>
            /// <param name="supportBlock">A block which has to be under the actual block</param>
            /// <param name="id">The id of the color</param>
            /// <param name="height">0 If the block can be placed on same height as last block. -1 if lower and +1 if higher. The height thing should go in the north direction.</param>
            public MapColor(int id, ID.Block block, byte red, byte green, byte blue, int height = 0, ID.Block? supportBlock = null)
            {
                Color = new RGBColor(red, green, blue);
                Block = block;
                SupportBlock = supportBlock;
            }

            /// <summary>
            /// Intializes a new map color
            /// </summary>
            /// <param name="color">The color of the block</param>
            /// <param name="block">The block which has the color</param>
            /// <param name="supportBlock">A block which has to be under the actual block</param>
            /// <param name="id">The id of the color</param>
            /// <param name="height">0 If the block can be placed on same height as last block. -1 if lower and +1 if higher. The height thing should go in the north direction.</param>
            public MapColor(int id, ID.Block block, RGBColor color, int height = 0, ID.Block? supportBlock = null)
            {
                Color = color;
                Block = block;
                SupportBlock = supportBlock;
            }

            /// <summary>
            /// The color of the block
            /// </summary>
            public RGBColor Color { get; private set; }

            /// <summary>
            /// The block which has the color
            /// </summary>
            public ID.Block Block { get; private set; }

            /// <summary>
            /// A block which has to be under the actual block
            /// </summary>
            public ID.Block? SupportBlock { get; private set; }

            /// <summary>
            /// The id of the color
            /// </summary>
            public int Id { get; private set; }

            /// <summary>
            /// 0 If the block can be placed on same height as last block. -1 if lower and +1 if higher. The height thing should go in the north direction.
            /// </summary>
            public int Height { get; private set; }
        }
    }
}
