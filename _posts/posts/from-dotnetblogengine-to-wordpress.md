---
layout: post
category: posts
title: "From DotNetBlogEngine to WordPress"
date: "2015-06-01"
categories: 
  - "wordpress"
---

So after losing 6 months worth of posts I have now converted this blog to use WordPress.

To transfers the posts I wrote a simple [linqpad](https://gist.github.com/chrismckelt/31a3c5ea9114f28f3170) script that uses [WordPressSharp](WordPressSharp)

After trying Syntax highlighter I instead choose this [code formatter.](https://code.google.com/p/codeformatterpluginforwindowslivewriter/downloads/detail?name=CodeFormatterPluginSetup2.5.1.msi&can=2&q=)

// need nuget WordPressSharp  internal class FileStore
{ public IList<Post\> Posts { get; private set; } public void ReadFiles()
   { const string folderPath \= @"C:\\temp\\posts";
       Posts \= new List<Post\>();
       var files \= Directory.GetFiles(folderPath);

       files.ToList().ForEach(ProcessFile);
   } private void ProcessFile(string filepath)
   { //Load xml  XDocument xdoc \= XDocument.Load(filepath); //Run query  var posts \= from lv1 in xdoc.Descendants("post")
                  select new Post()
                  {
                      Title \= lv1.Element("title").Value,
                      Content \= lv1.Element("content").Value,
                      PublishDateTime \= Convert.ToDateTime(lv1.Element("pubDate").Value),
                      CustomFields \= GetDesc(new KeyValuePair<string, string\>("Description", lv1.Element("description").Value))
                  };
      posts.ToList().ForEach(Posts.Add);
   } public CustomField\[\] GetDesc(params KeyValuePair<string,string\>\[\] fieldsToAdd)
   {
       var list \= new List<CustomField\>(); foreach (var keyValuePair in fieldsToAdd)
       {
           list.Add(new CustomField()
           {
               Id\=Guid.NewGuid().ToString(),
               Key\=keyValuePair.Key,
               Value \= keyValuePair.Value
           });
       } return list.ToArray();
   }
} private static void Main(string\[\] args)
        {
            var url \= "http://www.mckelt.com/blog/";
            var cfg \= new WordPressSiteConfig
            {
                BaseUrl \= "/blog/xmlrpc.php",

                BlogId \= 1,
                Username \= "username",
                Password \= "password" };

            var fs \= new FileStore();
            fs.ReadFiles(); foreach (var post1 in fs.Posts.OrderBy(x\=>x.PublishDateTime))
            {
                post1.Status \= "publish"; using (var client \= new WordPressClient(cfg))
                {
                    client.NewPost(post1);
                }
            }
        }
    }
