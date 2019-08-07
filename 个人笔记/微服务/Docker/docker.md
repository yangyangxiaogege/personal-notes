# Docker笔记

## 1.1 Docker 简介

### 1.1.1 概述

```reStructuredText
Docker 最初是 dotCloud 公司创始人 Solomon Hykes 在法国期间发起的一个公司内部项目，它是基于 dotCloud 公司多年云服务技术的一次革新，并于 2013 年 3 月以 Apache 2.0 授权协议开源，主要项目代码在 GitHub 上进行维护。Docker 项目后来还加入了 Linux 基金会，并成立推动 开放容器联盟（OCI）。

Docker 自开源后受到广泛的关注和讨论，至今其 GitHub 项目已经超过 4 万 6 千个星标和一万多个 fork。甚至由于 Docker 项目的火爆，在 2013 年底，dotCloud 公司决定改名为 Docker。Docker 最初是在 Ubuntu 12.04 上开发实现的；Red Hat 则从 RHEL 6.5 开始对 Docker 进行支持；Google 也在其 PaaS 产品中广泛应用 Docker。

Docker 使用 Google 公司推出的 Go 语言 进行开发实现，基于 Linux 内核的 cgroup，namespace，以及 AUFS 类的 Union FS 等技术，对进程进行封装隔离，属于 操作系统层面的虚拟化技术。由于隔离的进程独立于宿主和其它的隔离的进程，因此也称其为容器。最初实现是基于 LXC，从 0.7 版本以后开始去除 LXC，转而使用自行开发的 libcontainer，从 1.11 开始，则进一步演进为使用 runC 和 containerd。

Docker 在容器的基础上，进行了进一步的封装，从文件系统、网络互联到进程隔离等等，极大的简化了容器的创建和维护。使得 Docker 技术比虚拟机技术更为轻便、快捷。

下面的图片比较了 Docker 和传统虚拟化方式的不同之处。传统虚拟机技术是虚拟出一套硬件后，在其上运行一个完整操作系统，在该系统上再运行所需应用进程；而容器内的应用进程直接运行于宿主的内核，容器内没有自己的内核，而且也没有进行硬件虚拟。因此容器要比传统虚拟机更为轻便。

```

![](C:\Users\ASUS\Desktop\My application\My File\个人笔记\微服务\Docker\virtualization.png)

![](C:\Users\ASUS\Desktop\My application\My File\个人笔记\微服务\Docker\docker.png)

### 1.1.2 为什么要使用DOcker

- 概述

  ```tex
  作为一种新兴的虚拟化方式，Docker 跟传统的虚拟化方式相比具有众多的优势。
  ```

- 更高效的利用系统资源

  ```tex
  由于容器不需要进行硬件虚拟以及运行完整操作系统等额外开销，Docker 对系统资源的利用率更高。无论是应用执行速度、内存损耗或者文件存储速度，都要比传统虚拟机技术更高效。因此，相比虚拟机技术，一个相同配置的主机，往往可以运行更多数量的应用
  ```

- 更快速的启动时间

  ```tex
  传统的虚拟机技术启动应用服务往往需要数分钟，而 Docker 容器应用，由于直接运行于宿主内核，无需启动完整的操作系统，因此可以做到秒级、甚至毫秒级的启动时间。大大的节约了开发、测试、部署的时间
  ```

- 一致的运行环境

  ```tex
  开发过程中一个常见的问题是环境一致性问题。由于开发环境、测试环境、生产环境不一致，导致有些 bug 并未在开发过程中被发现。而 Docker 的镜像提供了除内核外完整的运行时环境，确保了应用运行环境一致性，从而不会再出现 「这段代码在我机器上没问题啊」 这类问题。
  ```

- 持续交付和部署

  ```tex
  对开发和运维（DevOps）人员来说，最希望的就是一次创建或配置，可以在任意地方正常运行。
  
  使用 Docker 可以通过定制应用镜像来实现持续集成、持续交付、部署。开发人员可以通过 Dockerfile 来进行镜像构建，并结合 持续集成(Continuous Integration) 系统进行集成测试，而运维人员则可以直接在生产环境中快速部署该镜像，甚至结合 持续部署(Continuous Delivery/Deployment) 系统进行自动部署。
  
  而且使用 Dockerfile 使镜像构建透明化，不仅仅开发团队可以理解应用运行环境，也方便运维团队理解应用运行所需条件，帮助更好的生产环境中部署该镜像。
  ```

- 更轻松的迁移

  ```tex
  由于 Docker 确保了执行环境的一致性，使得应用的迁移更加容易。Docker 可以在很多平台上运行，无论是物理机、虚拟机、公有云、私有云，甚至是笔记本，其运行结果是一致的。因此用户可以很轻易的将在一个平台上运行的应用，迁移到另一个平台上，而不用担心运行环境的变化导致应用无法正常运行的情况。
  ```

- 更轻松的维护和扩展

  ```tex
  Docker 使用的分层存储以及镜像的技术，使得应用重复部分的复用更为容易，也使得应用的维护更新更加简单，基于基础镜像进一步扩展镜像也变得非常简单。此外，Docker 团队同各个开源项目团队一起维护了一大批高质量的 官方镜像，既可以直接在生产环境使用，又可以作为基础进一步定制，大大的降低了应用服务的镜像制作成本。
  ```

- 对比传统的虚拟机总结

  | 特性       | 容器               | 虚拟机      |
  | :--------- | :----------------- | :---------- |
  | 启动       | 秒级               | 分钟级      |
  | 硬盘使用   | 一般为 `MB`        | 一般为 `GB` |
  | 性能       | 接近原生           | 弱于        |
  | 系统支持量 | 单机支持上千个容器 | 一般几十个  |

## 1.2 Docker 引擎

### 1.2.1 概述

- Docker 引擎是一个包含以下主要组件的客户端服务器应用程序（C/S）。
  - 一种服务器，它是一种称为守护进程并且长时间运行的程序。
  - REST API用于指定程序可以用来与守护进程通信的接口，并指示它做什么。
  - 一个有命令行界面 (CLI) 工具的客户端。

- Docker 引擎组件的流程如下图所示：

  ![](C:\Users\ASUS\Desktop\My application\My File\个人笔记\微服务\Docker\620140640_31678.png)

> 简单来说：就是我们在客户端通过Docker Server 端的一系列REST API 来远程操作docker server

## 1.3 Docker 系统架构

### 1.3.1 概述

- Docker 使用客户端-服务器 (C/S) 架构模式，使用远程 API 来管理和创建 Docker 容器。

- Docker 容器通过 Docker 镜像来创建。

- 容器与镜像的关系类似于面向对象编程中的对象与类。

  | Docker | 面向对象 |
  | :----- | :------- |
  | 容器   | 对象     |
  | 镜像   | 类       |

![](C:\Users\ASUS\Desktop\My application\My File\个人笔记\微服务\Docker\docker 容器构建过程.png)

| 标题            | 说明                                                         |
| :-------------- | :----------------------------------------------------------- |
| 镜像(Images)    | Docker 镜像是用于创建 Docker 容器的模板。                    |
| 容器(Container) | 容器是独立运行的一个或一组应用。                             |
| 客户端(Client)  | Docker 客户端通过命令行或者其他工具使用 Docker API (<https://docs.docker.com/reference/api/docker_remote_api>) 与 Docker 的守护进程通信。 |
| 主机(Host)      | 一个物理或者虚拟的机器用于执行 Docker 守护进程和容器。       |
| 仓库(Registry)  | Docker 仓库用来保存镜像，可以理解为代码控制中的代码仓库。Docker Hub([https://hub.docker.com](https://hub.docker.com/)) 提供了庞大的镜像集合供使用。 |
| Docker Machine  | Docker Machine是一个简化Docker安装的命令行工具，通过一个简单的命令行即可在相应的平台上安装Docker，比如VirtualBox、 Digital Ocean、Microsoft Azure。 |

## 1.4 Ubuntu 安装 Docker

### 1.4.1 安装注意事项

> 警告：切勿在没有配置 Docker APT 源的情况下直接使用 apt 命令安装 Docker。

###1.4.2 系统要求

Docker CE 支持以下版本(或者更高)的 [Ubuntu](https://www.ubuntu.com/server) 操作系统：

- Artful 17.10 (Docker CE 17.11 Edge +)
- Xenial 16.04 (LTS)
- Trusty 14.04 (LTS)

Docker CE 可以安装在 64 位的 x86 平台或 ARM 平台上。Ubuntu 发行版中，LTS（Long-Term-Support）长期支持版本，会获得 5 年的升级维护支持，这样的版本会更稳定，因此在生产环境中推荐使用 LTS 版本

###1.4.3 卸载旧版本（如果以前安装过较旧的版本）

- 旧版本的 Docker 称为 `docker` 或者 `docker-engine`，使用以下命令卸载旧版本：

  ```shell
  $ sudo apt-get remove docker \
                 docker-engine \
                 docker.io 
  ```

### 1.4.4 Ubuntu 14.04  可选内核模块

```tex
从 Ubuntu 14.04 开始，一部分内核模块移到了可选内核模块包 (linux-image-extra-*) ，以减少内核软件包的体积。正常安装的系统应该会包含可选内核模块包，而一些裁剪后的系统可能会将其精简掉。AUFS 内核驱动属于可选内核模块的一部分，作为推荐的 Docker 存储层驱动，一般建议安装可选内核模块包以使用 AUFS。

如果系统没有安装可选内核模块的话，可以执行下面的命令来安装可选内核模块包：
```

```shell
$ sudo apt-get update

$ sudo apt-get install \
    linux-image-extra-$(uname -r) \
    linux-image-extra-virtual
```

### 1.4.5 Ubuntu 16.04+

Ubuntu 16.04 + 上的 Docker CE 默认使用 `overlay2` 存储层驱动,无需手动配置(不用徐泽内核模块)

### 1.4.6 使用APT 安装

- 安装一些系统必要工具

  ```shell
  sudo apt-get update
  sudo apt-get -y install apt-transport-https ca-certificates curl software-properties-common
  ```

- 安装GPG证书

  ```shell
  curl -fsSL http://mirrors.aliyun.com/docker-ce/linux/ubuntu/gpg | sudo apt-key add -
  ```

- 写入软件源信息
  ```shell
  sudo add-apt-repository "deb [arch=amd64] http://mirrors.aliyun.com/docker-ce/linux/ubuntu $(lsb_release -cs) stable"
  ```

- 更新并安装Docker CE

  ```shell
  sudo apt-get -y update
  sudo apt-get -y install docker-ce
  ```

> 以上命令会添加稳定版本的 Docker CE APT 镜像源，如果需要最新或者测试版本的 Docker CE 请将 stable 改为 edge 或者 test。从 Docker 17.06 开始，edge test 版本的 APT 镜像源也会包含稳定版本的 Docker。

### 1.4.7 使用脚本自动安装

在测试或开发环境中 Docker 官方为了简化安装流程，提供了一套便捷的安装脚本，Ubuntu 系统上可以使用这套脚本安装：

```shell
$ curl -fsSL get.docker.com -o get-docker.sh
$ sudo sh get-docker.sh --mirror AzureChinaCloud
```

DaoCloud 脚本安装

```shell
curl -sSL https://get.daocloud.io/docker | sh
```



### 1.4.8 启动Docker 容器

```shell
$ sudo systemctl enable docker
$ sudo systemctl start docker
```

### 1.4.9 测试是否安装成功

```shell
$ sudo docker version
# 输出以下内容则安装成功
```

![](C:\Users\ASUS\Desktop\My application\My File\个人笔记\微服务\Docker\docker version.png)

### 1.4.10 配置Docker 镜像加速器

- 国内从 Docker Hub 拉取镜像有时会遇到困难（由网络原因造成），此时可以配置镜像加速器。Docker 官方和国内很多云服务商都提供了国内加速器服务，例如：
  - [Docker 官方提供的中国 registry mirror](https://docs.docker.com/registry/recipes/mirror/#use-case-the-china-registry-mirror)
  - [阿里云加速器](https://cr.console.aliyun.com/#/accelerator)
  - [DaoCloud 加速器](https://www.daocloud.io/mirror#accelerator-doc)

- 以 Docker 官方加速器为例进行介绍

  - 对于使用 [systemd](https://www.freedesktop.org/wiki/Software/systemd/) 的系统（Ubuntu 16.04+、Debian 8+、CentOS 7），请在 `/etc/docker/daemon.json` 中写入如下内容（**如果文件不存在请新建该文件**）

    ```shell
    {
      "registry-mirrors": [
        "https://registry.docker-cn.com"
      ]
    }
    ```

  - 重启服务

    ```shell
    $ sudo systemctl daemon-reload
    $ sudo systemctl restart docker
    ```

  - 检查加速器是否配置成功

    ```shell
    $ sudo docker info
    #如果看到以下内容，则配置成功
    Registry Mirrors:
     https://registry.docker-cn.com/
    ```

    

## 1.5 Docker 镜像

### 1.5.1 概述

```tex
镜像是Docker的三大组件之一，其余两个为容器和仓库

我们都知道，操作系统分为内核和用户空间。对于 Linux 而言，内核启动后，会挂载 root 文件系统为其提供用户空间支持。而 Docker 镜像（Image），就相当于是一个 root 文件系统。比如官方镜像 ubuntu:16.04 就包含了完整的一套 Ubuntu 16.04 最小系统的 root 文件系统。

Docker 镜像是一个特殊的文件系统，除了提供容器运行时所需的程序、库、资源、配置等文件外，还包含了一些为运行时准备的一些配置参数（如匿名卷、环境变量、用户等）。镜像不包含任何动态数据，其内容在构建之后也不会被改变。
```

### 1.5.2 分层存储

```markdown
因为镜像包含操作系统完整的 root 文件系统，其体积往往是庞大的，因此在 Docker 设计时，就充分利用 Union FS 的技术，将其设计为分层存储的架构。所以严格来说，镜像并非是像一个 ISO 那样的打包文件，镜像只是一个虚拟的概念，其实际体现并非由一个文件组成，而是由一组文件系统组成，或者说，由多层文件系统联合组成。

镜像构建时，会一层层构建，前一层是后一层的基础。每一层构建完就不会再发生改变，后一层上的任何改变只发生在自己这一层。比如，删除前一层文件的操作，实际不是真的删除前一层的文件，而是仅在当前层标记为该文件已删除。在最终容器运行的时候，虽然不会看到这个文件，但是实际上该文件会一直跟随镜像。因此，在构建镜像的时候，需要额外小心，每一层尽量只包含该层需要添加的东西，任何额外的东西应该在该层构建结束前清理掉。

分层存储的特征还使得镜像的复用、定制变的更为容易。甚至可以用之前构建好的镜像作为基础层，然后进一步添加新的层，以定制自己所需的内容，构建新的镜像。
```

### 1.5.3 获取镜像

- 之前提到过，[Docker Hub](https://hub.docker.com/explore/) 上有大量的高质量的镜像可以用，这里我们就说一下怎么获取这些镜像。

  从 Docker 镜像仓库获取镜像的命令是 `docker pull`。其命令格式为：

  ```shell
  docker pull [选项] [Docker Registry 地址[:端口号]/]仓库名[:标签]
  ```

  例：

  ```shell
  #命令中没有给出 Docker 镜像仓库地址，因此将会从 Docker Hub 获取镜像。而镜像名称是 ubuntu:16.04，因此将会获取官方镜像 library/ubuntu 仓库中标签为 16.04 的镜像。(如果配置了镜像加速器，则使用加速 器地址)
  $ docker pull ubuntu:16.04
  16.04: Pulling from library/ubuntu
  #下载过程中可以看到我们之前提及的分层存储的概念，镜像是由多层存储所构成。下载也是一层层的去下载，并非单一文件。下载过程中给出了每一层的 ID 的前 12 位。并且下载结束后，给出该镜像完整的 sha256 的摘要，以确保下载一致性。
  bf5d46315322: Pull complete
  9f13e0ac480c: Pull complete
  e8988b5b3097: Pull complete
  40af181810e7: Pull complete
  e6f7c7e5c03e: Pull complete
  Digest: sha256:147913621d9cdea08853f6ba9116c2e27a3ceffecf3b492983ae97c3d643fbbe
  Status: Downloaded newer image for ubuntu:16.04
  ```

###1.5.4 运行

有了镜像后，我们就能够以这个镜像为基础启动并运行一个容器。以上面的 ubuntu:16.04 为例，如果我们打算启动里面的 bash 并且进行交互式操作的话，可以执行下面的命令。

```shell
$ docker run -it --rm \
    ubuntu:16.04 \
    bash

root@e7009c6ce357:/# cat /etc/os-release
NAME="Ubuntu"
VERSION="16.04.4 LTS, Trusty Tahr"
ID=ubuntu
ID_LIKE=debian
PRETTY_NAME="Ubuntu 16.04.4 LTS"
VERSION_ID="16.04"
HOME_URL="http://www.ubuntu.com/"
SUPPORT_URL="http://help.ubuntu.com/"
BUG_REPORT_URL="http://bugs.launchpad.net/ubuntu/"
```

| **命令**   | 参数         | 说明                                                         |
| ---------- | ------------ | ------------------------------------------------------------ |
| docker run |              | 运行容器的命令                                               |
|            | -i           | 交互式                                                       |
|            | -t           | 终端                                                         |
|            | -- rm        | 这个参数是说容器退出后随之将其删除,可以避免浪费空间          |
|            | ubuntu:16.04 | 这是指用 `ubuntu:16.04` (image:tag)镜像为基础来启动容器      |
|            | bash         | 放在镜像名后的是**命令**，这里我们希望有个交互式 Shell，因此用的是 `bash` |

> 通过 `exit` 退出了这个容器

### 1.5.5 列出镜像

```shell
$ docker image ls # 或者使用 docker images
#仓库名               标签                镜像id              创建时间             大小
REPOSITORY           TAG                 IMAGE ID            CREATED             SIZE
redis                latest              5f515359c7f8        5 days ago          183 MB
nginx                latest              05a60462f8ba        5 days ago          181 MB
mongo                3.2                 fe9198c04d62        5 days ago          342 MB
<none>               <none>              00285df0df87        5 days ago          342 MB
ubuntu               16.04               f753707788c5        4 weeks ago         127 MB
ubuntu               latest              f753707788c5        4 weeks ago         127 MB
ubuntu               14.04               1e0c3dd64ccd        4 weeks ago         188 MB
```

**镜像 ID** 则是镜像的唯一标识，一个镜像可以对应多个**标签**。因此，在上面的例子中，我们可以看到 `ubuntu:16.04` 和 `ubuntu:latest` 拥有相同的 ID，因为它们对应的是同一个镜像。

### 1.5.6 镜像体积

```tex
如果仔细观察，会注意到，这里标识的所占用空间和在 Docker Hub 上看到的镜像大小不同。比如，ubuntu:16.04 镜像大小，在这里是 127 MB，但是在 Docker Hub 显示的却是 50 MB。这是因为 Docker Hub 中显示的体积是压缩后的体积。在镜像下载和上传过程中镜像是保持着压缩状态的，因此 Docker Hub 所显示的大小是网络传输中更关心的流量大小。而 docker image ls 显示的是镜像下载到本地后，展开的大小，准确说，是展开后的各层所占空间的总和，因为镜像到本地后，查看空间的时候，更关心的是本地磁盘空间占用的大小。

另外一个需要注意的问题是，docker image ls 列表中的镜像体积总和并非是所有镜像实际硬盘消耗。由于 Docker 镜像是多层存储结构，并且可以继承、复用，因此不同镜像可能会因为使用相同的基础镜像，从而拥有共同的层。由于 Docker 使用 Union FS，相同的层只需要保存一份即可，因此实际镜像硬盘占用空间很可能要比这个列表镜像大小的总和要小的多。
```

你可以通过以下命令来便捷的查看镜像、容器、数据卷所占用的空间。

```shell
$ docker system df

TYPE                TOTAL               ACTIVE              SIZE                RECLAIMABLE
Images              24                  0                   1.992GB             1.992GB (100%)
Containers          1                   0                   62.82MB             62.82MB (100%)
Local Volumes       9                   0                   652.2MB             652.2MB (100%)
Build Cache  
```

###1.5.7 虚悬镜像

上面的镜像列表中，还可以看到一个特殊的镜像，这个镜像既没有仓库名，也没有标签，均为 `<none>`。

```shell
<none>               <none>              00285df0df87        5 days ago          342 MB
```

这个镜像原本是有镜像名和标签的，随着官方镜像维护，发布了新版本后，重新 `docker pull xxx `时，`xxx`这个镜像名被转移到了新下载的镜像身上，而旧的镜像上的这个名称则被取消，从而成为了 `<none>`。除了 `docker pull` 可能导致这种情况，`docker build` 也同样可以导致这种现象。由于新旧镜像同名，旧镜像名称被取消，从而出现仓库名、标签均为 `<none>` 的镜像。这类无标签镜像也被称为 **虚悬镜像(dangling image)** ，可以用下面的命令专门显示这类镜像：

```shell
$ docker image ls -f dangling=true
REPOSITORY          TAG                 IMAGE ID            CREATED             SIZE
<none>              <none>              00285df0df87        5 days ago          342 MB
```

一般来说，虚悬镜像已经失去了存在的价值，是可以随意删除的，可以用下面的命令删除。

```shell
$ docker image prune
```

### 1.5.8 中间层镜像

为了加速镜像构建、重复利用资源，Docker 会利用 **中间层镜像**。所以在使用一段时间后，可能会看到一些依赖的中间层镜像。默认的 `docker image ls` 列表中只会显示顶层镜像，如果希望显示包括中间层镜像在内的所有镜像的话，需要加 `-a` 参数。

```shell
$ docker images -a
REPOSITORY          TAG                 IMAGE ID            CREATED             SIZE
mytest              latest              c3c2b8f0b99f        2 hours ago         506MB
<none>              <none>              da27d51fa0e1        2 hours ago         506MB
<none>              <none>              ebd10ea765f4        2 hours ago         506MB
<none>              <none>              ce2a4f1e6c59        2 hours ago         506MB
<none>              <none>              188301b593a2        3 hours ago         506MB
<none>              <none>              61f06a2c9285        3 hours ago         506MB
ubuntu              18.04               4c108a37151f        3 days ago          64.2MB
tomcat              latest              5377fd8533c3        7 days ago          506MB

```

这样会看到很多无标签的镜像，与之前的虚悬镜像不同，这些无标签的镜像很多都是中间层镜像，是其它镜像所依赖的镜像。这些无标签镜像不应该删除，否则会导致上层镜像因为依赖丢失而出错。实际上，这些镜像也没必要删除，因为之前说过，相同的层只会存一遍，而这些镜像是别的镜像的依赖，因此并不会因为它们被列出来而多存了一份，无论如何你也会需要它们。只要删除那些依赖它们的镜像后，这些依赖的中间层镜像也会被连带删除。

### 1.5.9 列出部分镜像

| 命令          | 参数                     | 说明                                                         |
| ------------- | ------------------------ | ------------------------------------------------------------ |
| docker images | 仓库名                   | 根据仓库名列出镜像                                           |
|               | 仓库名:标签              | 根据仓库和标签列出某个特定镜像                               |
|               | -f since=仓库名：标签    | 查看某个版本之后仓库中的镜像，例如（tomcat:8.5,即查看8.5以后的版本） |
|               | -f before=仓库名：标签名 | 和since 相反                                                 |

### 1.5.10 删除镜像

如果要删除本地的镜像，可以使用 `docker image rm` 命令，其格式为：

```shell
$ docker image rm [选项] <镜像1> [<镜像2> ...]
# 其中，<镜像> 可以是 镜像短 ID、镜像长 ID、镜像名(<仓库名>:<标签>) 或者 镜像摘要。
# 最精确的是使用 镜像摘要 删除镜像
# 查看摘要
$ docker image ls --digests
# DIGEST 项就是摘要信息
```

```tex
如果观察上面这几个命令的运行输出信息的话，你会注意到删除行为分为两类，一类是 Untagged，另一类是 Deleted。我们之前介绍过，镜像的唯一标识是其 ID 和摘要，而一个镜像可以有多个标签。

因此当我们使用上面命令删除镜像的时候，实际上是在要求删除某个标签的镜像。所以首先需要做的是将满足我们要求的所有镜像标签都取消，这就是我们看到的 Untagged 的信息。因为一个镜像可以对应多个标签，因此当我们删除了所指定的标签后，可能还有别的标签指向了这个镜像，如果是这种情况，那么 Delete 行为就不会发生。所以并非所有的 docker image rm 都会产生删除镜像的行为，有可能仅仅是取消了某个标签而已。

当该镜像所有的标签都被取消了，该镜像很可能会失去了存在的意义，因此会触发删除行为。镜像是多层存储结构，因此在删除的时候也是从上层向基础层方向依次进行判断删除。镜像的多层结构让镜像复用变动非常容易，因此很有可能某个其它镜像正依赖于当前镜像的某一层。这种情况，依旧不会触发删除该层的行为。直到没有任何层依赖当前层时，才会真实的删除当前层。这就是为什么，有时候会奇怪，为什么明明没有别的标签指向这个镜像，但是它还是存在的原因，也是为什么有时候会发现所删除的层数和自己 docker pull 看到的层数不一样的源。

除了镜像依赖以外，还需要注意的是容器对镜像的依赖。如果有用这个镜像启动的容器存在（即使容器没有运行），那么同样不可以删除这个镜像。之前讲过，容器是以镜像为基础，再加一层容器存储层，组成这样的多层存储结构去运行的。因此该镜像如果被这个容器所依赖的，那么删除必然会导致故障。如果这些容器是不需要的，应该先将它们删除，然后再来删除镜像。
```

## 1.6 使用Dockerfile 定制镜像

### 1.6.1 概述

```tex
镜像的定制实际上就是定制每一层所添加的配置、文件。如果我们可以把每一层修改、安装、构建、操作的命令都写入一个脚本，用这个脚本来构建、定制镜像，那么之前提及的无法重复的问题、镜像构建透明性的问题、体积的问题就都会解决。这个脚本就是 Dockerfile。

Dockerfile 是一个文本文件，其内包含了一条条的指令(Instruction)，每一条指令构建一层，因此每一条指令的内容，就是描述该层应当如何构建。
```

### 1.6.2 Dockerfile 指令

- **FROM指定了基础镜像**

  ```tex
  所谓定制镜像，那一定是以一个镜像为基础，在其上进行定制。就像我们之前运行了一个 nginx 镜像的容器，再进行修改一样，基础镜像是必须指定的。而 FROM 就是指定基础镜像，因此一个 Dockerfile 中 FROM 是必备的指令，并且必须是第一条指令。
  
  在 Docker Store 上有非常多的高质量的官方镜像，有可以直接拿来使用的服务类的镜像，如 nginx、redis、mongo、mysql、httpd、php、tomcat 等；也有一些方便开发、构建、运行各种语言应用的镜像，如 node、openjdk、python、ruby、golang 等。可以在其中寻找一个最符合我们最终目标的镜像为基础镜像进行定制。
  
  如果没有找到对应服务的镜像，官方镜像中还提供了一些更为基础的操作系统镜像，如 ubuntu、debian、centos、fedora、alpine 等，这些操作系统的软件库为我们提供了更广阔的扩展空间
  ```

  除了选择现有镜像为基础镜像外，Docker 还存在一个特殊的镜像，名为 scratch。这个镜像是虚拟的概念，并不实际存在，它表示一个空白的镜像。

  ```dockerfile
  FROM scratch
  ...
  ```

  ```tex
  如果你以 scratch 为基础镜像的话，意味着你不以任何镜像为基础，接下来所写的指令将作为镜像第一层开始存在。
  
  不以任何系统为基础，直接将可执行文件复制进镜像的做法并不罕见，比如 swarm、coreos/etcd。对于 Linux 下静态编译的程序来说，并不需要有操作系统提供运行时支持，所需的一切库都已经在可执行文件里了，因此直接 FROM scratch 会让镜像体积更加小巧。使用 Go 语言 开发的应用很多会使用这种方式来制作镜像，这也是为什么有人认为 Go 是特别适合容器微服务架构的语言的原因之一。
  ```

- **RUN 执行命令**

  `RUN` 指令是用来执行命令行命令的。由于命令行的强大能力，`RUN` 指令在定制镜像时是最常用的指令之一。其格式有两种：

  ```tex
  1、shell 格式：RUN <命令>，就像直接在命令行中输入的命令一样。RUN echo '<h1>Hello, Docker!</h1>' > /usr/local/tomcat/webappps/ROOT/index.html 就是这种格式。
  2、exec 格式：RUN ["可执行文件", "参数1", "参数2"]，这更像是函数调用中的格式。
  ```

- **COPY 拷贝**

  - `COPY <源路径>... <目标路径>`

  - `COPY ["<源路径1>",... "<目标路径>"]`

    ```tex
    和 RUN 指令一样，也有两种格式，一种类似于命令行，一种类似于函数调用。
    
    COPY 指令将从构建上下文目录中 <源路径> 的文件/目录复制到新的一层的镜像内的 <目标路径> 位置。比如：
    COPY package.json /usr/src/app/
    
    <源路径> 可以是多个，甚至可以是通配符，其通配符规则要满足 Go 的 filepath.Match 规则，如:
    COPY hom* /mydir/
    COPY hom?.txt /mydir/
    
    <目标路径> 可以是**容器内**的绝对路径，也可以是相对于工作目录的相对路径（工作目录可以用 WORKDIR 指令来指定）。目标路径不需要事先创建，如果目录不存在会在复制文件前先行创建缺失目录。
    
    此外，还需要注意一点，使用 COPY 指令，源文件的各种元数据都会保留。比如读、写、执行权限、文件变更时间等。这个特性对于镜像定制很有用。特别是构建相关文件都在使用 Git 进行管理的时候。
    ```

- **ADD 拷贝**

  ```shell
  ADD 指令和 COPY 的格式和性质基本一致。但是在 COPY 基础上增加了一些功能。
  
  比如 <源路径> 可以是一个 URL，这种情况下，Docker 引擎会试图去下载这个链接的文件放到 <目标路径> 去。下载后的文件权限自动设置为 600，如果这并不是想要的权限，那么还需要增加额外的一层 RUN 进行权限调整，另外，如果下载的是个压缩包，需要解压缩，也一样还需要额外的一层 RUN 指令进行解压缩。所以不如直接使用 RUN 指令，然后使用 wget 或者 curl 工具下载，处理权限、解压缩、然后清理无用文件更合理。因此，这个功能其实并不实用，而且不推荐使用。
  
  如果 <源路径> 为一个 tar 压缩文件的话，压缩格式为 gzip, bzip2 以及 xz 的情况下，ADD 指令将会自动解压缩这个压缩文件到 <目标路径> 去。
  ```

  在某些情况下，这个自动解压缩的功能非常有用，比如官方镜像 ubuntu 中：

  ```dockerfile
  FROM scratch
  ADD ubuntu-xenial-core-cloudimg-amd64-root.tar.gz /
  ...
  ```

  ```tex
  但在某些情况下，如果我们真的是希望复制个压缩文件进去，而不解压缩，这时就不可以使用 ADD 命令了。
  
  在 Docker 官方的 Dockerfile 最佳实践文档 中要求，尽可能的使用 COPY，因为 COPY 的语义很明确，就是复制文件而已，而 ADD 则包含了更复杂的功能，其行为也不一定很清晰。最适合使用 ADD 的场合，就是所提及的需要自动解压缩的场合。
  
  另外需要注意的是，ADD 指令会令镜像构建缓存失效，从而可能会令镜像构建变得比较缓慢。
  
  因此在 COPY 和 ADD 指令中选择的时候，可以遵循这样的原则，所有的文件复制均使用 COPY 指令，仅在需要自动解压缩的场合使用 ADD。
  ```

- **CMD**

  - `CMD` 指令的格式和 `RUN` 相似，也是两种格式：
    - `shell` 格式：`CMD <命令>`
    - `exec` 格式：`CMD ["可执行文件", "参数1", "参数2"...]`
    - 参数列表格式：`CMD ["参数1", "参数2"...]`。在指定了 `ENTRYPOINT` 指令后，用 `CMD` 指定具体的参数。

  ```tex
  在运行时可以指定新的命令来替代镜像设置中的这个默认命令，比如，ubuntu 镜像默认的 CMD 是 /bin/bash，如果我们直接 docker run -it ubuntu 的话，会直接进入 bash。我们也可以在运行时指定运行别的命令，如 docker run -it ubuntu cat /etc/os-release。这就是用 cat /etc/os-release 命令替换了默认的 /bin/bash 命令了，输出了系统版本信息。
  
  在指令格式上，一般推荐使用 exec 格式，这类格式在解析时会被解析为 JSON 数组，因此一定要使用双引号 "，而不要使用单引号。
  ```

  如果使用 `shell` 格式的话，实际的命令会被包装为 `sh -c` 的参数的形式进行执行。比如：

  ```dockerfile
  CMD echo $HOME
  ```

  在实际执行中，会将其变更为：

  ```dockerfile
  CMD [ "sh", "-c", "echo $HOME" ]
  ```

  这就是为什么我们可以使用环境变量的原因，因为这些环境变量会被 shell 进行解析处理。

  提到 `CMD` 就不得不提容器中应用在前台执行和后台执行的问题。这是初学者常出现的一个混淆。

  Docker 不是虚拟机，容器中的应用都应该以前台执行，而不是像虚拟机、物理机里面那样，用 upstart/systemd 去启动后台服务，容器内没有后台服务的概念。

  一些初学者将 `CMD` 写为：

  ```dockerfile
  CMD service nginx start
  ```

  然后发现容器执行后就立即退出了。甚至在容器内去使用 `systemctl` 命令结果却发现根本执行不了。这就是因为没有搞明白前台、后台的概念，没有区分容器和虚拟机的差异，依旧在以传统虚拟机的角度去理解容器。

  

  对于容器而言，其启动程序就是容器应用进程，容器就是为了主进程而存在的，主进程退出，容器就失去了存在的意义，从而退出，其它辅助进程不是它需要关心的东西。

  

  而使用 `service nginx start` 命令，则是希望 upstart 来以后台守护进程形式启动 `nginx` 服务。而刚才说了 `CMD service nginx start` 会被理解为 `CMD [ "sh", "-c", "service nginx start"]`，因此主进程实际上是 `sh`。那么当 `service nginx start` 命令结束后，`sh` 也就结束了，`sh` 作为主进程退出了，自然就会令容器退出。

  正确的做法是直接执行 `nginx` 可执行文件，并且要求以前台形式运行。比如：

  ```dockerfile
  CMD ["nginx", "-g", "daemon off;"]
  ```

- **ENTRYPOINT 指定容器启动程序及参数**

  `ENTRYPOINT` 的格式和 `RUN` 指令格式一样，分为 `exec` 格式和 `shell` 格式。

  

  `ENTRYPOINT` 的目的和 `CMD` 一样，都是在指定容器启动程序及参数。`ENTRYPOINT` 在运行时也可以替代，不过比 `CMD` 要略显繁琐，需要通过 `docker run` 的参数 `--entrypoint` 来指定。

  

  当指定了 `ENTRYPOINT` 后，`CMD` 的含义就发生了改变，不再是直接的运行其命令，而是将 `CMD` 的内容作为参数传给 `ENTRYPOINT` 指令，换句话说实际执行时，将变为：

  ```shell
  <ENTRYPOINT> "<CMD>"
  ```

  那么有了 `CMD` 后，为什么还要有 `ENTRYPOINT` 呢？这种 `<ENTRYPOINT> "<CMD>"` 有什么好处么？让我们来看几个场景：

  - 场景一：让镜像变成命令一样使用

    假设我们需要一个得知自己当前公网 IP 的镜像，那么可以先用 `CMD` 来实现：

    ```dockerfile
    FROM ubuntu:16.04
    RUN apt-get update \
        && apt-get install -y curl \
        && rm -rf /var/lib/apt/lists/*
        #使用 && 将各个所需命令串联起来，简化为了 1 层。在撰写 Dockerfile 的时候，要经常提醒自己，这并不是在写 Shell 脚本，而是在定义每一层该如何构建。清除不必要的文件也是一个好习惯
    CMD [ "curl", "-s", "http://ip.cn" ]
    ```

    假如我们使用 `docker build -t myip .` 来构建镜像的话，如果我们需要查询当前公网 IP，只需要执行：

    ```shell
    $ docker run myip
    您现在的 IP：14.124.117.1 所在地理位置：广东省广州市 电信
    ```

    嗯，这么看起来好像可以直接把镜像当做命令使用了，不过命令总有参数，如果我们希望加参数呢？比如从上面的 `CMD` 中可以看到实质的命令是 `curl`，那么如果我们希望显示 HTTP 头信息，就需要加上 `-i`参数。那么我们可以直接加 `-i` 参数给 `docker run myip` 么？

    ```shell
    $ docker run myip -i
    docker: Error response from daemon: invalid header field value "oci runtime error: container_linux.go:247: starting container process caused \"exec: \\\"-i\\\": executable file not found in $PATH\"\n".
    ```

    我们可以看到可执行文件找不到的报错，`executable file not found`。之前我们说过，跟在镜像名后面的是 `command`，运行时会替换 `CMD` 的默认值。因此这里的 `-i` 替换了原来的 `CMD`，而不是添加在原来的 `curl -s http://ip.cn` 后面。而 `-i` 根本不是命令，所以自然找不到。

    那么如果我们希望加入 `-i` 这参数，我们就必须重新完整的输入这个命令

    ```shell
    $ docker run myid curl -s http://ip.cn -i
    ```

    这显然不是很好的解决方案，而使用 `ENTRYPOINT` 就可以解决这个问题。现在我们重新用 `ENTRYPOINT`来实现这个镜像：

    ```dockerfile
    FROM ubuntu:16.04
    RUN apt-get update \
        && apt-get install -y curl \
        && rm -rf /var/lib/apt/lists/*
    ENTRYPOINT [ "curl", "-s", "http://ip.cn" ]
    ```

    这次我们再来尝试直接使用 `docker run myip -i`：

    ```shell
    $ docker run myip
    您现在的 IP：14.124.117.1 所在地理位置：广东省广州市 电信
    
    $ docker run myip -i
    HTTP/1.1 200 OK
    Server: nginx/1.8.0
    Date: Tue, 22 Nov 2016 05:12:40 GMT
    Content-Type: text/html; charset=UTF-8
    Vary: Accept-Encoding
    X-Powered-By: PHP/5.6.24-1~dotdeb+7.1
    X-Cache: MISS from cache-2
    X-Cache-Lookup: MISS from cache-2:80
    X-Cache: MISS from proxy-2_6
    Transfer-Encoding: chunked
    Via: 1.1 cache-2:80, 1.1 proxy-2_6:8006
    Connection: keep-alive
    您现在的 IP：14.124.117.1 所在地理位置：广东省广州市 电信
    ```

    可以看到，这次成功了。这是因为当存在 `ENTRYPOINT` 后，`CMD` 的内容将会作为参数传给 `ENTRYPOINT`，而这里 `-i` 就是新的 `CMD`，因此会作为参数传给 `curl`，从而达到了我们预期的效果。
  
- **ENV 设置环境变量**
  
    格式有两种：
    
    - `ENV <key> <value>`
    - `ENV <key1>=<value1> <key2>=<value2>...`
    
    这个指令很简单，就是设置环境变量而已，无论是后面的其它指令，如 `RUN`，还是运行时的应用，都可以直接使用这里定义的环境变量。
    
    ```dockerfile
    ENV VERSION=1.0 DEBUG=on \
        NAME="Happy Feet"
    ```
    
    这个例子中演示了如何换行，以及对含有空格的值用双引号括起来的办法，这和 Shell 下的行为是一致的。
    
    定义了环境变量，那么在后续的指令中，就可以使用这个环境变量。比如在官方 `node` 镜像 `Dockerfile` 中，就有类似这样的代码：
    
    ```dockerfile
    ENV NODE_VERSION 7.2.0
    
    RUN curl -SLO "https://nodejs.org/dist/v$NODE_VERSION/node-v$NODE_VERSION-linux-x64.tar.xz" \
      && curl -SLO "https://nodejs.org/dist/v$NODE_VERSION/SHASUMS256.txt.asc" \
      && gpg --batch --decrypt --output SHASUMS256.txt SHASUMS256.txt.asc \
      && grep " node-v$NODE_VERSION-linux-x64.tar.xz\$" SHASUMS256.txt | sha256sum -c - \
      && tar -xJf "node-v$NODE_VERSION-linux-x64.tar.xz" -C /usr/local --strip-components=1 \
      && rm "node-v$NODE_VERSION-linux-x64.tar.xz" SHASUMS256.txt.asc SHASUMS256.txt \
      && ln -s /usr/local/bin/node /usr/local/bin/nodejs
    ```
    
    在这里先定义了环境变量 `NODE_VERSION`，其后的 `RUN` 这层里，多次使用 `$NODE_VERSION` 来进行操作定制。可以看到，将来升级镜像构建版本的时候，只需要更新 `7.2.0` 即可，`Dockerfile` 构建维护变得更轻松了。
    
    
    
    下列指令可以支持环境变量展开： `ADD`、`COPY`、`ENV`、`EXPOSE`、`LABEL`、`USER`、`WORKDIR`、`VOLUME`、`STOPSIGNAL`、`ONBUILD`。
    
    
    
    可以从这个指令列表里感觉到，环境变量可以使用的地方很多，很强大。通过环境变量，我们可以让一份 `Dockerfile` 制作更多的镜像，只需使用不同的环境变量即可.
    
- **VOLUME**
  
    格式为：
  
    - `VOLUME ["<路径1>", "<路径2>"...]`
    - `VOLUME <路径>`
    
    
  之前我们说过，容器运行时应该尽量保持容器存储层不发生写操作，对于数据库类需要保存动态数据的应用，其数据库文件应该保存于卷(volume)中，后面的章节我们会进一步介绍 Docker 卷的概念。为了防止运行时用户忘记将动态文件所保存目录挂载为卷，在 `Dockerfile` 中，我们可以事先指定某些目录挂载为匿名卷，这样在运行时如果用户不指定挂载，其应用也可以正常运行，不会向容器存储层写入大量数据。
  
    ```dockerfile
    VOLUME /data
    ```
  
  这里的 `/data` 目录就会在运行时自动挂载为匿名卷，任何向 `/data` 中写入的信息都不会记录进容器存储层，从而保证了容器存储层的无状态化。当然，运行时可以覆盖这个挂载设置。比如：
  
    ```bash
    docker run -d -v mydata:/data xxxx
    ```
  
  在这行命令中，就使用了 `mydata` 这个命名卷挂载到了 `/data` 这个位置，替代了 `Dockerfile` 中定义的匿名卷的挂载配置。
  
- **EXPOSE 声明运行时容器提供服务端口**
  
    格式为 `EXPOSE <端口1> [<端口2>...]`。
  
    `EXPOSE` 指令是声明运行时容器提供服务端口，这只是一个声明，在运行时并不会因为这个声明应用就会开启这个端口的服务。在 Dockerfile 中写入这样的声明有两个好处，一个是帮助镜像使用者理解这个镜像服务的守护端口，以方便配置映射；另一个用处则是在运行时使用随机端口映射时，也就是 `docker run -P` 时，会自动随机映射 `EXPOSE` 的端口。
  
    此外，在早期 Docker 版本中还有一个特殊的用处。以前所有容器都运行于默认桥接网络中，因此所有容器互相之间都可以直接访问，这样存在一定的安全性问题。于是有了一个 Docker 引擎参数 `--icc=false`，当指定该参数后，容器间将默认无法互访，除非互相间使用了 `--links` 参数的容器才可以互通，并且只有镜像中 `EXPOSE` 所声明的端口才可以被访问。这个 `--icc=false` 的用法，在引入了 `docker network` 后已经基本不用了，通过自定义网络可以很轻松的实现容器间的互联与隔离。
  
    要将 `EXPOSE` 和在运行时使用 `-p <宿主端口>:<容器端口>` 区分开来。`-p`，是映射宿主端口和容器端口，换句话说，就是将容器的对应端口服务公开给外界访问，而 `EXPOSE` 仅仅是声明容器打算使用什么端口而已，并不会自动在宿主进行端口映射。
  
- **WORKDIR**
  
    格式为 `WORKDIR <工作目录路径>`。
  
    使用 `WORKDIR` 指令可以来指定工作目录（或者称为当前目录），以后各层的当前目录就被改为指定的目录，如该目录不存在，`WORKDIR` 会帮你建立目录。
  
    之前提到一些初学者常犯的错误是把 `Dockerfile` 等同于 Shell 脚本来书写，这种错误的理解还可能会导致出现下面这样的错误：
  
    ```docker
    RUN cd /app
    RUN echo "hello" > world.txt
    ```
  
    如果将这个 `Dockerfile` 进行构建镜像运行后，会发现找不到 `/app/world.txt` 文件，或者其内容不是 `hello`。原因其实很简单，在 Shell 中，连续两行是同一个进程执行环境，因此前一个命令修改的内存状态，会直接影响后一个命令；而在 `Dockerfile` 中，这两行 `RUN` 命令的执行环境根本不同，是两个完全不同的容器。这就是对 `Dockerfile` 构建分层存储的概念不了解所导致的错误。
  
    之前说过每一个 `RUN` 都是启动一个容器、执行命令、然后提交存储层文件变更。第一层 `RUN cd /app`的执行仅仅是当前进程的工作目录变更，一个内存上的变化而已，其结果不会造成任何文件变更。而到第二层的时候，启动的是一个全新的容器，跟第一层的容器更完全没关系，自然不可能继承前一层构建过程中的内存变化。
  
    因此如果需要改变以后各层的工作目录的位置，那么应该使用 `WORKDIR` 指令。

### 1.6.3 构建镜像，例：构建一个简单的 tomcat 镜像

在一个空白目录中，创建一个名为Dockerfile 的文件

```shell
$ mkdir /usr/local/docker/tomcat
$ cd /usr/local/docker/tomcat
$ vi Dockerfile
```

其内容为

```dockerfile
FROM tomcat
WORKDIR /usr/local/tomcat/webapps/ROOT
run rm -fr * \
	&& echo 'hello docker' >> index.html
WORKDIR /usr/local/tomcat
```

在Dockerfile 所在目录，执行命令，构建镜像 `docker build [选项] <上下文路径/URL/->`

```shell
$ docker build -t mytomcat .
```

镜像构建上下文解释

```tex
如果注意，会看到 docker build 命令最后有一个 .。. 表示当前目录，而 Dockerfile 就在当前目录，因此不少初学者以为这个路径是在指定 Dockerfile 所在路径，这么理解其实是不准确的。如果对应上面的命令格式，你可能会发现，这是在指定上下文路径。那么什么是上下文呢？

首先我们要理解 docker build 的工作原理。Docker 在运行时分为 Docker 引擎（也就是服务端守护进程）和客户端工具。Docker 的引擎提供了一组 REST API，被称为 Docker Remote API，而如 docker 命令这样的客户端工具，则是通过这组 API 与 Docker 引擎交互，从而完成各种功能。因此，虽然表面上我们好像是在本机执行各种 docker 功能，但实际上，一切都是使用的远程调用形式在服务端（Docker 引擎）完成。也因为这种 C/S 设计，让我们操作远程服务器的 Docker 引擎变得轻而易举。

当我们进行镜像构建的时候，并非所有定制都会通过 RUN 指令完成，经常会需要将一些本地文件复制进镜像，比如通过 COPY 指令、ADD 指令等。而 docker build 命令构建镜像，其实并非在本地构建，而是在服务端，也就是 Docker 引擎中构建的。那么在这种客户端/服务端的架构中，如何才能让服务端获得本地文件呢？

这就引入了上下文的概念。当构建的时候，用户会指定构建镜像上下文的路径，docker build 命令得知这个路径后，会将路径下的所有内容打包，然后上传给 Docker 引擎。这样 Docker 引擎收到这个上下文包后，展开就会获得构建镜像所需的一切文件。
```

如果在 `Dockerfile` 中这么写：

```dockerfile
COPY ./package.json /app/
```

```tex
这并不是要复制执行 docker build 命令所在的目录下的 package.json，也不是复制 Dockerfile 所在目录下的 package.json，而是复制 上下文（context） 目录下的 package.json。

因此，COPY 这类指令中的源文件的路径都是相对路径。这也是初学者经常会问的为什么 COPY ../package.json /app 或者 COPY /opt/xxxx /app 无法工作的原因，因为这些路径已经超出了上下文的范围，Docker 引擎无法获得这些位置的文件。如果真的需要那些文件，应该将它们复制到上下文目录中去。

理解构建上下文对于镜像构建是很重要的，避免犯一些不应该的错误。比如有些初学者在发现 COPY /opt/xxxx /app 不工作后，于是干脆将 Dockerfile 放到了硬盘根目录去构建，结果发现 docker build 执行后，在发送一个几十 GB 的东西，极为缓慢而且很容易构建失败。那是因为这种做法是在让 docker build 打包整个硬盘，这显然是使用错误。

一般来说，应该会将 Dockerfile 置于一个空目录下，或者项目根目录下。如果该目录下没有所需文件，那么应该把所需文件复制一份过来。如果目录下有些东西确实不希望构建时传给 Docker 引擎，那么可以用 .gitignore 一样的语法写一个 .dockerignore，该文件是用于剔除不需要作为上下文传递给 Docker 引擎的。

那么为什么会有人误以为 . 是指定 Dockerfile 所在目录呢？这是因为在默认情况下，如果不额外指定 Dockerfile 的话，会将上下文目录下的名为 Dockerfile 的文件作为 Dockerfile。

这只是默认行为，实际上 Dockerfile 的文件名并不要求必须为 Dockerfile，而且并不要求必须位于上下文目录中，比如可以用 -f ../Dockerfile.php 参数指定某个文件作为 Dockerfile。

当然，一般大家习惯性的会使用默认的文件名 Dockerfile，以及会将其置于镜像构建上下文目录中。

```



------

[^更多笔记内容请参考docker2.md]: 

