# webpack 笔记

## 1.0 nrm 的安装和使用

### 1.0.1  概述

nrm 提供了一些常用的npm包 镜像地址，能够让我们快速的切换安装npm包时的镜像地址

### 1.0.2 什么是镜像

npm 包本身存在于外国服务器，但是由于网络原因，经常访问不到，这时候我们可以在国内，创建一个和官网一样的npm服务器，经过一段时间和国外的服务器进行同步，相当于一面镜子，这样我们安装包时就可以通过指定的镜像地址房访问国内的官网，从而快速的满足需求

### 1.0.3 安装和使用

- 运行 `npm i nrm -g` 全局安装 `nrm` 包
- 使用`nrm ls` 查看该工具下的镜像地址还有当前使用的镜像地址
- 使用`nrm use 镜像名` 切换镜像地址
- 使用npm 进行安装包的操作，下载地址为nrm 工具当前所选中的地址

> 注意：nrm 只是提供了几个镜像地址，并且能够在这几个地址中进行切换，真正进行安装包的还是npm

## 1.1 webpack 概念引入

### 1.1.1 网页中经常引用的静态资源

- js

  - .js .jsx .coffee .ts (TypeScript  类 c# 语言)
- css

  - .css .less .sass .scss
- image

  - .jpg .png .gif .bmp .svg
- font

  - .svg  .ttf  .eot .woff .woff2
- 模板文件
  - .ejs .jade .vue[在webpack 中定义组件的方式，推荐这么用]

### 1.1.2 网页中引入的静态资源多了以后引发的问题

- 网页加载速度慢，因为会引发多次的二次请求
- 要处理错综复杂的依赖关系

### 1.1.3 解决1.1.2 中阐述的问题

- 方式1
  - 使用Gulp,它是基于 task 任务的

- 方式二

  - 使用webpack,它是基于整个项目进行构建的；借助于webpack 这个前端自动化构建工具，可以完美实现资源的合并、打包、压缩、混淆等诸多功能。
###1.1.4 什么是webpack

webpack 是一个前端的项目构建工具，它是基于node.js 开发出来的前端工具

### 1.1.5 webpack 打包过程

![](C:\Users\ASUS\Desktop\My File\个人笔记\vue\webpack打包过程.png)

  ## 1.2 webpack 的两种安装方式

### 1.2.1 全局安装

运行`npm i webpack -g ` ,进行全局安装，安装完成后就可以全局使用webpack 命令

### 1.2.2 针对项目进行安装

在项目根目录中运行`npm i webpack --save-dev`,安装到项目依赖中

  ## 1.3 webpack 的使用

### 1.3.1 项目结构

![](C:\Users\ASUS\Desktop\My File\个人笔记\vue\webpack项目结构.png)

### 1.3.2 基本使用方式1：通过webpack 命令直接使用

```powershell
PS C:\Users\ASUS\Desktop\vue\webpack-01>webpack ./src/main.js ./dist/bundle.js
>使用webpack 命令进行打包工作
>./src/main.js 为入口，即要打包的文件
>./dist/bundle.js 为出口，即打包后生成的文件      
```

### 1.3.3 基本使用方式2：通过配置文件进行打包工作

1.在项目根路径创建名称为 webpack.config.js 文件

```js
// Node 中的核心模块之一，专门用于处理路径问题
const path = require('path')
// 这个配置文件起始就是一个js文件，通过 Node 中的模块系统，向外暴露了一个配置对象
module.exports = {
	// 入口，表示要使用webpack打包哪个文件
	// __dirname 总是指向被执行 js 文件的绝对路径，
	// 所以当你在 /d1/d2/myscript.js 文件中写了 __dirname， 它的值就是 /d1/d2
	// __dirname 同时也指向了main.js 文件
	entry:path.join(__dirname,'./src/main.js'),
	// 配置输出文件的位置
	output:{
		// 指定输出到哪个目录下
		path:path.join(__dirname,'./dist'),
		// 指定了输出文件的名称
		filename:'bundle.js'
	}
}
```

2.开始打包

```powershell
PS C:\Users\ASUS\Desktop\vue\webpack-01>webpack
```

3.执行过程

```markdown
1.首先，webpack 发现我们并没有直接指定项目的入口和出口，便会在项目根路径下寻找名称为webpack.config.js 的配置文件
2.当找到此文件后，便会进行解析，最后将会得到一个配置对象
3.得到配置对象后，通过对象内配置的入口和出口，进行打包操作
```

4.遗留问题

```markdown
1.每次被打包的文件发生改变时，都需要指定webpack 命令同步打包文件
2.被打包的文件在经过webpack 处理后，需要在物理磁盘中生成相应的输出文件，较为消耗磁盘
```

5.使用webpack-dev-server工具解决上述遗留问题

```markdown
1.运行 npm i webpack-dev-server -d 命令在本地(当前项目)安装该工具,该工具的使用方法和webpack 一致
2.因为 该工具是在项目中安装的，不能在控制台中直接使用该工具的命令，所以需要借助于package.json 文件进行一些配置，package.json 文件中的scripts字段可以配置一些项目在控制台中可执行的一些命令
3.在scripts字段下添加配置:"dev":'webpack-dev-server',即可已在控制台中执行dev从而触发webpack-dev-server命令
4.注意:要想该工具正常运行,必须在项目中安装webpack,就算已经进行了全局安装webpack,也需要在项目中再安装一次
5.一切就绪后,执行 npm run dev 开始进行项目打包
6.webpack-dev-server 帮我们打包后的项目直接挂载到了服务器上
7.webpack-dev-server 帮我们的打包好的文件以一种虚拟的方式托管到了我们项目的根目录中,我们并看不到实际的文件,所以我们在引用打包好的文件时应该以根目录进行访问
```

6.配置webpack-dev-server的一些常用命令——方式1：借助于package.json 文件

```json
"scripts": {
    "dev": "webpack-dev-server --open --port 3000 --contentBase src --hot"
  },
1.--open 表示当我们运行了npm run dev 后直接打开浏览器
2.--port 为被挂载当前项目的服务器指定一个端口号
3.--contentBase 指定项目启动时以哪个目录作为根路径进行访问
4.--hot 热重载 热更新 减少一些不必要的更新,只更新发生改变的代码,而且当保存文件后,浏览器不用刷新既可以看到效果
```

7.配置webpack-dev-server的一些常用命令——方式2：在webpack.config.js 文件中进行配置

```js
// 启用热更新的第二步，引入webpack 模块
const webpack = require('webpack')
devServer:{
	open:true,
	port:3000,
	contentBase:'src',
	hot:true // 启用热更新的第一步
	},
plugins:[
	// 启用热跟新第三步。创建热更新模块对象
	new webpack.HotModuleReplacementPlugin()
]
```

### 1.3.4 html-webpack-plugin工具的介绍和使用

1.html-webpack-plugin 的作用

```js
// 原本在页面中需要引用打包后存储在物理磁盘中或者内存中的bundle.js 文件，才能使用相关的第三方依赖，而使用该工具后，html 页面中既可以省去这一步骤，即不需要如下代码：
// <script type="text/javascript" src="/bundle.js"></script>
// 原因：
// 1.该工具会自动在内存中根据指定的文件生成一个存储在内存中的html页面
// 2.自动把打包好的bundle.js 追加到页面中去
```

2.html-webpack-plugin 的使用

```powershell
>在项目中安装html-webpack-plugin
>npm i html-webpack-plugin -D
```

```js
// 修改webpack.config.js 文件
// 引入html-webpack-plugin 模块
const htmlWebpackPlugin = require('html-webpack-plugin')
// 在导出对象上添加节点plugins
plugins:[
		// 创建一个在内存中生成html 页面的插件
		new htmlWebpackPlugin({
			// 指定模板页面,将来会根据该页面在内存中生成一份一模一样的页面
			template:path.join(__dirname,'./src/index.html'),
			// 指定生成的页面的名称
			filename:'index.html'
		})
	]
```

## 1.4 loader 加载器的介绍和使用

### 1.4.1 概述

使用传统方式，直接在页面中通过相应标签引入第三方依赖，但是这样会引发二次请求，所以会影响一些性能，所以我们在使用webpack 打包工具时不再使用传统方式，而是通过项目的 js 入口文件进行导入或加载，但是Webpack 本身只能处理 JavaScript 模块，如果要处理其他类型的文件，就需要使用 loader 进行转换。Loader 可以理解为是模块和资源的转换器，它本身是一个函数，接受源文件作为参数，返回转换的结果。

### 1.4.2 loader使用的示例——css文件的导入

1.main.js

```js
// ES6 语法
import './css/index.css'
```

2.在项目中安装相应的处理器

```powershell
>建议使用 nrm 工具将包下载地址切换至国内一些相关镜像地址，避免一些网络原因导致安装失败
>nrm ls
* npm ---- https://registry.npmjs.org/
  cnpm --- http://r.cnpmjs.org/
  taobao - https://registry.npm.taobao.org/
  nj ----- https://registry.nodejitsu.com/
  npmMirror  https://skimdb.npmjs.com/registry/
  edunpm - http://registry.enpmjs.org/
>nrm use cnpm
>npm i style-loader css-loader -D
```

3.webpack.config.js

```js
// 此节点用于配置所有第三方模块的加载器
	module:{
		// 第三方模块的匹配规则
		rules:[
			// 配置处理css文件的加载器
             // test：匹配文件规则，使用的是正则匹配
             // use: 使用的加载器，如果只有一个加载器，不用使用数组的形式，可以直接写加载器
             // webpack 会从后往前进行加载器的使用
             // 即：webpack 会先使用css-loader 加载器将相应的文件解析为css文件，再交由style-loader 加载			 器渲染为css样式
			{test:/\.css$/,use:['style-loader','css-loader']}
		]
	}
```

### 1.4.3 一些常用文件对应的加载器及相关配置

1.css文件

```powershell
>npm i style-loader css-loader -D
```

```js
{test:/\.css$/,use:['style-loader','css-loader']}
```

2.less文件

```powershell
>less-loader内部依赖了less，所以需要安装一下less才能正常使用，又因为只是内部依赖，并不需要再加载器匹配规则中配置less
>npm i less-loader less -D
```

```js
// 由于less文件同属于css样式，所以需要css加载器的配合使用
{test:/\.less$/,use:['style-loader','css-loader','less-loader']}
```

3.sass文件

```powershell
>node-sass 是sass-loader 的内部依赖项
>npm i sass-loader node-sass -D
```

```js
// 再新版本的sass中，推荐使用文件使用.scss为后缀名
{test:/\.scss$/,use:['style-loader','css-loader','sass-loader']}
```

4.url-loader——处理css中引入图片的问题

```powershell
>默认情况下，webpack无法处理css文件中的url地址，不管是图片还是字体，只要是跟url相关的都无法处理,所以需要使用url-loader,file-loader 是 url-loader 中的内部依赖
>npm i url-loader file-loader -D
```

```js
// 默认情况下，使用了该加载器后，这些匹配的资源文件类型将会通过Base64编码格式被引用，而不是真实的地址，文件名字也会变为一串哈希值
// 我们可以通过配置一些参数进行改变，传参的方式和url传参一致
// limit指定文件的大小，以字节为单位，当引用的文件大于或等于指定的大小时，将会以Base64编码的格式引用，否则将会以路径的方式引用
// name指定文件的名称，值[name]表示源文件名，[ext]源文件后缀名
// 使用[name].[ext]在某些情况下，例如在不同的文件夹中，存在相同的文件名，会造成一些错误，所以不建议使用
// [hash:8]-[name].[ext]，这种方式可以解决上述问题，生成的形式为一个hash值的前八位加上 - 文件名
// hash取值最大为32
{test:/\.(jpg|png|gif|bmp|jpeg)$/,use:'url-loader?limit=文件大小&name=[name].[ext]'}
```

5.url-loader——处理字体文件的问题

```js
// 如果通过路径去引入node_module中的一些文件，可以省略node_module目录，webpack 会自动去node_module这一层目录下寻找
// 由于bootstrap.css文件中以路径的形式引入了一些字体文件，所以需要进行url处理
import 'bootstrap/dist/css/bootstrap.css'
```

```powershell
>npm i url-loader file-loader -D
```

```js
{test:/\.(ttf|eot|svg|woff|woff2)$/,use:'url-loader'}
```

## 1.5 webpack中的babel配置

### 1.5.1 概述

在webpack中，默认只能处理一部分的ES6新语法，一些更高级的ES6或者ES7语法webpack是处理不了的，这时候就需要借助于第三方的loader去进行一些处理，当第三方loader将高级语法转为低级语法后，会把结果交给webpack打包进bundle.sj文件中去，能够实现此功能的loader为Babel

### 1.5.2 Babel的使用

1.装包

```powershell
>总共需要两套包
>第一套:babel的一些转换工具
>npm i @babel/core babel-loader @babel/runtime @babel/plugin-transform-runtime @babel/plugin-proposal-class-properties -D
>第二套:ES语法
>npm i babel-preset-env -D
```

2.修改webpack.config.js配置文件

```js
module:{
	rules:[
        // 配置处理js的loader
        // exclude：表示排除哪些文件
        // 注意：在配置babel-loader时，必须把node_modules目录排除掉，否则babel-loader将会把node_modules目录下的所有js文件重新编译，最后webpack将其打包到bundle.js文件中，这样的话会严重消耗CPU，就算最后打包完成了，项目也不能正常运行
        {test:/\.js$/,use:'babel-loader',exclude:/node_modules/}
	]
}
```

3.在项目根目录新建名为`.babelrc` 的Babel配置文件，此配置文件数据json格式

```json
{
    "presets":["@babel/preset-evn"],
    "plugins":[
        "@babel/plugin-transform-runtime",
        "@babel/plugin-proposal-class-properties" -- 用于处理class属性
    ]
}
```

## 1.6 webpack结合vue进行使用

###1.6.1 基本使用

1.安装vue

```powershell
>npm i vue
```

2.在main.js中导入vue模块

```js
// 此时根据node的查找规则
// 首先找到项目中的node_modules目录下的vue
// 然后找到package.json
// 根据package.json 中的main节点找到dist/vue.runtime.common.js
// 由此可见通过直接导入vue模块的形式，导入的并不是完整的vue,此时导入的vue模块只是runtime-only 模式，所以一些操作不能使用，例如直接注册组件
// 若想导入完整的vue，可以在webpack.config.js中添加resolve节点进行配置
/*
resolve:{
	 alias:{// 设置vue被导入时的引用的js路径
		'vue$':'vue/dist/vue.js'
	 }
}
*/
// 或者直接导入相对路径
/*
import Vue from '../node_modules/vue/dist/vue.js'
*/
import Vue from 'vue'
```

### 1.6.2 使用render渲染组件

1.创建组件，可以通过字面量形式进行配置，不过webpack推荐使用以 `.vue` 结尾的文件进行配置

```vue
<template>
	<div>
        <h1>这里是通过.vue注册的组件</h1>
        <p>{{msg}}</p>
    </div>
</template>
<script>
	export default {
        data(){
            return {
                msg:'today is Thursday,there is no class'
            }
        }
    }
</script>
<style scoped lang="less">
    /*.vue中的style默认只支持css语言，要想使用其他语言进行样式编写，需要通过lang属性指定
    scoped属性指定了样式的作用域范围，不加此属性的时候，在此处定义的样式属于公共样式，所有的组件都可以通用，加上此样式后，仅当前组件和当前组件的子组件可用*/
    div{
        background-color:pink;
    }
</style>
```

2.安装合适的loader,由于这种文件类型不能被webpack直接解析，所以需要安装合适的loader——vue-loader

```powershell
>vue-template-compiler是vue-loader的依赖项
>npm i vue-loader vue-template-compiler -D
```

3.在webpack.config.js中配置插件

```js
var vueLoaderPlugin = require('vue-loader/lib/plugin')
plugins:[
		new vueLoaderPlugin()
	]
```

4.组件渲染

```js
import Vue from 'vue'

// 导入组件
import xxx from './xxx.vue'

var vm = new Vue({
	el:'#app',
	data:{
	},
    // 形参createElements是一个方法，调用这个方法并传入正确的参数可渲染组件
		// 方式1：老语法
		// render:function(createElements){
		// 	return createElements(login);
		// 	// return 的结果会替换el指定的容器中的内容
		// }
		// 方式2：es6新语法
		// 如果只有一个形参，可以省略（） 如果方法内部只有一行代码：那么{}可以省略
	render: c => c(xxx)
})
```

5.页面调用

```html
<div id="app">
    <xxx></xxx>
</div>
```

### 1.6.3 render和components的区别

通过render进行渲染的组件，会替换 `el` 中指定的容器中的所有内容，通过components 注册的组件只会替换指定位置区域的内容

