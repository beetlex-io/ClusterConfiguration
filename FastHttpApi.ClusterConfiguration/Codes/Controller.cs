using System;
using System.Collections.Generic;
using System.Text;
using BeetleX.FastHttpApi;

namespace FastHttpApi.ClusterConfiguration.Codes
{
    [BeetleX.FastHttpApi.Controller]
    [BeetleX.FastHttpApi.Admin.LoginFilter(LoginUrl = "/login.html")]
    public class Configuration : BeetleX.FastHttpApi.IController
    {
        [SkipFilter(typeof(BeetleX.FastHttpApi.Admin.LoginFilter))]
        public object _GetCluster(string cluster)
        {
            return new JsonResult(_GetClusterInfo(cluster));
        }

        public object _ListCluster()
        {

            return Codes.ConfigurationManager.Clusters;
        }

        public object _GetClusterInfo(string cluster)
        {
            var item = Codes.ConfigurationManager.GetCluster(cluster);
            if (item != null)
            {
                var result = (Modules.Cluster)item.Clone();
                if (result.Urls != null)
                    result.Urls.Sort((o, e) => o.Name.CompareTo(e.Name));
                return result;
            }
            else
            {
                item = new Modules.Cluster();
            }
            return item;
        }

        public void _CreateCluster(string cluster, string remark)
        {
            Codes.ConfigurationManager.CreateCluster(cluster, remark);
            Codes.ConfigurationManager.Save();
        }


        public void _DelCluster(string cluster)
        {
            Codes.ConfigurationManager.RemoveCluster(cluster);
            Codes.ConfigurationManager.Save();
        }

        public void _AddUrl(string cluster, string name, string remark)
        {
            var c = Codes.ConfigurationManager.GetCluster(cluster);
            if (c != null)
            {
                c.AddUrl(name, remark);
                c.UpdateVersion();
                Codes.ConfigurationManager.Save();
            }
        }

        public object _GetHostStatus(string cluster, string host)
        {
            var c = Codes.ConfigurationManager.GetCluster(cluster);
            if (c != null)
            {
                return c.GetHostStatus(host);
            }
            return null;
        }

        public object _ListHosts(string cluster)
        {
            var c = Codes.ConfigurationManager.GetCluster(cluster);
            IList<Modules.Host> hosts = null;
            if (c != null)
            {
                hosts = c.GetHosts();
                return new { c.Version, Hosts = hosts };
            }
            return new object();
        }

        public void _DelUrl(string cluster, string name)
        {
            var c = Codes.ConfigurationManager.GetCluster(cluster);
            if (c != null)
            {
                c.DelUrl(name);
                c.UpdateVersion();
                Codes.ConfigurationManager.Save();
            }
        }

        public void _AddHost(string cluster, string url, string host, int weight)
        {
            Uri uri = new Uri(host);
            var c = Codes.ConfigurationManager.GetCluster(cluster);
            if (c != null)
            {

                c.GetUrl(url)?.AddHost(host, weight);
                c.UpdateVersion();
                Codes.ConfigurationManager.Save();
            }
        }

        public void _DelHost(string cluster, string url, string host)
        {
            var c = Codes.ConfigurationManager.GetCluster(cluster);
            if (c != null)
            {
                c.GetUrl(url)?.DelHost(host);
                c.UpdateVersion();
                Codes.ConfigurationManager.Save();
            }
        }

        public void _UpateHost(string cluster, string url, string host, int weight, int maxRPS)
        {
            var c = Codes.ConfigurationManager.GetCluster(cluster);
            if (c != null)
            {
                c.GetUrl(url)?.GetHost(host)?.Update(weight, maxRPS);
                c.UpdateVersion();
                Codes.ConfigurationManager.Save();
            }
        }

        private void OnVerify(object state)
        {
            mVerifyTimer.Change(-1, -1);
            try
            {
                Codes.ConfigurationManager.Verify();
            }
            finally
            {
                mVerifyTimer.Change(5000, 5000);
            }
        }

        private void OnLoadStatus(object state)
        {
            Codes.ConfigurationManager.LoadStatus();
        }

        private System.Threading.Timer mVerifyTimer;

        private System.Threading.Timer mLoadStatusTimer;


        [NotAction]
        public void Init(HttpApiServer server)
        {
            Codes.ConfigurationManager.Load();

            mVerifyTimer = new System.Threading.Timer(OnVerify, null, 5000, 5000);

            mLoadStatusTimer = new System.Threading.Timer(OnLoadStatus, null, 900, 900);
        }
    }
}
