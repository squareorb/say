// Copyright (c) squareorb. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;

namespace say
{
    class Program
    {
        private SpeechSynthesizer synthesizer;

        static public string voice = "Microsoft David Desktop";

        static void Main(string[] args)
        {
            // Initialize a new instance of the SpeechSynthesizer.
            SpeechSynthesizer synth = new SpeechSynthesizer();

            // Configure the default audio output.
            synth.SetOutputToDefaultAudioDevice();

            //Initialize variables
            string textOutput = "";
            bool breakout = false;

            //Require arguments
            if (args.Length == 0)
            {
                DisplayHelp();
            }

            //Process arguments
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-h":
                        DisplayHelp();
                        breakout = true;
                        break;
                    case "-v":
                        if (i + 1 < args.Length)
                        {
                            voice = args[i + 1];
                            if (voice == "male" || voice == "Male")
                            {
                                voice = "Microsoft David Desktop";
                            }
                            else if (voice == "female" || voice == "Female")
                            {
                                voice = "Microsoft Zira Desktop";
                            }
                            else if (voice.Contains("-"))
                            {
                                Console.WriteLine("You must specify a voice!");
                                DisplayHelp();
                                breakout = true;
                            }
                            i++;
                        }
                        else
                        {
                            Console.WriteLine("You must specify a voice!");
                            DisplayHelp();
                            breakout = true;
                        }
                        break;
                    case "-o":
                        if (i + 1 < args.Length)
                        {
                            if (args[i + 1].Contains("-"))
                            {
                                Console.WriteLine("You must specify a filename!");
                                breakout = true;
                            };

                            if (!breakout)
                            {
                                synth.SetOutputToWaveFile(args[i + 1] + ".wav");
                                i++;
                            }
                        }
                        else
                        {
                            Console.WriteLine("You must specify a filename!");
                            DisplayHelp();
                            breakout = true;
                        }
                        break;
                    default:
                        char dash = '-';
                        if (args[i][0] == dash)
                        {
                            Console.WriteLine("Unrecognized option '" + args[i] +"'");
                            DisplayHelp();
                            breakout = true;
                        }
                        else
                        {
                            textOutput = textOutput + " " + args[i];
                        }
                        
                        break;
                }
                if (breakout)
                {
                    break;
                }

            }

            if (!breakout) {               
                try {
                    synth.SelectVoice(voice);
                }
                catch(ArgumentException e)
                {
                    Console.WriteLine("Unrecognized voice, using default. (Voices are case sensitive)");
                }
                
                // Speak a string or write to file
                synth.Speak(textOutput);
                //Console.WriteLine(textOutput + " " + args.Length);
            }
        }

        static public void DisplayHelp()
        {
            Console.WriteLine("Usage: say [-v voice] [-o out] [message]");
            Console.WriteLine("");
            Console.WriteLine("Example: say Hello world");
            Console.WriteLine("Example: say -v female -o myfile Hello world");
            Console.WriteLine("Example: say -v \"Microsoft David Desktop\"  Hello world");

        }
    }
}
