using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame
{
    class StateManager
    {
        public enum GameState
        {
            initilize,
            menu,
            playground,
            inventory,
            store,
            multiplayer,
            tournamentloby,
            fightlobby,
            fightscene,
            wagerloby       
        }

        private GameState _currentstate;
        public GameState currentstate
        {
            get 
            { 
                return _currentstate; 
            }
            set 
            {
                _currentstate = value; 
            }
        }

        public StateManager()
        {
            _currentstate = GameState.initilize;
        }
    }
}
