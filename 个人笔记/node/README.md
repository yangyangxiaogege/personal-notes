# Node.js 笔记

## 1.0 node.js 是什么
- Node.js® is a JavaScript runtime built on Chrome's V8 JavaScript engine.
   - node.js®是一个基于Chrome的V8 JavaScript引擎构建的JavaScript运行时
   - node.js不是一门语言，也不是一个库，它是一个JavaScript运行时环境
   - 在node.js中，没有BOM和DOM
   - node.js 中提供了一系列服务端操作相关的API
     - 例如：
     - 文件
     - http
     - 路径
     - 。。。
- Node.js uses an event-driven,non-blocking I/O model that makes it lightweight and efficient
   - node.js使用事件驱动的、无阻塞的I/O模型，使其轻量级和高效
-  Node.js's package ecosystem ,npm, is the largest ecosystem of open source libraries in the world
   - node.js的包生态系统npm是世界上最大的开源库生态系统
   - 绝大多数JavaScript相关的包都放在了npm中

## 1.1 node.js能做什么

- web 服务器后台
- 命令行工具
  - npm(node)
  - git(c语言)
  - hexo(node)

## 1.2 node中的JavaScript

- Ecmascript
- 核心模块
  - fs(用于操作文件的读写)
  - os(用于获取当前机器信息)
  - path(用于操作路径)
  - ...
- 第三方模块
- 用户自定义模块

## 1.3 node.js基础使用模板

```javascript
//加载http核心模块
var http = require('http')
//创建服务器
var server = http.createServer()
//监听request事件，并做处理
server.on('request',function(req,resp){
    //设置响应内容格式
    resp.setHeader('content-type','text/plain;charset=urt-8')
    //响应内容
    resp.end('hello world')
})
//绑定端口号
server.listen(3000,function(){
    console.log('server is running...');
})
```

> content-type 对照表 http://tool.oschina.net/commons

## 1.4 模块系统

- 在node中没有全局作用域 的概念
- 在node中只能通过require方法来加载执行多个JavaScript脚本
- 模块系统带来的好处
  - 可以加载执行多个文件
  - 避免了命名冲突问题

### 1.4.0 CommonJs模块规范

- 模块作用域
- 使用require方法加载模块
- 使用exports或者module.exports接口对象导出模块中的成员

### 1.4.1 require 和 exports

- `require`加载

  - 执行被加载模块中的代码
  - 接收被加载模块中的exports对象

- `exports`导出

  - 用于导出模块中的成员，提供给外界使用

- `exports`与`module.exports`的区别

  ```javascript
  //exports等价于module.exports
  //原理：
  //在node中每一个模块内部都有一个module对象，同时也有一个exports对象，可以认为exports对象默认为一个空对象
  var module = {
      exports:{
          
      }
  }
  //在代码的最后，返回的是module.exports
  //区别：
  //使用exports导出的是一个对象，使用module.exports是改变了对象本身的引用
  //例如：
  exports.a='a'
  exports.b='b'
  //此时得到的还是对象本身，使用时需要通过对象来调用，不会覆盖
  module.exports='a'
  //此时得到的就是a,如果有多个，后者会覆盖前者
  ```

### 1.4.2 require加载规则

- 优先从缓存加载

  ```javascript
  //模块a中
  var m = require('b')
  //模块b中
  console.log('我被加载了');
  exports.say='我是b模块'
  //结果
  执行第一次时，b模块中打印语句执行，a得到导出对象
  执行第二次时，a直接从缓存中得到导出对象，不执行打印语句
  ```

- 加载路径形式的模块（用于加载用户自定义模块）

  - `./`标识这是一个路径形式加载的模块
  - `../`
  - `/xxx` 以/为首位，代表的是当前模块所属磁盘的根路径，不常用

- 加载核心模块

  - 核心模块的本质也是文件
  - 核心模块文件已经被编译到了二进制文件，我们只用按照特定名字加载就OK

- 加载第三方模块

  - 都可以用npm命令进行安装

  - 安装后，可以通过require('包名')加载使用

  - 不可能有任何一个第三方包和核心模块同名

  - 加载过程

    ```markdown
    ### 例如加载art-template
    > require('art-template')
    + 先找到当前文件中的 `node_modules` 目录
    + node_modules/art-template
    + node_modules/art-template/package.json
    + 找到package.json 中的 `main` 属性，该属性记录了入口模块
    >> 如果该目录下没有package.json 或者 package.json中没有main, node会自动找该目录下的index.js(默认备选项)
    >>>以上条件都不满足，node 会跳到被加载模块的上一级中查找，如果还没有则继续往上查询，如果到了该文件所属的磁盘根目录中还没有找到，则会报出 can not find moudel xxx 错误
    
    ```

### 1.4.3 包说明文件
  ```javascript
  package.json
  安装包时，使用此命令可以将该包记录在包中
  例： npm install art-template --save;// --save 放在art-template 前后都可以
  作用：对当前项目进行描述，记录该项目的依赖包
  ```

  - 创建package.json

  ```powershell
  
  C:\Users\ASUS\Desktop\node\day03>md code
  
  C:\Users\ASUS\Desktop\node\day03>cd code
  
  C:\Users\ASUS\Desktop\node\day03\code>npm init // 快速生成 npm init -y
  This utility will walk you through creating a package.json file.
  It only covers the most common items, and tries to guess sensible defaults.
  
  See `npm help json` for definitive documentation on these fields
  and exactly what they do.
  
  Use `npm install <pkg>` afterwards to install a package and
  save it as a dependency in the package.json file.
  
  Press ^C at any time to quit.
  package name: (code) code //项目名称
  version: (1.0.0) 1.0  //版本号
  Invalid version: "1.0"
  version: (1.0.0) 1.0.1
  description: 这是一个测试项目 //项目描述
  entry point: (index.js) main.js //入口模块
  test command: 
  git repository:
  keywords:
  author: yxy
  license: (ISC)
  About to write to C:\Users\ASUS\Desktop\node\day03\code\package.json:
  
  {
    "name": "code",
    "version": "1.0.1",
    "description": "这是一个测试项目",
    "main": "main.js",
    "scripts": {
      "test": "echo \"Error: no test specified\" && exit 1"
    },
    "author": "yxy",
    "license": "ISC"
  }
  
  
  Is this OK? (yes) yes
  
  C:\Users\ASUS\Desktop\node\day03\code>
  
  
  ```

  > 如果项目中的node_modules 目录被删，package.json 文件还存在的话，使用npm install 命令可以直接将package.json中记录的依赖包安装回来

### 1.4.4 部分核心模块介绍

```javascript
var url = require('url')
url.parse('请求路径',true)//这里将会把请求路径解析为对象,第二个参数代表将查询字符串也解析为对象

var fs = require('fs')
//第二个参数是可选项，可以将读取到的data直接以utf8的编码格式转换为字符串
fs.readFile('xxx','utf8',function(err,data){
    
});
```

## 1.5 实现浏览器重定向

```javascript
//301 永久重定向，302 临时重定向
resp.statusCode = 302
resp.setHeader('location','/')
```

## 1.6 解决npm被墙的问题

- npm 存储包文件的服务器在国外，有时候速度很慢

- http://npm.taobao.org/ 这里是上述问题的解决方案，淘宝团队将国外的npm在国内做了一个备份，同步频率为十分钟一次

  

## 1.7  获取异步执行结果

```javascript
//通过回调函数获取
function test(callback){
 	//此方法是一个异步方法
    fs.readFile(path,function(err,data){
        if(err){
            //通过回调函数记录错误信息
            return callback(err);
        }
        callback(null,data);
    })
}
//调用
test(function(err,data){
    if(err){
        //操作失败
    }else{
        //操作成功，数据为data
    }
})
```

