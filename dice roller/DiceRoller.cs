using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dice_roller
{
    internal class DiceRoller
    {
        Random d6 = new Random();
        public void DiceRoll(int manyDice, List<int> diceRolls)
        {
            for (int i = 0; i < manyDice; i++)
            {
                int roll = d6.Next(1, 7);
                diceRolls.Add(roll);
            }
        }
    }
}
