---
title: "Map .html extension to the .Net razor view engine"
date: "2015-09-10"
categories: 
  - "net"
---

1\. Add the _buildProvider_ config for razor inside the compilation element

[![image](images/image_thumb2.png "image")](/wp-content/uploads/2015/09/image2.png)

2\. Application start –> Registers the html extension with razor

[![image](images/image_thumb1.png "image")](/wp-content/uploads/2015/09/image1.png)

3\. Create a Start.cshtml page and ingest the index.html page

[![image](images/image_thumb3.png "image")](/wp-content/uploads/2015/09/image3.png)

<compilation debug\="true" targetFramework\="4.6" \> <buildProviders\> <add extension\=".html" type\="System.Web.WebPages.Razor.RazorBuildProvider"/> </buildProviders\> </compilation\> System.Web.Razor.RazorCodeLanguage.Languages.Add("html", new CSharpRazorCodeLanguage()); WebPageHttpHandler.RegisterExtension("html");

@using Fasti.WebClient @{ Layout \= null; @RenderPage("~/index.html") } <!-- Version + @System.Reflection.Assembly.GetAssembly(typeof (Startup)).GetName().Version.ToString(); \-->
