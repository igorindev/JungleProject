using TMPro;
using UnityEngine;

public class LeaderboardCard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _playerName;
    [SerializeField] TextMeshProUGUI _playerWave;
    [SerializeField] TextMeshProUGUI _playerScore;
    [SerializeField] TextMeshProUGUI _playerPos;

    public void Setup(int pos, string name, int wave, int score)
    {
        _playerPos.text = pos.ToString();
        _playerScore.text = score.ToString();
        _playerName.text = name;
        _playerWave.text = wave.ToString();
    }
}
