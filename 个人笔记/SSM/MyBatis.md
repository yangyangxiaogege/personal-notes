# MyBatis

## 1.1 概述

- MyBatis 本是[apache](https://baike.baidu.com/item/apache/6265)的一个开源项目[iBatis](https://baike.baidu.com/item/iBatis), 2010年这个项目由apache software foundation 迁移到了google code，并且改名为MyBatis 。2013年11月迁移到Github。
- iBATIS一词来源于“internet”和“abatis”的组合，是一个基于Java的`持久层框架`。iBATIS提供的持久层框架包括SQL Maps和Data Access Objects（DAOs）
- 该框架对JDBC进行了封装，简化了JDBC开发

> 官方文档地址：http://www.mybatis.org/mybatis-3/

## 1.2 使用MyBatis步骤

### 1.2.1 导入jar包

- mybatis-3.4.6.jar

###1.2.2 配置核心配置文件

```xml
<?xml version="1.0" encoding="UTF-8" ?>
<!DOCTYPE configuration
  PUBLIC "-//mybatis.org//DTD Config 3.0//EN"
  "http://mybatis.org/dtd/mybatis-3-config.dtd">
<configuration> -----配置
    
    配置别名
    <typeAliases>
    	<package name="com.entity"/>---在映射文件中使用该包下的类可以省略完整限定名，直接写类名
    </typeAliases>
    
  <environments default="development"> -----环境
    <!-- 此处的环境可有多个，具体使用哪个，是看上述环境父标签调用的哪个--> 
    <environment id="development">
      <transactionManager type="JDBC"/>----事务管理类型
      <dataSource type="POOLED"> ----数据源
        <property name="driver" value="${driver}"/> -----数据库驱动
        <property name="url" value="${url}"/>  ----连接字符串
        <property name="username" value="${username}"/> ----用户名
        <property name="password" value="${password}"/> ----密码
      </dataSource>
    </environment>
  </environments>
    <!-- mapper映射文件 -->
  <mappers>
    <mapper resource="org/mybatis/example/BlogMapper.xml"/>
  </mappers>
</configuration>
```

###1.2.3 编写Mapper映射文件

```xml
<?xml version="1.0" encoding="UTF-8" ?>
<!DOCTYPE mapper
  PUBLIC "-//mybatis.org//DTD Mapper 3.0//EN"
  "http://mybatis.org/dtd/mybatis-3-mapper.dtd">
<mapper namespace="com.yxy.dao.StudentDao">----命名空间
    映射结果集
    <resultMap>
    
    </resultMap>
    resultType为返回类型
  <select id="selectAll" resultType="student">---执行某个查询
    select * from student 
  </select>
</mapper>
```

###1.2.4 开始调用---基础模板/升级模板

```java
//配置文件路径
string resource="mybatis-config.xml";
//加载配置文件
InputStream inputStream=Resouces.getResourceAsStream(resource);
//构建SqlSession工厂对象
SqlSessionFactory sqlSessionFactory = new SqlSessionFactoryBuilder().build(inputStream);
SqlSession sqlSession = sqlSessionFactory.openSession();//获取session

//基础模板
List<Student> list=sqlSession.selectList("selectAll");//纯手工调用
>selectAll 是mapper映射文件中的某个功能块的id

//升级模板，使用动态代理的方式
StudentDAO sDao=sqlSession.getMapper(StudentDAO.class);
sDAO.selectAll();//此处会根据方法名称，自动映射到mapper文件中进行匹配

sqlSession.close();//关闭session
```

### 1.2.5 BaseDAO

```java
package mybatis.dao;

import java.io.IOException;
import java.io.InputStream;

import org.apache.ibatis.io.Resources;
import org.apache.ibatis.session.SqlSession;
import org.apache.ibatis.session.SqlSessionFactory;
import org.apache.ibatis.session.SqlSessionFactoryBuilder;

public class BaseDAO {
	//声明SqlSessionFactory对象
	private static SqlSessionFactory sFactory;
	
	//加载配置文件，并生成sFactory对象
	static {
		String resource="mybatis-config.xml";
		try {
			//将配置文件以字节流的形式加载到内存中
			InputStream is=Resources.getResourceAsStream(resource);
			
			//构建sFactory对象
			sFactory=new SqlSessionFactoryBuilder().build(is);
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
	}
	//利用内部类，定义一个单例SqlSession对象
	public static class MySqlSessin{
		  private static SqlSession sqlSession=sFactory.openSession();
	}
	
	//获取session
	public SqlSession getSession() {
		
		return MySqlSessin.sqlSession;
	}
}

```



## 1.3MyBatis传参

- 单个参数
  - 传入的是基本数据类型：参数名自定义（因为只有一个参数，不会发生混淆）
  - 传入的是对象：直接写对象的属性名即可
- 传入多个参数
  - 可以用下标表示，例如#{0}，#{1}
  - 可以使用参数关键字表示，例如#{param1},{param2}
  - 用一个map来装载，调用时使用其key值，#{key}
  - 使用@param注解，给参数起别名，在映射文件中直接使用别名来调用

## 1.4 结果映射集

```xml
//用于把实体类和数据库字段相关联起来
<resultMap id="结果集标识" type="该结果的类型，例如student" extends="继承哪个结果集，可选">
	<id column="" property=""/>--用于映射主键
	<result column="数据库列名" property="类中对应的属性名"/>--普通字段
    
    //此处是为了映射实体类中的引用对象中的字段
    <association property="引用对象" javaType="引用对象类型" resultMap="引用的resultMap,要加上命名空间，写完整的标识">
	</association>
	//配置集合属性的映射
    <collection property="属性名" ofType="里边装的对象" resultMap="..."></collection>
</resultMap>
```

实例：

```xml
namespace com.dao.StudentDAO
<resultMap id="baseResultMap" type="student">
	<id column="stuId" property="stuId"/>
    <result column="stuName" property="stuName"/>
</resultMap>

<resultMap id="fullResultMap" type="student" extends="baseResultMap">
    <association property="grade" javaType="Grade" resultMap="com.dao.GradeDAO.baseResultMap"></association>
</resultMap>

namespace com.dap.GradeDAO
<resultMap id="baseResultMap" type="grade">
	<id column="gradeId" property="gradeId"/>
    <result column="gradeName" property="gradeName"/>
</resultMap>
```

## 1.5 动态sql语句

- if

  ```xml
  <if test="stu.name != null and stu.name != ''"
  ```

- where

  ```xml
  select * from student
  <where>
  	<if test="">
      	and stuName=xxx
      </if>
  </where>
  
  作用：
  1.动态添加where,如果where标签内的条件不满足，导致内容为空，则不添加，否则添加
  2.where 内的语句如果以and 或者 or开头，会自动帮你去除
  
  ```

- trim

  ```xml
  select * from student
  <trim prefix="where" prefixOverrides="and / or" suffix="" suffixOverrides>
  
  </trim>
  
  prefix:前缀 追加
  prefixOverrides： 前缀替换，内部拼接的sql语句，以该前缀开头，则去除
  suffix: 后缀 追加
  suffixOverrides:后缀替换
  ```

- set

  ```xml
  update student  
  <set>
  	<if test="">
          stuName=#{name}，
      </if>
  </set>
  作用：
  帮你添加一个set
  把多余的逗号去掉
  ```

- choose,when,otherwise

  ```xml
  <choose>
  	<when test=""></when>
      <when test=""></when>
      <otherwise test=""></otherwise>
  </choose>
  相当于else if结构
  从上往下进行判断，最后只会执行一个
  ```

- foreach

  ```xml
  select * from student where stuId in
  //item 遍历项，oppen 以什么开头 ，close 以什么结尾 ，separator 以什么分割
  <foreach collection="array" item="ele" oppen="(" close=")" separator=",">
  	#{ele}
  </foreach>
  
  collection ：
  1.当传入一个数组或一个集合时
  数组：collection="array"
  集合：collection="list"
  2.如果起了别名，直接写别名即可
  
  ```


## 1.6 注意

```java
使用mybatis时，做了增删该查，需要手动提交事务才起效
```

