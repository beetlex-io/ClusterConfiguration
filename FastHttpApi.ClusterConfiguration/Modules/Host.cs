using BeetleX.FastHttpApi;
using BeetleX.FastHttpApi.Clients;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

namespace FastHttpApi.ClusterConfiguration.Modules
{
    public class Host : IDisposable
    {
        public Host()
        {
            Weight = 10;
            ID = Guid.NewGuid().ToString("N");

            Status = new HostStatus();
        }

        private HttpClusterApi httpClusterApi;

        private IServerStatus serverStatus;

        public string ID { get; set; }

        public int MaxRPS { get; set; }

        public long Memory { get; set; }

        public double Cpu { get; set; }

        public long RequestPer { get; set; }

        public long TotalRequest { get; set; }

        public string Name { get; set; }

        public bool Available { get; set; }

        public int Weight { get; set; }

        public void Update(int weight, int maxRPS)
        {
            Weight = weight;
            MaxRPS = maxRPS;
        }

        private bool mInVerify = false;


        [JsonIgnore]
        public HostStatus Status { get; set; }

        private bool mInLoadStatus = false;

        [JsonIgnore]
        public Exception LoadStatusError { get; set; }

        public void LoadStatus(Url[] urls, Url url)
        {
            if (!mInLoadStatus)
            {
                mInLoadStatus = true;
                try
                {
                    if (httpClusterApi == null)
                    {
                        httpClusterApi = new BeetleX.FastHttpApi.Clients.HttpClusterApi();
                        httpClusterApi.AddHost("*", Name);
                        serverStatus = httpClusterApi.Create<IServerStatus>();
                    }
                    var result = serverStatus.Get();
                    result.Name = this.Name;
                    Cpu = result.Cpu;
                    Memory = result.Memory / 1024;
                    long total = 0, per = 0;
                    if (url.Name == "*")
                    {
                        foreach (var item in result.Actions)
                        {
                            bool match = false;
                            foreach (var u in urls)
                            {
                                if (u == url)
                                    continue;
                                if (Regex.IsMatch(item.Url, u.Name, RegexOptions.IgnoreCase))
                                {
                                    match = true;
                                    break;
                                }

                            }
                            if (!match)
                            {
                                total += item.Requests;
                                per += item.RequestsPer;
                            }
                        }
                    }
                    else
                    {
                        foreach (var item in result.Actions)
                        {
                            if (Regex.IsMatch(item.Url, url.Name, RegexOptions.IgnoreCase))
                            {
                                total += item.Requests;
                                per += item.RequestsPer;

                            }
                        }
                    }
                    TotalRequest = total;
                    RequestPer = per;
                    Status = result;
                }
                catch (Exception e_)
                {
                    LoadStatusError = e_;
                }
                finally
                {
                    mInLoadStatus = false;
                }
            }
        }

        public async void Verify()
        {
            if (!mInVerify)
            {
                try
                {
                    mInVerify = true;
                    HttpHost httpHost = new HttpHost(Name);
                    var request = httpHost.Get("/", null, null, null);
                    var response = await request.Execute();
                    Available = response.Exception == null || !response.Exception.SocketError;
                }
                catch
                {

                }
                finally
                {
                    mInVerify = false;
                }
            }
        }

        public void Dispose()
        {
            if (httpClusterApi != null)
                httpClusterApi.Dispose();
        }
    }
    [BeetleX.FastHttpApi.Clients.JsonFormater]
    public interface IServerStatus
    {
        [Get(Route = "__ServerStatus")]
        HostStatus Get();
    }

    public class HostStatus
    {
        public HostStatus()
        {
            Actions = new List<ActionStatus>();
        }

        public string Name { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

        public string RunTime { get; set; }

        public string ServerName { get; set; }

        public long TotalRequest { get; set; }

        public long TotalMemory { get; set; }

        public long RequestPer { get; set; }

        public long ConnectionsPer { get; set; }

        public long TotalConnections { get; set; }

        public long CurrentConnectinos { get; set; }

        public long CurrentRequest { get; set; }

        //  public long CurrentWSRequest { get; set; }

        public long CurrentHttpRequest { get; set; }

        public double TotalSendBytes { get; set; }

        public double TotalReceiveBytes { get; set; }

        public double SendBytesPer { get; set; }

        public double ReceiveBytesPer { get; set; }

        public long Memory { get; set; }

        public double Cpu { get; set; }

        public List<ActionStatus> Actions { get; set; }

    }

    public class ActionStatus
    {
        public string Url { get; set; }

        public long Requests { get; set; }

        public long RequestsPer { get; set; }
    }
}
