﻿using System;
using System.Diagnostics;
using System.IO;

namespace SeleniumPom_v1.Utilities
{
    public class FileHelper
    {
        private readonly string _filePath;
        private readonly int _timeoutInSeconds;
        protected const int EXTENDED_TIMEOUT = 30;

        public FileHelper(string filePath, int timeoutInSeconds = EXTENDED_TIMEOUT)
        {
            _filePath = filePath;
            _timeoutInSeconds = timeoutInSeconds;
        }

        public bool WaitForDownload()
        {
            var stopwatch = Stopwatch.StartNew();
            var timeout = TimeSpan.FromSeconds(_timeoutInSeconds);

            while (stopwatch.Elapsed < timeout)
            {
                if (File.Exists(_filePath) && IsFileReady())
                {
                    var downloadTime = stopwatch.Elapsed;
                    Logger.LogInfo($"File downloaded successfully after {downloadTime.TotalSeconds:F1} seconds");
                    return true;
                }
            }

            Logger.LogError($"File download timeout after {stopwatch.Elapsed.TotalSeconds:F1} seconds");
            return false;
        }

        private bool IsFileReady()
        {
            try
            {
                using (FileStream stream = File.Open(_filePath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
                return true;
            }
            catch (IOException)
            {
                Logger.LogInfo("File exists but is still being written to");
                return false;
            }
        }
    }
}
