using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.SaveGame
{

    public sealed class SaveManager : MonoBehaviour
    {
        private const string SaveFileName = "focusguard.json";

        private static SaveManager _instance;

        private readonly Dictionary<string, ISaveable> _saveables = new();

        private SaveGameData _data;
        public SaveGameData Data => _data;


        private string SavePath =>
            Path.Combine(
                Application.persistentDataPath,
                SaveFileName);

        public static SaveManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError(
                        "SaveManager has not been initialized.");

                }

                return _instance;
            }
        }

        public static bool IsInitialized =>
            _instance != null;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            if (_instance != null)
                return;

            var gameObject = new GameObject(nameof(SaveManager));

            // Verhindert, dass Awake während AddComponent ausgeführt wird.
            gameObject.SetActive(false);

            var manager = gameObject.AddComponent<SaveManager>();

            _instance = manager;

            // Das Savegame wird geladen, BEVOR die erste Szene
            // und deren normalen Komponenten initialisiert werden.
            manager.LoadFromDisk();

            DontDestroyOnLoad(gameObject);

            gameObject.SetActive(true);
        }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;

            DontDestroyOnLoad(gameObject);
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }

        public void Register(ISaveable saveable)
        {
            if (saveable == null)
                return;

            if (string.IsNullOrWhiteSpace(saveable.SaveId))
            {
                Debug.LogError(
                    $"Saveable object '{saveable}' has no SaveId.");

                return;
            }

            if (_saveables.TryGetValue(
                    saveable.SaveId,
                    out var existing))
            {
                if (!ReferenceEquals(existing, saveable))
                {
                    Debug.LogError(
                        $"Duplicate SaveId '{saveable.SaveId}'. " +
                        $"Objects must have unique SaveIds.");
                }

                return;
            }

            _saveables.Add(saveable.SaveId, saveable);
        }

        public void Unregister(ISaveable saveable)
        {
            if (saveable == null)
                return;

            if (_saveables.TryGetValue(
                    saveable.SaveId,
                    out var registered) &&
                ReferenceEquals(registered, saveable))
            {
                _saveables.Remove(saveable.SaveId);
            }
        }

        public void SaveGame()
        {
            foreach (var saveable in _saveables.Values)
            {
                saveable.CaptureState(_data);
            }

            SaveToDisk();
        }

        private void LoadFromDisk()
        {
            if (!File.Exists(SavePath))
            {
                _data = new SaveGameData();
                return;
            }

            try
            {
                var json = File.ReadAllText(SavePath);

                _data = JsonUtility.FromJson<SaveGameData>(json);

                if (_data == null)
                {
                    Debug.LogWarning(
                        "Savegame contained no valid data.");

                    _data = new SaveGameData();
                }
            }
            catch (System.Exception exception)
            {
                Debug.LogError(
                    $"Could not load savegame:\n{exception}");

                _data = new SaveGameData();
            }
        }

        private void SaveToDisk()
        {
            try
            {
                var json = JsonUtility.ToJson(
                    _data,
                    prettyPrint: true);

                File.WriteAllText(
                    SavePath,
                    json);
            }
            catch (System.Exception exception)
            {
                Debug.LogError(
                    $"Could not save game:\n{exception}");
            }
        }
    }

}