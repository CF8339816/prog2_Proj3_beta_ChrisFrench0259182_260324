using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog2_Proj3_beta_ChrisFrench0259182_260324
{

    public class DamageSystem
    {

        public DamageSystem()
        {



            if (Program.map._mapsCurrent[Program.player._y][Program.player._x] == '%')// applies lava damage 
            {
                Program.player._health = Program.player._health - 30;

                if (Program.player._health < 0)
                {
                    Program.player._health = 0;
                }
                HUD.AnakinMustafar();
               
                if (Program.player._health == 0)
                {
                    Program.isPlaying = false;
                }
            }

        }
    }
}
