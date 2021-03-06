﻿using System.Drawing;
using System.Drawing.Drawing2D;

namespace InternalsViewer.Ui.TestApp.Helpers.Colours
{
    /// <summary>
    /// Creates the colours and keys for the application
    /// </summary>
    public class ExtentColour
    {
        /// <summary>
        /// Return the background colour for a given colour, used for gradients
        /// </summary>
        public static Color BackgroundColour(Color color)
        {
            var red = color.R + 32 > 255 ? 255 : color.R + 32;
            var green = color.G + 32 > 255 ? 255 : color.G + 32;
            var blue = color.B + 32 > 255 ? 255 : color.B + 32;

            return Color.FromArgb(color.A, red, green, blue);
        }

        /// <summary>
        /// Return the light background colour for a given colour, used for gradients
        /// </summary>
        public static Color LightBackgroundColour(Color color)
        {
            var red = color.R + 48 > 255 ? 255 : color.R + 48;
            var green =  color.G + 48 > 255 ? 255 : color.G + 48;
            var blue =  color.B + 48 > 255 ? 255 : color.B + 48;

            return Color.FromArgb(color.A, red, green, blue);
        }

        /// <summary>
        /// Creates a Key bitmap for a given colour with a gradient
        /// </summary>
        public static Bitmap KeyImage(Color color)
        {
            var key = new Bitmap(16, 16);
            var keyRectange = new Rectangle(0, 0, key.Width - 1, key.Height - 1);
            var g = Graphics.FromImage(key);

            using (var brush = new LinearGradientBrush(keyRectange,
                                                       color,
                                                       BackgroundColour(color),
                                                       LinearGradientMode.Horizontal))
            {
                g.FillRectangle(brush, keyRectange);
                g.DrawRectangle(SystemPens.ControlDark, keyRectange);
            }

            return key;
        }
    }
}
