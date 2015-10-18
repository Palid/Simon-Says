using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Corale.Colore.Razer;
using Corale.Colore.Razer.Keyboard;
using Corale.Colore.Core;
using System.Timers;

namespace SimonSays
{
    class AnimatedKey
    {
        public Key key { get; private set; }
        public Color color { get; private set; }
        public double red { get; private set; }
        public double green { get; private set; }
        public double blue { get; private set; }
        public double alpha { get; private set; }

        public AnimatedKey(Key key, Color color)
        {
            this.key = key;
            this.color = color;
            this.red = color.R;
            this.green = color.G;
            this.blue = color.B;
            this.alpha = color.A;
        }

        private Double getNewRGBValue(int colorHue)
        {
            if (colorHue <= 0)
            {
                return 0;
            }
            var newHue = colorHue - (colorHue * 0.05);
            return newHue >= 0 ? newHue : 0;        
        }
        private List<double> darkenColor(int Red, int Green, int Blue)
        {
            var amt = 0;
            var R = (Red >> 16) + amt;
            var G = (Green >> 8 & 0x00FF) + amt;
            var B = (Blue & 0x0000FF) + amt;
            var list = new List<double>(2);
            list.Add(R);
            list.Add(G);
            list.Add(B);
            return list;
        }
        private Color getNewColor(Color color)
        {
            var darkenedColor = darkenColor(color.R, color.G, color.B);
            red = darkenedColor[0];
            green = darkenedColor[1];
            blue = darkenedColor[2];
            //temporarily not touching alpha, as it doesn't work at all.
            //Yet.
            alpha = color.A;
            
            return new Color(red, green, blue, alpha);
        }
        public Corale.Colore.Razer.Keyboard.Effects.Custom getNewEffect()
        {
            color = getNewColor(color);
            var currentEffect = Corale.Colore.Razer.Keyboard.Effects.Custom.Create();
            currentEffect[this.key] = color;

            return currentEffect;
        }
    }
}
