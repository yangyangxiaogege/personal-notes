# MongDB

## 1.1 概述

- mongdb 是一个非关系型数据库，也就是没有表的概念
- 里边的数据是以集合的形式存储的
- admin config local 为默认的数据库

## 1.2 使用命令行操作MongoDB

```shell
#连接数据库
mongo
#查看数据库
show dbs
#使用数据库（如果该数据库不存在，则会创建一个数据库，在该库未插入数据前，dbs命令查看不到）
use school
#查看当前使用的数据库
db 
#创建集合
db.createCollection("student") 相当于数据表
#查看集合
show collection
#向集合中插入数据
db.student.insert({"name":"张三","age":18})
```

> 更多操作请参考：http://www.runoob.com/mongodb/mongodb-tutorial.html

##1.3 在Node中操作MongoDB

- 使用官方提供的`mongodb` 包
- 使用第三方包`mongose` ,它是在官方提供的包的基础之上进行了封装
- 建议使用第二种
- mongose网址：https://mongoosejs.com/

## 1.4 基础操作

```javascript
//引入包
var mongoose=require('mongoose')

var Schema=mongoose.Schema

//连接数据库
mongoose.connect('mongodb://localhost:27017/test',{useNewUrlParser: true})

//设计文档架构，相当于设计表
var userSchema=new Schema({
	userName:{
		type:String,
		required:true
	}
	userAge:{
		type:Number,
		required:true
	}
})

//将文档结构发布为模型
//第一个参数，传入一个首字母大写名词单数字符串，表示数据库名称，mongoose 会自动将字符串改为，小写，复数形式的字符串
//即 User 最终会变为users
var User=mongoose.model('User',userSchema)

//有了模型构造函数后，就可以对users中的数据进行操作了（增删该查）
```

## 1.5Mongoose 中 CRUD 参考

> https://www.cnblogs.com/zhongweiv/p/mongoose.html

## 1.6 在Node 中操作MySql数据库

> https://www.npmjs.com/package/mysql

