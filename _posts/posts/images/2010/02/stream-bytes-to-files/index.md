---
title: "Stream bytes to files"
date: "2010-02-10"
categories: 
  - "net"
  - "code"
---

                using (var stream =
                    Assembly.GetAssembly(typeof(StubPolicy)).GetManifestResourceStream(
                        "Documents.TestHelpers.Files.test.msg"))
                {
                    const int bufferLength = 256;
                    var buffer = new Byte\[bufferLength\];
                    if (stream != null)
                    {
                        int bytesRead = stream.Read(buffer, 0, bufferLength);

                        using (var fs = new FileStream(filename, FileMode.CreateNew, FileAccess.Write))
                        {
                            // Write out the input stream
                            while (bytesRead > 0)
                            {
                                fs.Write(buffer, 0, bytesRead);
                                bytesRead = stream.Read(buffer, 0, bufferLength);
                            }
                            fs.Close();
                        }
                        stream.Close();
                    }
                }

.csharpcode, .csharpcode pre<br /> {<br /> font-size: small;<br /> color: black;<br /> font-family: consolas, "Courier New", courier, monospace;<br /> background-color: #ffffff;<br /> /\*white-space: pre;\*/<br /> }<br /> .csharpcode pre { margin: 0em; }<br /> .csharpcode .rem { color: #008000; }<br /> .csharpcode .kwrd { color: #0000ff; }<br /> .csharpcode .str { color: #006080; }<br /> .csharpcode .op { color: #0000c0; }<br /> .csharpcode .preproc { color: #cc6633; }<br /> .csharpcode .asp { background-color: #ffff00; }<br /> .csharpcode .html { color: #800000; }<br /> .csharpcode .attr { color: #ff0000; }<br /> .csharpcode .alt<br /> {<br /> background-color: #f4f4f4;<br /> width: 100%;<br /> margin: 0em;<br /> }<br /> .csharpcode .lnum { color: #606060; }
