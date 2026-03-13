using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog2_Proj3_beta_ChrisFrench0259182_260324
{
    public class MyEvents
    {
        public static int i = 1;
        public static List<Enemy> enemyRiderList = new List<Enemy>();

        public MyEvents()
        {
            MapCheck();
        
        } 


        public void MapCheck()
        {


            if (Program.map._currentMapIndex == 3)
            {
                enemyRiderList.Clear();
                enemyRiderList.Add(new Enemy("Slasher", 50, 4, 10, 'k', 25, ConsoleColor.Red));
                enemyRiderList.Add(new Enemy("Crasher", 20, 23, 8, 'k', 20, ConsoleColor.Red));
                enemyRiderList.Add(new Enemy("Harrier", 15, 12, 12, 'k', 30, ConsoleColor.Red));
                enemyRiderList.Add(new Enemy("PackAlphaNasty", 49, 19, 15, 'K', 40, ConsoleColor.DarkRed));
                enemyRiderList.Add(new Enemy("watcher", 2, 10, 0, '`', 1, ConsoleColor.DarkGray));



                PlayerRunner playerRunner = new PlayerRunner();

                EnemyRiders enemyRiders = new EnemyRiders(playerRunner);

                playerRunner.playerLocation(Program.player._x, Program.player._y);
            }
        } 
    }


        
        
   
        public class EnemyRiders
        {
           
            PlayerRunner _playerRunner;


            public EnemyRiders(PlayerRunner playerRunner)
            {
                playerRunner._ReachedPos += chasePlayerDown;
                //player._ReachedPosX += chasePlayerDown;
                //player._ReachedPosY += chasePlayerDown;

                _playerRunner = playerRunner;   
            }


            public void Unsubscribe()
            {
                _playerRunner._ReachedPos -= chasePlayerDown;
            }
            void chasePlayerDown()
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(60, 1);
                Console.Write($"enemy hears player's escape and is chasing them down");
            Console.ResetColor();

            foreach (var enemy in MyEvents.enemyRiderList)
            {
                Enemy.MoveTowards(enemy);
            }



        }


        }

        public class PlayerRunner
        {
            public int pPx;
            public int pPy;
            public int x; 
            public int y;

            public Action _ReachedPos = null;
            //public Action _ReachedPosY = null;
            //public Action _ReachedPosX = null;  

            public void playerLocation(int x, int y)

            {

                
                //x = (pPx = 18+i++);
                _ReachedPos?.Invoke();
                //_ReachedPosX?.Invoke();

                //y = (pPy = 10 + i++);
                _ReachedPos?.Invoke();
                //_ReachedPosY?.Invoke();



            }



        }


    }
          
    
  ///notes about events Action, Invokeand, and Unsubscribe  
    //Action myAction = null;


            ////if (myAction != null)
            ////{
            ////    myAction(); 
            ////}
            ////else
            ////{
            ////    Console.WriteLine("Action Null Could not invoke");
            ////}

            //myAction?.Invoke();//Above can be written as  


           // Console.ReadKey(true);

