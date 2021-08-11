# Gemini
Gemini 为 Web 开源工作提供选项框架支持.

## 功能

- 通过 Gemini 可以轻松的完成来自于 Configuration 的配置注册.
- 通过 Gemini 可以轻松的创建携带 options 实体的 builder 类.  

<br/>  

## 使用方法

### 1. 引入 `NMS.Gemini` 库.

### 2.使用 OPTIONS 

 - #### 定义 OPTIONS

```c#
//定义配置文件的根节点
[GeminiOptions("MyConfigRoot")]
public class ParentsOptions
{
    public string Url { get; set; }
    public string Description { get; set; }
}

//指定父节点 并设置并列子节点
[GeminiOptions(typeof(ParentsOptions), "InfoNode1", "InfoTest2")]
public class SubOptions
{
    public int Age { get; set; }
    public string Name { get; set; }
}
```  

 - #### 对应的配置节点为,也可以使用 AgileConfig 配置中心提供选项来源  

```json
{
  "MyConfigRoot": 
  {
  
    "Url": "url",
    "Description": "description",
    
    "InfoNode1": 
    {
      "Age": 10,
      "Name": "Info1"
    },
    
    "InfoTest2": 
    {
      "Age": 20,
      "Name": "Info2"
    }
  },
}
```

 - #### 使用 Gemini 框架进行注册

```C#
//1. 在 Program 中添加如下代码:
IHostBuilder.ConfigureAppConfiguration((context, config) =>
{
 config.AddGeminiConfig();
})

//2. 在 Startup 中添加如下代码
services.AddGeminiOptions();

//就可以使用了, 比如在DI初始化中使用
public Controller(IOptions<ParentsOptions> options){ }
public Controller(IOptionsMonitor<ParentsOptions> options){ }
public Controller(IOptionsSnapshot<ParentsOptions> options)
{ 
   _subOptions1 = options.Get("MyConfigRoot:InfoTest1");
}

//您可以配合 https://github.com/dotnetcore/AgileConfig 配置中心完成选项实体的配置
```   

- #### 实现 IGeminiBuilder   

```C#  
public class TestBuilder : IGeminiBuilder
{
   //你需要重载这个方法
   public override void Configuration()
   {
       var sqlOption = GetOptions<SqlOption>();
       //do sth
       //sql.Init(sqlOption.ConnectionString);
       //_service.AddScope(.....)
   }
}
```  

- #### 使用 GeminiBuilder

```C# 
//此方法可以包装一层 作为 services 的扩展方法对外提供. 会自动创建 Builder 并调用 builder.Configuration 方法.
services.AddGeminiBuilder<TestBuilder>();
services.AddGeminiBuilder<TestBuilder>(builder=>builder.OtherMethod());


```
<br/>  

## 计划

Gemini 提供了简单的选项注册和构建功能, 可以快速开发类库以及 WEB 项目配置.
后面将根据需求进一步修改.
