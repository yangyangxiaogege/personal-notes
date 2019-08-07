# Struts2

## 1.1 Struts2

### 1.1.1 概述

Struts2 是针对Struts1 的弊端进行了升级后的结果，Struts2 以WebWork 的设计思想为核心，吸收了Struts1的部分优点，建立了一个兼容web Work 和 Struts1 的MVC 框架

## 1.2 初步使用

### 1.2.1 导入相关jar包

### 1.2.2 配置中央过滤器

web.xml

```xml
<?xml version="1.0" encoding="UTF-8"?>
<web-app xmlns="http://xmlns.jcp.org/xml/ns/javaee"
         xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
         xsi:schemaLocation="http://xmlns.jcp.org/xml/ns/javaee http://xmlns.jcp.org/xml/ns/javaee/web-app_4_0.xsd"
         version="4.0">
	<filter>
	<filter-name>struts2</filter-name>
    <filter-class>org.apache.struts2.dispatcher.filter.StrutsPrepareAndExecuteFilter</filter-class>
    </filter>
    <filter-mapping>
        <filter-name>struts2</filter-name>
        <url-pattern>/*</url-pattern>
    </filter-mapping>    
</web-app>

```

### 1.2.3 配置核心配置文件

在src根目录下创建struts.xml

```xml
<?xml version="1.0" encoding="UTF-8"?>

<!DOCTYPE struts PUBLIC
        "-//Apache Software Foundation//DTD Struts Configuration 2.5//EN"
        "http://struts.apache.org/dtds/struts-2.5.dtd">
<struts>
    <!--
 	name:包名，此处的包为逻辑包，为了区分不同包
	namespace:包访问路径
	extends:继承包，此处必须继承struts2的default 包
	-->
	<package name="default" namespace="app" extends="struts-default">
        <!--
		配置对应的action 
		result 为execute 的返回值，并根据其值做出相应操作
 		-->
    	<action name="student" class="com.struts.action.StudentAction">
        	<result name="success">/success.jsp</result>
        </action>
    </package>
</struts>
```

### 1.2.4 创建Action

```java
public class StudentAction implements Action {
    
    @Overried
    public String execute() throws Exception {
        return "success";
    }
}
```

## 1.3 访问Servlet API 对象

### 1.3.1 解耦方式1——通过ActionContext获取

```java
public void test() {
    	// 通过ac 对象，获取到与之对应的map
		ActionContext ac = ActionContext.getContext();
		Map<String, Object> session = ac.getSession();
	}
```

###1.3.2 解耦方式2——通过特定接口注入

```java
public class TestServletAPI implements Action,RequestAware,SessionAware{

	private Map<String, Object> request;
	private Map<String, Object> session;
	@Override
	public void setRequest(Map<String, Object> request) {
		// TODO Auto-generated method stub
		
	}
	@Override
	public void setSession(Map<String, Object> session) {
		// TODO Auto-generated method stub
		
	}
}
```

### 1.3.3 耦合方式，获取真实API 对象

```java
public abstract class BaseAction extends ActionSupport implements Action,ServletRequestAware,ServletContextAware {

	public HttpSession session;
	public HttpServletRequest request;
	public ServletContext application;
	
	@Override
	public void setServletRequest(HttpServletRequest request) {
		// TODO Auto-generated method stub
		this.request = request;
		this.session = request.getSession();
	}
	
	@Override
	public void setServletContext(ServletContext application) {
		// TODO Auto-generated method stub
		this.application = application;
	}
}

```

## 1.4 后台数据校验

### 1.4.1 概述

对于一个web应用而言，所有的数据都是通过浏览器收集的，用户输入的信息是非常复杂的。用户操作不熟练，输入出错，硬件设备不正常，蓄意破坏等情况都可能造成系统出错，或者崩溃，所以一般情况下信息验证不只是在客户端进行，后台也会进行验证

### 1.4.2 使用教程

需要进行数据校验的action继承ActionSupport类，全称为com.opensymphony.xwork2.ActionSupport;

在action 中添加validate()方法，写法如下

```java
@Override
	public void validate() {
		if (stu.getName() == null || stu.getName() == "") {
            // 添加错误信息
			addFieldError("name", "名字不能为空");
		}
		if (stu.getId() == null) {
			addFieldError("id", "id 不能为空");
		}
	}
处理完成后，如果有错误，Struts2 框架会根据Action 配置匹配input，进行跳转
```

## 1.5 深入了解Action

### 1.5.1 概述

- 在Struts2 框架中，控制器是由两部分组成的
  - 核心控制器（Filter）用于拦截用户请求，对请求进行处理
  - 业务控制器（Action）调用相应的Model 类实现业务处理，返回结果
- Struts2 并不要求编写的Action 一定要实现Action 接口，只要该Java类含有一个返回字符串的无参public方法，然后在配置文件中进行action配置即可
- 在实际开发中，Action 通常都继承ActionSuppot ,以便于开发

### 1.5.2 作用

- 为给定的请求封装需要做的实际工作（调用特定的业务处理）
- 为数据的转移场所
- 帮助框架决定由哪个结果呈现请求相应

### 1.5.3 一个Action处理多个请求

- method 属性

  ```xml
  此属性是为了解决一个action 处理多个请求的问题
  method 指定了要调用指定action的哪个方法
  action 配置可以写多个，每个action指定不同的方法，即可实现一个action处理多个请求
  此方式会造成代码臃肿，不推荐使用
  <action name="student" class="com.action.StudentAction" method="findAll"></action>
  ```

- 动态方法调用

  ```xml
  使用动态方法调用，可以通过 action名字！方法名 的形式进行调用
  这种模式默认是关闭的，需要手动开启
  配置常量，开启动态方法调用
  <constant name="struts.enabke.DynamicMethodInvocaton" value="true"/>
  
  有些情况下会访问失败，原因是调用的方法名不再struts-default.xml 中 <global-allowed-methods> 标签中，即可以被访问的方法白名单，解决办法如下
  开放白名单,可以写特定方法名，也可以写通配符，此处的是开放所有的
  <global-allowed-methods>regex:.*</global-allowed-methods>
  ```

- 通配符

  ```xml
  通过 * 配置0 个或多个字符串
  {1} 代表的是第一个 * 
  <action name="student*" class="com.action.StudentAction" method="{1}">
  	<result name="success">/{1}.jsp</result>
  </action>
  ```

### 1.5.4 配置默认action

此配置一般用于找不到特定的action 时触发的404 异常

在每个package中只能有一个默认action

```xml
<package name="pk">
	<default-action-ref name="defaultAction"/>
    <action name="defaultAction">
    	<result>/error.jsp</result>
    </action>
    action 的class属性不写，默认为ActionSuppot 类，而这个类中的execute()方法，默认返回success
</package>
```

###1.5.5 result 配置详解

- type 结果类型,常用结果类型

  - dispathcer 默认值，意为转发
  - redirect 重定向
  - 更多类型在struts-default.xml 中可查看

- 动态结果

  - 即当你不知道执行后的结果是哪一个，可以通过表达式配置

  - 例

    ```java
    在action中，设置变量，用于保存动态结果
    public class StudentAction {
    	private String page;
    	
        public String login () throw Exception {
            if(admin) {
            	page="admin.jsp";
             }else{
             	page="common.jsp"   
             }
             return SUCCESS;
        }
        省略getter setter 方法
    }
    
    <action name="stu" class="StudentAction" method="login">
    	<result>${page}</result>
    </action>
    
    ```

- 全局结果

  - 多个action 需要访问同一个结果时，可以将此结果配置为全局结果

    ```xml
    <global-results>
    	<result name="errot">/error.jsp</result>
        可有多个
    </global-results>
    ```

## 1.6 OGNL

### 1.6.1 概述

OGNL 的 全称是 Object Graph Navigation Language ，即对象导图语言，它是一个开源项目，工作在视图层，用来取代页面中的Java脚本，简化数据的访问

### 1.6.2 OGNL 作用示意图

![](C:\Users\ASUS\Desktop\My File\个人笔记\SSH\Struts2\OGNL.png)

### 1.6.3 自定义类型转换器

自定义一个转换器，继承StrutsTypeConverter，放在converter包下

 ```java
public class MyDateConverter extends StrutsTypeConverter{

	// 定义多个转换格式
	private final DateFormat [] dfs = {
			new SimpleDateFormat("yyyy年MM月dd日"),
			new SimpleDateFormat("yyyy-MM-dd"),
			new SimpleDateFormat("yyyy/MM/dd"),
			new SimpleDateFormat("yyyy.MM.dd日")
		};
	@Override
	public Object convertFromString(Map arg0, String[] arg1, Class arg2) {
		// 获取传送过来的日期字符串
		if (arg1 == null || arg1.length == 0) return null;
		String dateStr = arg1[0];
		for (int i = 0; i < dfs.length; i++) {
			
			try {
				
				return dfs[i].parse(dateStr);
				
			} catch (Exception e) {
				continue;
			}
		}
		// 如果循环后还没有转换成功，抛出此异常
		throw new TypeConversionException();
	}
 ```

配置自定义类型转换器

方式1：应用于全局范围的类型转换器

在classpath根路径下创建名为xwork-conversion.properties 的属性文件

内容为

特定类的名称=类型转换器的全名称



方式2：应用于特定类的类型转换器

在特定类的相同目录下创建一个名为 类名-conversion.properties 的属性文件

内容为

特定类的属性名=类型转换器的全名称



这里使用方式1：

java.util.Date=com.converter.MyDateConverter

此时在页面中输入日期格式时，会使用该转化器进行转换

### 1.6.4 访问ActionContext 中的数据

ActionContext 中的对象

![](C:\Users\ASUS\Desktop\My File\个人笔记\SSH\Struts2\ActionContext.png)

- ValueStack 是默认的根对象，访问此对象内的数据可以直接使用表达式获取，也可以通过指定的名字的跟对象进行访问，例如：#session.user.userName
- application 用于访问application 属性，可以通过#application.username 或 #application["username"] 两种方式进行调用，相当于调用application.getAttribute("username")
- session 用于访问session 作用域中的值
- request 用于访问request 作用域中
- paraneters 用于访问http 的请求参数，相当于调用request.getparameterValues（“ ”），返回一个数组
- attr 按照pageContext——request——session——application 作用域进行查找，找到停止

## 1.7 拦截器

### 1.7.1 概述

拦截器是一种可以在请求处理之前或者之后执行的Struts2 组件，将通用的操作动态的插入到Action执行的前后，这样有利于系统的解耦

### 1.7.2 为什么把拦截器的执行看作递归过程？

框架通过第一次调用ActionInvocation 的 invoke() 方法开始这一过程，ActionInvocation 通过调用拦截器的intercept() 方法把控制权转交给Action 配置的第一个拦截器。最重要的是intercept() 把ActionInvocation 实例看作参数，在拦截器的处理过程中，它会调用ActionInvocation 对象的invoke() 方法来调用后续的拦截器

### 1.7.3 拦截器的执行周期

```java
public class MyInterceptor extends AbstractInterceptor {
    @Overried
    public String intercept(ActionInvocation invocation) throws Exception {
        // 1.执行Action之前的操作
        
        // 2.执行后续的拦截器或者Action
        String result = invocation.invoke();
        
        // 3.执行Action 之后的操作
        
        // 4.返回结果字符串
        return result;
    }
}
```

### 1.7.4 配置拦截器

- 方式1

```xml
<package name="default" namespace="/" extends="struts-default">
	<!-- 定义拦截器 -->
    <interceptors>
    	<interceptor name="myInterceptor" class="com.interceptor.MyInterceptor"/>
    </interceptors>
    <!-- 使用拦截器 -->
    <action name="action" class="com.action.MyAction">
    	<result>/index.jsp</result>
        <!-- 自定义的拦截器 -->
        <interceptor-ref name="myInterceptor"/>
        <!-- struts2 默认的拦截器，如果引入的自定义的拦截器，没有显示调用默认的，那么默认的拦截器不会生效 -->
        <interceptor-ref name="defaultStack"/>
    </action>
</package>
```

- 方式2

```xml
<package name="default" namespace="/" extends="struts-default">
	<!-- 定义拦截器 -->
    <interceptors>
    	<interceptor name="myInterceptor" class="com.interceptor.MyInterceptor"/>
        <!-- 定义拦截器栈 -->
        <interceptor-stack name="myStack">
        	<interceptor-ref name="myInterceptor"/>
        	<interceptor-ref name="defaultStack"/>
        </interceptor-stack>
    </interceptors>
    <!-- 使用拦截器 -->
    <action name="action" class="com.action.MyAction">
    	<result>/index.jsp</result>
        <!-- 引用拦截器栈 -->
        <interceptor-ref name="myStack"/>
    </action>
</package>
```

## 1.8 文件上传与下载

文件上传

```jsp
<from action="" method="post" enctype="multipart/form-data">
	<input type="file" name="upload">
	<input type="submit">
</from>
```

