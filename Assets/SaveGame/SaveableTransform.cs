using Assets.SaveGame;
using UnityEngine;

[DefaultExecutionOrder(-1000)]
public sealed class SaveableTransform : MonoBehaviour, ISaveable
{
    [SerializeField]
    private string _saveId;

    public string SaveId => _saveId;

    private void Awake()
    {
        var saveManager = SaveManager.Instance;

        saveManager.Register(this);

        RestoreState(saveManager.Data);
    }

    private void OnDestroy()
    {
        if (!SaveManager.IsInitialized)
            return;

        SaveManager.Instance.Unregister(this);
    }

    public void CaptureState(SaveGameData data)
    {
        data.SetTransform(
            _saveId,
            transform.position,
            transform.rotation,
            transform.localScale);
    }

    public void RestoreState(SaveGameData data)
    {
        if (data == null)
            return;

        var state = data.GetTransform(_saveId);

        if (state == null)
            return;

        transform.position = state.position;
        transform.rotation = state.rotation;
        transform.localScale = state.scale;
    }
}
