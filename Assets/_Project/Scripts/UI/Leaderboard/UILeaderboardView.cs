using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface IUILeaderboardView
{
    void Setup(LeaderboardSave save, float timer, int wave, int _finalScore, Action onSave);
}

public class UILeaderboardView : UIView, IUILeaderboardView
{
    [SerializeField] Transform _contentTransform;
    [SerializeField] LeaderboardCard _leaderboardCard;
    [SerializeField] TMP_InputField _inputField;

    LeaderboardSave _save;
    float _timer;
    int _wave;
    int _score;

    readonly List<GameObject> _cards = new List<GameObject>();

    Action _onSave;

    public void Setup(LeaderboardSave save, float timer, int wave, int score, Action onSave)
    {
        _timer = timer;
        _save = save;
        _wave = wave;
        _onSave = onSave;
        _score = score;

        CreateCards();
    }

    void CreateCards()
    {
        for (int i = 0; i < _save.Players.Count; i++)
        {
            LeaderboardSave.Player item = _save.Players[i];
            CreateCardView(item, i + 1);
        }
    }

    void CreateCardView(LeaderboardSave.Player data, int pos)
    {
        LeaderboardCard leaderboardCard = Instantiate(_leaderboardCard, _contentTransform);
        leaderboardCard.Setup(pos, data._name, data._wave, data._score);
        _cards.Add(leaderboardCard.gameObject);
    }

    void DestroyOldCards()
    {
        for (int i = 0; i < _cards.Count; i++)
        {
            Destroy(_cards[i]);
        }

        _cards.Clear();
    }

    public void AddPlayer()
    {
        _save.AddPlayer(_timer, _inputField.text, _wave, _score);
        _onSave?.Invoke();

        DestroyOldCards();
        CreateCards();
    }
}
