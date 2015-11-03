using System.Collections.Generic;
using Corale.Colore.Razer.Keyboard;
using Corale.Colore.Core;

namespace SimonSays
{
    class AnimatedKey
    {
        public string key { get; set; }
        public Key razerKey { get; private set; }
        public Color color { get; private set; }
        public double red { get; private set; }
        public double green { get; private set; }
        public double blue { get; private set; }
        public double alpha { get; private set; }

        public AnimatedKey(Key key, Color color)
        {
            this.razerKey = key;
            this.color = color;
            this.red = color.R;
            this.green = color.G;
            this.blue = color.B;
            this.alpha = color.A;
        }

        private List<double> darkenColor(int Red, int Green, int Blue)
        {
            var amt = 0.1;
            var R = (Red - Red * amt);
            var G = (Green - Green * amt);
            var B = (Blue - Blue * amt);
            var list = new List<double>();
            list.Add(R);
            list.Add(G);
            list.Add(B);
            return list;
        }
        private Color getNewColor(Color color)
        {
            var magicNumber = 255;
            var darkenedColor = darkenColor((int)red, (int)green, (int)blue);
            red = darkenedColor[0];
            green = darkenedColor[1];
            blue = darkenedColor[2];
            //temporarily not touching alpha, as it doesn't work at all.
            //Yet.
            alpha = color.A;

            return new Color(red / magicNumber, green / magicNumber, blue / magicNumber, alpha);
        }
        public Color getKeyColor()
        {
            color = getNewColor(color);
            return color;
        }
    }
}
