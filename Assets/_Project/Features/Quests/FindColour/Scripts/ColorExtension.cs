using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FindColorExtension
{
    public struct NewColor
    {
        public static Color orange
        {
            get
            {
                return new Color(1f, 0.5f, 0f, 1f);
            }
        }

        public static Color violet
        {
            get
            {
                return new Color(0.5f, 0f, 1f, 1f);
            }
        }

        public static Color purple
        {
            get
            {
                return new Color(1f, 0.31f, 1f, 1f);                
            }
        }
    }

    public static class ColorExtension
    {
        public static string GetRuString(this Color color)
        {
            string ruString = "";

            if (color == Color.yellow)
            {
                ruString = "желтый";
            }
            else if (color == Color.white)
            {
                ruString = "белый";
            }
            else if (color == Color.black)
            {
                ruString = "черный";
            }
            else if (color == Color.blue)
            {
                ruString = "синий";
            }
            else if (color == Color.cyan)
            {
                ruString = "голубой";
            }
            else if (color == Color.green)
            {
                ruString = "зеленый";
            }
            else if (color == Color.red)
            {
                ruString = "красный";
            }
            else if (color == NewColor.orange)
            {
                ruString = "оранжевый";
            }
            else if (color == NewColor.purple)
            {
                ruString = "розовый";
            }
            else if (color == NewColor.violet)
            {
                ruString = "фиолетовый";
            }

            return ruString;
        }
    }

}

