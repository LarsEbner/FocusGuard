using System;
using System.Globalization;
using System.IO;

namespace Assets.Debugging
{
    /// <summary>
    /// Schreibt Objekte des Typs T zeilenweise in eine CSV-Datei.
    /// Die Datei wird beim Erzeugen geöffnet und bleibt während der
    /// Lebensdauer des Writers geöffnet.
    /// </summary>
    internal sealed class CsvStreamWriter<T> : IDisposable
    {
        private readonly StreamWriter writer;
        private readonly (string Name, Func<T, object> Accessor)[] fields;

        public CsvStreamWriter(string filePath, params (string Name, Func<T, object> Accessor)[] fields)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Ein Dateipfad muss angegeben werden.", nameof(filePath));
            
            this.fields = fields ?? throw new ArgumentNullException(nameof(fields));
            string directory = Path.GetDirectoryName(filePath);

            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }


            writer = new StreamWriter(filePath, false);
            WriteHeader();
        }

        private void WriteHeader()
        {
            for (int i = 0; i < fields.Length; i++)
            {
                if (i > 0) writer.Write(',');
                writer.Write(fields[i].Name);
            }
            writer.WriteLine();
        }

        public void Write(T item)
        {
            for (int i = 0; i < fields.Length; i++)
            {
                if (i > 0) writer.Write(',');
                object value = fields[i].Accessor(item);

                if (value != null)
                {
                    writer.Write(Convert.ToString(value, CultureInfo.InvariantCulture));
                }
            }

            writer.WriteLine();
        }

        public void Dispose()
        {
            writer?.Dispose();
        }
    }
}
