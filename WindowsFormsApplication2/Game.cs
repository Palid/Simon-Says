using System;
using System.Collections.Generic;
using System.Timers;
using Corale.Colore.Razer.Keyboard;
using Corale.Colore.Core;

namespace SimonSays
{
    enum ScoringKeys
    {
        F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12
    }
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
        private Timer failAnimationTimer = new Timer();
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


        int currentLevel = 1;

        int redPos = 0;
        int bluePos = 1;
        int pinkPos = 2;
        int redPos2 = 3;
        Color failOne = new Color(System.Drawing.Color.Red);
        Color failTwo = new Color(System.Drawing.Color.OrangeRed);
        Color failThree = new Color(System.Drawing.Color.MediumVioletRed);

        public Game(Color keysColor)
        {
            init();
            this.keysColor = keysColor;
        }

        public Game(System.Drawing.Color keysColor)
        {
            init();
            this.keysColor = new Color(keysColor);
        }

        private void init()
        {
            keyboardAnimationTimer.Interval = 20;
            keyboardAnimationTimer.Elapsed += animationFrame;
            failAnimationTimer.Interval = 125;
            failAnimationTimer.Elapsed += failAnimationFrame;
            votingAnimationTimer.Interval = 100;
            votingAnimationTimer.Elapsed += (sender, args) =>
            {
                if (!hasFinishedLevel)
                {
                    try
                    {
                        keyboard.Clear();
                        var effect = Corale.Colore.Razer.Keyboard.Effects.Custom.Create();
                        scoringFrames(effect);
                        keyboard.SetCustom(effect);
                    }
                    catch (Exception)
                    {
                        //Ignore native code exceptions because SD
                    }
                } else
                {
                    currentLevel++;
                    currentKeyIndex = 0;
                    hasFinishedLevel = false;
                    keyboard.Clear();
                    startNextLevel();
                    animationFramesCount = 0;
                    Console.WriteLine(currentLevel);

                }
                votingAnimationTimer.Stop();
            };

            isFinished = false;
        }
        public void destroy()
        {
            stopAnimations();
            resetGameVariables();
            stopAnimations();
            keyboard.Clear();
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
            currentLevel = 1;
        }

        private void stopAnimations()
        {
            keyboardAnimationTimer.Stop();
            failAnimationTimer.Stop();
            votingAnimationTimer.Stop();
            keyboard.Clear();
        }

        private UsableKeys getRandomKey()
        {
            return usableKeysValues[random.Next(0, usableKeysValues.Length)];
        }

        public void showFailure()
        {
            stopAnimations();
            failAnimationTimer.Start();
            isFinished = true;
            isVotingTime = false;
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
                showFailure();
            }
            return isSame;
        }

        private void failAnimationFrame(Object source, ElapsedEventArgs e)
        {
            var tempRed = redPos;
            var tempBlue = bluePos;
            var tempPink = pinkPos;
            var tempRed2 = redPos2;
            redPos = tempBlue;
            bluePos = tempPink;
            pinkPos = tempRed2;
            redPos2 = tempRed;
            var arr = new Color[Constants.MaxRows][];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = new Color[Constants.MaxColumns];
                for (int y = 0; y < Constants.MaxColumns; y++)
                {
                    var modulo = y % 4;
                    if (modulo == pinkPos)
                    {
                        arr[i][y] = failOne;
                    }
                    else if (modulo == redPos || modulo == redPos2)
                    {
                        arr[i][y] = failTwo;

                    }
                    else if (modulo == bluePos)
                    {
                        arr[i][y] = failThree;

                    }
                }
            }
            keyboard.SetGrid(arr);
        }

        private Corale.Colore.Razer.Keyboard.Effects.Custom scoringFrames(Corale.Colore.Razer.Keyboard.Effects.Custom effect)
        {
            Array ScoringKeys = Enum.GetValues(typeof(ScoringKeys));
            int fKeysAmount = currentLevel;
            for (int i = 12; i > 0; i--)
            {
                int possibleKey = fKeysAmount - i;
                if (possibleKey == 0)
                {
                    ScoringKeys Fkey = (ScoringKeys)ScoringKeys.GetValue(i - 1);
                    Key RazerFKey = (Key)Enum.Parse(typeof(Key), Fkey.ToString().ToUpper());
                    effect[RazerFKey] = Color.Orange;
                    break;
                } else if (possibleKey > 0)
                {
                    ScoringKeys Fkey = (ScoringKeys)ScoringKeys.GetValue(i - 1);
                    Key RazerFKey = (Key)Enum.Parse(typeof(Key), Fkey.ToString().ToUpper());
                    fKeysAmount = possibleKey;
                    effect[RazerFKey] = Color.Orange;
                }
            }
            return effect;

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
                    var effect = Corale.Colore.Razer.Keyboard.Effects.Custom.Create();
                    scoringFrames(effect);
                    keyboard.SetCustom(effect);
                }
            } else
            {
                var currentEffect = Corale.Colore.Razer.Keyboard.Effects.Custom.Create();
                currentEffect[currentKey.razerKey] = currentKey.getKeyColor();
                var scoredEffect = scoringFrames(currentEffect);
                try
                {
                    keyboard.SetCustom(scoredEffect);

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
            if (isStarted && !isFinished && currentLevel != 78)
            {
                if (keyboardAnimationTimer.Interval >= 6)
                {
                    keyboardAnimationTimer.Interval -= 5;
                }
                hasFinishedLevel = false;
                simonSaysKeys.Add(getRandomKey());
                startAnimatingKey();
            }
        }
    }

}