using cmdwtf.Treemap;

using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MapViewer
{
    /// <summary>
    /// A class to color treemap nodes.
    /// </summary>
    internal static class TreemapViewColoring
    {
        /// <summary>
        /// A set of colors to use by default.
        /// </summary>
        private readonly static Color[] _defaultColors = new Color[] {
                    Color.DarkRed,
                    Color.DarkGreen,
                    Color.Blue,
                    Color.DarkCyan,
                    Color.DarkMagenta,
                    Color.DarkGoldenrod,
                    Color.DarkOrange,
                    Color.DarkViolet,
                    Color.DarkOrchid,
                    Color.DarkSeaGreen,
                    Color.DarkSalmon,
                    Color.DarkTurquoise,
                    Color.DarkSlateGray,
                    Color.DarkKhaki,
                    Color.DarkSlateBlue,
                    Color.DarkGray
        };

        /// <summary>
        /// Todo: specific color schemes per mode?
        /// </summary>
        private readonly static Dictionary<TreemapViewColoringMode, Color[]> _colorSchemes = new Dictionary<TreemapViewColoringMode, Color[]>
        {
            { TreemapViewColoringMode.None, new[] { SystemColors.Control } },
            { TreemapViewColoringMode.Global, _defaultColors },
            { TreemapViewColoringMode.Section, _defaultColors },
            { TreemapViewColoringMode.FileName, _defaultColors },
        };

        /// <summary>
        /// Colors a <see cref="TreemapNode"/> by the given mode and data in
        /// the <see cref="Symbol"/>. Uses a simple hash function to pseudorandomly
        /// choose a color.
        /// </summary>
        /// <param name="node">The node to color.</param>
        /// <param name="symbol">The symbol to base the color on.</param>
        /// <param name="mode">The color mode to use.</param>
        public static void ColorNode(this TreemapNode node, Symbol symbol, TreemapViewColoringMode mode)
        {
            if (!_colorSchemes.ContainsKey(mode))
            {
                mode = TreemapViewColoringMode.None;
            }

            Color[] scheme = _colorSchemes[mode];

            int colorIndex = 9;

            switch (mode)
            {
                case TreemapViewColoringMode.Global:
                    colorIndex = symbol.GlobalScope;
                    break;
                case TreemapViewColoringMode.Section:
                    colorIndex = symbol.SectionName.Hash8Bit();
                    break;
                case TreemapViewColoringMode.FileName:
                    colorIndex = symbol.FileName.Hash8Bit();
                    break;
            };

            colorIndex = System.Math.Abs(colorIndex) % scheme.Length;

            node.BackColor = scheme[colorIndex];
        }


        /// <summary>
        /// A simple 8 bit string hash.
        /// </summary>
        /// <param name="input">The string to hash.</param>
        /// <returns>The hash of the string.</returns>
        private static byte Hash8Bit(this string input)
        {
            byte result = 0b1010_0101;
            byte[] bytes = Encoding.UTF8.GetBytes(input);

            foreach (var b in bytes)
            {
                result = (byte)(result ^ b);
            }

            return result;
        }
    }
}
