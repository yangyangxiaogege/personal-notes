# Vue 笔记

## 1.1 什么是Vue?

- Vue.js 是目前最火的一个前端框架
- Vue.js 是一套构建用户界面的框架，只关注视图层，还便于与第三方库或既有项目整合

## 1.2 MVC 和 MVVM 的关系图

![1549684535678](C:\Users\ASUS\AppData\Local\Temp\1549684535678.png)

## 1.3 Vue ——hello world 

```html
<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="UTF-8">
	<title>Vue初体验</title>
	<script type="text/javascript" src="./node_modules/vue/dist/vue.js"></script>
</head>
<body>
	<div id="app">
		<h1>{{ msg }}</h1>
	</div>
</body>
</html>
<script type="text/javascript">
	//创建一个vue实例
	var app = new Vue({
		el:'#app', //此处说明了创建的vue实例控制的是页面的哪块区域
		data:{ //被控制区域所需的数据
			msg:"hello world"
		}
	})
</script>
```

## 1.4 v-cloak v-text v-html等基本指令

- v-block

  ```html
  <style>
      [v-cloak] {
          display:none;
      }
  </style>
  
  <div>
      <p v-cloak>{{ msg }}</p>
  </div>
  使用css配合v-cloak 可以解决在网络环境较差时，插值表达式的闪烁问题
  ```

- v-text

  ```html
  例：msg的值为hello
  <p>----{{msg}}-----</p> ==><p>----hello-----</p> 
  <p v-text="msg">------</p> ==><p>hello</p>
  
  v-text 和 插值表达式的作用都是为了渲染值，
  插值表达式在使用时不会替换原有内容，只会操作占位符区域
  v-text 会覆盖原有的值
  ```

- v-html

  ```html
  例：msg的值为<h1>hello</h1>
  
  <div v-html="msg">
  </div>
  
  v-html 会将msg作为html去解析
  ```

- v-bind 

  ```HTML
  例：mytitle 值为 这是一个按钮
  <input type='button' v-bind:title='mytitle'>
  v-bind 是用来绑定属性的，如果不加这个指令，mytitle 将会被识别为一个普通字符串，而不是一个变量
  v-bind 可以简写为 ：   v-bind:title == :title
  ```

- v-on 

  ```html
  <button v-on:click="show">按钮</button>
  
  var vue=new Vue({
  	el:'',
  	data:{
  
  	},
  	methods:{//被控制区域所需的方法
  		show:function(){
  			console.log('这里是show方法');
  		}
  	}
  })
  
  v-on 事件绑定机制  缩写 @
  ```

  > 在vue中，要想在内部获取data，或者调用methods中的方法，需要通过this来操作

## 1.5 事件修饰符

使用方式：在事件后通过`.`的方式调用，例如：@click.stop

- stop 阻止冒泡 
- prevent 阻止默认行为
- capture 让事件以捕获的形式触发
- self 只触发自身的，不关心别人的
- once 事件只触发一次

## 1.6 v-model 和数据双向绑定

```html
<input type='text' v-model='msg'/>
此时在页面修改了msg值后，内存中的msg也将会改变，反之，也成立
注意：v-model 只能在表单元素中使用
```

## 1.7 更多Vue指令的介绍

https://www.jianshu.com/p/c4a87e1b4ef7

## 1.8 过滤器

### 1.8.1 概述

Vue.js 允许用户自定义过滤器，可被用作一些常见的文本格式化，过滤器可以用在两个地方,mustache插值表达式和v-bind 表达式，过滤器应该添加在JavaScript表达式的底部，由管`道符`指示

### 1.8.2 定义全局过滤器

```html
<script>
	Vue.filter("filter_name",function(params){
    	return 处理过后的内容；
	})
</script>
--------------
调用
<h1>{{msg | filter_name}}</h1>
msg 将会是filter_name 过滤器的第一个参数

---------多个参数---------
<script>
	Vue.filter("filter_name",function(msg,param1){})
</script>
--------------
调用
<h1>{{msg | filter_name(param1)}}</h1>
msg 将会是filter_name 过滤器的第一个参数

```

### 1.8.3 定义私有过滤器

```html
var vm = new Vue({
	el:,
	data:{},
	methods:{},
    filters:{
        filter_name:function(msg) {

        }
    }
})
```

## 1.9 自定义指令

### 1.9.1 概述

除了核心功能默认内置的指令 (`v-model` 和 `v-show`)，Vue 也允许注册自定义指令。注意，在 Vue2.0 中，代码复用和抽象的主要形式是组件。然而，有的情况下，你仍然需要对普通 DOM 元素进行底层操作，这时候就会用到自定义指令 

### 1.9.2 定义全局指令示例

```javascript
// 第一个参数是指令名，定义指令的时候指令名可以不加v- 前缀，但是调用的时候需要加上，例如 v-focus
// 第二个参数是一个对象，这个对象提供了几个钩子函数，可以在特定的阶段执行一些操作
Vue.directive('focus',{
    inserted:function(el){
        // el 是绑定了此指令的dom元素
        el.focus();
    }
})
```

### 1.9.3 为指令提供的几个钩子函数介绍

- `bind`：只调用一次，指令第一次绑定到元素时调用。在这里可以进行一次性的初始化设置。 
- `inserted`：被绑定元素插入父节点时调用 (仅保证父节点存在，但不一定已被插入文档中)。
- `update`：所在组件的 VNode 更新时调用，**但是可能发生在其子 VNode 更新之前**。指令的值可能发生了改变，也可能没有。但是你可以通过比较更新前后的值来忽略不必要的模板更新 
-  `componentUpdated`：指令所在组件的 VNode **及其子 VNode** 全部更新后调用。
- `unbind`：只调用一次，指令与元素解绑时调用。

### 1.9.4 指令钩子函数的参数

- `el`：指令所绑定的元素，可以用来直接操作 DOM 。
- `binding`：一个对象，包含以下属性： 
  - `name`：指令名，不包括 `v-` 前缀。
  - `value`：指令的绑定值，例如：`v-my-directive="1 + 1"` 中，绑定值为 `2`。
  - `oldValue`：指令绑定的前一个值，仅在 `update` 和 `componentUpdated` 钩子中可用。无论值是否改变都可用。
  - `expression`：字符串形式的指令表达式。例如 `v-my-directive="1 + 1"`中，表达式为 `"1 + 1"`。
  - `arg`：传给指令的参数，可选。例如 `v-my-directive:foo` 中，参数为 `"foo"`。
  - `modifiers`：一个包含修饰符的对象。例如：`v-my-directive.foo.bar` 中，修饰符对象为 `{ foo: true, bar: true }`。`name`：指令名，不包括 `v-` 前缀。
  - `value`：指令的绑定值，例如：`v-my-directive="1 + 1"` 中，绑定值为 `2`。
  - `oldValue`：指令绑定的前一个值，仅在 `update` 和 `componentUpdated` 钩子中可用。无论值是否改变都可用。
  - `expression`：字符串形式的指令表达式。例如 `v-my-directive="1 + 1"`中，表达式为 `"1 + 1"`。
  - `arg`：传给指令的参数，可选。例如 `v-my-directive:foo` 中，参数为 `"foo"`。
  - `modifiers`：一个包含修饰符的对象。例如：`v-my-directive.foo.bar` 中，修饰符对象为 `{ foo: true, bar: true }`。
- `vnode`：Vue 编译生成的虚拟节点。
- `oldVnode`：上一个虚拟节点，仅在 `update` 和 `componentUpdated` 钩子中可用。

### 1.9.5 定义局部的指令

```javascript
directives:{
    focus(el){
        el.focus();
    }
}
```

## 2.0 动画

动画能够提高用户体验，能够更好的帮助用户了解一些事情

###2.0.1 动画过度示意图

![](C:\Users\ASUS\Desktop\My File\个人笔记\vue\过度类名和执行过程.png)

### 2.0.2 使用过度类名完成动画

```html
<style type="text/css">
	    /*定义两组样式，和transition 标签配合使用*/
	    .v-enter,
	    .v-leave-to {
	    	/*v-enter 动画未开始时，v-leave-to 动画已经结束*/
	    	opacity: 0;
	    	transform: translateX(100px);
	    }

	    .v-enter-active,
	    .v-leave-active {
	    	/*v-enter-active 入场时触发的动画，v-leave-active 离场时触发的动画*/

	    	transition: all 0.8s ease;
    }
    </style>

 <div id="app">
        <input type="button" value="开始动画" @click="flag=!flag">
        <!-- transition 是vue提供的一个标签,可以将要做动画的元素放入该标签内进行管理 -->
        <transition>
            <h1 v-if="flag">hello world</h1>
        </transition>
 </div>
```

> 注意：过度类名不一定非要以v- 前缀定义，也可进行自定义，要想使用自定义前缀的类名，transition 标签需要将name属性设置为你自定义的前缀名

### 2.0.3 使用钩子函数完成半场动画效果

```html
<!-- 
            在transition上定义钩子函数，完成动画
            @before-enter 动画未开始时
            @enter 动画执行中
            @after-enter 动画结束
         -->
        <transition
            @before-enter="beforeEnter" 
            @enter="enter"
            @after-enter="afterEnter"
        >
            <h1 class="ball" v-show="flag"></h1>
        </transition>

	var vm = new Vue({
    el: '#app',
    data: {
        flag: false
    },
    methods: {
        // el 是一个原生的js DOM 对象 此处的el代表要做动画的元素的dom
        beforeEnter(el) {
            el.style.transform="translate(0,0)";
        },
        enter(el,done) {
            // 这句代码没有实际意义，但是如果不写的话，过度效果不会生效
            el.offsetWidth

            el.style.transform="translate(100px,400px)";
            el.style.transition="all 1s ease";
            // done 代表afterEnter 函数，调用这个函数后，动画才会立即完成
            done();
        },
        afterEnter(el) {
            this.flag=!this.flag;
            // this.beforeEnter();
        }
    }
})
```

### 2.0.4 定义过渡类样式，使元素在离场时也有动画

```css
 /*定义元素被移除时的动画 以下两组样式配合使用才会生效*/
        .v-move {
            transition: all 0.8s ease;   
        }
        .v-leave-active {
            position: absolute;
        }
```

### 2.0.5 transition-group

```html
 <!-- 如果需要进行动画的是一个列表，那么就需要使用此标签进行包裹 -->
            <!-- 给transition-group元素添加appear 属性，可以在最初页面渲染时也呈现动画效果 -->
            <!-- tag 属性，指定了transition-group将要渲染成什么标签，不指定的话，默认渲染成span -->
            <transition-group appear tag="ul">
                <li v-for="(item,i) in list" :key="item.id" @click="del(i)">
                    {{item.id}}------------{{item.name}}
                </li>
            </transition-group>
```

## 2.1 组件

### 2.1.1 定义组件的方式

方式1：

```js
/*
	定义一个全局组件
	组件必须要且只有一个根标签
	*/ 
	// 定义全局组件
	Vue.component("mycomponent",{
		template:"<div><h1>hello world</h1></div>"
	});
```

方式2：

```html
<template id="tmpl">
		<div>
			<h1>this is component</h1>
		</div>
</template>

Vue.component("mycomponent",{
	template:"#tmpl";
});
```

### 2.1.2 组件中的data

```html
<template id="hello">
		<div>
			<h1>登陆组件,{{msg}}</h1>
		</div>
</template>

// 这里的data必须是一个函数，而且还要返回一个对象
	Vue.component("login",{
		template:"#hello",
		data:function(){
			return {
				msg:"hello world"
			}
		}
	}); 
```

### 2.1.3 组件切换

方式1：通过标识符控制切换

```html
<a href="" @click.prevent="flag=true">登陆</a>
<a href="" @click.prevent="flag=false">注册</a>
<login v-if="flag"></login>
<register v-else="flag"></register>
```

方式2：通过vue提供的component标签进行切换

```html
<a href="" @click.prevent="componentName='login'">登陆</a>
<a href="" @click.prevent="componentName='register'">注册</a>
<component :is="componentName"></component>
```

## 2.2 路由vue-router

### 2.2.1 概述

- 定义：在单页面应用，大部分页面结构不变，只改变部分内容的使用
- 优点：用户体验好，不需要每次都从服务器全部获取，快速展现给用户
- 缺点：
  - 使用浏览器的前进，后退键的时候会重新发送请求，没有合理地利用缓存
  - 单页面无法记住之前滚动的位置，无法在前进，后退的时候记住滚动的位置
- 说白了也就是在单个页面中实现组件之间的切换

### 2.2.2 基本使用

js

```js
// 创建一个路由对象，当引入了vue-router.js 文件后，在window 全局对象中就多了一个VueRouter对象
	var routerObj = new VueRouter({
		// 配置对象中的routes表示路由匹配规则
		routes:[
			// 每个路由规则都是一个对象，该对象有两个属性
			// path 表示监听那个路由的地址
			// component 如果匹配，则显示那个组件
            // login 必须是一个字面量形式的组件，否则不会生效
			{path:'/login',component:login},
			{path:'/register',component:register}
		]
	});
// 在vue 实例上挂载路由
var vm = new Vue({
    router:routerObj
})
```

html

```html
<!-- 此标签由vue-router 提供，默认会渲染一个a标签，可以通过tag 属性指定 -->
<router-link to="/login" tag="span">登陆</router-link>
<router-link to="/register">注册</router-link>
<!-- 匹配路由规则后，组件将在此处显示 -->
<router-view></router-view>
```

### 2.2.3 选中路由高亮显示

路由被选中后，默认会加上此类名: "router-link-active"，可以通过路由对象的`linkActiveClass ` 属性改变类样式的引用

### 2.2.4 路由传参

当我们创建了VueRouter对象后，并挂载Vue实例上后，Vue 对象上便多了一个_router 属性，其内部有一个$route 属性，可以直接通过 this 获取 ,并获取一些参数信息

获取示例

```js
var routerObj = new VueRouter({
		
		routes:[
		
			// 配置默认的匹配规则
			{path:'/',redirect:'/login'},
			{path:'/login?name=admin',component:login},
			{path:'/register',component:register}
		]
	});

	//创建一个vue实例
	var vm = new Vue({
		el:'#app', 
		data:{ 
			name:''
		},
		methods:{
            getUrlParams(){
                // 当查询字符串以 ?name=admin 形式传参时可以使用此方式获取
                this.name = $route.query.name
                // 当查询字符串以 /:name 形式赋值时，可以使用如下方式获取
                // 此种传参方式相当于rest风格，在输入地址时，可以直接按照rest风格输入，例如
                // login/:name/:pwd ===> login/admin/123
                this.name = $route.params.name
            }	
		},
		router:routerObj
	})
```

### 2.2.5 路由嵌套

html

```html
<div id="app">
        <router-view></router-view>
    </div>
    <template id="parent">
        <div>
            <h1>这里是一个父组件</h1>
            <router-link to="/parent/login">登陆</router-link>
            <router-link to="/parent/register">注册</router-link>
            <router-view></router-view>
        </div>
    </template>
    <template id="login">
        <h1>登陆组件</h1>
    </template>
    <template id="register">
        <h1>注册组件</h1>
    </template>
```

js

```js
var parent = {
    template: '#parent'
}
var login = {
    template: '#login'
}

var register = {
    template: '#register'
}

var routerObj = new VueRouter({

    routes: [

        // 配置默认的匹配规则
        // 通过此种方式传参：vue 上的router 实例会自动根据匹配规则帮我们解析参数
        // #/login/admin/123 即name=admin&pwd=123
        { path: '/', redirect: '/parent' },
        {
            path: '/parent',
            component: parent,
            children: [
                { path: 'login', component: login },
                { path: 'register', component: register }
            ]
        }
    ]
});

... 省略注册Vue 实例
```

### 2.2.6 命名视图

html

```html
<div id="app">
        <router-view></router-view>
        <div class="contianer">
            <router-view name="left"></router-view>
            <router-view name="right"></router-view>
        </div>
    </div>
```

js

```js
// 路由配置
var routerObj = new VueRouter({
    routes: [{
        path: '/',
        components: {
            default: main,
            left: left,
            right: right
        }
    }]
});
```

## 2.3 watch 属性 和 computed 属性

### 2.3.1 watch 监视属性

将在data上的数据定义完成后，并且在watch 中定义，该属性将会被监视，当该属性的值发生改变时，将会触发对应的函数

例：

html

```html
 <div id="app">
        <input type="text" v-model="firstName"> +
        <input type="text" v-model="lastName"> =
        <input type="text" v-model="fullName">
    </div>
```

js

```js
var vm = new Vue({
    el: '#app',
    data: {
        firstName:'',
        lastName:'',
        fullName:''
    },
    methods: {
    },
    // 这个属性可以监听到data数据的改变，并触发相应的事件
    watch:{
        // 当data 上的firstName 属性的值发生变化时，将会触发此函数
        firstName(newVal,oldVal){
            this.fullName = newVal+'-'+this.lastName;
        },
        lastName(newVal,oldVal){
            this.fullName = this.firstName+'-'+oldVal;
        }
    }
})
```

###2.3.2 computed 计算属性

在computed 中的属性为计算属性
计算属性本身是一个函数，但不会被当成一个函数调用，
它就相当于java 中的get 和 set 方法
当data 中的数据发生改变时，计算属性将会重新计算结果
区别：computed优先从缓存中加载，methods每次重新渲染都会执行，相对methos,前者的性能更高
注意：计算属性中的属性一定要有返回值

使用示例：

html:

```html
<div id="app">
		<h1>{{getMsg1}}</h1>
	</div>
```

js

```js
var vm = new Vue({
		el:'#app',
		data:{
			msg:'hello'
		},
		computed:{
			//方式1
			//这里提供了一个计算属性，相当于一个getter
			getMsg(){
				return this.msg+' world' 
			},
			//方式2
			getMsg1:{
				//getter
				get(){
					return this.msg+' world'		
				},
				//setter
				set(newMsg){
					this.msg=newMsg
				}
			}

		}
	})
	vm.getMsg1="haha"
```

### 2.3.3 watch 和 computed 的异同

- 相同点
  - 内部的属性的值都是一个函数
  - 都不可以直接作为函数被调用
  - 当data 中的数据发生改变时会触发属性对应的函数
- 不同点
  - watch 不需要返回值
  - computed 需要有返回值
  - computed 优先从缓存中读取数据

## 2.4 Vuex 的介绍和使用

### 2.4.1 vuex的概念及作用

Vuex 是一个专为 Vue.js 应用程序开发的**状态管理模式**。它采用集中式存储管理应用的所有组件的状态，并以相应的规则保证状态以一种可预测的方式发生变化。Vuex 也集成到 Vue 的官方调试工具 [devtools extension](https://github.com/vuejs/vue-devtools)，提供了诸如零配置的 time-travel 调试、状态快照导入导出等高级调试功能。

说白了就是用来管理组件之间的共享数据，将所有组件需要用到的共享数据交由vuex管理，从而避免了繁琐的父子组件传值，传方法等操作

详细信息请参照：https://vuex.vuejs.org/zh/

### 2.4.2 使用vuex

1.安装

```powershell
>npm install vuex --save
```

2.使用

```js
import Vue from 'vue'
import Vuex from 'vuex'
Vue.use(Vuex)

// 创建一个store
const store = new Vuex.Store({
    // 这里存放的是共享数据
    // 可以通过this.$store.state.count 进行访问
  state: {
    count: 0
  },
    // 这里存放的是对公共数据的操作的方法
    // 不建议在组件内部直接操作共享数据，容易造成数据的紊乱
    // 统一在此处操作便于管理和维护
    // 调用:this.$store.commit("increment")
  mutations: {
    increment (state) {
      state.count++
    }
  }
})

var vm = new Vue({
  el: '#app',
  // 把 store 对象提供给 “store” 选项，这可以把 store 的实例注入所有的子组件
  store
})
```

