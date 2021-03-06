---
layout: post
category: posts
title: "Castle Windsor – WCF Endpoint Configuration"
date: "2010-02-01"
categories: 
  - "net"
  - "code"
---

          const int maxSize = 52428800;

            var binding = new BasicHttpBinding();
            binding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;
            binding.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.None;
            binding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;
            binding.Security.Message.AlgorithmSuite = SecurityAlgorithmSuite.Default;
            binding.MaxReceivedMessageSize = 1000000;
            binding.CloseTimeout = new TimeSpan(0, 1, 0);
            binding.OpenTimeout = new TimeSpan(0,1,0);
            binding.ReceiveTimeout = new TimeSpan(0,10,0);
            binding.SendTimeout = new TimeSpan(0,1,0);
            binding.AllowCookies = false;
            binding.BypassProxyOnLocal = false;
            binding.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
            binding.MaxBufferSize = maxSize;
            binding.MaxBufferPoolSize = maxSize;
            binding.MaxReceivedMessageSize = maxSize;
            binding.MessageEncoding = WSMessageEncoding.Mtom;
            binding.TextEncoding = Encoding.UTF8;
            binding.TransferMode = TransferMode.Buffered;
            binding.UseDefaultWebProxy = true;

            binding.ReaderQuotas.MaxDepth = 32;
            binding.ReaderQuotas.MaxStringContentLength = maxSize;
            binding.ReaderQuotas.MaxArrayLength = maxSize;
            binding.ReaderQuotas.MaxBytesPerRead = maxSize;
            binding.ReaderQuotas.MaxNameTableCharCount = maxSize;

            container = new IocContainer(LifestyleType.Transient);
            container.AddFacility<WcfFacility>().Register(
                Component
                    .For<ISharePointFacadeService>()
                    .Named("DmsGateway")
                    .ActAs(
                        new DefaultClientModel()
                        {
                            Endpoint = WcfEndpoint
                                        .BoundTo(binding)
                                        .At("http://localhost/SharepointFacade/DMSService.svc/mex")
                        }));

.csharpcode, .csharpcode pre<br /> {<br /> font-size: small;<br /> color: black;<br /> font-family: consolas, "Courier New", courier, monospace;<br /> background-color: #ffffff;<br /> /\*white-space: pre;\*/<br /> }<br /> .csharpcode pre { margin: 0em; }<br /> .csharpcode .rem { color: #008000; }<br /> .csharpcode .kwrd { color: #0000ff; }<br /> .csharpcode .str { color: #006080; }<br /> .csharpcode .op { color: #0000c0; }<br /> .csharpcode .preproc { color: #cc6633; }<br /> .csharpcode .asp { background-color: #ffff00; }<br /> .csharpcode .html { color: #800000; }<br /> .csharpcode .attr { color: #ff0000; }<br /> .csharpcode .alt<br /> {<br /> background-color: #f4f4f4;<br /> width: 100%;<br /> margin: 0em;<br /> }<br /> .csharpcode .lnum { color: #606060; }
