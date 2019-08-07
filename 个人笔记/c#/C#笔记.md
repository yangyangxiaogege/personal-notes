# c#笔记

## 1.1 .Net和C#语言

- .Net平台是微软的一个开发平台，是开发领域的另一个大的领域
- c#语言
  - 运行在.Net平台上的一门语言
  - 面向对象的编程语言
  - `#----->sharp(锋利)`
- .Net Framework 是开发和运行的环境，其作用类似java中的Jdk
- 开发工具
  - visual studio

##1.2 认识`c#`程序

- namespace 命名空间

- using 使用，相当于java中的import

- Main() 程序入口

  - 返回参数可以是void和int
  - 传入参数可选
  - 首字母大写

- 整个应用会编译成exe文件，存储在项目中的bin目录中，运行此文件需要.Net Framework 环境的支持

- 命名不能使用$符号

- 定义常量

  - 使用关键字const修饰
  - 常量的值不允许被修改
  - 必须在定义的时候初始化赋值

- 向控制台中输出

  - Console.Write() 输出内容，不换行
  - Console.WriteLine() 输出内容，换行

- 向控制台输入

  - Console.ReadLine();
  - 该方法是一个阻塞方法，返回的是用户输入的内容

- 使用占位符

  ```c#
  string weekDay = "星期一";
  Console.WriteLine("hello,今天是{0}",weekDay);//{0} 0代表下标
  //输出结果：hello,今天是星期一
  //相当于
  Console.WriteLine("hello,今天是"+weekDay);
  ```

## 1.3 字段封装

```c#
//在c#程序中，通过属性的形式来封装的字段
public class Demo{
	private int age;//字段
    public int Age{//此处是属性，不是方法
        get{
            return age
        }
        set{
            age=value;//set相当于一个返回类型为void的函数，value是一个隐含的输入参数，用于动态接收设置			参数
        }
    }
    Demo d = new Demo();
    d.Age=18;
    Console.WriteLine(d.Age);
}

```

## 1.4 值传递和引用传递

- 值传递是传递了一个副本，引用传递是传递了一个自身的引用
- 值传递不会对本身造成影响
- 值传递类型
  - int
  - double
  - float
  - string
  - ...
- 引用传递类型
  - 对象
  - 数组
  - ...
- 将值传递变为引用传递，需要加 `ref` 关键字

## 1.5 使用ADO.NET 连接数据库

- ADO .NET 是.net framework 的一个重要组成部分
- 是 .net 平台用于访问数据库的一种技术
- 连接不同的数据库需要不同的数据提供程序

### 1.5.1 连接sql server 数据库

- 连接字符串中的键代表的意义
  - Data Source 服务器名称 值：`.`，本机 `127.0.0.1` ，`本机ip`,`电脑账户名称`
  - Initial Catalog 数据库名称
  - User ID 用户名
  - pwd 密码

```c#
//引入命名空间
using System.Data.SqlClient;

//连接字符串
//windows身份验证
string connStr = "Data Source=.;Initial Catalog=myschool1;Integrated Security=True";
//sql server 身份验证方式
string connStr = "Data Source=.;Initial Catalog=myschool1;User ID=sa;pwd=123";

//创建连接对象 参数为连接字符串
SqlConnection conn = new SqlConnection(conStr);

//准备sql语句
string sql = "select count(*) from student";
// 打开连接
conn.Open();

//创建SqlCommand对象，该对象的作用是发送sql语句
SqlCommand comm = new SqlCommand(sql,conn);// 第一个参数，要执行的sql语句，第二个参数，连接对象

//执行comm命令
int count = (int)comm.ExecuteScalar();//该方法返回的查询结果中的第一行第一列的值

//关闭连接
conn.Close();

```

### 1.5.2 SqlCommand对象的方法介绍

- ExecuteScalar() //查询单行单列，返回的是查询结果中的第一行第一列
- ExecuteNonQuery() //更新，执行增删改操作，返回受影响的行数
- ExecuteReader() //查询多行多列，返回DataReader对象

### 1.5.3 SqlDataReader 对象

- 用于接收ExecuteReader()方法的返回结果

- 使用

  ```c#
  SqlDataReader dr = comm.ExecuteReader();
  while(dr.read()){//read()是一个只读只进方法，返回值为布尔类型，每执行一次就读取一行数据
      Console.WriteLine(dr[index]);//读取列值，方式1
      Console.WriteLine(dr["colName"]);// 读取列值， 方式2
  }
  //使用完毕后需要进行Close（）
  dr.Close();
  ```


### 1.5.4 数据集`DataSet`

- DataSet 对象，是一个临时数据仓库，存储在内存中

- 内部结构

  ```c#
  DataSet 数据仓库
  DataTable 数据表
  DataColumn 数据列
  DataRow 数据行
  	
  ```

### 1.5.5 数据适配器`SqlDataAdapter`

- 用于填充数据集，是数据的搬运工
- 通过适配器的Fill()方法填充数据集，参数为要进行填充的数据集
- 该对象内部封装了打开和关闭数据库的连接，无需我们手动打开和关闭

### 1.5.6 使用适配器进行数据集的填充实例

```c#
//建立连接对象
string connStr="Data Source=.;Initial Catalog=myschool1;Integrated Security=true";
SqlConnection conn = new SqlConnection(connStr);

//数据集
DataSet ds = new DataSet();

//适配器对象
string sql = "select * from student";
SqlDataAdapter adapter = new SqlDataAdapter(sql,conn);//参数为sql语句，连接对象

//填充数据集
adapter.Fill(ds);
```

### 1.5.7 DataView对象

- 数据视图，用于对数据进行筛选

```c#
DataTable dt=new DataTable();
DataView dv=new DataView(dt);
dv.RowFilter="sex='男'";//将会将sex除了男以外的全部过滤掉
dv.Sort="stuId desc";//进行排序

xx.DataSource=dv;//将筛选后的数据作为数据源使用，

```

- 此对象并不常用

### 1.5.8 更新数据集

- SqlCommandBuilder 对象能够自动生成 增删改命令
- SqlCommandBuilder scb=new SqlCommandBuilder(已创建的DataAdapter对象);
- 此对象只能进行单表操作，且要修改的数据中必须存在主键（意思就是查询并填充dataset时，查询与剧中包含主键列，且只能是单表查询）

```c#
DataSet ds=new DataSet();
SqlDataAdapter adapter = new SqlDataAdapter(sql,conn);
adapter.Fill(ds,"student");
SqlcommandBuilder scb=new SqlCommandBuilder(adapter);
adapter.update(ds,"student");
```

