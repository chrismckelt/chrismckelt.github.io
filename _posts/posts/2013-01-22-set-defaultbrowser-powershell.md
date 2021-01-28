---
title: "Set-DefaultBrowser powershell"
date: "2013-01-22"
categories: 
  - "powershell"
---

[function](http://search.microsoft.com/default.asp?so=RECCNT&siteid=us%2Fdev&p=1&nq=NEW&qu=function&IntlSearch=&boolean=PHRASE&ig=01&i=09&i=99) [Set](http://search.microsoft.com/default.asp?so=RECCNT&siteid=us%2Fdev&p=1&nq=NEW&qu=Set&IntlSearch=&boolean=PHRASE&ig=01&i=09&i=99)\-DefaultBrowser

{

    param($defaultBrowser)

    switch ($defaultBrowser)

    {

        'IE' {

            [Set](http://search.microsoft.com/default.asp?so=RECCNT&siteid=us%2Fdev&p=1&nq=NEW&qu=Set&IntlSearch=&boolean=PHRASE&ig=01&i=09&i=99)\-ItemProperty 'HKCU:\\Software\\Microsoft\\Windows\\Shell\\Associations\\UrlAssociations\\ftp\\UserChoice' -name ProgId IE.FTP

            [Set](http://search.microsoft.com/default.asp?so=RECCNT&siteid=us%2Fdev&p=1&nq=NEW&qu=Set&IntlSearch=&boolean=PHRASE&ig=01&i=09&i=99)\-ItemProperty 'HKCU:\\Software\\Microsoft\\Windows\\Shell\\Associations\\UrlAssociations\\http\\UserChoice' -name ProgId IE.HTTP

            [Set](http://search.microsoft.com/default.asp?so=RECCNT&siteid=us%2Fdev&p=1&nq=NEW&qu=Set&IntlSearch=&boolean=PHRASE&ig=01&i=09&i=99)\-ItemProperty 'HKCU:\\Software\\Microsoft\\Windows\\Shell\\Associations\\UrlAssociations\\https\\UserChoice' -name ProgId IE.HTTPS

        }

        'FF' {

            [Set](http://search.microsoft.com/default.asp?so=RECCNT&siteid=us%2Fdev&p=1&nq=NEW&qu=Set&IntlSearch=&boolean=PHRASE&ig=01&i=09&i=99)\-ItemProperty 'HKCU:\\Software\\Microsoft\\Windows\\Shell\\Associations\\UrlAssociations\\ftp\\UserChoice' -name ProgId FirefoxURL

            [Set](http://search.microsoft.com/default.asp?so=RECCNT&siteid=us%2Fdev&p=1&nq=NEW&qu=Set&IntlSearch=&boolean=PHRASE&ig=01&i=09&i=99)\-ItemProperty 'HKCU:\\Software\\Microsoft\\Windows\\Shell\\Associations\\UrlAssociations\\http\\UserChoice' -name ProgId FirefoxURL

            [Set](http://search.microsoft.com/default.asp?so=RECCNT&siteid=us%2Fdev&p=1&nq=NEW&qu=Set&IntlSearch=&boolean=PHRASE&ig=01&i=09&i=99)\-ItemProperty 'HKCU:\\Software\\Microsoft\\Windows\\Shell\\Associations\\UrlAssociations\\https\\UserChoice' -name ProgId FirefoxURL

        }

		  'Chrome' {

            [Set](http://search.microsoft.com/default.asp?so=RECCNT&siteid=us%2Fdev&p=1&nq=NEW&qu=Set&IntlSearch=&boolean=PHRASE&ig=01&i=09&i=99)\-ItemProperty 'HKCU:\\Software\\Microsoft\\Windows\\Shell\\Associations\\UrlAssociations\\ftp\\UserChoice' -name ProgId ChromeHTML

            [Set](http://search.microsoft.com/default.asp?so=RECCNT&siteid=us%2Fdev&p=1&nq=NEW&qu=Set&IntlSearch=&boolean=PHRASE&ig=01&i=09&i=99)\-ItemProperty 'HKCU:\\Software\\Microsoft\\Windows\\Shell\\Associations\\UrlAssociations\\http\\UserChoice' -name ProgId ChromeHTML

            [Set](http://search.microsoft.com/default.asp?so=RECCNT&siteid=us%2Fdev&p=1&nq=NEW&qu=Set&IntlSearch=&boolean=PHRASE&ig=01&i=09&i=99)\-ItemProperty 'HKCU:\\Software\\Microsoft\\Windows\\Shell\\Associations\\UrlAssociations\\https\\UserChoice' -name ProgId ChromeHTML

        }

    } 

\# thanks [to](http://search.microsoft.com/default.asp?so=RECCNT&siteid=us%2Fdev&p=1&nq=NEW&qu=to&IntlSearch=&boolean=PHRASE&ig=01&i=09&i=99) http://poshcode.org/3504

<#

([Get](http://search.microsoft.com/default.asp?so=RECCNT&siteid=us%2Fdev&p=1&nq=NEW&qu=Get&IntlSearch=&boolean=PHRASE&ig=01&i=09&i=99)\-ItemProperty 'HKCU:\\Software\\Microsoft\\Windows\\Shell\\Associations\\UrlAssociations\\ftp\\UserChoice').ProgId

([Get](http://search.microsoft.com/default.asp?so=RECCNT&siteid=us%2Fdev&p=1&nq=NEW&qu=Get&IntlSearch=&boolean=PHRASE&ig=01&i=09&i=99)\-ItemProperty 'HKCU:\\Software\\Microsoft\\Windows\\Shell\\Associations\\UrlAssociations\\http\\UserChoice').ProgId

([Get](http://search.microsoft.com/default.asp?so=RECCNT&siteid=us%2Fdev&p=1&nq=NEW&qu=Get&IntlSearch=&boolean=PHRASE&ig=01&i=09&i=99)\-ItemProperty 'HKCU:\\Software\\Microsoft\\Windows\\Shell\\Associations\\UrlAssociations\\https\\UserChoice').ProgId

#>

}

\# [Set](http://search.microsoft.com/default.asp?so=RECCNT&siteid=us%2Fdev&p=1&nq=NEW&qu=Set&IntlSearch=&boolean=PHRASE&ig=01&i=09&i=99)\-DefaultBrowser ff

\# [Set](http://search.microsoft.com/default.asp?so=RECCNT&siteid=us%2Fdev&p=1&nq=NEW&qu=Set&IntlSearch=&boolean=PHRASE&ig=01&i=09&i=99)\-DefaultBrowser ie

[Set](http://search.microsoft.com/default.asp?so=RECCNT&siteid=us%2Fdev&p=1&nq=NEW&qu=Set&IntlSearch=&boolean=PHRASE&ig=01&i=09&i=99)\-DefaultBrowser Chrome
