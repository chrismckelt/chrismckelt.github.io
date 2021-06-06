---
layout: post
category: posts
title: "Winforms export datatable to excel without COM+"
date: "2008-07-22"
tags: code dotnet
---

Simple export of a datatable to excel without using COM+

Excel will ask if you want to open this as 'An XML List'

    SaveFileDialog fileDialog = new SaveFileDialog();
    fileDialog.Filter = "Excel files (\*.xls)|\*.xls";
    fileDialog.FileName = "My\_File\_Name.xls";
    fileDialog.ShowDialog();

    DataTable dt = GetFilteredDataTable();

    using (FileStream fs = (FileStream)fileDialog.OpenFile())
    using (StreamWriter sw = new StreamWriter(fs))
    {
      sw.Write("<?xml version=\\"1.0\\" standalone=\\"yes\\"?>");
      dt.WriteXml(sw, XmlWriteMode.IgnoreSchema);
      sw.Close();
      fs.Close();
    }
}
