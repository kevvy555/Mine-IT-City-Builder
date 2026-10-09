using System;
using UnityEngine;

namespace MineIT.CityBuilder.Bootstrap
{
    [Serializable]
    public sealed class BuildInfoData
    {
        public string version;
        public string gitSha;
        public string runNumber;
        public string unityVersion;
        public string universeCommit;
        public string canonContentHash;
        public string createdUtc;
    }

    public static class BuildInfo
    {
        private static BuildInfoData _cached;

        public static BuildInfoData Current
        {
            get
            {
                if (_cached != null)
                {
                    return _cached;
                }

                var asset = Resources.Load<TextAsset>("build-info");
                if (asset != null)
                {
                    _cached = JsonUtility.FromJson<BuildInfoData>(asset.text);
                }

                _cached ??= new BuildInfoData
                {
                    version = Application.version,
                    gitSha = "local",
                    runNumber = "local",
                    unityVersion = Application.unityVersion,
                    universeCommit = "unknown",
                    canonContentHash = "unknown",
                    createdUtc = "runtime"
                };

                return _cached;
            }
        }
    }
}
