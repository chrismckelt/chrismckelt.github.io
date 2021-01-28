---
title: "TIPS"
date: "2009-04-19"
categories: 
  - "net"
  - "notes"
---

@if (HttpContext.Current.IsDebuggingEnabled)
{      <span\>Path</span\> @this.VirtualPath
}

 

# How to add a css class to a Html.TextBox

<% using(Html.BeginForm()) { %>
    <div style\="width:500px; padding-top:15px;"\>
        <span class\="loginleft"\>
            <label for\="username" class\="largetext"\>Email:</label\>&nbsp;&nbsp;
        </span\>
        <span class\="loginright"\>
            <%\= Html.TextBox("email", string.Empty, new { @class = "textbox" }) %>  
        </span\>
         <br />
         <br />
        <span class\="loginleft"\>
            <label for\="password" class\="largetext"\>Password:</label\>&nbsp;&nbsp;
        </span\>
        <span class\="loginright"\>
            <%\= Html.Password("password", string.Empty, new { @class = "textbox" }) %>  
        </span\>
        <br /> 
        <br />
        <span class\="loginleft"\>
        &nbsp;
        </span\>
        <span class\="loginright"\>
            <%\= Html.SubmitButton("loginSubmit", "Login", new { @class = "button" }) %>
        </span\>
    </div\>
    <% } %>

 

 

## How to find out which process you want to connect to

 

cd C:\\Windows\\System32\\inetsrv

appcmd list wp

pause

 

[![clip_image002](images/image.axd?picture=clip_image002_thumb.jpg "clip_image002")](http://www.mckelt.com/blog/image.axd?picture=clip_image002.jpg)
