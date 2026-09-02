using System;
using System.Collections.Generic;
using System.IO;
using Assets.EyeTracking;
using Assets.Measurement;
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

        private readonly List<IDisposable> registrations = new();

        private void OnEnable()
        {
            string directory = GetMeasurementDirectory();
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            Register<PupilSize>(
                _pupilDilation,
                x => _pupilDilation.PupilSizeMeasured += x,
                x => _pupilDilation.PupilSizeMeasured -= x,
                Path.Combine(directory, $"{timestamp}_PupilDilation.csv"),
                ("Timestamp", m => m.Timestamp),
                ("RightSize", m => m.RightSize),
                ("LeftSize", m => m.LeftSize)
            );

            Register<EyeSample>(
                _microsaccadeDetection,
                x => _microsaccadeDetection.EyeSampleMeasured += x,
                x => _microsaccadeDetection.EyeSampleMeasured -= x,
                Path.Combine(directory, $"{timestamp}_EyeSamples.csv"),
                ("Timestamp", m => m.Timestamp),
                ("PositionRightHorizontal", m => m.PositionRight.x),
                ("PositionRightVertical", m => m.PositionRight.y),
                ("PositionLeftHorizontal", m => m.PositionLeft.x),
                ("PositionLeftVertical", m => m.PositionLeft.y),
                ("VelocityRightHorizontal", m => m.VelocityRight?.x),
                ("VelocityRightVertical", m => m.VelocityRight?.y),
                ("VelocityLeftHorizontal", m => m.VelocityLeft?.x),
                ("VelocityLeftVertical", m => m.VelocityLeft?.y),
                ("DeviationRightHorizontal", m => m.VelocityDeviationRight?.x),
                ("DeviationRightVertical", m => m.VelocityDeviationRight?.y),
                ("DeviationLeftHorizontal", m => m.VelocityDeviationLeft?.x),
                ("DeviationVelocityLeftVertical", m => m.VelocityDeviationLeft?.y),
                ("ThresholdRightHorizontal", m => m.VelocityThresholdRight?.x),
                ("ThresholdRightVertical", m => m.VelocityThresholdRight?.y),
                ("ThresholdLeftHorizontal", m => m.VelocityThresholdLeft?.x),
                ("ThresholdVelocityLeftVertical", m => m.VelocityThresholdLeft?.y),
                ("CandidateRight", m => m.MicrosaccadeCandidateRight),
                ("CandidateLeft", m => m.MicrosaccadeCandidateLeft),
                ("Candidate", m => m.MicrosaccadeCandidate)
            );

            Register<Distraction>(
                _distractionDetection,
                x => _distractionDetection.DistractionUpdated += x,
                x => _distractionDetection.DistractionUpdated -= x,
                Path.Combine(directory, $"{timestamp}_Distractions.csv"),
                ("LookAwayTime", m => m.LookAwayTime),
                ("LookAtTime", m => m.LookAtTime),
                ("Duration", m => m.Duration),
                ("IsOngoing", m => m.IsOngoing)
            );
        }


        private void Register<T>(MonoBehaviour source, Action<Action<T>> subscribe, Action<Action<T>> unsubscribe, string path,
            params (string Name, Func<T, object> Accessor)[] fields)
        {
            if (source == null)
            {
                return;
            }

            CsvStreamWriter<T> writer = new CsvStreamWriter<T>(path, fields);
            Action<T> handler = measurement => writer.Write(measurement);
            subscribe(handler);

            registrations.Add(new EventRegistration<T>(writer, handler, unsubscribe));
        }


        private void OnDisable()
        {
            foreach (IDisposable registration in registrations)
            {
                registration.Dispose();
            }

            registrations.Clear();
        }


        private string GetMeasurementDirectory()
        {
            string projectDirectory = new DirectoryInfo(Application.persistentDataPath).FullName;
            string measurementDirectory = Path.Combine(projectDirectory, "Measurements");

            Directory.CreateDirectory(measurementDirectory);
            return measurementDirectory;
        }


        private sealed class EventRegistration<T> : IDisposable
        {
            private readonly CsvStreamWriter<T> writer;
            private readonly Action<T> handler;
            private readonly Action<Action<T>> unsubscribe;

            public EventRegistration(CsvStreamWriter<T> writer, Action<T> handler, Action<Action<T>> unsubscribe)
            {
                this.writer = writer;
                this.handler = handler;
                this.unsubscribe = unsubscribe;
            }

            public void Dispose()
            {
                unsubscribe(handler);
                writer.Dispose();
            }
        }
    }
}
