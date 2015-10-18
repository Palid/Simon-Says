using System;
using System.Collections.Generic;
using System.Timers;
using Corale.Colore.Razer.Keyboard;
using Corale.Colore.Core;

namespace SimonSays
{
    enum UsableKeys
    {
        Q,W,E,R,T,Y,U,I,O,P,
        A,S,D,F,G,H,J,K,L,
        Z,X,C,V,B,N,M
    }

    public class Game
    {
        private List<UsableKeys> simonSaysKeys = new List<UsableKeys>();
        private Random random = new Random();
        private Timer keyboardAnimationTimer = new Timer();
        private int currentKeyIndex = 0;
        private AnimatedKey currentKey;
        public bool isFinished {get; set;}
        private IKeyboard keyboard = Keyboard.Instance;
        private int animationFramesCount = 0;
        public bool isVotingTime { get; private set; }
        private UsableKeys[] usableKeysValues = (UsableKeys[])Enum.GetValues(typeof(UsableKeys));
        private Key[] keyboardKeysValues = (Key[])Enum.GetValues(typeof(Key));

        public Game()
        {
            init();
        }
        private void init()
        {
            keyboardAnimationTimer.Interval = 250;
            keyboardAnimationTimer.Elapsed += animationFrame;
            isFinished = false;
        }
        public void destroy()
        {

        }
        private UsableKeys getRandomKey()
        {
            return usableKeysValues[random.Next(0, usableKeysValues.Length)];
        }
        public void start()
        {
            for (int i = 0; i < 4; i++)
            {
                var key = getRandomKey();
                simonSaysKeys.Add(key);
            }
            anotherKey();

        }

        private void votingTime()
        {
            Console.WriteLine("Voting time activated. Does nothing at the moment.");
        }

        private void animationFrame(Object source, ElapsedEventArgs e)
        {
            if (animationFramesCount == 10)
            {
                keyboardAnimationTimer.Stop();
                currentKeyIndex++;
                if (currentKeyIndex < simonSaysKeys.Count)
                {
                    anotherKey();
                    animationFramesCount = 0;

                } else
                {
                    keyboard.Clear();
                    votingTime();

                }
            } else
            {
                var currentEffect = currentKey.getNewEffect();
                keyboard.SetCustom(currentEffect);
                animationFramesCount++;
            }
        }
        private Key getShinyKey(UsableKeys key)
        {
            var re = Enum.Parse(typeof(Key), key.ToString().ToUpper());
            return (Key)re;
        }
        private void anotherKey()
        {
            currentKey = new AnimatedKey(getShinyKey(simonSaysKeys[currentKeyIndex]), Color.Orange);
            keyboardAnimationTimer.Start();

        }
    }

}