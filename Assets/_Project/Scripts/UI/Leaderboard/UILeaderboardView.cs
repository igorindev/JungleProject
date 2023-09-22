using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface IUILeaderboardView
{
    void Setup(LeaderboardSave save, float timer, int wave, Action onSave);
}

public class UILeaderboardView : UIView, IUILeaderboardView
{
    [SerializeField] Transform _contentTransform;
    [SerializeField] LeaderboardCard _leaderboardCard;
    [SerializeField] TMP_InputField _inputField;

    LeaderboardSave _save;
    float _timer;
    int _wave;

    readonly List<GameObject> _cards = new List<GameObject>();

    Action _onSave;

    public void Setup(LeaderboardSave save, float timer, int wave, Action onSave)
    {
        _timer = timer;
        _save = save;
        _wave = wave;
        _onSave = onSave;

        CreateCards();
    }

    void CreateCards()
    {
        foreach (LeaderboardSave.Player item in _save.Players)
        {
            CreateCardView(item);
        }
    }

    void CreateCardView(LeaderboardSave.Player data)
    {
        LeaderboardCard leaderboardCard = Instantiate(_leaderboardCard, _contentTransform);
        leaderboardCard.Setup(data._timer, data._name, data._wave);
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
        _save.AddPlayer(_timer, _inputField.text, _wave);
        _onSave?.Invoke();

        DestroyOldCards();
        CreateCards();
    }
}
