﻿using System;
using System.Windows.Media;

namespace InternalsViewer.Ui.TestApp.Helpers.Colours
{
    /// <summary>
    /// Convert between HSV (Hue, Saturation, Value) and RGB colour
    /// </summary>
    /// <remarks>
    /// Adapted from 'A Primer on Building a Color Picker User Control with GDI+ in Visual Basic .NET or C#' by Ken Getz
    /// MSDN Magazine July 2003 - http://msdn.microsoft.com/en-gb/magazine/cc164113.aspx
    /// </remarks>
    internal class HsvColour
    {
        internal static Color HsvToColor(int hue, int saturation, int value)
        {
            double r = 0;
            double g = 0;
            double b = 0;

            var h = (double)hue / 255 * 360 % 360;
            var s = (double)saturation / 255;
            var v = (double)value / 255;

            if (s == 0)
            {
                r = v;
                g = v;
                b = v;
            }
            else
            {
                var sectorPos = h / 60;
                var sectorNumber = (int)Math.Floor(sectorPos);
                var fractionalSector = sectorPos - sectorNumber;

                var p = v * (1 - s);
                var q = v * (1 - s * fractionalSector);
                var t = v * (1 - s * (1 - fractionalSector));

                switch (sectorNumber)
                {
                    case 0:
                        r = v;
                        g = t;
                        b = p;
                        break;

                    case 1:
                        r = q;
                        g = v;
                        b = p;
                        break;

                    case 2:
                        r = p;
                        g = v;
                        b = t;
                        break;

                    case 3:
                        r = p;
                        g = q;
                        b = v;
                        break;

                    case 4:
                        r = t;
                        g = p;
                        b = v;
                        break;

                    case 5:
                        r = v;
                        g = p;
                        b = q;
                        break;
                }
            }

            return Color.FromArgb(255, (byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
        }
    }
}
