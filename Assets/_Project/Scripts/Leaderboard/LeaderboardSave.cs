using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class LeaderboardSave
{
    List<Player> _players = new List<Player>();

    public void Order()
    {
        _players = _players.OrderByDescending(x => x._timer).ThenByDescending(x => x._wave).ToList();
    }

    public List<Player> Players => _players;

    public void AddPlayer(float timer, string name, int wave)
    {
        Player player = new Player(timer, name, wave);
        Players.Add(player);

        Order();
    }

    [Serializable]
    public class Player
    {
        public float _timer;
        public string _name;
        public int _wave;

        public Player()
        {

        }

        public Player(float timer, string name, int wave)
        {
            _timer = timer;
            _name = name;
            _wave = wave;
        }
    }
}
