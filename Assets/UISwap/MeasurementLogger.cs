using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class MeasurementLogger : MonoBehaviour
{
    [Header("Logging")]
    [Tooltip("Wenn aktiviert, werden Messwerte als CSV-Dateien gespeichert.")]
    [SerializeField] private bool enableLogging = false;

    [Tooltip("Unterordner, in dem die Log-Dateien pro Sitzung abgelegt werden. Im Editor unter Assets/, im Build unter Application.persistentDataPath.")]
    [SerializeField] private string logFolderName = "Logs";

    [Header("Quellen")]
    [SerializeField] private DistractionDetection distractionDetection;
    [SerializeField] private PupilDilation pupilDilation;
    [SerializeField] private MicrosaccadeDetection microsaccadeDetection;

    private string sessionDirectory;
    private readonly Dictionary<string, StreamWriter> writers = new Dictionary<string, StreamWriter>();

    private void Awake()
    {
        if (!enableLogging) return;

        string baseDirectory =
#if UNITY_EDITOR
            Path.Combine(Application.dataPath, logFolderName);
#else
            Path.Combine(Application.persistentDataPath, logFolderName);
#endif

        string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        sessionDirectory = Path.Combine(baseDirectory, timestamp);
        Directory.CreateDirectory(sessionDirectory);

        Debug.Log($"[MeasurementLogger] Logging aktiv. Dateien unter: {sessionDirectory}");
    }

    private void OnEnable()
    {
        if (!enableLogging) return;

        if (distractionDetection != null) distractionDetection.OnLookAwayEnded += HandleLookAwayEnded;
        if (pupilDilation != null) pupilDilation.OnPupilMeasured += HandlePupilMeasured;
        if (microsaccadeDetection != null) microsaccadeDetection.OnSaccadeMeasured += HandleSaccadeMeasured;
    }

    private void OnDisable()
    {
        if (distractionDetection != null) distractionDetection.OnLookAwayEnded -= HandleLookAwayEnded;
        if (pupilDilation != null) pupilDilation.OnPupilMeasured -= HandlePupilMeasured;
        if (microsaccadeDetection != null) microsaccadeDetection.OnSaccadeMeasured -= HandleSaccadeMeasured;
    }

    // --- Event-Handler: übersetzen die jeweilige Event-Signatur in den generischen Log-Aufruf ---

    private void HandleLookAwayEnded(float lookAwayTime, float lookAtTime, float duration)
    {
        Log("LookAwayEvents", "LookAwayTime;LookAtTime;Duration", lookAwayTime, lookAtTime, duration);
    }

    private void HandlePupilMeasured(float rightSize, float leftSize, float timestamp)
    {
        Log("PupilSize", "Timestamp;RightSize;LeftSize", timestamp, rightSize, leftSize);
    }

    private void HandleSaccadeMeasured(bool valid, float timestamp)
    {
        Log("Microsaccade", "Timestamp;Valid", timestamp, valid);
    }

    // --- Generischer Logging-Mechanismus: kennt keine konkreten Typen mehr ---

    private void Log(string category, string header, params object[] values)
    {
        if (!enableLogging) return;

        string line = string.Join(";", values.Select(FormatValue));
        WriteLine(Sanitize(category), header, line);
    }

    private void WriteLine(string category, string header, string line)
    {
        if (!writers.TryGetValue(category, out StreamWriter writer))
        {
            string path = Path.Combine(sessionDirectory, $"{category}.csv");
            bool isNewFile = !File.Exists(path);
            writer = new StreamWriter(path, append: true) { AutoFlush = true };
            writers[category] = writer;
            if (isNewFile)
            {
                writer.WriteLine(header);
            }
        }
        writer.WriteLine(line);
    }

    private static string FormatValue(object value)
    {
        if (value == null) return "";
        if (value is float f) return f.ToString("F3");
        if (value is double d) return d.ToString("F3");
        return value.ToString();
    }

    private static string Sanitize(string name)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            name = name.Replace(c, '_');
        }
        return name;
    }

    private void OnApplicationQuit() => CloseAll();
    private void OnDestroy() => CloseAll();

    private void CloseAll()
    {
        foreach (StreamWriter writer in writers.Values)
        {
            writer.Flush();
            writer.Close();
        }
        writers.Clear();
    }
}