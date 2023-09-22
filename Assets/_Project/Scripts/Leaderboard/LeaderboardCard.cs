using TMPro;
using UnityEngine;

public class LeaderboardCard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _playerName;
    [SerializeField] TextMeshProUGUI _playerTime;
    [SerializeField] TextMeshProUGUI _playerWave;

    public void Setup(float time, string name, int wave)
    {
        _playerTime.text = time.ToString("00:00.000");
        _playerName.text = name;
        _playerWave.text = wave.ToString();
    }
}
