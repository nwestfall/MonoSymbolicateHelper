using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace MonoSymbolicateHelper.Core
{
    public class ArchiveScanner
    {
        public string ArchivePath { get; }
        private IDictionary<string, ArchiveInfo> _archives;

        public ArchiveScanner(string archivePath)
        {
            ArchivePath = archivePath;
        }


        public bool Scan()
        {
            _archives = new Dictionary<string, ArchiveInfo>();
            foreach (var dateFolder in Directory.EnumerateDirectories(ArchivePath))
            {
                foreach (var projectFolder in Directory.EnumerateDirectories(dateFolder))
                {
                    if (projectFolder.Contains("Packages"))
                    {
                        var info = ReadArchiveInfo(projectFolder);
                        // check if existing archive for this key and only keep latest creation date
                        if (_archives.ContainsKey(info.Key))
                        {
                            var exist = _archives[info.Key];
                            if (exist.CreationDate > info.CreationDate)
                            {
                                continue;
                            }
                        }
                        _archives[info.Key] = info;
                    }
                }
            }

            return true;
        }

        public ArchiveInfo GetArchiveInfo(string packageName, string versionCode)
        {
            return _archives[ArchiveInfo.GetKey(packageName, versionCode)];
        }

        private ArchiveInfo ReadArchiveInfo(string projectFolder)
        {
            var info = new ArchiveInfo {ArchivePath = projectFolder};

            //Get versions from folder
            var parts = projectFolder.Split('\\');
            var versionPart = parts[6];
            var version = versionPart.Split('_')[1];
            var versionCodeSplit = version.Split('.');
            var versionCode = string.Format("{0}{1}", versionCodeSplit[0], versionCodeSplit[1]);
            if (versionCodeSplit[2].Length == 2)
                versionCode = versionCode.Insert(2, string.Format("0{0}", versionCodeSplit[2]));
            else
                versionCode = versionCode.Insert(2, versionCodeSplit[2]);
            info.Name = "Tyler Drive";
            info.PackageName = "com.tyler.versatrans.mdd";
            info.PackageVersionCode = versionCode;
            info.PackageVersionName = version;
            info.CreationDate = DateTime.MinValue.Ticks;

            return info;
        }
    }
}