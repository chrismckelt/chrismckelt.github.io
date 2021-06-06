---
layout: post
category: posts
title: "Castle Windsor – WCF Endpoint Configuration"
date: "2010-02-01"
tags: dotnet
---

>           const int maxSize = 52428800;
> 
>             var binding = new BasicHttpBinding();
>             binding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
>             binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;
>             binding.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.None;
>             binding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;
>             binding.Security.Message.AlgorithmSuite = SecurityAlgorithmSuite.Default;
>             binding.MaxReceivedMessageSize = 1000000;
>             binding.CloseTimeout = new TimeSpan(0, 1, 0);
>             binding.OpenTimeout = new TimeSpan(0,1,0);
>             binding.ReceiveTimeout = new TimeSpan(0,10,0);
>             binding.SendTimeout = new TimeSpan(0,1,0);
>             binding.AllowCookies = false;
>             binding.BypassProxyOnLocal = false;
>             binding.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
>             binding.MaxBufferSize = maxSize;
>             binding.MaxBufferPoolSize = maxSize;
>             binding.MaxReceivedMessageSize = maxSize;
>             binding.MessageEncoding = WSMessageEncoding.Mtom;
>             binding.TextEncoding = Encoding.UTF8;
>             binding.TransferMode = TransferMode.Buffered;
>             binding.UseDefaultWebProxy = true;
> 
>             binding.ReaderQuotas.MaxDepth = 32;
>             binding.ReaderQuotas.MaxStringContentLength = maxSize;
>             binding.ReaderQuotas.MaxArrayLength = maxSize;
>             binding.ReaderQuotas.MaxBytesPerRead = maxSize;
>             binding.ReaderQuotas.MaxNameTableCharCount = maxSize;
> 
>             container = new IocContainer(LifestyleType.Transient);
>             container.AddFacility<WcfFacility>().Register(
>                 Component
>                     .For<ISharePointFacadeService>()
>                     .Named("DmsGateway")
>                     .ActAs(
>                         new DefaultClientModel()
>                         {
>                             Endpoint = WcfEndpoint
>                                         .BoundTo(binding)
>                                         .At("http://localhost/SharepointFacade/DMSService.svc/mex")
>                         }));

