#Docker 笔记（上接docker.md）

## 1.0 Docker 容器

### 1.0.1 概述

容器是 Docker 又一核心概念。

简单的说，容器是独立运行的一个或一组应用，以及它们的运行态环境。对应的，虚拟机可以理解为模拟运行的一整套操作系统（提供了运行态环境和其他系统环境）和跑在上面的应用。

###1.0.2 启动容器

新建并启动

```shell
docker run <容器名:tag> 
```

以交互的形式启动一个终端

```shell
docker run -it <container:tag> bash
```

退出容器时自动移除

```shell
docker run -it --rm <container:tag> bash
```

启动已经停止的容器

```shell
# 查看已经停止的容器
docker ps -a
# 启动
docker <container> start
```

守护态运行(让 Docker 在后台运行而不是直接把执行命令的结果输出在当前宿主机下)

```shell
docker run --name <自定义名称> -p <主机端口:容器端口> -it --rm <container> -d
# 查看容器运行时输出的信息
docker container logs [container ID or name]
```

###1.0.3 进入容器

```shell
docker exec -it <container ID or name> bash 
```

## 1.1 数据卷

### 1.1.1 概述

`数据卷` 是一个可供一个或多个容器使用的特殊目录，它绕过 UFS，可以提供很多有用的特性：

- `数据卷` 可以在容器之间共享和重用
- 对 `数据卷` 的修改会立马生效
- 对 `数据卷` 的更新，不会影响镜像
- `数据卷` 默认会一直存在，即使容器被删除

> 注意：`数据卷` 的使用，类似于 Linux 下对目录或文件进行 mount，镜像中的被指定为挂载点的目录中的文件会隐藏掉，能显示看的是挂载的 `数据卷`。

### 1.1.2 基本操作

创建一个数据卷

```shell
docker volume create my-vol
```

查看所有的数据卷

```shell
docker volume ls
```

删除数据卷

```bash
$ docker volume rm my-vol
```

`数据卷` 是被设计用来持久化数据的，它的生命周期独立于容器，Docker 不会在容器被删除后自动删除 `数据卷`，并且也不存在垃圾回收这样的机制来处理没有任何容器引用的 `数据卷`。如果需要在删除容器的同时移除数据卷。可以在删除容器的时候使用 `docker rm -v` 这个命令。

无主的数据卷可能会占据很多空间，要清理请使用以下命令

```shell
$ docker volume prune
```

