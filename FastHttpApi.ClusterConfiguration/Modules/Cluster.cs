using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastHttpApi.ClusterConfiguration.Modules
{
    public class Cluster : ICloneable
    {
        public Cluster()
        {
            Urls = new List<Url>();
            Version = Guid.NewGuid().ToString("N");

        }

        public string Version { get; set; }

        public string Name { get; set; }

        public string Remark { get; set; }

        public int Count
        {
            get
            {
                return Urls.Count;
            }
            set { }
        }

        public List<Url> Urls { get; set; }

        public void AddUrl(string name, string remark)
        {
            name = name.ToLower();
            var item = GetUrl(name);
            if (item == null)
            {
                Urls.Add(new Url { Name = name, Remark = remark });

            }
        }

        public void UpdateVersion()
        {
            Version = Guid.NewGuid().ToString("N");
        }

        public void DelUrl(string name)
        {
            name = name.ToLower();
            var item = GetUrl(name);
            if (item != null)
            {
                item.Dispose();
                Urls.Remove(item);
            }
        }

        public Url GetUrl(string name)
        {
            name = name.ToLower();
            return Urls.Find(u => u.Name == name);
        }

        public void Verify()
        {
            for (int i = 0; i < Urls.Count; i++)
            {
                Urls[i].Verify();
            }
        }

        private Dictionary<string, Host> mHosts = new Dictionary<string, Host>();

        public HostStatus GetHostStatus(string name)
        {
            if (mHosts.TryGetValue(name, out Host host))
            {
                return host.Status;
            }
            return new HostStatus();
        }

        public Host[] GetHosts()
        {
            return mHosts.Values.ToArray();
        }

        public void LoadHostStatus()
        {
            Dictionary<string, Host> hosts = new Dictionary<string, Host>();
            Url[] urls = Urls.ToArray();
            for (int i = 0; i < urls.Length; i++)
            {
                var url = urls[i];
                for (int k = 0; k < url.Hosts.Count; k++)
                {
                    var host = url.Hosts[k];
                    if (!hosts.ContainsKey(host.Name))
                    {
                        hosts.Add(host.Name, host);
                    }
                    Task.Run(() => { host.LoadStatus(urls, url); });
                }
            }
            mHosts = hosts;
        }

        public object Clone()
        {
            Cluster cluster = new Cluster();
            cluster.Name = this.Name;
            cluster.Version = this.Version;
            cluster.Remark = this.Remark;
            cluster.Urls.AddRange(Urls.ToArray());
            cluster.mHosts = this.mHosts;
            return cluster;
        }
    }
}
