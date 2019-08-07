# Regsitory

## 1.1 概述

### 1.1.1 什么是Registry

官方的 Docker Hub 是一个用于管理公共镜像的地方，我们可以在上面找到我们想要的镜像，也可以把我们自己的镜像推送上去。但是，有时候我们的服务器无法访问互联网，或者你不希望将自己的镜像放到公网当中，那么你就需要 Docker Registry，它可以用来存储和管理自己的镜像。

### 1.1.2 安装

通过docker-compose 进行安装，yml文件存储于docker-compose文件中

## 1.2 测试

启动成功后需要测试服务端是否能够正常提供服务，有两种方式：

- 浏览器访问
  - http://ip:5000/v2/

- 终端访问

  ```shell
  curl http://ip:5000/v2/
  ```

## 1.3 配置Docker Registry 客户端

### 1.3.1 配置

Ubuntu Server 18.04 LTS 版本，属于 `systemd` 系统，需要在 `/etc/docker/daemon.json` 中增加如下内容（如果文件不存在请新建该文件）

```json
{
  "registry-mirrors": [
    "https://registry.docker-cn.com"
  ],
  "insecure-registries": [
    "ip:5000"
  ]
}
```

> 注意：该文件必须符合 json 规范，否则 Docker 将不能启动

之后重新启动服务

```shell
$ sudo systemctl daemon-reload
$ sudo systemctl restart docker
```

### 1.3.2 检查客户端配置是否生效

使用 `docker info` 命令手动检查，如果从配置中看到如下内容，说明配置成功（192.168.78.128 为实验机 IP）

```shell
Insecure Registries:
 192.168.78.128:5000
 127.0.0.0/8
```

## 1.4 测试

### 1.4.1 测试镜像上传

```shell
## 拉取一个镜像
docker pull tomcat

## 查看全部镜像
docker images

## 标记本地镜像并指向目标仓库（ip:port/image_name:tag，该格式为标记版本号）
docker tag tomcat 192.168.78.128:5000/tomcat

## 提交镜像到仓库
docker push 192.168.78.128:5000/tomcat
```

### 1.4.2 查看全部镜像

以Tomcat 为例，查看以提交的列表

```shell
curl -XGET http://192.168.78.128:5000/v2/tomcat/tags/list
```

### 1.4.3 拉取镜像

先删除镜像（为了演示效果）

```shell
docker rmi nginx
docker rmi 192.168.78.128:5000/tomcat
```

拉取

```shell
docker pull 192.168.78.128:5000/tomcat
```

## 1.5 部署Docker Registry WebUI

私服安装成功后就可以使用 docker 命令行工具对 registry 做各种操作了。然而不太方便的地方是不能直观的查看 registry 中的资源情况。如果可以使用 UI 工具管理镜像就更好了。这里介绍两个 Docker Registry WebUI 工具

- [docker-registry-frontend](https://github.com/kwk/docker-registry-frontend)
- [docker-registry-web](https://hub.docker.com/r/hyper/docker-registry-web/)

两个工具的功能和界面都差不多，我们以 `docker-registry-fontend` 为例讲解

通过docker-compose安装后，可以通过浏览器进行访问：http://ip/port

![](C:\Users\ASUS\Desktop\My application\My File\personal-notes\个人笔记\微服务\Registry\images\docker-registry-webui.png)



 



