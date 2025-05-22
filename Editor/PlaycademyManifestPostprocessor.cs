#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System.IO;
using System.IO.Compression;

namespace Playcademy.ManifestExport
{
    [System.Serializable]
    public class PlaycademyManifestData
    {
        public string version;
        public string bootMode;
        public string entryPoint;
        public string[] styles;
        public string platform;
        public string createdAt;
    }

    /// <summary>Writes playcademy.manifest.json into every WebGL build folder.</summary>
    public class PlaycademyManifestPostprocessor : IPostprocessBuildWithReport // modern API
    {
        public int callbackOrder => 0;

        public void OnPostprocessBuild(BuildReport report)
        {
            if (report.summary.platform != BuildTarget.WebGL) return;

            string buildDir = report.summary.outputPath;
            string entryHtml = "index.html";
            string manifestPath = Path.Combine(buildDir, "playcademy.manifest.json");

            var manifest = new PlaycademyManifestData
            {
                
                version   = "1",
                bootMode  = "iframe",
                entryPoint= entryHtml,
                styles    = new string[0],
                platform    = $"unity@{Application.unityVersion}",
                createdAt = System.DateTime.UtcNow.ToString("o")
            };

            string json = JsonUtility.ToJson(manifest, true);
            File.WriteAllText(manifestPath, json);

            try
            {
                string exportDirName = Path.GetFileName(buildDir.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
                string parentDirPath = Path.GetDirectoryName(buildDir);
                if (string.IsNullOrEmpty(exportDirName))
                {
                    exportDirName = "UnityWebGL_Build"; 
                }
                if (string.IsNullOrEmpty(parentDirPath))
                {
                    parentDirPath = ".";
                }

                string zipFileName = $"{exportDirName}_playcademy_export.zip";
                string zipFilePath = Path.Combine(parentDirPath, zipFileName);

                if (File.Exists(zipFilePath))
                {
                    File.Delete(zipFilePath);
                }

                ZipFile.CreateFromDirectory(buildDir, zipFilePath, System.IO.Compression.CompressionLevel.Optimal, true);
                
                Debug.Log($"[CademyManifest] Successfully created ZIP file at {zipFilePath}");

                // Delete the original manifest file from the build directory
                if (File.Exists(manifestPath))
                {
                    File.Delete(manifestPath);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"[CademyManifest] Error creating ZIP file: {e.Message}");
            }
        }
    }
}
#endif
