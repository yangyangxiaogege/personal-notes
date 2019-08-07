# SpringMVC

## 1.1概述

- Model 模型（JavaBean ，DAO ,Biz）
- View 视图（JSP）
- Controller (控制器层，对servlet进行封装，更加方便的把控全局)

## 1.2 初始配置即使用

### 1.2.1web.xml   此文件是为了加载springmvc

```xml
<?xml version="1.0" encoding="UTF-8"?>
<web-app xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns="http://xmlns.jcp.org/xml/ns/javaee" xsi:schemaLocation="http://xmlns.jcp.org/xml/ns/javaee http://xmlns.jcp.org/xml/ns/javaee/web-app_3_1.xsd" id="WebApp_ID" version="3.1">
  
   <!--配置一个编码过滤器-->
   <filter>
  	<filter-name>encodingFilter</filter-name>
  	<filter-class>org.springframework.web.filter.CharacterEncodingFilter</filter-class>
  	<init-param>
  		<param-name>encoding</param-name>
  		<param-value>UTF-8</param-value>
  	</init-param>
  </filter>
  <filter-mapping>
  	<filter-name>encodingFilter</filter-name>
  	<url-pattern>*.action</url-pattern>
  </filter-mapping>  
  
  <servlet>
  	<!--配置servlet调度者-->
     <servlet-name>dispatcher</servlet-name>
  	<servlet-class>org.springframework.web.servlet.DispatcherServlet</servlet-class>
  	<init-param>
  		<param-name>contextConfigLocation</param-name>
        <!--springmvc配置文件-->
  		<param-value>classpath:springmvc-servlet.xml</param-value>
  	</init-param>
  	<load-on-startup>1</load-on-startup>
  </servlet>
  <servlet-mapping>
  	<servlet-name>dispatcher</servlet-name>
    <!--访问形式，*代表任意字符-->
  	<url-pattern>*.action</url-pattern>  ----》/* 拦截所有
  </servlet-mapping>
</web-app>
```

###1.2.2 springmvc-servlet.xml

```xml
<?xml version="1.0" encoding="UTF-8"?>
<beans  xmlns = "http://www.springframework.org/schema/beans" 
	xmlns:context = "http://www.springframework.org/schema/context"
    xmlns:mvc = "http://www.springframework.org/schema/mvc"
    xmlns:xsi = "http://www.w3.org/2001/XMLSchema-instance" 
    xsi:schemaLocation = "http://www.springframework.org/schema/beans
        http://www.springframework.org/schema/beans/spring-beans.xsd
        http://www.springframework.org/schema/context
        http://www.springframework.org/schema/context/spring-context.xsd
	    http://www.springframework.org/schema/mvc
        http://www.springframework.org/schema/mvc/spring-mvc.xsd">
	
    <!--以/login访问该控制器
	<bean name="/login" class="com.spring.controller.LoginController"/>
	-->
    <!--开启注解驱动，支持@RequestMapping-->
    <mvc:annotation-driven/>
    
    <!--开启组件扫描，支持@Component-->
    <context:component-scan base-package="com.spring"/>
    
    <!--配置视图解析器-->
    <bean class="org.springframework.web.servlet.view.InternalResourceViewResolver">
    	<property name="prefix" value="/WEB-INF/pages"/>
        <property name="suffix" value=".jsp"/>
    </bean>
</beans>
```

###1.2.3 controller---初始

```java
package com.spring.controller;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.springframework.web.servlet.ModelAndView;
import org.springframework.web.servlet.mvc.AbstractController;

public class LoginController extends AbstractController{
	
	@Override
	protected ModelAndView handleRequestInternal(HttpServletRequest arg0, HttpServletResponse arg1) throws Exception {
		System.out.println("hello world");
		return null;
	}
}
```

###1.2.4 controller---升级

```java
@controller
@RequestMapping("/user")
public class UserController{
 
    @requestMapping("/login")  //此注解的作用是设置该方法的访问路径
    public string login(@RequestParam(name="userName")String name,String pwd){
        //@RequestParam注解是用来给请求参数起别名的，则表单提交时需要使用此别名给表单元素命名
    }
}
```

## 1.3 后台接收值，和传值

- 接受请求参数

  ```java
  @RequestMapping("/login")
  public String login(String name,String pwd){
      //参数名为，前台表单名，springmvc会自动匹配，并做相应的数据类型转换
  }
  @RequestMapping("/login1")
  public String login(User user){
      //表单名可以直接写对象的属性名
      //如果对象中有日期字段，则要加上@DateTimeFormat("日期格式")
  }
  //同时也可以通过@RequestParam注解给参数起别名
  ```

- 传值到前台

  ```java
  @RequestMapping("/login")
  public String login(User user,Model model){
   //通过model对象即可向前台传值
      model.addAttribute("name", value)
  }
  ```


## 1.4 异常处理

- 在Controller中

  ```java
  @ExecptionHandler(value={Execption.class})//value值可有多个，用逗号隔开
  public string handler(Execption ex){
      System.out.println(ex.getMessage());
      
      return "error";//跳转至友好的提示页面
  }
  ```

- 配置异常处理器

  ```xml
  <bean class="org.springframework.web.servlet.handler.SimpleMappingExceptionResolver">
  	<property name="execptionMappings">
      	<props>
              如果发生此异常，则跳转至error1页面
          	<prop key="java.lang.NullPointerExecption">error1</prop>
              ....
          </props>
      </property>
  </bean>
  ```

## 1.5 spring表单标签

- Spring提供的轻量级标签库
- 可在JSP页面中渲染HTML元素的标签
- 用法
  - 必须在JSP页面的开头处声明taglib指令
  - <%@ taglib prefix="fm"  uri="http://www.springframework.org/tags/form" %>

## 1.6 后台数据验证

- Bean Validation

- JSR 303标准

- SpringMVC支持此标准

- 使用的时候需要导入jar包

  - hibernate-validtor-4.0.1.GA.jar
  - validation-api-1.0.0.GA.jar

- 和@valid配合使用

- 使用实例

  ```java
  User.java
  @NotEmpty(message="用户名不能为空")
  private String userName;
  ...
   
  UserController.java
  //@Valid 说明了被验证的对象，后边紧跟的result为错误结果，顺序必须是这样，不能调换
  public String login(@Valid User user,BindingResult result){
      
      if(result.hasErrors()){
          return "error";
      }   
  }
  
  error.jsp
  <fm:form ModelAttribute="user">
  	<fm:error path="userName"/>
  </fm:form>
  ```

## 1.7 REST风格

- 一种软件架构风格

- 主要体现在URL的改变上

- 由原来的查询字符串融入到路径中

- 更简短更优雅

- 但是中文不好处理，大量数据不好处理，选择使用

- SpringMVC支持REST风格，@PathVariable注解

- 实例

  jsp

  ```html
  //查询字符串
  <a href="delete.action?userId=1000"/>
  
  //REST
  <a href="delete/1000.action"/>
  
  ```

  java

  ```java
  @RequestMapping("/delete/{userId}")
  public String delete(@PathVariable("userId") String userId){
      System.out.println(userId);//1000
  }
  ```

## 1.8 文件上传

配置文件

```xml
<!-- 
		配置文件上传解析器 
		该解析器的id不能更改 
	-->
	<bean id="multipartResolver" class="org.springframework.web.multipart.commons.CommonsMultipartResolver">
		<property name="maxUploadSize" value="50000000"/>
		<property name="defaultEncoding" value="UTF-8"/>
	</bean>
```

jsp

```jsp
<form action="user/fileUpload.html" method="post" enctype="multipart/form-data">
		<input type="file" name="mFile">
		<input type="submit" value="上传">
	</form>
```

java

```java
@RequestMapping("/fileUpload")
	public String fileUpload(MultipartFile mFile,HttpServletRequest request) throws IllegalStateException, IOException {
		//获取文件的拓展名
		String extensionName=FilenameUtils.getExtension(mFile.getOriginalFilename());
		//生成新的文件名
		String newFileName=UUID.randomUUID().toString().replaceAll("-", "")+"."+extensionName;
		
		//获取网站运行根目录
		String root=request.getServletContext().getRealPath("/");
		
		//要存在在网站上的相对路径
		String relativePath="/upload/"+newFileName;
		
		//完整路径
		String fullPath=root+relativePath;
		
		//将文件上传到项目中
		mFile.transferTo(new File(fullPath));
		
		return "success";
	}
```

## 1.9 消息转换器

```xml
注解驱动，支持@RequestMapping
<mvc:annotation-driven>
 //消息转换器
<mvc:message-converters >
    注意：不同的消息转换器，先后顺序对执行结果是有影响的，如果返回内容都为字符串，那么前者的有先级高一些
		<!--用于处理ajax响应结果的字符集的编码格式 -->
			<bean class="org.springframework.http.converter.StringHttpMessageConverter">
				<property name="supportedMediaTypes">
					<list>
                        这里的先后顺序也会对结果造成影响
						<value>text/plain;charset=utf-8</value>
						<value>application/json;charset=utf-8</value>
					</list>
				</property>
			</bean>
			<!-- fastjson自带的json转换器，用于将对象转换为json字符串 -->
			<bean class="com.alibaba.fastjson.support.spring.FastJsonHttpMessageConverter">
				<property name="supportedMediaTypes">
					<list>
						<value>text/html;charset=utf-8</value>
						<value>application/json;charset=utf-8</value>
					</list>
				</property>
			</bean>
		</mvc:message-converters>
    </mvc:annotation-driven>
```

## 2.1 拦截器

```xml
<!-- 配置拦截器 -->
	<mvc:interceptors>
		<mvc:interceptor>
			<!-- 拦截stuC4下的所有方法 -->
			<mvc:mapping path="/stuC4/**"/>
			<!-- 不用进行拦截的方法 -->
			<mvc:exclude-mapping path="/stuC4/getId.action"/>
			<!-- 指定拦截类 -->
			<bean class="com.yxy.interceptor.StudentInterceptor"/>
		</mvc:interceptor>
	</mvc:interceptors>
```

```java
public class StudentInterceptor extends HandlerInterceptorAdapter{
    
    //进入Controller前做的事
    @Overried
    public boolean preHandle(HttpServletRequest request){
        if(request.getAttribute("user")!=null){
            return true;
        }else{
            System.out.println("请先登陆在做操作");
            return false;
        }
    }
}
```

