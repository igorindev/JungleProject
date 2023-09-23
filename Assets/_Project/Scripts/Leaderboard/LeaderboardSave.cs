using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class LeaderboardSave
{
    List<Player> _players = new List<Player>();

    public void Order()
    {
        _players = _players.OrderByDescending(x => x._score).ThenByDescending(x => x._wave).ToList();
    }

    public List<Player> Players => _players;

    public void AddPlayer(string name, int wave, int score)
    {
        Player player = new Player(name, wave, score);
        Players.Add(player);

        Order();
    }

    [Serializable]
    public class Player
    {
        public string _name;
        public int _wave;
        public int _score;

        public Player()
        {

        }

        public Player(string name, int wave, int score)
        {
            _name = name;
            _wave = wave;
            _score = score;
        }
    }
}
