using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Rendering;

public struct Color
{
    public float R { get; set; }
    public float G { get; set; }
    public float B { get; set; }
    public float A { get; set; }

    public Color(float r, float g, float b)
    {
        R = r;
        G = g;
        B = b;
        A = 1;
    }
    public Color(float r, float g, float b, float a)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }
    public Color()
    {
        R = 255;
        G = 255;
        B = 255;
        A = 1;
    }

    public static readonly Color White = new(255, 255, 255);
    public static readonly Color Black = new(0, 0, 0);
    public static readonly Color Red = new(255, 0, 0);
    public static readonly Color Green = new(0, 255, 0);
    public static readonly Color Blue = new(0, 0, 255);
    public static readonly Color Cyan = new(0, 255, 255);
    public static readonly Color Magenta = new(255, 0, 255);
    public static readonly Color Yellow = new(255, 255, 0);
    public static readonly Color Gray = new(128, 128, 128);
    public static readonly Color LightGray = new(211, 211, 211);
    public static readonly Color DarkGray = new(169, 169, 169);
    public static readonly Color Maroon = new(128, 0, 0);
    public static readonly Color Olive = new(128, 128, 0);
    public static readonly Color Navy = new(0, 0, 128);
    public static readonly Color Purple = new(128, 0, 128);
    public static readonly Color Teal = new(0, 128, 128);
    public static readonly Color Silver = new(192, 192, 192);
    public static readonly Color Aqua = new(0, 255, 255);
    public static readonly Color Fuchsia = new(255, 0, 255);
    public static readonly Color Lime = new(0, 255, 0);
    public static readonly Color YellowGreen = new(154, 205, 50);
    public static readonly Color DarkRed = new(139, 0, 0);
    public static readonly Color DarkGreen = new(0, 100, 0);
    public static readonly Color DarkBlue = new(0, 0, 139);
    public static readonly Color DarkCyan = new(0, 139, 139);
    public static readonly Color DarkMagenta = new(139, 0, 139);
    public static readonly Color AliceBlue = new(240, 248, 255);
    public static readonly Color AntiqueWhite = new(250, 235, 215);
    public static readonly Color AquaMarine = new(127, 255, 212);
    public static readonly Color Azure = new(240, 255, 255);
    public static readonly Color Beige = new(245, 245, 220);
    public static readonly Color Bisque = new(255, 228, 196);
    public static readonly Color BlanchedAlmond = new(255, 235, 205);
    public static readonly Color BlueViolet = new(138, 43, 226);
    public static readonly Color Brown = new(165, 42, 42);
    public static readonly Color BurlyWood = new(222, 184, 135);
    public static readonly Color CadetBlue = new(95, 158, 160);
    public static readonly Color Chartreuse = new(127, 255, 0);
    public static readonly Color Chocolate = new(210, 105, 30);
    public static readonly Color Coral = new(255, 127, 80);
    public static readonly Color CornflowerBlue = new(100, 149, 237);
    public static readonly Color Cornsilk = new(255, 248, 220);
    public static readonly Color Crimson = new(220, 20, 60);
    public static readonly Color DarkGoldenRod = new(184, 134, 11);
    public static readonly Color DarkKhaki = new(189, 183, 107);
    public static readonly Color DarkOliveGreen = new(85, 107, 47);
    public static readonly Color DarkOrange = new(255, 140, 0);
    public static readonly Color DarkOrchid = new(153, 50, 204);
    public static readonly Color DarkSalmon = new(233, 150, 122);
    public static readonly Color DarkSeaGreen = new(143, 188, 143);
    public static readonly Color DarkSlateBlue = new(72, 61, 139);
    public static readonly Color DarkSlateGray = new(47, 79, 79);
    public static readonly Color DarkTurquoise = new(0, 206, 209);
    public static readonly Color DarkViolet = new(148, 0, 211);
    public static readonly Color DeepPink = new(255, 20, 147);
    public static readonly Color DeepSkyBlue = new(0, 191, 255);
    public static readonly Color DimGray = new(105, 105, 105);
    public static readonly Color DodgerBlue = new(30, 144, 255);
    public static readonly Color FireBrick = new(178, 34, 34);
    public static readonly Color FloralWhite = new(255, 250, 240);
    public static readonly Color ForestGreen = new(34, 139, 34);
    public static readonly Color Gainsboro = new(220, 220, 220);
    public static readonly Color GhostWhite = new(248, 248, 255);
    public static readonly Color Gold = new(255, 215, 0);
    public static readonly Color GoldenRod = new(218, 165, 32);
    public static readonly Color GreenYellow = new(173, 255, 47);
    public static readonly Color HoneyDew = new(240, 255, 240);
    public static readonly Color HotPink = new(255, 105, 180);
    public static readonly Color IndianRed = new(205, 92, 92);
    public static readonly Color Indigo = new(75, 0, 130);
    public static readonly Color Ivory = new(255, 255, 240);
    public static readonly Color Khaki = new(240, 230, 140);
    public static readonly Color Lavender = new(230, 230, 250);
    public static readonly Color LavenderBlush = new(255, 240, 245);
    public static readonly Color LawnGreen = new(124, 252, 0);
    public static readonly Color LemonChiffon = new(255, 250, 205);
}
