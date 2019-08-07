# ubuntu操作系统笔记

## 1.1 IaaS (基础设施即服务)

```markdown
IaaS（Infrastructure as a Service），即基础设施即服务。
消费者通过Internet 可以从完善的计算机基础设施获得服务。这类服务称为基础设施即服务。基于 Internet 的服务（如存储和数据库）是 IaaS的一部分。Internet上其他类型的服务包括平台即服务（Platform as a Service，PaaS）和软件即服务（Software as a Service，SaaS）。PaaS提供了用户可以访问的完整或部分的应用程序开发，SaaS则提供了完整的可直接使用的应用程序，比如通过 Internet管理企业资源。
```



##1.2 Linux 简介

```markdown
Linux 是一种自由和开放源码的类 UNIX 操作系统，使用 Linux 内核。目前存在着许多不同的 Linux 发行版，可安装在各种各样的电脑硬件设备，从手机、平板电脑、路由器和影音游戏控制台，到桌上型电脑，大型电脑和超级电脑。 Linux 是一个领先的操作系统，世界上运算最快的 10 台超级电脑运行的都是 Linux 操作系统。

Linux 操作系统也是自由软件和开放源代码发展中最著名的例子。只要遵循 GNU 通用公共许可证,任何人和机构都可以自由地使用 Linux 的所有底层源代码，也可以自由地修改和再发布。严格来讲，Linux 这个词本身只表示 Linux 内核，但在实际上人们已经习惯了用 Linux 来形容整个基于 Linux 内核，并且使用 GNU 工程各种工具和数据库的操作系统 (也被称为 GNU/ Linux)。通常情况下，Linux 被打包成供桌上型电脑和服务器使用的 Linux 发行版本。一些流行的主流 Linux 发行版本，包括 Debian (及其衍生版本 Ubuntu)，Fedora 和 OpenSUSE 等。Kernel + Softwares + Tools 就是 Linux Distribution

目前市面上较知名的发行版有：Ubuntu、RedHat、CentOS、Debian、Fedora、SuSE、OpenSUSE、TurboLinux、BluePoint、RedFlag、Xterm、SlackWare等。
```



##1.3 Linux 远程控制管理

### 1.3.1 概述

```markdown
传统的网络服务程序，FTP、POP、telnet 本质上都是不安全的，因为它们在网络上通过明文传送口令和数据，这些数据非常容易被截获。SSH 叫做 Secure Shell。通过 SSH，可以把传输数据进行加密，预防攻击，传输的数据进行了压缩，可以加快传输速度。
```

### 1.3.2 OpenSSH

```markdown
SSH 是芬兰一家公司开发。但是受到版权和加密算法限制，现在很多人都使用 OpenSSH。OpenSSH 是 SSH 的替代软件，免费。

OpenSSH 由客户端和服务端组成。

基于口令的安全验证：知道服务器的帐号密码即可远程登录，口令和数据在传输过程中都会被加密。

基于密钥的安全验证：此时需要在创建一对密钥，把公有密钥放到远程服务器上自己的宿主目录中，而私有密钥则由自己保存。
```

- 检查是否安装

  ```powershell
  apt-cache policy openssh-client openssh-server
  ```

- 安装服务端

  ```powershell
  apt-get install openssh-server
  ```

- 安装客户端

  ```powershell
  apt-get install openssh-client
  ```

- OpenSSH 服务器的主要配置文件为 `/etc/ssh/sshd\_config`，几乎所有的配置信息都在此文件中。

### 1.3.3 XShell

```markdown
XShell 是一个强大的安全终端模拟软件，它支持 SSH1, SSH2, 以及 Microsoft Windows 平台的 TELNET 协议。XShell 通过互联网到远程主机的安全连接以及它创新性的设计和特色帮助用户在复杂的网络环境中享受他们的工作。

XShell 可以在 Windows 界面下用来访问远端不同系统下的服务器，从而比较好的达到远程控制终端的目的。
```

效果图如下：

![](C:\Users\ASUS\Desktop\My application\My File\个人笔记\linux\ubuntu\XShell.PNG)



## 1.4 Linux的目录结构

![](C:\Users\ASUS\Desktop\My application\My File\个人笔记\linux\ubuntu\linux-dir.png)

| **目录** | **说明**                                                     |
| -------- | ------------------------------------------------------------ |
| bin      | 存放二进制可执行文件（ls,cat,mkdir等）                       |
| boot     | 存放用于系统引导时使用的各种文件                             |
| dev      | 用于存放设备文件                                             |
| etc      | 存放系统配置文件                                             |
| home     | 存放所有用户文件的根目录                                     |
| lib      | 存放跟文件系统中的程序运行所需要的共享库及内核模块           |
| mnt      | 系统管理员安装临时文件系统的安装点                           |
| opt      | 额外安装的可选应用程序包所放置的位置                         |
| proc     | 虚拟文件系统，存放当前内存的映射                             |
| root     | 超级用户目录                                                 |
| sbin     | 存放二进制可执行文件，只有root才能访问                       |
| tmp      | 用于存放各种临时文件                                         |
| usr      | 用于存放系统应用程序，比较重要的目录/usr/local 本地管理员软件安装目录 |
| var      | 用于存放运行时需要改变数据的文件                             |



##1.5 Linux 操作文件目录的命令

| 命令  | 说明                               | 语法                                            | 参数  | 参数说明                           |
| :---- | :--------------------------------- | :---------------------------------------------- | :---- | :--------------------------------- |
| ls    | 显示文件和目录列表                 | ls [-alrtAFR] [name...]                         |       |                                    |
|       |                                    |                                                 | -l    | 列出文件的详细信息                 |
|       |                                    |                                                 | -a    | 列出当前目录所有文件，包含隐藏文件 |
| mkdir | 创建目录                           | mkdir [-p] dirName                              |       |                                    |
|       |                                    |                                                 | -p    | 父目录不存在情况下先生成父目录     |
| cd    | 切换目录                           | cd [dirName]                                    |       |                                    |
| touch | 生成一个空文件                     |                                                 |       |                                    |
| echo  | 生成一个带内容文件                 | echo abcd > 1.txt，echo 1234 >> 1.txt           |       |                                    |
| cat   | 显示文本文件内容                   | cat [-AbeEnstTuv] [--help] [--version] fileName |       |                                    |
| cp    | 复制文件或目录                     | cp [options] source dest                        |       |                                    |
| rm    | 删除文件                           | rm [options] name...                            |       |                                    |
|       |                                    |                                                 | -f    | 强制删除文件或目录                 |
|       |                                    |                                                 | -r    | 同时删除该目录下的所有文件         |
| mv    | 移动文件或目录                     | mv [options] source dest                        |       |                                    |
| find  | 在文件系统中查找指定的文件         |                                                 |       |                                    |
|       |                                    |                                                 | -name | 文件名                             |
| grep  | 在指定的文本文件中查找指定的字符串 |                                                 |       |                                    |
| tree  | 用于以树状图列出目录的内容         |                                                 |       |                                    |
| pwd   | 显示当前工作目录                   |                                                 |       |                                    |
| ln    | 建立软链接                         |                                                 |       |                                    |
| more  | 分页显示文本文件内容               |                                                 |       |                                    |
| head  | 显示文件开头内容                   |                                                 |       |                                    |
| tail  | 显示文件结尾内容                   |                                                 |       |                                    |
|       |                                    |                                                 | -f    | 跟踪输出                           |



## 1.6 Linux 系统管理命令

| 命令     | 说明                                         |
| :------- | :------------------------------------------- |
| stat     | 显示指定文件的相关信息,比ls命令显示内容更多  |
| who      | 显示在线登录用户                             |
| hostname | 显示主机名称                                 |
| uname    | 显示系统信息                                 |
| top      | 显示当前系统中耗费资源最多的进程             |
| ps       | 显示瞬间的进程状态                           |
| du       | 显示指定的文件（目录）已使用的磁盘空间的总量 |
| df       | 显示文件系统磁盘空间的使用情况               |
| free     | 显示当前内存和交换空间的使用情况             |
| ifconfig | 显示网络接口信息                             |
| ping     | 测试网络的连通性                             |
| netstat  | 显示网络状态信息                             |
| clear    | 清屏                                         |
| kill     | 杀死一个进程                                 |



## 1.7 Linux 开关机命令

| shutdown | shutdown [-t seconds] [-rkhncfF] time [message] |            |                                                              |
| -------- | ----------------------------------------------- | ---------- | ------------------------------------------------------------ |
|          |                                                 | -t seconds | 设定在几秒钟之后进行关机程序                                 |
|          |                                                 | -k         | 并不会真的关机，只是将警告讯息传送给所有只用者               |
|          |                                                 | -r         | 关机后重新开机（重启）                                       |
|          |                                                 | -h         | 关机后停机                                                   |
|          |                                                 | -n         | 不采用正常程序来关机，用强迫的方式杀掉所有执行中的程序后自行关机 |
|          |                                                 | -c         | 取消目前已经进行中的关机动作                                 |
|          |                                                 | -f         | 关机时，不做 fcsk 动作(检查 Linux 档系统)                    |
|          |                                                 | -F         | 关机时，强迫进行 fsck 动作                                   |
|          |                                                 | time       | 设定关机的时间                                               |
|          |                                                 | message    | 传送给所有使用者的警告讯息                                   |

- 重启
  - reboot
  - shutdown -r now
- 关机
  - shutdown -h now

## 1.8 Linux 压缩命令

- tar(操作文件夹)

  | 命令 | 语法                                        | 参数 | 参数说明                        |
  | :--- | :------------------------------------------ | :--- | :------------------------------ |
  | tar  | tar [-cxzjvf] 压缩打包文档的名称 欲打包目录 |      |                                 |
  |      |                                             | -c   | 建立一个归档文件的参数指令      |
  |      |                                             | -x   | 解开一个归档文件的参数指令      |
  |      |                                             | -z   | 是否需要用 gzip 压缩            |
  |      |                                             | -j   | 是否需要用 bzip2 压缩           |
  |      |                                             | -v   | 压缩的过程中显示文件            |
  |      |                                             | -f   | 使用档名，在 f 之后要立即接档名 |
  |      |                                             | -tf  | 查看归档文件里面的文件          |

  **例子：**

  - 压缩文件夹：`tar -zcvf test.tar.gz test\`
  - 解压文件夹：`tar -zxvf test.tar.gz`

- gzip

  | 命令 | 语法                               | 参数 | 参数说明                                                     |
  | :--- | :--------------------------------- | :--- | :----------------------------------------------------------- |
  | gzip | gzip [选项] 压缩（解压缩）的文件名 |      |                                                              |
  |      |                                    | -d   | 解压缩                                                       |
  |      |                                    | -l   | 对每个压缩文件，显示压缩文件的大小，未压缩文件的大小，压缩比，未压缩文件的名字 |
  |      |                                    | -v   | 对每一个压缩和解压的文件，显示文件名和压缩比                 |
  |      |                                    | -num | 用指定的数字num调整压缩的速度，-1或--fast表示最快压缩方法（低压缩比），-9或--best表示最慢压缩方法（高压缩比）。系统缺省值为6 |

  **说明**：压缩文件后缀为`gz`

- bzip2

  | 命令  | 语法         | 参数 | 参数说明                                                     |
  | :---- | :----------- | :--- | :----------------------------------------------------------- |
  | bzip2 | bzip2 [-cdz] |      |                                                              |
  |       |              | -d   | 解压缩                                                       |
  |       |              | -z   | 压缩参数                                                     |
  |       |              | -num | 用指定的数字num调整压缩的速度，-1或--fast表示最快压缩方法（低压缩比），-9或--best表示最慢压缩方法（高压缩比）。系统缺省值为6 |

  **说明**：压缩文件后缀为`bz2`

## 1.9 软件包管理

### 1.9.1 概述

```markdown
APT(Advanced Packaging Tool) 是 Debian/Ubuntu 类 Linux 系统中的软件包管理程序(centos 使用的时yum), 使用它可以找到想要的软件包, 而且安装、卸载、更新都很简便；也可以用来对 Ubuntu 进行升级; APT 的源文件为 /etc/apt/ 目录下的 sources.list 文件。
```

### 1.9.2 修改数据源

由于国内的网络环境问题，我们需要将 Ubuntu 的数据源修改为国内数据源（下载安装会更快），操作步骤如下：

- 查看系统版本

  ![](C:\Users\ASUS\Desktop\My application\My File\个人笔记\linux\ubuntu\查看系统版本.png)

- 编辑数据源

  - 可用源

  | 源       | 源地址                       | ping测试 min/avg/max(ms) |
  | -------- | ---------------------------- | ------------------------ |
  | 阿里云   | mirrors.aliyun.com           | 6.850/8.869/19.176       |
  | 中科大   | mirrors.ustc.edu.cn          | 30.355/35.616/50.826     |
  | 网易     | mirrors.163.com              | 7.026/8.235/18.526       |
  | 清华大学 | mirrors.tuna.tsinghua.edu.cn | 30.747/32.264/33.513     |
  | 东北大学 | mirror.neu.edu.cn            | 55.342/56.719/61.365     |

  - 修改

    进入镜像网站，再点击`ubuntu->帮助`就会看到相应的源内容，根据版本号，复制相应的源，替换掉/etc/apt/ 目录下的 sources.list 文件中的内容

- 更新源

  ```shell
  apt-get update
  ```

- 补充内容

  - 源后面main restricted universe multiverse的含义
  - main: 完全的自由软件。
  - restricted: 不完全的自由软件。
  - universe: Ubuntu官方不提供支持与补丁，全靠社区支持

### 1.9.3 常用APT命令

安装软件包

```text
apt-get install packagename
```

删除软件包

```text
apt-get remove packagename
```

更新软件包列表

```text
apt-get update
```

升级有可用更新的系统（慎用）

```text
apt-get upgrade
```

搜索

```text
apt-cache search package
```

获取包信息

```text
apt-cache show package
```

删除包及配置文件

```text
apt-get remove package --purge
```

了解使用依赖

```text
apt-cache depends package
```

查看被哪些包依赖

```text
apt-cache rdepends package
```

安装相关的编译环境

```text
apt-get build-dep package
```

下载源代码

```text
apt-get source package
```

清理无用的包

```text
apt-get clean && apt-get autoclean
```

检查是否有损坏的依赖

```text
apt-get check
```

## 2.0 安装java

### 2.0.1 下载jdk

![](C:\Users\ASUS\Desktop\My application\My File\个人笔记\linux\ubuntu\下载jdk.png)

### 2.0.2 将下载的压缩包传输到服务器

![](C:\Users\ASUS\Desktop\My application\My File\个人笔记\linux\ubuntu\xshell打开传输工具.png)

![](C:\Users\ASUS\Desktop\My application\My File\个人笔记\linux\ubuntu\xftp传输.png)

### 2.0.3 开始安装

- 解压缩

  ```shell
  tar -zxvf jdk-8u211-linux-x64.tar.gz
  ```

- 创建安装目录

  ```shell
  mkdir -p /usr/local/java
  ```

- 移动安装包

  ```shell
  mv jdk-8u211-linux-x64/ /usr/local/java/
  ```

- 配置用户环境变量

  - 打开配置文件

  ```shell
  vi /etc/profile
  ```

  - 添加如下语句

    ![](C:\Users\ASUS\Desktop\My application\My File\个人笔记\linux\ubuntu\配置java用户环境变量.png)

> 系统变量位置：/etc/environment,此处配不配都可以

- 使用户环境变量生效

  ```shell
  source /etc/profile
  ```

- 测试是否安装成功

  ```shell
  java -version
  ```

  

## 2.1 安装Tomcat

步骤基本同 2.0 一致

## 2.2 安装MySql

- 可以使用apt-get 直接安装

  ```shell
  apt-get install mysql-server
  ```

- 配置远程访问

  - 修改配置文件

    ```shell
    nano /etc/mysql/mysql.conf.d/mysqld.cnf
    ```

    ![](C:\Users\ASUS\Desktop\My application\My File\个人笔记\linux\ubuntu\配置mysql远程访问.png)

    此时所有的主机都可以进行远程访问mysql,默认值为127.0.0.1

  - 重启mysql

    ```shell
    service mysql restart
    ```

  - 登陆mysql

    ```shell
    mysql -u root -p
    ```

  - 授权root用户，允许所有人连接root

    ```shell
    grant all privileges on *.* to 'root'@'%' identified by '你的 mysql root 账户密码';
    ```




## 2.3 磁盘逻辑分区扩容

### 2.3.1 查看计算机空间大小

```shell
root@ubuntu:~# parted -l
Model: VMware, VMware Virtual S (scsi)
Disk /dev/sda: 21.5GB
Sector size (logical/physical): 512B/512B
Partition Table: gpt
Disk Flags: 

Number  Start   End     Size    File system  Name  Flags
 1      1049kB  2097kB  1049kB                     bios_grub
 2      2097kB  1076MB  1074MB  ext4
 3      1076MB  21.5GB  20.4GB
......
```

### 2.3.2 查看文件系统占用情况

```shell
root@ubuntu:~# df -h 或 df -h [path]
# 文件系统                         大小   已占用 可用     占用率 挂载点
Filesystem                         Size  Used Avail Use% Mounted on
udev                               457M     0  457M   0% /dev
tmpfs                               98M  1.3M   97M   2% /run
/dev/mapper/ubuntu--vg-ubuntu--lv  3.9G  3.8G  1.4G  100% /
tmpfs                              488M     0  488M   0% /dev/shm
tmpfs                              5.0M     0  5.0M   0% /run/lock
tmpfs                              488M     0  488M   0% /sys/fs/cgroup
/dev/loop0                          89M   89M     0 100% /snap/core/6964
/dev/loop1                          91M   91M     0 100% /snap/core/6350
/dev/sda2                          976M  143M  767M  16% /boot
tmpfs                               98M     0   98M   0% /run/user/0

```

### 2.3.3 扩展逻辑分区

```shell
$ lvextend   -L   +<size> <文件系统> # size 大小，例如2G

# 使用resize2fs 重新加载逻辑卷大小
$ resize2fs <文件系统>
```

例：

扩容

![](C:\Users\ASUS\Desktop\My application\My File\个人笔记\linux\ubuntu\扩容.png)

重新加载逻辑卷

![](C:\Users\ASUS\Desktop\My application\My File\个人笔记\linux\ubuntu\重新加载逻辑卷大小.png)

查看是否生效

![](C:\Users\ASUS\Desktop\My application\My File\个人笔记\linux\ubuntu\2019-06-24_171151.png)