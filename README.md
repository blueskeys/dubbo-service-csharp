# dubbo-service-csharp

该组件提供dubbo的csharp（c#）的client，使用zookeeper作为注册中心，使用hessian做为通信协议，可以和Java服务端进行相互调用；因为公司主要使用java开发语言，历史原因部分模块使用的是c#，因此单独开发出来这个功能；

### 功能特点

1、dubbo client使用

2、dubbo server使用，需要配置的较多

3、自动获取zookeeper注册中心的服务列表，然后实例化本地的服务

### 使用方法

1、目录结构

```yaml
├── dubbo-service-csharp
|  └── trunk
|     ├── dotnet-hessian-client           # dubbo-charp-client  核心包
|     ├── java-hessian-client
|     └── java-hessian-server
├── dubbo-service-demo            		  #  测试demo
|  └── packages          				  #  依赖组件
|     ├── Dubbo.Service.1.0.0.6
|     ├── KNET.Yqy.PubLibrary.1.0.1
|     ├── log4net.1.2.10
|     ├── sharpconfig.3.0.1
|     ├── Wyying.HessianCSharp.1.0.0
|     └── ZooKeeper.Net.3.4.6.2
├── hessiancharp                     	  #  hessiancharp 源码，略作调整
├── LICENSE
├── README.md
```





2、使用方式

1）、将java的接口转换为c#语言的接口，有转换工具，可以找下，转换后需要进行调整

2）、修改dubbo.cfg

```properties
[registry]
dubbo.registry.address=192.168.100.242:2181
[application]
dubbo.application.name=java-hessian-server
dubbo.application.owner=lab
dubbo.application.organization=wyying
[client]
dubbo.service.interface.assembly=dubbo-demo
dubbo.service.loadbalance=0
dubbo.service.reties=3
dubbo.service.timeout=3
dubbo.service.threadpool.size=3
[service]
dubbo.service.port=36955
dubbo.service.protocol=hessian
dubbo.service.assembly=dotnet-hessian-client

```

3、根据几个demo项目测试就行了。



ps：

1、hessian官方版本已经很久不更新了，传输的使用最好不好多层嵌套对象，可以多使用json

