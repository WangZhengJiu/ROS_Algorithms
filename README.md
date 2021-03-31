# ROS_Algorithms
  * [Offical Website](https://www.ros.org/)
  
## Apache apollo配置
  * [Project](https://github.com/apache/activemq-apollo)
  * 安装JRE，配置环境变量JAVA_HOME  
  * 下载apache apollo
  * 解压后运行"apollo create BrokerName BrokerFolder"
  * 进入BrokerFolder/bin目录运行"apollo-broker run"
  * 进入"http:\/\/127.0.0.1:61683"登录并配置
  * 在我的使用中，直接编辑user/group文件添加用户会导致无法登陆，所以建议通过网页的方式配置
  * 如果网页配置后新账户仍然无法登陆，将"Authencation enable=false"
  * [配置](https://blog.csdn.net/bksqmy/article/details/84305405)

## A* like Algorithm
  * [A*介绍](https://blog.csdn.net/qq_36946274/article/details/81982691)
  * [A* Wiki](https://en.wikipedia.org/wiki/A*_search_algorithm)
  * [LifeLong Planning A*](https://en.wikipedia.org/wiki/Lifelong_Planning_A*)
  * [算法动态演示](https://github.com/zhm-real/PathPlanning)
  * [Path Finding A*](https://theory.stanford.edu/~amitp/GameProgramming/Heuristics.html)
  * [Path Finding in Games](https://www.redblobgames.com/)
  * [Red Blob Blogs](https://simblob.blogspot.com/)
  * [Factorio RRA*](https://factorio.com/blog/post/fff-317)

  
## Casun
  * [Official Website](http://www.casun.cn/)
   
## C#教程
  * [无废话WCF入门教程一[什么是WCF]](https://www.cnblogs.com/iamlilinfeng/archive/2012/09/25/2700049.html)
  * [无废话MVC入门教程一[概述、环境安装、创建项目]](https://www.cnblogs.com/iamlilinfeng/archive/2013/02/24/2922869.html)
  * [C#教程之自己动手写映射第一节[动机]](https://www.cnblogs.com/iamlilinfeng/archive/2012/07/20/2601753.html)
  * [Tutorial: Create a web API with ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-5.0&tabs=visual-studio)
  * [Create Simple Web API In ASP.NET MVC](https://www.c-sharpcorner.com/article/create-simple-web-api-in-asp-net-mvc/)
  * [C# NuGet包管理命令](https://www.cnblogs.com/zhaogaojian/p/8398531.html)
  * [async/await的用法](https://blog.csdn.net/qc530167365/article/details/83108848)
  * [String Format](https://docs.microsoft.com/en-us/dotnet/api/system.string?view=net-5.0)
  * [Composite Formatting](https://docs.microsoft.com/en-us/dotnet/standard/base-types/composite-formatting)
  * [利用AssemblyResolve事件加载任意位置的程序集](http://blog.junwen38.com/archives/656)
  * [Resolve assembly loads](https://docs.microsoft.com/zh-cn/dotnet/standard/assembly/resolve-loads)
  * [lock usage](https://docs.microsoft.com/zh-cn/dotnet/csharp/language-reference/keywords/lock-statement)
  * [设置程序PrivatePath，配置引用程序集的路径（分离exe和dll）](https://blog.csdn.net/Vblegend_2013/article/details/79712911)

## Database
  * [PostgreSQL](https://www.postgresql.org/docs/devel/)
  * [SP_GiST](https://www.postgresql.org/docs/devel/spgist-intro.html)
  * [PostgreSQL Indexes](https://leopard.in.ua/2015/04/13/postgresql-indexes)
  * [install MariaDB](https://www.cnblogs.com/ruichow/p/11399367.html)  

## DWA Algorithm
  * [DWA介绍](https://www.cnblogs.com/kuangxionghui/p/8484803.html)

## Git/TortoiseGit分支管理
  ```
  HEAD: 当前活跃分支指针
  master: 主干
  origin: 远程仓库名字
  git remote -v: 查看origin指向
  git clone分支:
                使用TortoiseGit在界面选择制定分支即可
                使用git clone -b <分支名称> <url>
  git push origin brach_name
  创建分支:
          使用TortoiseGit=>Create Branch
          使用git branch <branch_name>
  提交分支:
          使用TortoiseGit=>Checkout branch=>commit
          使用git checkout <branch_name>; git push --set-upstream origin <branch_name>
  ```
  
## IO多路复用
  * [select](https://www.cnblogs.com/skyfsm/p/7079458.html)
  * [poll](https://www.cnblogs.com/orlion/p/6142838.html)
  * [epoll](https://blog.csdn.net/shenya1314/article/details/73691088)
  
## Jenkins
  * [Jenkins自动化部署入门详细教程](https://www.cnblogs.com/wfd360/p/11314697.html)
  
## MES/WMS/ERP
  * [MES Wiki](https://en.wikipedia.org/wiki/Manufacturing_execution_system)
  * [WMS Wiki](https://en.wikipedia.org/wiki/Warehouse_management_system)
  * [ERP Wiki](https://en.wikipedia.org/wiki/Enterprise_resource_planning)
  * [What’s a MES? How does it differ from an ERP and a WMS?](https://www.interlakemecalux.com/blog/mes-manufacturing-execution-system)
  * [ERP: definition and how it is different from a WMS](https://www.interlakemecalux.com/blog/erp-definition-differences-wms)
  * [Manufacturing execution systems (MES) meet the warehouse](https://www.logisticsmgmt.com/article/manufacturing_execution_systems_mes_meets_the_warehouse)
  
## MQTT
  * [ActiveMQ 5.x Features](https://activemq.apache.org/features)
  * [ActiveMQ Message Cursors](https://activemq.apache.org/message-cursors)
  * [ActiveMQ Prefetch Limit For](https://activemq.apache.org/what-is-the-prefetch-limit-for)
  * [ActiveMQ Sercurity](https://activemq.apache.org/security)
  * [ActiveMQ参数优化，失败缓存机制](http://blog.itpub.net/28624388/viewspace-1424905/)
  * [ActiveMQ参数调整，消息超时](https://blog.csdn.net/luoww1/article/details/84852519)
  * [ActiveMQ XML Configuration](https://activemq.apache.org/xml-configuration)
  * [ActiveMQ Undelivered Message](https://activemq.apache.org/components/artemis/documentation/1.4.0/undelivered-messages.html)
  * [ActiveMQ Message Redelivery and DLQ Handling](http://activemq.apache.org/message-redelivery-and-dlq-handling.html)
  * [ActiveMQ Essential Series: Introduction](https://www.hivemq.com/blog/mqtt-essentials-part-1-introducing-mqtt/)
  * [ActiveMQ Essential Series: QoS](https://www.hivemq.com/blog/mqtt-essentials-part-6-mqtt-quality-of-service-levels/)
  * [ActiveMQ Essential Series: Retain](https://www.hivemq.com/blog/mqtt-essentials-part-8-retained-messages/)
  * [ActiveMQ Essential Series: Persistent Session](https://www.hivemq.com/blog/mqtt-essentials-part-7-persistent-session-queuing-messages/)
  * [ActiveMQ Essential Series: MQTT Topics & Best Practices](https://www.hivemq.com/blog/mqtt-essentials-part-5-mqtt-topics-best-practices/)
  * [消息队列----ActiveMQ基本原理](https://blog.csdn.net/ningjiebing/article/details/90599455)
  * [Message Durability in ActiveMQ 5.x](https://blog.christianposta.com/activemq/message-durability-in-activemq-5-x/)
  * [ActiveMQ Queue vs. ActiveMQ Topic](https://www.openlogic.com/blog/activemqs-dynamic-queue-creation-working-you)
  * [ActiveMQ Displatch Policy](https://activemq.apache.org/dispatch-policies)
  * [ActiveMQ CMS-C++ Messaging Service](http://activemq.apache.org/components/cms/documentation)
  * [ActiveMQ NMS-.Net Message Service API](http://activemq.apache.org/components/nms/documentation)
  * [ActiveMQ日志输出，打印详细日志](https://blog.csdn.net/sinat_36938266/article/details/53503410)
  * [ActiveMQ Logging Interceptor](https://activemq.apache.org/logging-interceptor)
  * [ActiveMQ In Action about Logging](https://livebook.manning.com/book/activemq-in-action/chapter-14/30)
  * [ActiveMQ Monitor Tool](https://www.site24x7.com/help/log-management/activemq-logs.html)
  * [ActiveMQ服务重启收不到消息](https://blog.csdn.net/tiantiandjava/article/details/50914013)
  * [ActiveMQ高并发发送消息异常解决方法](https://blog.csdn.net/wsyyyyy/article/details/79888521)
  * [ActiveMQ 用户名密码设置](https://www.cnblogs.com/MIC2016/p/6196789.html)
  * [ActiveMQ主题/订阅模式添加用户名、密码、IP的连接验证](https://blog.csdn.net/qq_37306041/article/details/82626785)
  * [MQTT 5.x Protocol Details](https://docs.oasis-open.org/mqtt/mqtt/v5.0/os/mqtt-v5.0-os.html)
  * [MQTTNet](https://github.com/chkr1011/MQTTnet)
  * [C#使用MQTTnet快速实现MQTT通信](https://blog.csdn.net/panwen1111/article/details/79245161)
  * [MQTTNet Github](https://github.com/chkr1011/MQTTnet)
  * [MQTTNet Client](https://github.com/chkr1011/MQTTnet/wiki/Client)
  * [MQTTNet ManagedClient](https://github.com/chkr1011/MQTTnet/wiki/ManagedClient)
  * [MQTTNet Tips and Tricks](https://github.com/chkr1011/MQTTnet/wiki/Tips-and-Tricks)
  * [MQTTNet Topics](https://github.com/chkr1011/MQTTnet/wiki/MQTT-topics)
  * [MQTTNet Trace](https://github.com/chkr1011/MQTTnet/wiki/Trace)
  * [C#使用MQTTnet快速实现MQTT通信](https://blog.csdn.net/panwen1111/article/details/79245161)
  
## MRTA
  * [Task allocation in other company](http://www.okagv.com/agvzs_14442827.html)
  * [多机器人任务分配研究与实现](https://www.docin.com/p-779098001.html)
  * [A survey of literature on automated storage and retrieval systems](https://www.semanticscholar.org/paper/A-survey-of-literature-on-automated-storage-and-Roodbergen-Vis/797a66b2d8ab1cef38662e6579b80576fe284d78)
  * [Multi-robot Task Allocation: A Review of the State-of-the-Art](https://www.semanticscholar.org/paper/Multi-robot-Task-Allocation%3A-A-Review-of-the-Khamis-Hussein/ed65a6529e158c1402ea6bdeb679f5654ba33584)
  
## NHibernate
  * [Fluent NHibernate介绍和入门指南](https://www.cnblogs.com/13yan/p/5685307.html)
  * [NHibernate Tutorial](https://www.tutorialspoint.com/nhibernate/nhibernate_quick_guide.htm)

## OpenTCS
  * [Overview](https://www.opentcs.org/en/index.html)
  * [Distribution Documentation](https://www.opentcs.org/docs/5.0/index.html)
  * [Dev Guide](https://www.opentcs.org/docs/5.0/developer/developers-guide/opentcs-developers-guide.html)
  * [User Guide](https://www.opentcs.org/docs/5.0/user/opentcs-users-guide.html)
  
## Operating System
  * [CS-Notes](https://github.com/CyC2018/CS-Notes)
  * [计算机操作系统_银行家算法](https://blog.csdn.net/qq_36260974/article/details/84404369)
  
## ROS调度以及路径规划算法
  * [Introduction](https://blog.csdn.net/hcx25909/article/details/8795043)
  * [Architecture](https://blog.csdn.net/hcx25909/article/details/8795211)
  * [Tutorial](https://blog.csdn.net/hcx25909/article/details/8811313)
  * [AVG调度方法入门](https://blog.csdn.net/robinvista/article/details/73348711)
  * [基于时间窗的AGV调度算法优化](https://blog.csdn.net/jack_20/article/details/79453336)
  * [基于时间窗的AGV任务调度方法](https://xueshu.baidu.com/usercenter/paper/show?paperid=1p6b0rr0y95g08q0bt2a0cv0ff321571&site=xueshu_se)
  * [无向Petri网的多AGV最优路径方法研究](https://xueshu.baidu.com/usercenter/paper/show?paperid=6706f8516585e20dd31be412b77af2e3)
  * [基于CBS算法的物流分拣多AGV路径规划的研究](http://www.doc88.com/p-1476197853385.html)
  * [Reservation Table Introduction](http://www.ecs.umass.edu/ece/koren/architecture/ResTable/SimpRes/)
  * [Path planning for Robotic Mobile Fulfillment Systems](https://arxiv.org/abs/1706.09347)
  * [WHCA* - Windowed Hirarchical Cooperative A *](https://www.aaai.org/Library/AIIDE/2005/aiide05-020.php)
  * [Conflict-Based Search For Optimal Multi-Agent Path Finding](https://www.sciencedirect.com/science/article/pii/S0004370214001386)
  * [CBS Implementation: RawSim-O](https://github.com/merschformann/RAWSim-O)
  * [MA-CBS](https://www.sciencedirect.com/science/article/pii/S0004370214001386)
  * [MO-CBS](https://arxiv.org/abs/2101.03805)
  
## Spark教程
  * [Spark入门教程（1）——spark是什么及发展趋势概述](https://blog.csdn.net/xwc35047/article/details/51072145)
  * [Spark入门教程(2)---开发、编译配置](https://blog.csdn.net/xwc35047/article/details/51119608)
  * [spark入门教程（3）--Spark 核心API开发](https://blog.csdn.net/xwc35047/article/details/51146622)
 
## 多线程
  * [三种分布式锁](https://blog.csdn.net/wuzhiwei549/article/details/80692278)
  * [函数可重入性](https://blog.csdn.net/acs713/article/details/20034511) 
  
## 古月居
  * [CSDN](http://blog.csdn.net/hcx25909)
  * [个人网站](https://www.guyuehome.com/)
 
