# ClusterConfiguration
Beetlex Webapi cluster configuration

## run
`dotnet FastHttpApi.ClusterConfiguration.dll`
## change manager password
open`HttpConfig.json`file
change
```
    "Manager": "admin",
    "ManagerPWD": "123456",
```
## Cluster Management
![](https://i.imgur.com/xftjdx5.png)

## FastHttpApi using
```
HttpClusterApi = new BeetleX.FastHttpApi.Clients.HttpClusterApi();
await HttpClusterApi.LoadNodeSource("default", "http://localhost:8080");
```
## create interface proxy
```
    public interface IDataService
    {
        [Get(Route = "hello/{name}")]
        string Hello(string name);
    }

 DataService = HttpClusterApi.Create<IDataService>();
 var result = DataService.Hello("henry");
```
[sample code](https://github.com/IKende/FastHttpApi/tree/master/samples)
Cluster projects
