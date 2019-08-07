# Hibernate

## 1.1 Hibernate 框架简介

### 1.1.1 概述

- Hibernate 是数据持久化工具，也是一个开放源代码的ORM(Object Relational Mapping) 解决方案。
- Hibernate 内部封装了通过JDBC 访问数据库的操作，向上层应用提供面向对象的数据访问API
- Gavin King 是Hibernate 的创始人
- 基于ORM，Hibernate 在对象模型和关系数据库的表之间建立了一座桥梁
- 通过Hibernate，程序员就不需要再使用SQL语句来操作数据库中的表，使用API直接操作JavaBean 对象就可以实现数据的存储，查询等一系列操作

### 1.1.2 Hibernate 的优缺点及适用场合

- 优点
  - 功能强大，是Java 应用于关系数据库之间的桥梁，代码量减少，提高开发速度，降低维护成本
  - Hibernate 支持许多面向对象的特性，如组合、继承、多态等，使得开发人员不必再面向业务领域的对象模型和面向数据库的关系数据模型之间来回的切换
  - 可移植性好
  - Hibernate框架开源免费
- 缺点
  - 不适合以数据为中心大量使用存储过程的应用
  - 大规模的批量插入、修改和删除不适合使用

## 1.2 初步使用

### 1.2.1 在src根目录下添加配置文件`hibernate.cfg.xml`

```xml
<?xml version="1.0" encoding="UTF-8" ?>
<!DOCTYPE hibernate-configuration PUBLIC
	"-//Hibernate/Hibernate Configuration DTD 3.0//EN"
	"http://www.hibernate.org/dtd/hibernate-configuration-3.0.dtd">
<hibernate-configuration>
	<session-factory>
        <!-- 方言：即告诉Hibernate使用的是什么数据库 -->
		<property name="hibernate.dialect">org.hibernate.dialect.Oracle10gDialect</property>
    	<!-- jdbc驱动类 -->
        <property name="hibernate.connection.driver_class">oracle.jdbc.driver.OracleDriver</property>
		<property name="hibernate.connection.username">scott</property>
		<property name="hibernate.connection.password">orcl</property>
		<property name="hibernate.connection.url">jdbc:oracle:thin:@localhost:1521:orcl</property>
		
		<!-- 在控制台输出生成的sql语句 -->
		<property name="show_sql">true</property>
		<!-- 格式化sql -->
        <property name="format_sql">true</property>
		<!--
			openSession() 是新建一个线程，可能会造成安全隐患， 
			此配置的作用是将会话与当前线程绑定，以免造成多线程数据访问时出现数据错误
			配置此项后，可以通过getCurrentSession()获取会话，如果当前线程没有绑定，则会新建
			如果已经绑定，则会返回已被绑定的会话
			配置此项后，在使用session做任何操作时都需要先开启事务
		 -->
		<property name="current_session_context_class">thread</property>
		
		<!-- 加载ORM 映射关系 -->
		<mapping resource="com/hibernate/entity/Student.hbm.xml"/>
	</session-factory>
</hibernate-configuration>
```

### 1.2.2 创建持久化类和映射文件

持久化类是指其实例状态需要被Hibernate 持久化到数据库中的类，映射文件即该实体类与数据库表的ORM映射，其作用是将类和数据库的表关联

映射文件Student.hbm.xml 示例

```xml
<?xml version="1.0" encoding="UTF-8" ?>
<!DOCTYPE hibernate-mapping PUBLIC 
    "-//Hibernate/Hibernate Mapping DTD 3.0//EN"
    "http://www.hibernate.org/dtd/hibernate-mapping-3.0.dtd">
<hibernate-mapping>
    name：关联类 table:对应数据库中的表，如果表名和名一致，可以省略不写
	<class name="com.hibernate.entity.Student" table="stuinfo">
        id:配置主键，如果字段名和列名一致，可以省略column
        type:数据类型，可省略
		<id name="stuNo" column="stuNo" type="java.lang.Integer"/>
        property:配置普通字段
		<property name="stuName" column="stuName" />
		<property name="stuAge" column="stuAge" />
		<property name="stuId" column="stuID" />
		<property name="stuSeat" column="stuSeat" />		
	</class>
</hibernate-mapping>
```

### 1.2.3 主键生成策略

- generator:id元素的子元素，用于指定主键的生成策略

  - class 属性用来指定具体的策略
  - param元素用来指定参数

- 常见的主键生成策略如下

  - assigned 主键主要由程序负责（即手动操作），无需Hibernate参与，属于默认项

  - increment 对类型为long short 或int 的主键，以自动增长的方式生成主键的值。主键按数值顺序递增，增量为1

  - identity 对如SQL Server、DB2、MySql 等支持标识列的数据库，可以使用者个策略，但前提是在数据库中需要将该id 设为标志列

  - sequence 支持序列的数据库可以使用，需要通过子元素传入参数

    - ```xml
      <id name="">
      	<generator class="sequence">
          	<param name="sequence_name">sequence_name</param>
          </generator>
      </id>
      ```

  - native 由Hibernate 根据底层数据自行判断采用何种主键生成策略

  - uuid 使用uuid 生成主键

### 1.2.4 Hibernate 工具类`HibernateUtil.java`

```java
package com.hibernate.util;

import org.hibernate.Session;
import org.hibernate.SessionFactory;
import org.hibernate.cfg.Configuration;

public class HibernateUtil {
	private static SessionFactory sessionFactory;
	
	static {
		// 加载配置文件
		Configuration cf = new Configuration().configure();
		// 获取会话工厂
		sessionFactory = cf.buildSessionFactory();
	}
	// 返回和当前线程绑定的session
	public static Session getSession() {
		return sessionFactory.getCurrentSession();
	}
	//关闭会话工厂
	public static void closeSF() {
		sessionFactory.close();
	}
	//关闭会话
	public static void closeSession(Session session) {
		if (session.isOpen()) { // 判断当前会话是否为打开状态
			session.close();	
		}
	}
}
```

### 1.2.5 进行简单的使用示例

```java
public class Test {
    public static void main() {
        Session session = HibernateUtil.getSession();
        // 开启事务
        session.beginTransaction();
        try {
            Student stu = session.get(Student.class,1);
            session.commit();
        }catch(Exception ex){
            session.rollBack();
        }finally{
            HibernateUtil.closeSF();
        }
    }
}
```

### 1.2.6 主键查询

- Object get(Class clazz,Serializable id);
  - 方法一调用，立即执行
  - 如果没有查询结果，将返回null
- Objec load(Class clazz,Serializable id)
  - 当使用其返回结果时，才会执行查询
  - 如果没有查询结果，将返回null

## 1.3 Hibernate 中 Java对象的三种状态

- 瞬时状态
  - 瞬时状态又称临时状态。
  - 如果Java 对象没有和数据库中的数据相关联，没有通过session 进行操作，此时Session 对于瞬时状态的Java对象是一无所知的，当对象不再被引用时，它的所有数据也就丢失了，对象将会被Java虚拟机按照垃圾回收机制处理
- 持久状态
  - 当对象与Session 关联，被Session 管理时，他就处于持久状态，此时，对象的内部数据改变后，Hibernate会选择合适的实际（如事务提交时）将变更数据同步到数据库中
- 游离状态
  - 游离状态又称脱管状态，即脱离与其关联的Session 的管理后，对象就处于游离状态
  - 例如通过session 的update 方法后，对象便会处于游离状态

## 1.4 脏检查及刷新缓存机制

- 脏检查

  ```java
  ts = session.beginTransaction();
  Student stu = session.load(Student.class,1);// 此时stu处于持久状态
  stu.setName('hello'); // 此时stu对象被修改，称为脏对象
  ts.commit();
  ```

  - 当stu 被加载到session缓存中时，session会为stu对象复制一份快照
  - 当stu属性发生改变时，stu对象称为脏对象
  - 在事务提交时，session会比较stu对象和它的快照，看看是否有发生变化，称为脏检查
  - 如果有变化，则会将数据同步到数据库中

- 刷新缓存机制

  - 当session中的缓存中的对象的属性发生变化时，session并不会立即执行脏检查，而是在特定的时间点执行
  - 1，调用session的flush()方法
  - 2，调用事务的commit()方法

## 1.5 HQL（Hibernate Query Language）

### 1.5.1 概述

Hibernate 支持三种查询方式：HQL 查询，Griteria(标准) 查询及原生SQL(Native SQL) 查询。HQL 是一种面向对象的查询语言，其中没有表和字段的概念，只有对象和属性。HQL 中除了对象和属性，对大小写不敏感

### 1.5.2 使用示例

```java
// 定义HQL 语句
String hql = "from Student";
// 构建查询对象
Query query = session.createQuery(hql);
// 执行查询
List<Student> list = query.list();
```

### 1.5.3 在HQL 语句中绑定参数

- 方式1，使用`?`占位符

  ```java
  String hql = "from Student where stuName =?0 ";
  Query query = session.createQuery(hql);
  query.setString(0,"zs"); //1
  -------------
  query.setParameter(0,"zs"); //2
  ```

- 方式2,通过命名参数绑定

  ```java
  String hql = "from Student where stuName =:name ";
  Query query = session.createQuery(hql);
  query.setString("name","zs");
  -----------------
  query.setParameter("name","zs");
  ```

- 方式3,setProperties

  ```java
  String hql = "from Student where stuName =:stuName ";
  Query query = session.createQuery(hql);
  Student stu = new Student();
  stu.setStuName("zs");
  query.setProperties(stu);
  ```

### 1.5.4 分页

```java
//Hibernate 提供了简便的方法实现分页，案例如下

String hql = "from Student order by stuNo";
Query query = session.createQuery(hql);

query.setFirstResult((pageNo-1)*pageSize);// 设置起始下标
query.setMaxResults(pageSize); // 查询几条记录
query.list();// 得到结果
```

### 1.5.5 投影查询

需求场景：不需要获取对象的全部属性，只需要查询出一个或几个属性值

- 查询结果只包含一个结果列，返回结果为`List<Object>`

  ```java
  String hql = "select stuName from Student";
  List<String> list = session.createQuery(hql).list();
  ```

- 查询结果包含多列，返回结果为`List<Object[]>`

  ```java
  String hql = "select stuName,stuAge from Student";
  List<Object []> list = session.createQuery(hql).list();
  ```

- 通过构造函数，将查询结果封装为对象

  ```java
  // 需要有对应的Student（stuName,stuAge） 构造
  String hql = "select new Studetn(stuName,stuAge) from Student";
  List<Student> list = session.createQuery(hql).list();
  ```


## 1.6 关联映射

### 1.6.1 cascade 属性

级联操作

| cascade 属性值 | 描述                                             |
| :------------- | :----------------------------------------------- |
| none           | 当Session 操纵对象时，忽略其他关联对象。是默认值 |
| save-update    | 做保存或更新时，同时级联操作所有关联的对象       |
| delete         | 做删除操作时，会级联删除关联对象                 |
| all            | 包含save-update 和 delete 所有行为               |

### 1.6.2 inverse

意为反转，此属性制定了关联关系的方向，值为true 和 false(默认值)

true:将控制权交由一方处理，避免出现重复的操作

false:关系由双方维护，会出现重复的操作

### 1.6.3 延迟加载

| 级别   | Lazy属性取值                                                 |
| ------ | ------------------------------------------------------------ |
| 类级别 | <class> 元素中 lazy的值为true(延迟加载) 和 false(立即加载)，true为默认值 |
| 一对多 | <set> 元素中 lazy属性 的值可为true(延迟)、extra(增强延迟)、false(立即)，true为默认值 |
| 多对一 | <many-to-one> 元素中lazy 属性的可选值为proxy(延迟) 、no-proxy(无代理延迟)、false(立即)，默认值为proxy |

## 1.7 HQL 连接查询

###1.7.1 HQL 支持的常用连接类型 

| 连接类型     | HQL 语法                                 |
| ------------ | ---------------------------------------- |
| 内连接       | inner join 或 join                       |
| 迫切内连接   | inner join fetch 或 join fetch           |
| 左外连接     | left outer join 或 left join             |
| 迫切左外连接 | left outer join fetch 或 left join fetch |
| 右外连接     | right outer join 或 right join           |

### 1.7.2 子查询关键字

| 关键字 | 说明                       |
| ------ | -------------------------- |
| all    | 子查询语句返回的所有记录   |
| any    | 子查询语句反回任意一条记录 |
| some   | 与any意思相近              |
| in     |                            |
| exists | 子查询语句至少返回一条记录 |

##1.8  注解

###1.8.1 持久化类中常用注解 

| 注解              | 含义和作用                                                   |
| ----------------- | ------------------------------------------------------------ |
| @Entity           | 将一个类声明为一个持久化类                                   |
| @Table            | 为持久化类映射指定表（table）、目录（catalog）、和schema 的名称 |
| @Id               | 声明持久化类对应的主键                                       |
| @GeneratedValue   | 定义主键生成策略                                             |
| @UniqueConstraint | 定义表的唯一约束                                             |
| @Lob              | 表示属性将被持久化为Blob 或者Clob类型                        |
| @Column           | 将属性映射为数据库字段                                       |
| @Transient        | 指定可以忽略的属性，不用持久化到数据库                       |

> 如果类名和数据库中对应的表名一致，可以省略注解配置，属性也是同理
>
> 主键生成详解：https://www.cnblogs.com/ph123/p/5692194.html

### 1.8.2 使用注解配置关联关系

| 注解        | 含义和作用                     |
| ----------- | ------------------------------ |
| @OneToOne   | 一对一，相当于类与类之间的引用 |
| @OneToMany  | 一对多                         |
| @ManyToOne  | 多对一                         |
| @ManyToMany | 多对多                         |

