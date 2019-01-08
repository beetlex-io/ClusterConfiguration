using System;
using System.Collections.Generic;
using System.Text;

namespace FastHttpApi.ClusterConfiguration.Modules
{
    public class Url : IDisposable
    {
        public Url()
        {
            Hosts = new List<Host>();
            ID = Guid.NewGuid().ToString("N");
        }


        public long RequestPer
        {
            get
            {
                long result = 0;
                foreach (var item in Hosts)
                {
                    result += item.RequestPer;
                }
                return result;

            }
            set
            {

            }
        }

        public long TotalRequest
        {
            get
            {

                long result = 0;
                foreach (var item in Hosts)
                {
                    result += item.TotalRequest;
                }
                return result;
            }

            set { }
        }

        public string ID { get; set; }

        public string Name { get; set; }

        public string Remark { get; set; }

        public List<Host> Hosts { get; set; }

        public void AddHost(string name, int weight)
        {
            name = name.ToLower().Trim();
            var host = GetHost(name);
            if (host == null)
            {
                Hosts.Add(new Host { Name = name, Weight = weight });
            }
        }


        public void DelHost(string name)
        {
            name = name.ToLower().Trim();
            var host = GetHost(name);
            if (host != null)
            {
                host.Dispose();
                Hosts.Remove(host);
            }
        }

        public void Dispose()
        {
            for (int i = 0; i < Hosts.Count; i++)
            {
                Hosts[i].Dispose();
            }
        }

        public Host GetHost(string name)
        {
            name = name.ToLower();
            return Hosts.Find(h => h.Name == name);
        }

        public void Verify()
        {
            for (int i = 0; i < Hosts.Count; i++)
            {
                Hosts[i].Verify();
            }
        }
    }
}
