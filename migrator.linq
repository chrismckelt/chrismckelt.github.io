<Query Kind="Program">
  <Output>DataGrids</Output>
</Query>

/*
 npx wordpress-export-to-markdown
 
*/
string Folder = @"C:\temp\output\";

void Main()
{
	string find = @"title:";
	string replace = @"layout: post
category: posts
title:";

	Replace(find, replace);

	find = @"http://blog.mckelt.com/https://raw.githubusercontent.com/chrismckelt/chrismckelt.github.io/master/_posts/posts/images//";
	replace = @"https://raw.githubusercontent.com/chrismckelt/chrismckelt.github.io/master/_posts/posts/images/";

	Replace(find, replace);
	
	Replace("http://mckeltblog.azurewebsites.net/https://raw.githubusercontent.com/chrismckelt/chrismckelt.github.io/master/_posts/posts/images/", "https://raw.githubusercontent.com/chrismckelt/chrismckelt.github.io/master/_posts/posts/images/");

	//Images();

	foreach (var f in Directory.GetFiles(@"C:\temp\output", "*.md")) {
		string fn = Path.GetFileName(f);
		File.Move(f, @$"C:\dev\mckelt\chrismckelt.github.io\_posts\posts\{fn}",true);
	}


}

void Replace(string find, string replace)
{
	foreach (var f in Directory.GetFiles(Folder, "*.md"))
	{
		string content = File.ReadAllText(f);
		if (content.Contains(find))
		{
			Console.WriteLine(f);

			string updated = content.Replace(find, replace);

			File.WriteAllText(f, updated);
		}
	}
}


void Images() {


	foreach (var f in Directory.GetFiles(Folder, "2021-01-03-hello-azure-wordpress-blog.md"))
	{
		 string fn = Path.GetFileName(f);
		 var splits = fn.Split('-');
		 string year = splits[0];
		 string month = splits[1];
		 
		 Console.WriteLine(year);
		 Console.WriteLine(month);
		 
		 var images =  Directory.GetFiles(  @"C:\dev\mckelt\chrismckelt.github.io\_posts\posts\images", "*.*", SearchOption.AllDirectories)
		 .Where(x => x.Contains(year))
		 .Where(x => x.Contains(month))
		 .Select(x => new KeyValuePair<string,string>(x, Path.GetFileName(x)));

		foreach (var item in images)
		{
			string u = $"/{year}/{month}/{item.Value}";
			Console.WriteLine(u);

			string content = File.ReadAllText(f);
			if (content.Contains(item.Value))
			{
				Console.WriteLine(f);

				string updated = content.Replace(item.Value, u);

				File.WriteAllText(f, updated);
			}
		}

	}
//
//	var oldImages =  Directory.GetFiles( @"C:\temp\output\images", "*.*").Select(x => new KeyValuePair<string,string>(x, Path.GetFileName(x)));
//	var newImages =  Directory.GetFiles(  @"C:\dev\mckelt\chrismckelt.github.io\_posts\posts\images", "*.*", SearchOption.AllDirectories).Select(x => new KeyValuePair<string,string>(x, Path.GetFileName(x)));
//	oldImages.Dump("oldImages");
//	newImages.Dump("newImages");
//	var dic = new Dictionary<string, string>();
//	foreach (var o in oldImages) {
//
//		var n = newImages.Where(x => x.Value == o.Value);
//		if (n.Count() > 2) {
//			Console.WriteLine(n);	
//		}
//		
//	}
//	
}