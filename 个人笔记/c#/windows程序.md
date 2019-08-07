# windows 程序

## 1.1 初识windiws程序

- 他是一种有图形化界面的软件模式，用户体验更好
- 基本单位是窗体
- 每一个窗体都由三个文件组成
  - Form1.cs 窗体文件，存放事件处理代码，程序员主要操作这个类，在这个文件中，this指代当前窗体
  - Form1.Designer.cs 窗体设计文件，一般自动生成
  - Form1.resx 资源文件，用来配置当前窗体所用的字符串，图片等资源信息
- partial 关键字，表示部分类，就是说将一个类拆成几部分，便于维护

## 1.2 窗体的基本常用属性

- Name 窗体名称
- BackColor 背景色
- BackgroundImage 背景图片
- StarPosition 起始位置
- Text 窗体标题
- TopMost 始终在其他窗体上方
- MaximizeBox 是否允许出现最大化按钮

## 1.3 常用控件

- Label 一般用于说明

  ```markdown
  个别属性说明
  
  -自动调整大小
  AutoSize true/false
  true:不可以通过鼠标拉伸调整大小
  false:与上述相反
  -字体调整
  Font
  -字体颜色
  ForeColor
  > 该控件自带背景色，可以将其设置为透明
  ```

- TextBox 文本框

- ComboBox 下拉文本框

- Button 按钮

- PictureBox 图片框

## 1.4 事件

- 事件  ---用于捕捉用户的操作，从而做出相应的处理
- 事件类型  ---指触发事件的动作，例如click
- 事件源  ---触发事件的源头
- 事件对象

## 1.5 MessageBox 的使用

```c#
//以对话框的形式，进行信息提示
//第一个参数，提示内容
//第二个参数，提示框标题
//第三个参数，按钮类型
//第四个参数，图标类型

//返回类型 DialogResult
DialogResult result = MessageBox.Show("hello world", "温馨提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

//进行判断
if(DialogResult.OK==result){
    //执行操作
}
```

## 1.6 MDI窗口

- 多文档界面，类似于Excel
- 有父窗体和子窗体之分
- 最多只能有一个MDI父窗体
- IsMdiContainer 用于设置当前窗体是否是MDI父窗体
- 子窗口的活动范围只能是父窗体内
- 父窗口关闭，所有子窗体也随之关闭
- 子窗口通过MdiParent属性指向父窗体

## 1.7 ListView控件

- 列表视图控件，类似于windows资源管理系统右侧的控件效果

- 它有多重视图效果

  - LargeIcon 大图标
  - smallIcon 小图标
  - List 列表
  - Detail 详细信息
  - Title 平铺
- 通过View属性进行视图切换
###1.7.1 添加数据
  ```c#
  this.lvStudent.Items.Clear();//清空所有的首项，防止重复添加数据到列表中
  //需要先添加首项，然后在添加子项
  ListViewItem item=this.lvStudent.Items.Add("");//每行数据只能有一个首项
  item.SubItems.Add(xxx);
  ....
  ```

## 1.8 枚举

目的是为了防止不合理赋值，提高代码的可读性

使用：

```c#
enum Gender{
    Male,Female
}
private Gender sex;
sex=Gender.Male;
//将其转为int
int(sex);//默认第一个为0，后边的一次按照前边的进行+1，也可以在初始化的时候进行设置，
//例如：Gender{Male=1}
//将字符串转为枚举对象
string str="Male";
Gender g=(Gender)Enum.parse(typeof(Gender),str);
//ToString()方法可以查看枚举的字符串内容
```

## 1.9 绑定数据到ComboBox中

```c#
 this.comGrade.DisplayMember = "gradeName";// 显示在列表框中的值
 this.comGrade.ValueMember = "gradeId";// 实际隐藏的值
 this.comGrade.DataSource = ds.Tables[0];// 添加数据源
```



