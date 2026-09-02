using System;
using System.IO;
using Assets.EyeTracking;
using UnityEngine;

namespace Assets.Debugging
{
    internal sealed class MeasurementDebugRecorder : MonoBehaviour
    {
        [SerializeField]
        private PupilDilation _pupilDilation;

        [SerializeField]
        private MicrosaccadeDetection _microsaccadeDetection;

        [SerializeField]
        private DistractionDetection _distractionDetection;


        private CsvStreamWriter<PupilSize> pupilWriter;
        private CsvStreamWriter<Microsaccade> microsaccadeWriter;
        private CsvStreamWriter<Distraction> distractionWriter;


        private void OnEnable()
        {
            string directory = GetMeasurementDirectory();
            Debug.LogWarning("Starting measurements in " + directory);
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            pupilWriter = new CsvStreamWriter<PupilSize>(Path.Combine(directory, $"{timestamp}_PupilDilation.csv"),
                    ("Timestamp", m => m.Timestamp),
                    ("RightSize", m => m.RightSize),
                    ("LeftSize", m => m.LeftSize)
                );

            microsaccadeWriter = new CsvStreamWriter<Microsaccade>(Path.Combine(directory,$"{timestamp}_Microsaccades.csv"),
                    ("Timestamp", m => m.Timestamp),
                    ("Valid", m => m.Valid),
                    ("RightX", m => m.RightX),
                    ("RightY", m => m.RightY),
                    ("LeftX", m => m.LeftX),
                    ("LeftY", m => m.LeftY),
                    ("RotationRight", m => m.RotationRight),
                    ("RotationLeft", m => m.RotationLeft)
                );


            distractionWriter = new CsvStreamWriter<Distraction>(Path.Combine(directory, $"{timestamp}_Distractions.csv"),
                    ("LookAwayTime", m => m.LookAwayTime),
                    ("LookAtTime", m => m.LookAtTime),
                    ("Duration", m => m.Duration),
                    ("IsOngoing", m => m.IsOngoing)
                );


            if (_pupilDilation != null)
            {
                _pupilDilation.PupilSizeMeasured += HandlePupilSize;
            }

            if (_microsaccadeDetection != null)
            {
                _microsaccadeDetection.MicrosaccadeMeasured += HandleMicrosaccade;
            }

            if (_distractionDetection != null)
            {
                _distractionDetection.DistractionUpdated += HandleDistraction;
            }
        }


        private void HandlePupilSize(PupilSize measurement)
        {
            pupilWriter?.Write(measurement);
        }


        private void HandleMicrosaccade(Microsaccade measurement)
        {
            microsaccadeWriter?.Write(measurement);
        }


        private void HandleDistraction(Distraction measurement)
        {
            distractionWriter?.Write(measurement);
        }


        private void OnDisable()
        {
            if (_pupilDilation != null)
            {
                _pupilDilation.PupilSizeMeasured -= HandlePupilSize;
            }

            if (_microsaccadeDetection != null)
            {
                _microsaccadeDetection.MicrosaccadeMeasured -= HandleMicrosaccade;
            }

            if (_distractionDetection != null)
            {
                _distractionDetection.DistractionUpdated -= HandleDistraction;
            }

            pupilWriter?.Dispose();
            pupilWriter = null;

            microsaccadeWriter?.Dispose();
            microsaccadeWriter = null;

            distractionWriter?.Dispose();
            distractionWriter = null;
        }


        private string GetMeasurementDirectory()
        {
            string projectDirectory = new DirectoryInfo(Application.persistentDataPath).FullName;
            string measurementDirectory = Path.Combine(projectDirectory, "Measurements");

            Directory.CreateDirectory(measurementDirectory);
            return measurementDirectory;
        }
    }
}
