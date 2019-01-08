using FastHttpApi.ClusterConfiguration.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastHttpApi.ClusterConfiguration.Codes
{
    public class ConfigurationManager
    {
        private const string ConfigurationFile = "Configuration.json";

        private static List<Cluster> mClusters = new List<Cluster>();

        public static List<Cluster> Clusters => mClusters;

        public static Cluster GetCluster(string name)
        {           
            var result = mClusters.Find(c => c.Name == name);
            return result;
        }

        public static void LoadStatus()
        {
            try
            {
                for (int i = 0; i < mClusters.Count; i++)
                {
                    mClusters[i].LoadHostStatus();
                }
            }
            catch
            {

            }
        }

        public static void Verify()
        {
            try
            {
                for (int i = 0; i < mClusters.Count; i++)
                {
                    mClusters[i].Verify();
                }
            }
            catch
            {

            }
        }

        public static void CreateCluster(string name, string remark)
        {
            var c = GetCluster(name);
            if (c == null)
            {
                mClusters.Add(new Cluster { Name = name, Remark = remark });
            }
        }

        public static void RemoveCluster(string name)
        {
            var c = GetCluster(name);
            if (c != null)
                mClusters.Remove(c);
        }

        public static void Load()
        {
            string file = System.IO.Directory.GetCurrentDirectory() + System.IO.Path.DirectorySeparatorChar + ConfigurationFile;
            if (System.IO.File.Exists(file))
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(file))
                {
                    string value = reader.ReadToEnd();
                    mClusters = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cluster>>(value);
                }
            }
        }
        public static void Save()
        {
            string file = System.IO.Directory.GetCurrentDirectory() + System.IO.Path.DirectorySeparatorChar + ConfigurationFile;
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(file, false))
            {
                writer.Write(Newtonsoft.Json.JsonConvert.SerializeObject(mClusters));
                writer.Flush();
            }
        }
    }
}
