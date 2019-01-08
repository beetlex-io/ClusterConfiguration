/************************************************************************************
FastHttpApi javascript api Generator Copyright © henryfan 2018 email:henryfan@msn.com
https://github.com/IKende/FastHttpApi
**************************************************************************************/




var Configuration_GetClusterUrl = '/_GetCluster';
/**
* 'var result= await Configuration_GetCluster(params);'
**/
function Configuration_GetCluster(cluster, useHttp) {
    return api(Configuration_GetClusterUrl, { cluster: cluster }, useHttp).sync();
}
/**
* 'Configuration_GetClusterAsync(params).execute(function(result){},useHttp);'
**/
function Configuration_GetClusterAsync(cluster, useHttp) {
    return api(Configuration_GetClusterUrl, { cluster: cluster }, useHttp);
}
var Configuration_ListClusterUrl = '/_ListCluster';
/**
* 'var result= await Configuration_ListCluster(params);'
**/
function Configuration_ListCluster(useHttp) {
    return api(Configuration_ListClusterUrl, {}, useHttp).sync();
}
/**
* 'Configuration_ListClusterAsync(params).execute(function(result){},useHttp);'
**/
function Configuration_ListClusterAsync(useHttp) {
    return api(Configuration_ListClusterUrl, {}, useHttp);
}
var Configuration_GetClusterInfoUrl = '/_GetClusterInfo';
/**
* 'var result= await Configuration_GetClusterInfo(params);'
**/
function Configuration_GetClusterInfo(cluster, useHttp) {
    return api(Configuration_GetClusterInfoUrl, { cluster: cluster }, useHttp).sync();
}
/**
* 'Configuration_GetClusterInfoAsync(params).execute(function(result){},useHttp);'
**/
function Configuration_GetClusterInfoAsync(cluster, useHttp) {
    return api(Configuration_GetClusterInfoUrl, { cluster: cluster }, useHttp);
}
var Configuration_CreateClusterUrl = '/_CreateCluster';
/**
* 'var result= await Configuration_CreateCluster(params);'
**/
function Configuration_CreateCluster(cluster, remark, useHttp) {
    return api(Configuration_CreateClusterUrl, { cluster: cluster, remark: remark }, useHttp).sync();
}
/**
* 'Configuration_CreateClusterAsync(params).execute(function(result){},useHttp);'
**/
function Configuration_CreateClusterAsync(cluster, remark, useHttp) {
    return api(Configuration_CreateClusterUrl, { cluster: cluster, remark: remark }, useHttp);
}
var Configuration_DelClusterUrl = '/_DelCluster';
/**
* 'var result= await Configuration_DelCluster(params);'
**/
function Configuration_DelCluster(cluster, useHttp) {
    return api(Configuration_DelClusterUrl, { cluster: cluster }, useHttp).sync();
}
/**
* 'Configuration_DelClusterAsync(params).execute(function(result){},useHttp);'
**/
function Configuration_DelClusterAsync(cluster, useHttp) {
    return api(Configuration_DelClusterUrl, { cluster: cluster }, useHttp);
}
var Configuration_AddUrlUrl = '/_AddUrl';
/**
* 'var result= await Configuration_AddUrl(params);'
**/
function Configuration_AddUrl(cluster, name, remark, useHttp) {
    return api(Configuration_AddUrlUrl, { cluster: cluster, name: name, remark: remark }, useHttp).sync();
}
/**
* 'Configuration_AddUrlAsync(params).execute(function(result){},useHttp);'
**/
function Configuration_AddUrlAsync(cluster, name, remark, useHttp) {
    return api(Configuration_AddUrlUrl, { cluster: cluster, name: name, remark: remark }, useHttp);
}
var Configuration_GetHostStatusUrl = '/_GetHostStatus';
/**
* 'var result= await Configuration_GetHostStatus(params);'
**/
function Configuration_GetHostStatus(cluster, host, useHttp) {
    return api(Configuration_GetHostStatusUrl, { cluster: cluster, host: host }, useHttp).sync();
}
/**
* 'Configuration_GetHostStatusAsync(params).execute(function(result){},useHttp);'
**/
function Configuration_GetHostStatusAsync(cluster, host, useHttp) {
    return api(Configuration_GetHostStatusUrl, { cluster: cluster, host: host }, useHttp);
}
var Configuration_ListHostsUrl = '/_ListHosts';
/**
* 'var result= await Configuration_ListHosts(params);'
**/
function Configuration_ListHosts(cluster, useHttp) {
    return api(Configuration_ListHostsUrl, { cluster: cluster }, useHttp).sync();
}
/**
* 'Configuration_ListHostsAsync(params).execute(function(result){},useHttp);'
**/
function Configuration_ListHostsAsync(cluster, useHttp) {
    return api(Configuration_ListHostsUrl, { cluster: cluster }, useHttp);
}
var Configuration_DelUrlUrl = '/_DelUrl';
/**
* 'var result= await Configuration_DelUrl(params);'
**/
function Configuration_DelUrl(cluster, name, useHttp) {
    return api(Configuration_DelUrlUrl, { cluster: cluster, name: name }, useHttp).sync();
}
/**
* 'Configuration_DelUrlAsync(params).execute(function(result){},useHttp);'
**/
function Configuration_DelUrlAsync(cluster, name, useHttp) {
    return api(Configuration_DelUrlUrl, { cluster: cluster, name: name }, useHttp);
}
var Configuration_AddHostUrl = '/_AddHost';
/**
* 'var result= await Configuration_AddHost(params);'
**/
function Configuration_AddHost(cluster, url, host, weight, useHttp) {
    return api(Configuration_AddHostUrl, { cluster: cluster, url: url, host: host, weight: weight }, useHttp).sync();
}
/**
* 'Configuration_AddHostAsync(params).execute(function(result){},useHttp);'
**/
function Configuration_AddHostAsync(cluster, url, host, weight, useHttp) {
    return api(Configuration_AddHostUrl, { cluster: cluster, url: url, host: host, weight: weight }, useHttp);
}
var Configuration_DelHostUrl = '/_DelHost';
/**
* 'var result= await Configuration_DelHost(params);'
**/
function Configuration_DelHost(cluster, url, host, useHttp) {
    return api(Configuration_DelHostUrl, { cluster: cluster, url: url, host: host }, useHttp).sync();
}
/**
* 'Configuration_DelHostAsync(params).execute(function(result){},useHttp);'
**/
function Configuration_DelHostAsync(cluster, url, host, useHttp) {
    return api(Configuration_DelHostUrl, { cluster: cluster, url: url, host: host }, useHttp);
}
var Configuration_UpateHostUrl = '/_UpateHost';
/**
* 'var result= await Configuration_UpateHost(params);'
**/
function Configuration_UpateHost(cluster, url, host, weight, maxRPS, useHttp) {
    return api(Configuration_UpateHostUrl, { cluster: cluster, url: url, host: host, weight: weight, maxRPS: maxRPS }, useHttp).sync();
}
/**
* 'Configuration_UpateHostAsync(params).execute(function(result){},useHttp);'
**/
function Configuration_UpateHostAsync(cluster, url, host, weight, maxRPS, useHttp) {
    return api(Configuration_UpateHostUrl, { cluster: cluster, url: url, host: host, weight: weight, maxRPS: maxRPS }, useHttp);
}
