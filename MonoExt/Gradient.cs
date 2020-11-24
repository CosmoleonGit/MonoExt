using Microsoft.Xna.Framework;
using SharpHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoExt
{
    public struct Gradient
    {
        public Gradient(GradientStop[] _stops)
        {
            stops = _stops;
        }

        public GradientStop[] stops;

        public Color Evaluate(float p)
        {
            if (stops.Length == 0)
                throw new InvalidOperationException("There must be at least 1 stop in this gradient.");
            else if (stops.Length == 1)
                return stops[0].color;
            else
            {
                GradientStop? first = null, 
                              second = null;

                for (int i = 0; i < stops.Length; i++)
                {
                    GradientStop gS = stops[i];

                    if (gS.point > p)
                    {
                        second = gS;
                        break;
                    } else
                    {
                        first = gS;
                    }
                }

                if (first == null)
                    return stops[0].color;
                else if (second == null)
                    return first.Value.color;
                else
                    return Color.Lerp(first.Value.color, 
                                      second.Value.color, 
                                      (second.Value.point - first.Value.point).Map(first.Value.point, second.Value.point, 0, 1));
            }
        }
    }

    public struct GradientStop
    {
        public GradientStop(Color c, float p)
        {
            color = c;
            point = p;
        }

        public Color color;
        public float point;
    }
}
