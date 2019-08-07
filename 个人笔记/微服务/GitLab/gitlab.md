# GitLab

## 1.1 概述

### 1.1.1 什么是Git

![](C:\Users\ASUS\Desktop\My application\My File\personal-notes\个人笔记\微服务\GitLab\images\git.png)

- Git 是一个开源的分布式版本控制系统，用于敏捷高效地处理任何或小或大的项目。
- Git 是 Linus Torvalds 为了帮助管理 Linux 内核开发而开发的一个开放源码的版本控制软件。
- Git 与常用的版本控制工具 CVS, Subversion 等不同，它采用了分布式版本库的方式，不必服务器端软件支持。

### 1.1.2 什么是GitLab

GitLab 是利用 Ruby on Rails 一个开源的版本管理系统，实现一个自托管的 Git 项目仓库，可通过 Web 界面进行访问公开的或者私人项目。它拥有与 Github 类似的功能，能够浏览源代码，管理缺陷和注释。可以管理团队对仓库的访问，它非常易于浏览提交过的版本并提供一个文件历史库。团队成员可以利用内置的简单聊天程序 (Wall) 进行交流。它还提供一个代码片段收集功能可以轻松实现代码复用，便于日后有需要的时候进行查找。

### 1.1.3 Git 的工作流程

- 克隆 Git 资源作为工作目录。
- 在克隆的资源上添加或修改文件。
- 如果其他人修改了，你可以更新资源。
- 在提交前查看修改。
- 提交修改。
- 在修改完成后，如果发现错误，可以撤回提交并再次修改并提交。

![](C:\Users\ASUS\Desktop\My application\My File\personal-notes\个人笔记\微服务\GitLab\images\git-process.png)

## 1.2 安装Git

### 1.2.1 下载

下载地址：https://git-scm.com/downloads

### 1.2.2 安装

详情见网上教程

##1.3 TortoiseGit 简化Git操作

###1.3.1 概述

TortoiseGit, 中文名海龟 Git. 海龟 Git 只支持 Windows 系统, 有一个前辈海龟 SVN, TortoiseSVN 和 TortoiseGit 都是非常优秀的开源的版本库客户端. 分为 32 位版与 64 位版.并且支持各种语言,包括简体中文

### 1.3.2 下载

下载地址：https://tortoisegit.org/download/

同时可以下载相同版本的语言包

### 1.3.3 安装

详情见网上教程

### 1.3.4 配置

空白处点击鼠标右键, 选择 --> TortoiseGit --> Settings, 然后就可以看到配置界面

选中General,在右边的 Language中选择中文. 不勾选自动升级的复选框,可能还需要指定 Git.exe 文件的路径

再次点击鼠标右键,可以看到弹出菜单中已经变成中文. 原来的 Settings 变成 设置; Clone 变为 克隆\

## 1.4 基于Docker 安装GitLab

### 1.4.1 安装

通过docker-compse进行安装，具体的yml文件存储在（docker-compose笔记中）

### 1.4.2 安装完成后的工作

- 访问地址：http://ip:8080
- 端口 8080 是因为我在配置中设置的外部访问地址为 8080，默认是 80
- 初始化安装完成后效果如下：

![](C:\Users\ASUS\Desktop\My application\My File\personal-notes\个人笔记\微服务\GitLab\images\gitlab-init.png)

- 设置管理员初始密码，这里的密码最好是 字母 + 数字 组合，并且 大于等于 8 位
- 配置完成后登录，管理员账号是 root

![](C:\Users\ASUS\Desktop\My application\My File\personal-notes\个人笔记\微服务\GitLab\images\gitlab-login.png)

**注意：** 如果服务器配置较低，启动运行可能需要较长时间，请耐心等待

## 1.5 使用GitLab

使用基本同GitHub一样

