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
        private Timer votingAnimationTimer = new Timer();
        private AnimatedKey currentKey;
        private IKeyboard keyboard = Keyboard.Instance;
        private int animationFramesCount = 0;
        private UsableKeys[] usableKeysValues = (UsableKeys[])Enum.GetValues(typeof(UsableKeys));
        private Key[] keyboardKeysValues = (Key[])Enum.GetValues(typeof(Key));
        private int currentKeyIndex = 0;
        public bool isFinished { get; set; }
        private int startingKeysAmount = 3;
        public bool isVotingTime { get; private set; }
        private bool isStarted = false;
        private bool hasFinishedLevel = false;
        private Color keysColor { get; set; }

        readonly int animationCount = 30; //Great shadowing animation for key.
        readonly bool DEBUG = true;

        public Game(Color keysColor)
        {
            init();
            this.keysColor = keysColor;
        }

        private void init()
        {
            keyboardAnimationTimer.Interval = 80;
            keyboardAnimationTimer.Elapsed += animationFrame;
            votingAnimationTimer.Interval = 100;
            votingAnimationTimer.Elapsed += (sender, args) =>
            {
                if (!hasFinishedLevel)
                {
                    try
                    {
                        keyboard.Clear();
                    }
                    catch (Exception)
                    {
                        //Ignore native code exceptions because SD
                    }
                } else
                {
                    currentKeyIndex = 0;
                    hasFinishedLevel = false;
                    keyboard.Clear();
                    startNextLevel();
                    animationFramesCount = 0;

                }
                votingAnimationTimer.Stop();
            };

            isFinished = false;
        }
        public void destroy()
        {
            keyboardAnimationTimer.Stop();
            votingAnimationTimer.Stop();
        }

        public void start()
        {
            if (!isStarted)
            {
                for (int i = 0; i < startingKeysAmount; i++)
                {
                    var key = getRandomKey();
                    simonSaysKeys.Add(key);
                }
                isStarted = true;
                startNextLevel();
            }

        }

        private void resetGameVariables()
        {
            isStarted = false;
            isFinished = false;
            keyboardAnimationTimer.Interval = 80;

        }

        private UsableKeys getRandomKey()
        {
            return usableKeysValues[random.Next(0, usableKeysValues.Length)];
        }

        public bool verifyPushedButton(char key)
        {
            var currentKey = simonSaysKeys[currentKeyIndex];
            var isSame = currentKey.ToString() == key.ToString().ToUpper();
            if (isSame)
            {
                keyboard.SetAll(Color.Green);
                currentKeyIndex++;
                votingAnimationTimer.Start();
                if (currentKeyIndex == simonSaysKeys.Count)
                {
                    currentKeyIndex = 0;
                    hasFinishedLevel = true;
                    isVotingTime = false;
                }
            } else
            {
                isFinished = true;
                isVotingTime = false;
                keyboard.SetAll(Color.Red);
            }
            return isSame;
        }

        private void animationFrame(Object source, ElapsedEventArgs e)
        {
            if (animationFramesCount == this.animationCount)
            {
                keyboardAnimationTimer.Stop();
                currentKeyIndex++;
                if (currentKeyIndex < simonSaysKeys.Count)
                {
                    startAnimatingKey();
                    animationFramesCount = 0;

                } else
                {
                    keyboard.Clear();
                    currentKeyIndex = 0;
                    isVotingTime = true;

                }
            } else
            {
                var currentEffect = currentKey.getNewEffect();
                try
                {
                    keyboard.SetCustom(currentEffect);

                } catch (Exception)
                {
                    //Native code SDK exception. Ignore it. 
                    //However, Colore guys would probably like it.
                }
                animationFramesCount++;
            }
        }
        private Key getShinyKey(UsableKeys key)
        {
            var re = Enum.Parse(typeof(Key), key.ToString().ToUpper());
            return (Key)re;
        }
        private void startAnimatingKey()
        {
            if (DEBUG)
            {
                Console.WriteLine("Current key is: " + simonSaysKeys[currentKeyIndex]);
            }
            currentKey = new AnimatedKey(getShinyKey(simonSaysKeys[currentKeyIndex]), keysColor);
            keyboardAnimationTimer.Start();

        }
        public void startNextLevel()
        {
            if (isStarted && !isFinished)
            {
                keyboardAnimationTimer.Interval -= 5;
                hasFinishedLevel = false;
                simonSaysKeys.Add(getRandomKey());
                startAnimatingKey();
            }
        }
    }

}