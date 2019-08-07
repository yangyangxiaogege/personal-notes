# Orcale

## 1.1 概述

- Oracle 是一个大型的数据库，由Oracle（甲骨文）公司研发
- 和SQL Server 、MySql一样，均是基于客户端/服务器模式的关系型数据库
- 可跨平台
- 1978年诞生，距今约40年历史 

### 1.1.1 Oracle 数据库的特点

- 支持多用户，大事务量的事务处理
- 在保持数据安全性和完整性方面性能优越
- 支持分布式数据处理
- 具有可移植性



## 1.2 Orcale中的数据类型

- 字符数据类型
  - char 长度不可变
  - varchar2 可变长度，可节省内存空间，常用
  - nchar 以unicode 形式存储的双字节数据，不可变长度
  - nvarchar2 以unicode形式存储的双字节数据，长度可变
- 数值类型
  - number(p,s)
    - p（precision）:精度，表示数字的有效位数，从左边第一个不为0的数算起
    - s（scope）:范围，表示小数点右边的位数
  - 示例
    - number(4,2)-------------123.89--------->报错，有效位数为五位，超过了四位
    - number(4,2)-------------12.789--------->12.79
    - number(3,4)-------------1----------------->1.0000 超过三位有效数字了
    - number(3,4)-------------0.0001--------->0.0001
    - number(3,4)-------------0.1--------------->0.1000 超过三位有效数字了
- 日期类型
  - date
  - timestamp 精确到毫秒
- LOB类型（Large Object）
  - CLOB 大文本，大量字符数据，例如一篇文章
  - BLOB 大的二进制对象，图形，视频等
  - BFILE binary lob,存储一个二进制文件的引用，文件存储在操作系统中
  - NCLOB Unicode格式的CLOB

## 1.3 Oracle 中常用的sql语句

- distinct 使用此关键字可以去除查询结果中的重复数据
- select sysdate from dual 查询当前系统时间
- create table xxx as select * from xxxx where 1=2 表复制，不加条件，将会把表结构和数据一起复制，条件结果为false只复制表结构

## 1.4 Oracle 中的伪列

伪列就像Oracle中的一个表列，但实际上它并未存储在标中。伪劣可以从表中查询，但是不能手动插入，更新或删除他们的值

- ROWID 
  - 数据库的每一行都有一个行地址，ROWID伪列返回改行的地址
  - 可以使用ROWID值来定位表中的某一行，可以唯一的标识数据库中的某一行
  - 用途
    - 能以最快的方式访问表中的一行
    - 能显示表的行是如何存储的
    - 可以作为表中行的唯一标识
- ROWNUM
  - 返回查询结果的每一行的行号第一行为1，后边的依照顺序依次递增
  - 只能查询小于某个行号或等于行号等于1的数据，不能用来查询大于某一行号的数据
  - 用途
    - 可以结合子查询做分页查询
    - select * from (
             select emp_sal.*,rownum rn from (select * from emp order by sal) emp_sal
      ) where rn between 1 and 3;

## 1.5 事务控制

- commit 提交事务，即把事务中对数据库的修改进行永久保存
- rollback 回滚事务，即取消对数据库所做的修改
- savepoint 在事务中创建储存点
- rollback to <savepoint> 将事务回滚到储存点

> 注意：Oracle 默认不会自动提交事务，需要手动提交，提交后才能做到持久性保存

## 1.6 SQL 语言简介

- SQL 语言是高级结构化查询语言
- 经过多年发展，SQL语言已经称为关系型数据库的标准语言
- SQL语言支持如下类别的命令
  - DDL(Data Definition Language) 数据定义语言
    - CREATE (创建)
    - ALTER(修改)
    - TRUNCATE(截断)
    - DROP(删除)
  - DML(Data Manipulation Language ) 数据操作语言
    - INSERT(插入)
    - SELECT(选择)
    - DELETE(删除)
    - UPDATE(更新)
  - TCL(Transaction Control Language)事务控制语言
    - COMMIT(提交)
    - SAVEPOINT(保存点)
    - ROLLBACK(回滚)
  - DCL(Data Control Language)数据控制语言
    - GRANT(授予)
    - REVOKE(回收)

### 1.6.1 SQL 函数

- Oracle SQL 提供了用于执行特定操作的专用函数。Oracle 将函数大致划分为单行函数、聚合函数和分析函数

- 单行函数可大致分为字符串函数、日期函数、数字函数、转换函数及其他函数

- 转换函数

  - TO_CHAR(d | n ,fmt) d是日期，n是数字，fmt是指定日期或者数字的格式

    ```sql
    SELECT TO_CHAR(SYSDATE,'YYYY"年"fmMM"月"fmDD"日" HH24:MI:SS') FROM dual
    输出结果：
    2019年2月24日 16:27:16
    此案例使用了填充模式 "fm" 格式掩码来避免空格填充和数字补零填充，不加的话，输出结果将会变为
    2019年02月24日 16：27：16
    ```

  - TO_DATE(CHAR,fmt) 将char 或者varchar 类型的转换为日期类型

  - TO_NUMBER() 将包含数字的字符串转换为数字类型

- 其它函数

  - NVL(exp1,exp2) 如果exp1的值为null，则返回exp2的值，否则返回exp1
  - NVL(exp1,exp2,exp3) 如果exp1的值为null,则返回exp3的值，否则返回exp2的值
  - DECOEE(value,if1,then1.if2,then2,...,else) 如果value的值为if1则返回then1，如果为if2,则返回then2...,否则返回else的值

- 分析函数

  - Oracle 从 8.1.6 版本开始提供分析函数。分析函数是对一组查询结果进行运算，然后获得结果
  - 语法 函数名([参数]) over (`[分区子句][排序子句]`)
  - 分区子句 PARTITION BY 表示将查询结果分为不同的组，功能类似于GROUP BY,是分析函数工作的基础。默认将所有结果分为一个组
  - 排序子句 ORDER BY 表示将每一个分区进行排序
  - 例如：
    - RANK 、DENSE_RANK、ROW_NUMBER 用于解决累计排名问题

## 1.7 表空间

### 1.7.1 概述

Oracle 数据库包含逻辑结构和物理结构。数据库的物理结构是指构成数据库的一组操作系统文件。数据库的逻辑结构是指描述数据组织方式的一组逻辑概念及他们之间的关系。表空间是数据库逻辑结构的一个中中重要组件。表空间可以存放各种应用对象，如表，索引。而每个表空间由一个或多个数据文件组成

### 1.7.2 表空间的分类

- 永久性表空间	 一般保存表、视图、过程和索引等数据。SYSTEM、SYSAUX、USERS、EXAMPLE、表空间是默认安装的
	 临时性表空间	只用于保存系统中短期活动的数据，如排序数据等
- 撤销表空间 用来帮助回退未提交的事务数据，已提交了的数据在这里是不可以恢复的。
	 一般不需要建临时和撤销表空间，除非把他们转移到其他磁盘中以提高性能	

### 1.7.3 表空间的目的

- 对不同用户分配不同的表空间，对不同的模式对象分配不同的表空间，方便对用户数据的操作，对模式对象的管理
- 可以将不同数据文件创建到不同的磁盘中，有利于管理磁盘空间，有利于提高I/O 性能，有利于备份和恢复数据

### 1.7.4 创建，删除表空间

- 语法

  ```sql
  创建
  CREATE TABLESPACE tablespacename
  DATAFILE 'filename'
  SIZE integer [k | M]
  AUTOEXTEND [OFF | ON]
  删除
  DROP TABLESPACE tablespacename
  ```

- tablespacename 表空间名称

- DATEFILE 指定组成表空间的一个或多个数据文件，当有多个时，用逗号隔开，值为路径+文件名

- SIZE 指定文件的额大小 k代表千字节大小，m代表兆字节大小

- AUTOEXTEND 启用或禁用数据文件的自动扩展，即当默认空间已经使用完后，是否自动扩建文件

## 1.8 用户

### 1.8.1 概述

当创建一个新数据库时，Oracle将创建一些默认数据库用户，如Sys,System和Scott等

- Sys 

  - 是Oracle中的一个超级用户，数据库中所有的数据字典和视图都存储在SYS模式中。
  - 数据字典存储了用来管理数据库对象的所有信息，是Oracle数据库中非常重要的系统信息
  - Sys用户主要用来维护系统信息和管理实例
  - Sys用户只能以SYSOPER 或 SYSDBA 角色登陆系统

- System

  - 是Oracle中默认的系统管理员，它拥有DBA权限。
  - 该用户拥有Oracle管理工具使用的内部表和视图
  - 通常通过System用户管理Oracle数据库的与用户、权限和存储等。
  - 不建议在System模式中创建用户表。System用户不能以SYSOPER或SYSDBA角色登陆系统，只能以默认的方式登陆

- ### 1.8.2 创建、修改、删除用户

```sql
-- 创建
CREATE USER username
IDENTIFIED BY password
[DEFAULT TABLESPACE tablespace] 默认表空间，不设置，Oracle会将user设为默认表空间
[TEMPORARY TABLESPACE tablespace] 临时表空间，不设置，Oracle会将TEMP设为临时表空间

-- 修改密码
ALTER USER user
IDENTIFIED BY pwd

-- 删除
DROP USER user CASCADE
```

## 1.9 数据库权限管理

### 1.9.1 概述

权限是用户对一项功能的执行权力，在Oracle中，根据系统管理方式的不同，可以将权限分为系统权限和对象权限两种

### 1.9.2 系统权限

- 系统权限是指被授权用户是否可以连接到数据库上及在数据库中可以进行那些系统操作
- 常见的系统权限
  - CREATE SESSION 连接到数据库
  - CREATE TABLE 创建表
  - CREATE VIEW 创建视图
  - CREATE SEQUENCE 创建序列

### 1.9.3 对象权限

- 对象权限是指用户对数据库中具体对象所拥有的权限
- 对象权限是针对某个特定的模式对象执行操作的权力，只能针对模式对象来设置和管理对象权限

### 1.9.4 授予权限的方式

- 管理员直接向用户授予权限
- 管理员将权限授予角色，然后将角色授予一个或多个用户

### 1.9.5 Oracle 常见的预定义角色

- CONNECT 需要连接上数据库的用户，特别是那些不需要创建表的用户，经常授予该角色 
- RESOURCE 更为可靠和正式的数据库用户可以授予该角色，可以创建表，触发器等
- DBA 数据库管理员角色，拥有管理数据库的最高权限，不建议给用户此权限

### 1.9.6 权限操作的语法

```sql
-- 授予权限
GRANT 权限 | 角色 To user
-- 撤销权限
REVOKE 权限 | 角色 FROM user
```

### 1.9.7 数据库安全设计原则

- 数据库用户权限按照最小分配原则
- 数据库用户分为管理，应用，维护，备份四类用户
- 不允许使用Sys和System用户建立数据库应用对象
- 禁止GRANT DBA TO user

## 2.0 序列

### 2.0.1 概述

序列是用来生成序号的数据库对象，生成的序号具有如下特点

-  唯一
- 连续
- 整数
- 序列的值只能访问，不能人工操作
- 序列通常用于生成自动增长的主键值

### 2.0.2 序列的属性

- NextVal
  - 序列创建后第一次使用nextVal时，将返回该序列的初始值
  - 以后再访问，则使用 increment by 子句来增加序列值，并将其返回
- CurrVal
  - 返回序列的当前值，及最后一次访问Next Val时返回的值

### 2.0.3 创建和删除主键

```sql
-- 创建
CREATE SEQUENCE squence_name
[START WITH integer] 指定生成的第一个序列号
[INCREMENT BY integer] 指定序列号之间的间隔值
[MAXVALUE integer | NOMAXVALUE] MAXVALUE指定最大值 NOMAXVALUE 默认选项，将升序序列的最大值设为10^27
[MINVALUE integer | NOMINVALUE] 
[CYCLE | NOCYCLE] 是否循环
[CACHE | NOCACHE] 是否缓存

-- 删除
DROP SEQUENCE sequence_name
```

## 2.1 同义词

### 2.1.1 概述

- 同义词就是给数据库对象起一个别名，通过该别名也可以进行查询
- 同义词不会占用存储空间
- 分为公有和私有

### 2.1.2 用途

- 简化SQL语句
- 隐藏对象的名称和所有者
- 分布式数据库的远程对象提供了位置透明性
- 提供对对象的公共访问

### 2.1.3 语法

```sql
-- 私有
CREATE SYNONYM synonym_name FOR object_name

-- 公有
CREATE PUBLIC SYNONYM synonym_name FOR object_name
```

### 2.1.4 公有同义词和私有同义词的区别

- 私有同义词只能在当前模式下访问，且不能与当前模式的对象重名
- 公有同义词可被所有的数据库用户访问

## 2.2 索引

### 2.2.1 概述

索引是与表关联的可选结构，是一种快速访问数据的途径，可提高数据库性能

### 2.2.2 索引的分类

- 物理分类
  - 分区或非分区索引
  - B 树索引（标准索引）
  - 正常或反向键索引
  - 位图索引
- 逻辑分类
  - 单列或组合索引
  - 唯一或非唯一索引
  - 基于函数索引

### 2.2.3 语法

CREATE [UNIQUE] INDEX index_name ON table_name(column_list)

[TABLESPACE tablespace_name]

- index_name 索引名称
- table_name 要创建索引的表
- column_list 创建索引的列，多个用逗号隔开
- tablespace_name

### 2.2.4 创建索引的原则

- 频繁搜索的列可以作为索引
- 经常排序分组的列可以作为索引
- 经常用作连接的列（主外键）
- 将索引放在一个单独的表空间中，不要放在有回退段，临时段和表的表空间中
- 对于大型索引而言，考虑使用NOLOGGING子句创建大型索引
- 根据业务数据放生的频率，定期重新生成或组织索引，并进行碎片整理
- 仅包含几个不同值的列不可以创建为b数索引，可以根据需要创建位图索引
- 不要在仅包含几行的表中创建索引

## 2.3 分区表

### 2.3.1 概述

- Oracle 允许用户把一个表中的所有行分为几个部分，并将这些部分存储在不同的位置。
- 被分区的表称为分区表，分成的每个部分称为一个分区

### 2.3.2 创建分区表的条件

- 数据量大于2GB
- 已有的数据和新增的数据有明显的界限划分

### 2.3.3 分区表的优点

- 改善表的查询性能
- 表更容易管理
- 便于备份和恢复
- 提高数据安全性

### 2.3.4 常见分区的分类

- 范围分区 是应用分区范围比较广的分区方式，它以列值范围作为条件进行分区

  ```sql
  create table sales(
      sales_id number(3,0),
  	sales_date date not null
  )
  
  partition by range (sales_date)
  (
  	partition p1 values less than (to_date('2013-04-1','yyyy-mm-dd')),
      partition p2 values less than (to_date('2014-04-1','yyyy-mm-dd')),
      partition p3 values less than (maxvalue) //在按时间分区时，如果最终时间无法预计，可以使用maxvalue
  )
  
  //查询2013年的销售记录
  select * from sales partition(p1)
  ```

- 间隔分区 是Orcle 11g 版本新引入的分区方法，可以实现分区自动化

  ```sql
  partition by range(sales_date)
  interval (numtoyminterval(3,'MONTH'))
  (partition p1 values less than(to_date('2013-04-1','yyyy-mm-dd')))
  //此段代码的作用，每隔3个月，将自动创建一个分区
  ```

## 2.4 数据的导入和导出

### 2.4.1 概述

通过数据的导入导出，可以实现数据备份，防止数据意外破坏或丢失

### 2.4.2 导入导出的方式

- 用Oracle自带工具exp导出和imp导入
- 用PL/SQL Developer完成
  - Oracle Export 本质就是exp命令方式完成的效果，导出文件以dmp作为后缀名，数据为二进制数据，可以跨平台
  - SQL Insert 导出sql文件，可以使用编辑器查看，通用性比较好，但是效率不高，不适合进行大量数据导出，表中不能有大字段
  - PL/SQL Developer 导出为pde 格式的，仅供此工具使用