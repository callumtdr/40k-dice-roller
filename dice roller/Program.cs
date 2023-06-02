using System.Reflection.Metadata.Ecma335;

namespace dice_roller
{
    internal class Program
    {
        static void Main(string[] args)
        {

            bool playing = true;

            Console.WriteLine("Welcome to the dice roller app.");
            Console.ReadLine();

            while (playing)
            {
                Console.Clear();

                int manyDice = 0;
                int hitsOn = 0;
                int explSix = 0;
                int numOfHits = 0;
                int wepStr = 0;
                int unitTough = 0;
                int woundsOn = 0;
                int numOfWounds = 0;
                int apValue = 0;
                int defSave = 0;
                int woundsTaken = 0;

                int[] woundHits = { 2, 3, 4, 5, 6 };

                List<int> diceRolls = new List<int>();

                DiceRoller roller = new DiceRoller();

                bool validNum = false;

                while (!validNum)
                {
                    Console.WriteLine("How many Dice are to be rolled?");

                    string input = Console.ReadLine();
                    
                    if (int.TryParse(input, out manyDice))
                    {
                        validNum = true;
                    }
                    else
                    {
                        Console.WriteLine("Try entering a number");
                    }
                }
                
                validNum = false;

                while (!validNum)
                {
                    Console.WriteLine("What is the BS or WS?");

                    string input = Console.ReadLine();

                    if (int.TryParse(input, out hitsOn))
                    {
                        validNum = true;
                    }
                    else 
                    { 
                        Console.WriteLine("Try Entering a number"); 
                    }

                }
                

                roller.DiceRoll(manyDice, diceRolls);

                //Detecting Nat 1's

                bool hasOnes = diceRolls.Contains(1);

                bool validChoice = false;

                if (hasOnes)
                {
                    int onesCount = diceRolls.Count(d => d == 1);

                    Console.WriteLine($"You rolled {onesCount} 1's.");    

                    while (!validChoice)
                    {
                        Console.WriteLine("Would you like to re roll all 1's? Y/N...");

                        string reroll = Console.ReadLine();

                        if (reroll == "y")
                        {
                            int toReRoll = 0;

                            for (int i = 0; i < diceRolls.Count; i++)
                            {
                                if (diceRolls[i] == 1)
                                {
                                    diceRolls.RemoveAt(i);
                                    toReRoll++;
                                    i--;
                                }
                            }

                            Console.WriteLine("Rerolling 1's...");

                            manyDice = toReRoll;

                            roller.DiceRoll(manyDice, diceRolls);

                            validChoice = true;

                        }

                        else if (reroll == "n")
                        {
                            validChoice = true;
                        }

                        else
                        {
                            Console.WriteLine("I dont understand, try again");
                            validChoice = false;
                        }
                    }
                }

                //Exploding 6's
                bool hasSixes = diceRolls.Contains(6);

                validChoice = false;

                if (hasSixes) 
                {
                    int sixCount = diceRolls.Count(d => d == 6);

                    Console.WriteLine($"You rolled {sixCount} 6's.");

                    while (!validChoice)
                    {
                        Console.WriteLine("Do your Sixes explode? Y/N");

                        string sixSplode = Console.ReadLine();

                        if (sixSplode == "y")
                        {

                            

                            validNum = false;

                            while (!validNum)
                            {
                                Console.WriteLine("how many hits does it apply?");

                                string input = Console.ReadLine();

                                if (int.TryParse(input, out explSix))
                                {
                                    validNum = true;
                                }
                                else
                                {
                                    Console.WriteLine("Try Entering a number");
                                }

                            }

                            for (int i = 0; i < sixCount; i++)
                            {
                                numOfHits = numOfHits + explSix - 1;
                            }
                            Console.WriteLine($"You've made {numOfHits} extra hit's");

                            validChoice = true;

                        }

                        else if (sixSplode == "n")
                        {
                            validChoice = true;
                        }

                        else
                        {
                            Console.WriteLine("I dont understand, try again");
                            validChoice = false;
                        }

                    }
                }


                //Adding Modifiers.

                validChoice = false;

                while (!validChoice)
                {
                    Console.WriteLine("Do your attacks have modifiers? Y/N");

                    string hasMod = Console.ReadLine();

                    if (hasMod == "y")
                    {
                        Console.WriteLine("Enter your modifier");

                        string input = Console.ReadLine();
                        int mod = 0;
                        if (int.TryParse(input, out mod))
                        {
                            for (int i = 0; i < diceRolls.Count; i++)
                            {
                                diceRolls[i] += mod;
                            }
                        }
                        
                        validChoice = true;
                    }
                    else if (hasMod == "n") 
                    { 
                        validChoice = true;
                    }
                    else
                    {
                        Console.WriteLine("I dont understand, try again");
                        validChoice = false;
                    }
                }


                foreach (int d in diceRolls)
                {
                    if (d >= hitsOn)
                    {
                        numOfHits++;
                    }
                }

                Console.WriteLine($"You've hit {numOfHits} times.");
                Console.ReadLine();

                //To Wound
                validNum = false;

                while (!validNum)
                {
                    Console.WriteLine("Enter weapon Strength");
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out wepStr))
                    {
                        validNum = true;
                    }
                    else
                    {
                        Console.WriteLine("Try entering a number");
                    }
                }


                validNum = false;

                while (!validNum)
                {
                    Console.WriteLine("Enter Unit Toughness");
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out unitTough))
                    {
                        validNum = true;
                    }
                    else
                    {
                        Console.WriteLine("Try entering a number");
                    }
                }

                //calculate WoundsOn

                if (wepStr >= 2 * unitTough)
                {
                    woundsOn = woundHits[0];
                }
                else if (wepStr > unitTough)
                {
                    woundsOn = woundHits[1];
                }
                else if (wepStr == unitTough)
                {
                    woundsOn = woundHits[2];
                }
                else if (unitTough >= 2 * wepStr)
                {
                    woundsOn = woundHits[4];
                }
                else if (wepStr < unitTough)
                {
                    woundsOn = woundHits[3];
                }

                Console.WriteLine($"you will wound on {woundsOn}+");
                Console.WriteLine("Hit any key to roll your wounds...");
                Console.ReadLine();

                manyDice = numOfHits;

                diceRolls.Clear();

                roller.DiceRoll(manyDice, diceRolls);

                foreach (var item in diceRolls)
                {
                    if (item >= woundsOn)
                    {
                        numOfWounds++;
                    }
                }

                Console.WriteLine($"you make {numOfWounds} Wounds");

                Console.ReadLine(); 

                validNum = false;

                while (!validNum)
                {
                    Console.WriteLine("what is the attacks AP?");
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out apValue))
                    {
                        validNum = true;
                    }
                    else
                    {
                        Console.WriteLine("Try entering a number");
                    }
                }

                validNum = false;

                while (!validNum)
                {
                    Console.WriteLine("What is the defending units Save?");
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out defSave))
                    {
                        validNum = true;
                    }
                    else
                    {
                        Console.WriteLine("Try entering a number");
                    }
                }

                diceRolls.Clear();
                manyDice = numOfWounds;

                roller.DiceRoll(manyDice, diceRolls);

                foreach (var item in diceRolls)
                {
                    if (item < defSave + apValue)
                    {
                        woundsTaken++;
                    }
                }

                Console.WriteLine($"Defending unit has received {woundsTaken} wounds");
                Console.ReadLine();

            }
        }
    }
}