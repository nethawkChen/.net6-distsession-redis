## .Net Core 使用 Redis 做分散式 Session

使用 Redis 做為 Asp.net core 分散式 session 的範例﹐詳細過程參考 [Asp.Net Core 分散式Session – 使用 Redis](https://www.dotblogs.com.tw/nethawk/2023/03/08/net-redis-session) 

本篇範例搭配 Nginx 做 loadbalancer 導流﹐整理架構圖如下
[![](https://github.com/nethawkChen/.net6-distsession-redis/blob/main/%E6%9E%B6%E6%A7%8B%E5%9C%96.png)](https://github.com/nethawkChen/.net6-distsession-redis/blob/main/%E6%9E%B6%E6%A7%8B%E5%9C%96.png "架構圖")