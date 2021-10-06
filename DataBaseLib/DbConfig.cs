using System;
using System.IO;
using System.Text.Json;

namespace DataBaseLib
{
    public class DbConfig
    {
        public string DataSource { get; set; }
        public string Version { get; set; }

        public override string ToString() => $"Data Source={DataSource};";

        public static DbConfig Import(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException();

            if (!File.Exists(path)) throw new FileNotFoundException();

            using var file = new FileStream(path, FileMode.Open);
        
            return JsonSerializer.DeserializeAsync<DbConfig>(file).Result;
        }
    }
}