# ROS_Algorithms
algorithms about robot operation systems
  * [Offical Website](https://www.ros.org/)

## A* Algorithm
  * [A*介绍](https://blog.csdn.net/qq_36946274/article/details/81982691)
  * [算法动态演示](https://github.com/zhm-real/PathPlanning)
## DWA Algorithm
  * [DWA介绍](https://www.cnblogs.com/kuangxionghui/p/8484803.html)
## ROS
  * [Introduction](https://blog.csdn.net/hcx25909/article/details/8795043)
  * [Architecture](https://blog.csdn.net/hcx25909/article/details/8795211)
  * [Tutorial](https://blog.csdn.net/hcx25909/article/details/8811313)
## Casun
  * [Official Website](http://www.casun.cn/)
## 古月居
  * [CSDN](http://blog.csdn.net/hcx25909)
  * [个人网站](https://www.guyuehome.com/)
## IO多路复用
  * [select](https://www.cnblogs.com/skyfsm/p/7079458.html)
  * [poll](https://www.cnblogs.com/orlion/p/6142838.html)
  * [epoll](https://blog.csdn.net/shenya1314/article/details/73691088)
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
