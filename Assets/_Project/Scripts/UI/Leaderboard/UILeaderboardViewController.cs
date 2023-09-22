using System.IO;
using UnityEngine;

public class UILeaderboardViewController : UIViewController<UILeaderboardView>
{
    readonly float _finalTimer;
    readonly int _finalWave;
    readonly int _finalScore;
    readonly ISave<LeaderboardSave> _saveData;
    readonly LeaderboardSave _save;

    readonly string _savePath = Application.persistentDataPath + "/LeaderboardSave.xml";

    public UILeaderboardViewController(UILeaderboardView view, float finalTimer, int finalWave, int score, ILoad<LeaderboardSave> loadData, ISave<LeaderboardSave> saveData) : base(view)
    {
        _finalTimer = finalTimer;
        _saveData = saveData;
        _finalWave = finalWave;
        _finalScore = score;
        _save = loadData.LoadData(_savePath) ?? new LeaderboardSave();
        _save.Order();
    }

    public override void Present()
    {
        _view.Setup(_save, _finalTimer, _finalWave, _finalScore, Save);
        base.Present();
    }

    public override void Destroy()
    {
        base.Destroy();
    }

    void Save()
    {
        _saveData.SaveData(_save, _savePath);
    }

#if UNITY_EDITOR
    [UnityEditor.MenuItem("Leaderboard/Delete Save")]
    static void Delete()
    {
        File.Delete(Application.persistentDataPath + "\\LeaderboardSave.xml");
    }
#endif
}
