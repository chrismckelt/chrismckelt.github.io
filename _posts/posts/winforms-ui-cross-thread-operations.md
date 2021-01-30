---
layout: post
category: posts
title: "Winforms UI cross thread operations"
date: "2009-02-27"
categories: 
  - "net"
  - "code"
---

ThreadPool.QueueUserWorkItem(new WaitCallback(LoadUsers));

private void LoadUsers(Object stateInfo)

    var site = new SPSite(testHarnessSettings.Url);

     var web = site.OpenWeb();

      foreach(SPUser user in web.AllUsers)

      {

       if (usersListBox.InvokeRequired)

       {

             usersListBox.Invoke(new MethodInvoker(delegate { usersListBox.Items.Add((user.LoginName)); }));

       }

 

http://weblogs.asp.net/justin\_rogers/articles/126345.aspx
