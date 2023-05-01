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
                int numOfHits = 0;


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
                    //Console.WriteLine(d);
                }

                Console.WriteLine($"youve hit {numOfHits} times.");
                Console.ReadLine();
            }
        }
    }
}