servlet(小服务程序)

1、概述
	是一个符合特定规范的Java程序，是一个基于Java技术的web组件，Servlet运行在服务器端，由
	Servlet容器管理，用于处理客户端请求并作出响应
2、生命周期
	实例化--》初始化--》服务--》销毁
				init   service   destory
3、三种创建方式
	1)实现javax.servlet.Servlet接口
	2)继承javax.servlet.GenericServlet类(适配器模式)
	3)继承javax.servlet.HttpServlet类(模板设计模式)推荐使用
4、三种创建servlet的方式之间的联系
	GenericServlet类 实现了 Servlet接口 HttpServlet类 继承了GenericServlet类
5、线程安全
	不要使用全局变量，要使用局部变量
6、ServletConfig
	获取配置文件的信息
	获得servletContext对象
7、ServletContext
	代表的是整个应用，一个应用只有一个ServletContext对象。单实例
	域对象：在一定范围内(当前应用)，使多个Servlet共享
8、与Servlet相关的对象
		1)Servlet
		2)GenericServlet
		3)HttpServlet
		4)ServletConfig {servlet配置对象}
		5)ServletContext {定义了servlet用来与servlet容器通信的一组方法}
		6)ServletPequest{请求}
		7)ServletResponse{响应}
		8)HttpServletRequest
		9)HttpServletResponse
		10)RequestDispatcher {转发请求}
		
9、消息头
setHeader("content-type", "text/html;char=utf-8")  内容是什么类型
							//调配           附件       文件名      
setHeader("content-disposition", "attachment;filename="+fileName); 告诉客户端要下载文件 
//告诉客户端不使用缓存
setHeader("pragma","no-cache");
setHeader("cache-control","no-cache");
setIntHeader("expires",0);

setHeader("refresh","3;url=...");//实现刷新功能，单位为S即指定秒数后刷新并作相应事情

setCharacterEncoding("UTF-8");//告诉服务器应使用什么编码
setContentType("utf-8");//同时告诉服务器和客户端使用什么编码

10、getWriter();//获取字符输出流
	getOutputStream();//字节输出流
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	