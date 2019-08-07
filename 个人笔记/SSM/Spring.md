# Spring

## 1.1概述

### 1.1.1 什么是Spring框架？Spring框架有哪些主要模块？

- Spring框架是一个为Java应用程序的开发提供了综合，广泛的基础性支持的Java平台。Spring帮助开发者解决了开发中的基础性的问题，使得开发人员可以专注于应用程序的开发。Spring框架本身亦是按照设计模式精心打造
- Spring框架至今已集成了20多个模块。这些模块主要分为，核心容器，数据访问/继承，web,AOP,工具，消息和测试模块

## 1.2 核心模块----Ioc(控制反转)，DI(依赖注入)

### 1.2.1 什么是Ioc控制反转？

- inversion of control 
- 控制反转模式；控制反转是将组件间的依赖关系从程序内部提到外部来管理 
- **控制反转IOC是说创建对象的控制权进行转移，以前创建对象的主动权和创建时机是由自己把控的，而现在这种权力转移到第三方** 

### 1.2.2 什么是DI依赖注入

- dependency injection 
- 依赖注入模式；依赖注入是指将组件的依赖通过外部以参数或其他形式注入 

> 注入前提：满足标准的JavaBean，要有set方法

### 1.2.3 注入方式

```xml
方式1
<!--通过set方法注入-->
<bean id="stu" class="com.domain.Student">
	<property name="stuName" value="zhangsan"/>
</bean>
方式2
<!--通过构造函数注入-->
<bean>
	<constructor-arg index="" type="" value=""></constructor-arg>
	<constructor-arg name="" type="" value=""></constructor-arg>
	<constructor-arg index="" type="" ref=""></constructor-arg>
</bean>
方式3
<!--通过命名空间注入
本质还是通过set方法注入，只不过简化了写法
1.先引入命名空间p：xmlns:p="http://www.springframework.org/schema/p"
2.注入引用对象，p:grade-ref=""
-->
<bean id="stu" class="com.domain.Student"
      p:stuName="张三"/>


注入集合
<bean>
	<property name="hobbies">
		<list>
			<value>打篮球</value>
		</list>
	</property>
</bean>
```

### 1.2.4 配置核心文件+初步使用

- applocationContext.xml

  ```xml
  <?xml version="1.0" encoding="UTF-8"?>
  <beans  xmlns = "http://www.springframework.org/schema/beans" 
      xmlns:xsi = "http://www.w3.org/2001/XMLSchema-instance" 
      xsi:schemaLocation = "http://www.springframework.org/schema/beans
          http://www.springframework.org/schema/beans/spring-beans.xsd">
  
      <bean id="grade" class="com.spring.domain.Grade"></bean>
  	<!-- 配置托管类 id为标识   class是被Spring托管的类-->
      <bean  id = "stu"  class = "com.spring.domain.Student">
      	<!-- 注入值 -->
          name:属性名 value:属性值 ref:引用值
          <property name="stuName" value="张三"/>
          <property name="grade" ref="grade"/>
      </bean>
  </beans>
  ```

- 初步使用

  ```java
  //此类用于向Spring索要JavaBean
  public class SpringTool {
  	private static final String CONFIG_FILE="applicationContext.xml";
  	private static ApplicationContext ctx;
  	
  	static {
  		ctx=new ClassPathXmlApplicationContext(CONFIG_FILE);
  	}
  	
  	public static Object getBean(String beanId) {
  		return ctx.getBean(beanId);
  	}
  }
  
  public class Test{
      public static void main(string [] args){
          Student stu=(Student)SpringTool.getBean("stu");
      }
  }
  ```

##1.3 AOP(面向切面编程)

### 1.3.1 OOP和AOP的关系

- AOP(aspect oriented programming) 面向切面编程
- OOP(object oriented programming) 面向对象编程
- OOP和AOP互为补充
- 没有谁更厉害之说
- 没有谁替代谁之说

### 1.3.2 AOP介绍

- 它的本质就是方法拦截
- 能够在指定方法执行之前或者执行之后，自动完成某些事情

### 1.3.3 在核心文件中配置AOP

```xml
<?xml version="1.0" encoding="UTF-8"?>
<beans  xmlns = "http://www.springframework.org/schema/beans" 
       xmlns:aop = "http://www.springframework.org/schema/aop"//引入aop命名空间 
    xmlns:xsi = "http://www.w3.org/2001/XMLSchema-instance" 
    xsi:schemaLocation = "http://www.springframework.org/schema/beans
        http://www.springframework.org/schema/beans/spring-beans.xsd
                          http://www.springframework.org/schema/aop //说明文档位置关系
        http://www.springframework.org/schema/aop/spring-aop.xsd">

	<!-- 托管通知（做拦截的对象）对象 -->
    <bean  id = "beforeAdvice"  class = "com.spring.advice.BeforeAdvice" /> 
    
    <!-- 配置AOP -->
    <aop:config>
    	<!-- 配置一个切面（也就是哪些包下的哪些方法，会被拦截）-->
        <aop:pointcut id="pt" expression=""/>
        <!--
			expression:表达式，切入点
			execution(modifiers-pattern? ref-type-pattern declaring-type-pattern? name-pattern throws-pattern？)
			execution(访问修饰符 返回值类型 包名 方法名（参数列表）声明抛出的异常类型)
			除了返回类型参数（ref-type-pattern），方法名（name-pattern），方法参数,其它的都为可选参数
			注意：
				1.省略不写的地方，表示不做出要求
				2.可以带上包名，对指定包下的指定类进行拦截execution(* com.dao.*.*(..))
				3.方法名也可用通配符execution(* com.dao.*.add*(..))
				4.可以对包下的后代包进行拦截execution(* com.dao..*.*(..))用两个 . 表示
		-->
        
        <!-- 引用一个通知对象，由这个通知对象来拦截-->
        <aop:aspect ref="beforeAdvice">
            <!--这里是指拦截的时机，即什么时候拦截，拦截之后做什么-->
        	<aop:before method="doBefore" point-ref="pt"/>
        </aop:aspect>
        
        <aop:aspect ref="beforeAdvice">
            <!--后置通知 returning 返回值-->
        	<aop:after-returning method="doBefore" returning="result" point-ref="pt"/>
        </aop:aspect>
    </aop:config>
</beans>
```

### 1.3.4 获取被拦截方法的请求参数

- 前置通知

  ```java
  public class BeforeAdvice{
      public void doBefore(JoinPiont jp){
          //通过JOinPiont 对象可以获取到被拦截方法的一系列信息
          //例如：
          //参数：jp.getArgs()
          //触发对象：jp.getTarget();
      }
  }
  ```

- 后置通知

  ```java
  public class AfterAdvice{
      public void doAfter(JoinPoint jp,Object result){
          //result 便是返回结果
      }
  }
  ```

## 1.4 注解的使用

`使用注解托管类，需要先开启组件扫描：<context:component-scan base-package="哪个包下的注解需要被扫描"/>`

 

### 1.4.1 使用注解托管类

```xml
@Component:组件，部件
@Repository:仓库，数据，数据访问对象
@Service:服务，业务
@Controller:控制

这四个注解都是放在类上的，
相当于<bean id="xxx" class="xxx">
他们的作用是一样的，用不同的注解放在不同的层上，使代码的可读性更好
例如，业务层使用@Service
```



### 1.4.2 自动注入值----@Autowried



相当于

<bean>

​	<property name="xxx" value="xxx">

</bean>

```java
@Autowried:自动注入，自动装配（spring自带的注入注解）
1.根据类型注入，如果有多个相同类型，则报错
org.springframework.beans.factory.BeanCreationException
expected single matching bean but found 2,大概意思为，该接口下有多个实现类，而这里只能注入一个，所以需要指明注入哪一个
2.根据名字来注入，则需要再借助一个注解
@Autowried
@Qualifier("studentDAOImpl1")
3.此注解加在私有属性上，则直接通过反射机制直接注入
如果加在set方法上，则通过set方法进行注入
```

###1.4.3 注入值----@Resource

```java
该注解的作用等同于@Autowried,是javaEE自带的注解
根据名字注入
@Resource("studentDAOImpl1")
```

## 1.5  AOP通知类型，使用注解的方式，配置通知

- AOP的实现和配置有两套

  - xml方式，Spring自带的
  - 注解方式，使用AspectJ组件实现的，Spring只是做了封装

- 通知类型

  - 前置通知 before
  - 后置通知 after-returning
  - 异常通知 after-throwing
  - 最终通知 after
  - 环绕通知 around

- 使用

  ```java
  xml文件中，开启自动代理
  <aop:aspectj-autoproxy/>
  @Component
  @AspectJ----此注解的作用是说明，该类是一个AOP通知
  public class AroundAdvice{
  
  	//说明此处是一个环绕通知，execution说明了拦截哪个包下的哪个类的哪个方法
  	//环绕通知是一个功能强大的通知，可以包含所有通知，包括要不要调用被拦截的方法
  	//@Around @Before ...
  	@Around("execution(* com.spring.biz.*.*(..))")
  	public Integer doAround(ProceedingJoinPoint jp){
  		Object result=null;
          try{
          	System.out.println("在方法执行前，为前置通知");
          	result=jp.proceed();//调用被拦截的方法
          	System.out.println("在方法执行后，为后置通知");
          }catch(Exeception ex){
          	System.out.println("异常通知");
          }finally{
          	System.out.println("最终通知");
          }
          return result;//此处才是被拦截的方法最终的执行结果
  	}   
  }
  ```
## 1.6 mybatis,spring 整合

  ###1.6.1 配置文件+配置声明式事务

  ```properties
  配置文件 database.properties
  jdbc.driver=com.mysql.jdbc.Driver
  jdbc.url=jdbc:mysql://localhost:3306/studentmanager
  jdbc.user=root
  jdbc.pwd=123
  ```

  ```xml
  <?xml version="1.0" encoding="UTF-8"?>
  <beans xmlns="http://www.springframework.org/schema/beans"
  	xmlns:aop="http://www.springframework.org/schema/aop" 
  	xmlns:context="http://www.springframework.org/schema/context"
  	xmlns:tx="http://www.springframework.org/schema/tx"
  	xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
  	xsi:schemaLocation="http://www.springframework.org/schema/beans
          http://www.springframework.org/schema/beans/spring-beans.xsd
          http://www.springframework.org/schema/aop
          http://www.springframework.org/schema/aop/spring-aop.xsd
          http://www.springframework.org/schema/context
          http://www.springframework.org/schema/context/spring-context.xsd
          http://www.springframework.org/schema/tx
          http://www.springframework.org/schema/tx/spring-tx.xsd">
  
  	<!-- 加载属性文件 -->
  	<bean class="org.springframework.beans.factory.config.PropertyPlaceholderConfigurer">
  		<property name="location" value="classpath:database.properties"></property>
  	</bean>
      
  	<!-- 配置数据源，以dbcp作为数据连接池 -->
  	<bean id="dataSource" class="org.apache.commons.dbcp.BasicDataSource"
  		destroy-method="close">
          <!--value的值可以直接写入，也可以通过被加载的本地属性配置文件中的键获取-->
  		<property name="driverClassName" value="${jdbc.driver}" />
  		<property name="url" value="${jdbc.url}" />
  		<property name="username" value="${jdbc.user}" />
  		<property name="password" value="${jdbc.pwd}" />
  	</bean>
  	
  	<!-- 配置会话工厂 -->
  	<bean id="sqlSessionFactory" class="org.mybatis.spring.SqlSessionFactoryBean">
          <!--mybaits配置文件-->
  		<property name="configLocation" value="classpath:mybatis-cofig.xml"/>
  		<!--数据源-->
          <property name="dataSource" ref="dataSource"/>
  		<!--加载mapper文件-->
          <property name="mapperLocations">
  			<list>
  				<value>com/yxy/mapper/*.xml</value>
  				<!--  <value>com/yxy/mapper/GradeMapper.xml</value>-->
  			</list>
  		</property>
  	</bean>
  	
  	<!-- 配置方式1 ==》配置mapper对应的代理对象
  	<bean id="stuDao" class="org.mybatis.spring.mapper.MapperFactoryBean">
  		<property name="sqlSessionFactory" ref="sqlSessionFactory"></property>
  		<property name="mapperInterface" value="com.yxy.dao.StudentDao"></property>
  	</bean>
  	-->
  	<!-- 配置方式2 配置一个mapper扫描器，批量生成mapper代理对象-->
  	<bean class="org.mybatis.spring.mapper.MapperScannerConfigurer">
  		<!-- basePackage指定要扫描的包，再此包下的所有映射器都会被扫描到，如果有多个包，用逗号或者分号隔开 -->
  		<property name="basePackage" value="com.yxy.dao"></property>
  	</bean>
  	
  	<!-- 开启组件扫描 -->
  	<context:component-scan base-package="com.yxy.service" />
  	
  	<!-- 配置事务管理 
  		事务管理配置：Mybatis自动参与到spring事务管理中，无需额外配置，
  		只要org.mybatis.spring.sqlSessionFactoryBean中引用的数据源，与dataSourceTransactionManager中
  		引用的数据源相同即可，否则事务管理不会生效
  	-->
  	<bean id="transactionManager" class="org.springframework.jdbc.datasource.DataSourceTransactionManager">
  		<property name="dataSource" ref="dataSource"></property>
  	</bean>
  	<!-- 配置AOP -->
  	<aop:aspectj-autoproxy/>
  	<!-- 使用声明式配置事物（事物通知） 
  		spring中的常用事务：
  		REQUIRED 支持当前事务，如果没有当前事务，则新建一个
  		SUPPORTS 支持当前事务，如果没有，就以非事务的方式处理
  		MANDATORY 支持当前事务，如果没有，则抛出异常
  		REQUIRES_NEW 新建事务，如果存在当前事务，当前事务挂起
  		NOT_SUPPORTED 以非事务的方式执行，如果存在当前事务，当前事务挂起
  		NEVER 以事务的方式执行，如果存在当前事务，则抛出异常
  	-->
  	<tx:advice id="txAdvice" transaction-manager="transactionManager">
  		<tx:attributes>
              <!--哪些方法需要事务-->
  			<tx:method name="add*" propagation="REQUIRED"/>
  			<tx:method name="update*" propagation="REQUIRED"/>
  			<tx:method name="delete*" propagation="REQUIRED"/>
  			<tx:method name="*" propagation="SUPPORTS" read-only="true"/>
  		</tx:attributes>
  	</tx:advice>
  	<!-- 将通知应用到指定的切入点 -->
  	<aop:config proxy-target-class="true">
  		<aop:pointcut expression="execution(* com.yxy.service..*.*(..))" id="txPointCut"/>
  		<aop:advisor advice-ref="txAdvice" pointcut-ref="txPointCut"/>
  	</aop:config>
  ```

### 1.6.2 使用 

```java
dao层不需要实现类了，也不需要baseDao了，因为此时的sqlSessionFactory已经交由spring托管了
mapper也通过spring自动生成了代理对象，所以使用时直接通过spring获取其对象就ok了

```

  ## 1.7  spring原型模式

- 你什么时候用，就什么时候创建对象，用完之后销毁
- 用几次就创建几次，每次都是一个新的对象
- 非原型模式则是在初始化时全部一次性创建好的
- 配置文件中通过<bean scope="property"/>设置原型模式
- 注解可使用@Scope("property")

