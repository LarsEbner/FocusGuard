namespace Assets.SaveGame
{
    public interface ISaveable
    {
        string SaveId { get; }

        void CaptureState(SaveGameData data);
        void RestoreState(SaveGameData data);
    }

}
