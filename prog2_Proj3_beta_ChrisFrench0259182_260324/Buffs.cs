using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace prog2_Proj3_beta_ChrisFrench0259182_260324
{
    public class Buffs
    {   
        
        
        
      //  static int xp = 0;
        static int level = 1;
        static Random randomATK = new Random();
        static Random randomXP = new Random();
        public Buffs() 
        {
        
        }



        public static void IncreaseXP(int exp) // evil witchcraft

        {
          
            int giveXP = randomXP.Next(15, 30);

            exp = giveXP; //randomizes exp
            Player.plXP += exp; //modifies xp to be  xp + exp

            if (Player.plXP >= (level * 100)) // defines level of xp where level will increase
            {
                level++; //increases level by 1
                Player.plMaxHP += 10;
                Program.plaAtkUP += 5;

            }
        }
        public static void IncreaseATK(int Atk) // a warlock did it

        {
            
            int upATK = randomATK.Next(2, 5);

            Atk = upATK; //randomizes attack power up
            Program.plaAtkUP += Atk; //modifies adds attack buff to attack power

           
        }

        public static void IncreaseMaxHealth(int HpMax) // a warlock did it

        {

            int upHpMax = randomATK.Next(2, 5);

            HpMax = upHpMax; //randomizes attack power up
            Player.plMaxHP += HpMax; //modifies adds attack buff to attack power


        }

    }
}
