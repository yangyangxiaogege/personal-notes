# PL/SQL

##1.1 什么是PL/SQL

### 1.1.1 概述

- PL/SQL（Procedural Language/SQL）过程语言/SQL，
- 结合了Oracle过程语言和结构化查询语言的一种扩展语言
- 使用PL/SQL可以写很多高级功能的程序

### 1.1.2 优点

- PL/SQL 具有编程语言的特点，它能把一组SQL语句放到一个模块中，使其更具模块化程序的特点
- PL/SQL 可以采用过程性语言控制程序的结构，也就是说，我们可以在PL/SQL中增加逻辑判断
- PL/SQL 可以对程序中的错误进行自动处理，使程序能够在遇到错误时不会中断
- PL/SQL 程序块具有更好的一移植性，可以移植到另一个Oracle数据库中
- PL/SQL 程序减少了网络的交互，有助于提高程序的性能

### 1.1.3 PL/SQL 体系结构

PL/SQL 引擎用来编译和执行PL/SQL块或子程序，该引擎驻留在Oracle服务器中。PL/SQL引擎仅执行过程语句，而将SQL语句发送给Oracle服务器的SQL语句执行器，由SQL语句执行器执行这些SQL语句

![img](http://s1.sinaimg.cn/middle/8020e411tb14b64fc4d70&690) 

### 1.1.4 PL/SQL 块

- PL/SQL是一种快结构语言，它将一组语句放在一个块中。

- PL/SQL块将逻辑上相关的声明和语句组合在一起

- 匿名块是一个未在数据库中命名的PL/SQL块，在运行时被传递到PL/SQL引擎以便执行

- 在PL/SQL块中可以使用SELECT,INSERT,UPDATE,DELETE 等DML语句，事务控制语句，及SQL函数等

- PL/SQL块中不允许直接使用CREATE,DROP,ALERT等DDL 语句，但可以通过动态SQL来执行它们

- 一个PL/SQL 块由三部分组成

  - 声明部分

  - 执行部分

  - 异常处理部分

    ```sql
    [declare] -- 声明部分，在此声明PL/SQL用到的变量，游标，以及局部的存储过程和函数
    
    BEGIN --执行部分：过程语句及SQL语句，及程序的主要部分
    
    [exception] -- 异常处理部分：错误处理
    
    END；
    ```

## 1.2 PL/SQL 语法

### 1.2.1 变量声明

```sql
variable_name data_type [(size)] [:=init_value]
-- 可以边声明边赋值，也可以先声明，后赋值
```

###1.2.2 常量声明 

```sql
variable_name constant data_type := value
```

> 注意：
>
> 标识符名不能超过30个字符
>
> 第一个字母必须为字母
>
> 不区分大小写
>
> 不能使用‘-’,减号
>
> 不能是SQL保留字

### 1.2.3 输出打印

```sql
DBMS_OUTPUT.PUL_LINE(CONTENT);
```

### 1.2.4 给变量赋值的方式

```sql
declare 
  -- v_sal number(7,2) := 800; 边声明边赋值 方式1
  	 v_sal number(7,2);
begin
  -- v_sal := 800; 方式2
  -- 通过查询语句给变量赋值 方式3
  select sal into v_sal from emp where empno = 7369;
  dbms_output.put_line(v_sal);
end;
```

## 1.3 数据类型

### 1.3.1 标量数据类型

- 标量数据类型包含单个值，没有内部组件。
- 标量数据类型包括数字、字符、布尔值和日期类型

### 1.3.2 LOB 数据类型

- 用来存储大的数据对象的类型

### 1.3.3 属性类型

- 属性类型用于引用变量或数据库列的数据类型，以表示表中的一行的记录类型

- %TYPE

  - 定义一个变量，其数据类型与已经定义的某个数据变量（尤其是表中的某一列的数据）的数据类型一致
  - 优点：
    - 可以不必知道所引用的数据库列的数据类型
    - 所引用的数据库列的数据类型可以实时改变，容易保持一直，不用修改PL/SQL 程序

- %ROWTYPE

  - 返回一条记录的数据类型，其数据类型和数据库表的数据结构相一致
  - 优点
    - 可以不必知道所引用的数据库中列的个数和数据类型
    - 所引用的数据库中列的个数和数据类型可以实时改变，容易保持一致，不用修改PL/SQL程序

- 例：

  ```sql
  declare
  	v_empno employee.empno%TYPE := 7788; 改变量的数据类型将和员工表中empno字段的类型保持一致
  	v_rec employee%ROWTYPE; 改变量的数据类型将和员工表的结构类型相一致
  begin
  	select * into v_rec from employee where empno=v_empno;
  	dbms_output.put_line(v_rec.ename || v_rec.empno);
  end;
  ```

## 1.4 PL/SQL 控制语句

### 1.4.1 概述

PL/SQL程序可以通过控制结构来控制命令执行的流程。标准的SQL没有流程控制的概念，控制结构共有三种类型，具体包括条件控制，循环控制和顺序控制

### 1.4.2 条件控制

- 条件控制用于根据条件执行一系列的语句，条件控制包括if语句和case语句

- IF

  ```
  IF <布尔表达式> THEN
  	PL/SQL 和 SQL 语句
  END IF;
  --------------
  IF <布尔表达式> THEN
  	PL/SQL 和 SQL 语句
  ELSE
  	其他语句
  END IF;
  ---------------
  IF <布尔表达式> THEN
  	PL/SQL 和 SQL 语句
  ELSIF <布尔表达式>
  	PL/SQL 和 SQL 语句
  ...
  ELSE
  	其他语句
  END IF;
  ```

- CASE

  ```SQL
  CASE 条件表达式
  	WHEN 结果1 THEN
  		语段1
  	...
  	[ELSE 语句段]
  END CASE;
  ---------------------
  CASE 
  	WHEN 条件表达式 THEN
  		语段1
  	...
  	[ELSE 语句段]
  END CASE;
  ```

### 1.4.3 循环控制

- 循环控制用于重复执行一系列语句。循环控制包括LOOP 和 EXIT 语句，使用 EXIT 可以随时退出循环

- 循环共有三种类型，包括LOOP,WHILE和FOR

- LOOP

  ```SQL
  LOOP
  	要执行的语句；
  	EXIT WHEN <条件语句> ---满足条件退出
  END LOOP；
  ```

- WHILE

  ```sql
  WHILE <布尔表达式> LOOP
  	要执行的语句;
  END LOOP;
  ```

- FOR

  ```SQL
  FOR 循环计数器 IN [REVERSER] 下限...上限 LOOP
  	要执行的语句；
  END LOOP；
  
  例：
  declare
    v_count number(3);
  begin
    for v_count in 10 .. 20 loop
      dbms_output.put_line(v_count);
    end loop;    
  end;
  ```

### 1.4.4 顺序控制

- 顺序控制用于按顺序执行语句。顺序控制包括NULL语句和GOTO语句。GOTO语句不推荐使用
- NULL 语句
  - NULL　语句是一个可执行语句，相当于一个占位符或不执行任何操作的空语句
  - 它可以使某些语句变得有意义，提高程序的可读性，保证其他语句结构的完整性和正确性

##１.５ 异常处理

### 1.5.1 概述

在运行程序时出现的错误叫做异常。发生异常后，语句将停止执行，PL/SQL 引擎立即将控制转到PL/SQL 块的异常处理部分，值得注意的时，PL/SQL编译错误放生在PL/SQL 程序执行之前，所以不能有异常部分处理

### 1.5.2 预定义异常

Oracle 预定义异常情况大约有24个，对于这种异常情况的处理，无需在程序中dinginess，可由Oracle 自动引发

语法

```sql
begin
  sequence_of_statements;
  exception
    when <exception_name> then
      sequence_of_statements;
    when others then
      sequence_of_statements;
end;
```

> PL/SQL 块只能有一个others 异常处理程序。可以使用函数sqocode 和 sqlerrm来返回错误代码和错误文本信息

### 1.5.3 用户自定义异常

处理步骤

- 在PL/SQL 块的定义部分定义异常情况
  - <异常情况> exception；
- 抛出异常情况
  - palse <异常情况>；
- 在PL/SQL 块的异常情况处理部分对异常情况做出相应的处理

## 1.6 游标

### 1.6.1 概述

- 游标是指向查询结果区域中的一个指针
- 游标为应用程序提供了一种对多行数据查询结果集中出现的每行数据的单独处理的办法

### 1.6.2 游标的使用过程

- 声明游标

  ```sql
  CURSOR cursor_name [(parameter [,parameter]...)]
  [RETURN return_type] IS select_statement [OF UPDATE]
  --------------------
  cursor_name 游标的名称
  parameter 用于为游标指定输入参数。在指定数据类型时，不能使用长度约束，直接写类型名称即可
  return_type 用于定义游标提取的行的数据类型
  select_statement 指定游标定义的查询语句
  ```

- 打开游标

  ```sql
  OPEN cursor_name [(parameters)]
  ```

- 提取游标

  ```sql
  FETCH cursor_name INTO variables
  --------------
  variables 变量名
  ```

- 关闭游标

  ```sql
  CLOSE cursor_name
  ```

- 示例

  ```sql
  -- 使用游标输出员工姓名
  declare
     name emp.ename%TYPE;
     -- 声明游标
     CURSOR emp_cursor IS select ename from emp;
  begin
     -- 打开游标
     open emp_cursor;
     loop
     	 -- 提取游标
       fetch emp_cursor into name;
       exit when emp_cursor%notfound; -- 判读退出条件
       dbms_output.put_line(name); -- 可在此处做一系列的复杂操作
     end loop;
  end; 
  ```

### 1.6.3 游标属性

- %FOUND 只有在DML 语句影响一行或多行时，%FOUND 才返回true
- %NOTFOUND 与%FOUND 相反
- %ROWCOUNT 返回DML 语句影响的行数，如果没有，返回0
- %ISOPEN 游标是否打开

> 注意：如果在处理过程中需要更新或删除行，在定义游标时必须使用select...for update,在执行delete 和 update语句时，使用where current of 子句指定游标的当前行

## 1.7 存储过程

### 1.7.1 概述

从根本上讲，存储过程就是有命名的PL/SQL 程序块，他可以被赋予参数并存储在数据库中，然后由一个应用程序或其他PL/SQL 块掉用，没有名字的过程为匿名过程

### 1.7.2 创建存储过程

```sql
create [or replace] procedure procedure_name [(parameter_list)]
{is | as}
    [local_declarations]
begin
  executable_statements
[exception]
  [exception_handlers]
end [procedure_name]

--------------------
调用方式
1：在begin...end中调用   procedure_name(parameters_list);
2:在sqlplus 中 exec procedure_name(parameters_list)
```

### 1.7.2 接收过程的返回值案例

```sql
create or replace procedure proc_test(num1 number,result out number) -- result 为输出参数
as
begin
  result := num1+10;
end;

declare
  n number(3); -- 定义一个变量，用于存储过程的返回值
begin
  proc_test(5,n);
  
  dbms_output.put_line(n);
  end;
```

### 1.7.3 参数模式

```sql
procedure_name(parameter in | out | in out dataType [:=default])
in 为输入，默认，可以不填
out 输出
in out 既可以输入又可以输出
```

### 1.7.4 存储过程的访问权限

```sql
-- 授予 a 执行 add 权限
grant execute on add to a
-- 撤销
revoke execute on add from a
```

