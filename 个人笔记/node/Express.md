# Express

## 1.0 概述

- 原生的http在某些方面的表现不足以满足我们的开发需求，所以我们需要使用框架来提高我们的开发效率，框架的作用就是提高我们的开发效率，让我们的代码更加高度统一
- 官方网站 http://www.expressjs.com.cn/

## 1.1 起步

- 安装
  - npm install express --save

## 1.2 修改完代码自动重启服务器

- 使用第三方工具`nodemon`,它是一个基于node开发的一个第三方命令行工具
- 安装
  - npm install --global nodemon
- 使用
  - 老方式（node app.js） 安装后 nodemon app.js
- 通过nodemon 启动的服务，它会监视你的文件变化，当文件发生改变时，它会帮你自动重启服务

## 1.3 基础使用模板

```javascript
var http = require('express')
var app = express()
//开放静态资源，给外界访问
app.use('请求前缀',express.static('资源所在项目中的路径'))；
//例：
    app.use('/public/',express.static('./public'))
	可以通过前缀/public/访问路径为./public下的静态资源
//get请求
app.get('请求路径',function(req,resp){
    //处理
})
//post请求
app.post('请求路径',function(req,resp){
    //处理
});
app.listen(3000,function(){
    console.log('app is running...')
});
```

## 1.4 在Express 中配置使用`art-template`

官方文档地址：https://aui.github.io/art-template/

- 安装

```shell
npm install --save art-template
npm install --save express-art-template		
```

- 配置

```javascript
var express = require('express');
var app = express();
//第一个参数表示当访问以xxx结尾的文件时，使用art-template模板
//express-art-template是专门用来将art-template兼容到express中的
//它依赖了art-template
app.engine('文件后缀', require('express-art-template'));
app.set('view options', {
    debug: process.env.NODE_ENV !== 'production'
});
```

- 使用

```javascript
app.get('/', function (req, res) {
	//render方法默认是不可用的，当配置了模板引擎后才可以使用
    //第一个参数不能直接写路经
    //默认会去项目中的views目录中查找模板
    //修改默认模板目录：app.set('views','默认路径')
    res.render('index.art', {
        user: {
            name: 'aui',
            tags: ['art', 'template', 'nodejs']
        }
    });
});
```

## 1.5 在Express中的请求

- 在Express中一个请求路径可以根据请求方式的不同，写多个处理函数,例如：app.get('/post'),app.post('/post')

### 1.5.1 get方式提交,获取请求参数

```javascript
req.query	
```

###1.5.2 post方式提交,获取请求参数

在Express中没有内置的API处理post请求提交的参数，这里需要引入一个第三方包，`body-parser`

使用方法

- 安装

  ```shell
  npm install body-parser
  ```

- 配置

  ```javascript
  var express = require('express')
  //引包
  var bodyParser = require('body-parser')
  
  var app = express()
  
  // parse application/x-www-form-urlencoded
  app.use(bodyParser.urlencoded({ extended: false }))
  
  // parse application/json
  app.use(bodyParser.json())
  
  app.use(function (req, res) {
    res.setHeader('Content-Type', 'text/plain')
    res.write('you posted:\n')
    //使用了该包后，在内置对象req中就添加了一个body属性，通过req.body就可以获取到post提交的参数
    res.end(JSON.stringify(req.body, null, 2))
  })
  ```

## 1.6 模块提取

目的：让每个模块功能独立，做具体的事情，职责清晰，更好维护，相当于web开发中的分层

- 提取模块方式一

  ```javascript
  //入口模块
  app.js
  module.export=app;
  //其他模块，可以是任意模块，这里以路由模块为例
  router.js
  var router = require('./app')
  //此方式需要先加载路由模块才生效，不建议使用
  ```

- 提取模块方式二

  ```javascript
  //入口模块
  app.js
  var router = require('./router')
  router(app)
  //路由模块
  router.js
  module.export=function(app){
      ...
  }
  ```

- 提取模块方式三

  ```javascript
  //通过Express封装的API进行提取
  //入口模块
  app.js
  var router = require('./router')
  //将路由模块挂载到app 服务器上
  app.use(router)
  //路由模块
  router.js
  var express = require('express')
  //创建一个路由容器
  var router = express.Router()
  //处理请求
  router.get('/',function(){})
  router.post('/',function(){})
  
  ```

  

