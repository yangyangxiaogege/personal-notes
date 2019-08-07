# webpack 笔记

## 1.0 webpack 结合 vue中的路由进行使用

### 1.0.1 基本使用

1.安装路由

```powershell
>npm i vue-router
```

2.在入口文件中导入路由模块 

```js
import Vue from 'vue'
import VueRouter from 'vue-router'
// 安装路由
Vue.use(VueRouter)
// 导入登陆组件
import login from './login.vue'
// 创建路由对象
var router = new VueRouter({
    routes:[
        {path:'/login',component:login}
    ]
})
// 创建vm实例，挂载路由
var vm = new Vue({
    el:'#app',
    data:{},
    methods:{},
    router
})
```

### 1.0.2 嵌套子组件

1.创建父组件

```vue
<template>
	<div>
		<h3>there is account</h3>
		<router-link to="/account/login">login</router-link>
		<router-link to="/account/register">register</router-link>
		<router-view></router-view>
	</div>
</template>
<script></script>
<style></style>
```

2.创建子组件

```vue
<template>
	<div>
		<h4>login</h4>
	</div>
</template>
<script></script>
<style></style>
```

```vue
<template>
	<div>
		<h4>register</h4>
	</div>
</template>
<script></script>
<style></style>
```

3.修改VueRouter对象

```js
var router = new VueRouter({
	routes:[
		{
			path:'/account',
			component:account,
			children:[
				{path:'login',component:login},
				{path:'register',component:register}
			]
		}
	]
})
```

## 1.2 mint-ui 的介绍和使用

### 1.2.1 概述

基于 Vue.js 的移动端组件库

Mint UI 包含丰富的 CSS 和 JS 组件，能够满足日常的移动端开发需要。通过它，可以快速构建出风格统一的页面，提升开发效率

真正意义上的按需加载组件。可以只加载声明过的组件及其样式文件，无需再纠结文件体积过大。

考虑到移动端的性能门槛，Mint UI 采用 CSS3 处理各种动效，避免浏览器进行不必要的重绘和重排，从而使用户获得流畅顺滑的体验。

依托 Vue.js 高效的组件化方案，Mint UI 做到了轻量化。即使全部引入，压缩后的文件体积也仅有 ~30kb (JS + CSS) gzip。

### 1.2.2 使用mint-ui

1.安装mint-ui

```powershell
>npm i mint-ui -D
```

2.在main.js中使用mint-ui——方式1：直接导入

```js
import Vue from 'vue'
// 导入mint-ui 组件
import MintUi from 'mint-ui'
// 将mint-ui 中的组件注册到Vue中
Vue.use(MintUi)
// 导入mint-ui 组件所需的样式表
import 'mint-ui/lib/style.css'

// 经过上边几个步骤，就可以使用mint-ui 中的组件了
// 注意：由于我们导入的vue模块属于runtime-only模式，并不是完整的，所以使用时不能直接在页面中通过组件名引用，需要借助render渲染一个组件，然后在render渲染的组件内部引用
```

3.在main.js中使用mint-ui——方式1：按需导入

```js
// 1.安装babel插件
// npm i babel-plugin-component -D
// 2.在babelrc文件中的plugins节点下新增配置
/*
"plugins": [["component", [
    {
      "libraryName": "mint-ui",
      "style": true
    }
  ]]]
*/
import Vue from 'vue'
// 导入mint-ui 组件
import {Button,...} from 'mint-ui'
// 注册全局组件
Vue.component(Button.name,Button)
```

> 注意：此方式尚存bug，待解决

## 1.3 MUI 的介绍和使用

### 1.3.1 概述

MUI不同于mint-ui,MUI只是开发出来的一套比较好用的代码片段，类似于bootstrap,而mint-ui是基于Vue.js封装的一套组件，MUI在任何项目都可以使用，mint-ui 只能在vue项目中使用

### 1.3.2 使用

mui并没有存放在npm仓库中，所以不能使用npm 进行安装，需要到github中下载mui,并将其解压，然后将需要的文件拷贝到项目中去

mui下载地址：https://github.com/dcloudio/mui/

我们通常会将mui解压后的dist目录拷贝到项目中，并改名为mui，例如下图：

![](C:\Users\ASUS\Desktop\My File\个人笔记\vue\mui目录.PNG)

