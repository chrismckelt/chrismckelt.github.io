---
layout: post
category: posts
title: "Web API Test to ensure public routes do not change"
date: "2014-05-19"
tags: dotnet
---

```
[TestFixture] 
public class RouteTestFixture {

  [Test]
  public void Routes _should _not _change() {
    const string good = @ "all existing routes go here (hint run the test first then copy output to here)";
    var goodRoutes = good.Split(Convert.ToChar(";"));
    var notFound = new List < string > ();
    var ass = Assembly.GetAssembly(typeof (EventStatisticsController));
    var sb = new StringBuilder();
    foreach(var type in ass.GetTypes()) {
      try {
        var members = type.GetMembers();
        for (int i = 0; i < members.Length; i++) {
          if (members[i].IsDefined(typeof (RouteAttribute), false)) {
            Object[] atts = members[i].GetCustomAttributes(typeof (RouteAttribute), false);
            for (int j = 0; j < atts.Length; j++) {
              var routeAttribute = (RouteAttribute) atts[j];
              string route = routeAttribute.Template + @ "/" + routeAttribute.Name;
              Console.WriteLine(route);
              sb.Append(route + ";");
              if (!goodRoutes.Contains(route)) {
                notFound.Add(route);
              }
            }
          }
        }
      } catch (Exception e) {
        Console.WriteLine(@ "An exception occurred: {0}", e.Message);
      }
    }
    if (notFound.Any()) {
      Console.WriteLine(@ "-- MISSING ROUTES --");
      foreach(var nf in notFound) {
        Console.WriteLine(nf);
      }
      Assert.Fail("Missing Routes: " + notFound.Count);
    } else {
      Assert.True(true, "All routes matched");
    }
    Console.WriteLine(@ "------------------------------------------------");
    Console.WriteLine(@ "Match the following string for future tests");
    Console.WriteLine(sb.ToString());
  }
}
```