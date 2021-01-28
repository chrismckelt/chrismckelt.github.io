---
title: "Windows SharePoint Services Notes"
date: "2008-10-12"
categories: 
  - "net"
  - "notes"
---

**SharePoint main objects**

SPSite = Site Collection SPWeb = Site Web

SPSite siteCollection = new SPSite("http://localhost/sites/sitename"); SPWeb web = siteCollection.OpenWeb();

**Features**

Features define a mechanism for defining site elements and adding them to a target site or site collection through a process called _feature activation_

An example feature

 <Feature Id\="6AA03A00-02B5-4d83-A041-1CEB0DE9EF9E" Title\="Chris Document Manager" Description\="Test description" Version\="1.0.0.0"  Scope\="Web" ImageUrl\="TestImage.gif" ReceiverAssembly\="DocumentManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=52cb0cd67d773ef7" ReceiverClass\="DocumentManager.FeatureReceiver" xmlns\="http://schemas.microsoft.com/sharepoint/"\> <ElementManifests\> <ElementManifest Location\="elements.xml" /> </ElementManifests\> </Feature\>

**Elements**

 The element types that can be defined by a feature include

- Menu commands
- Link commands
- Pages templates
- Page instances
- List definitions
- List instances
- Event handlers
- Workflows

  <Elements xmlns\="http://schemas.microsoft.com/sharepoint/"\> <CustomAction  Id\="SiteActionsToolbar" GroupId\="SiteActions" Location\="Microsoft.SharePoint.StandardMenu" Sequence\="100" Title\="Hello World" Description\="A custom menu item added using a feature" ImageUrl\="\_layouts/images/crtsite.gif" >       <UrlAction Url\="http://msdn.microsoft.com"/> </CustomAction\> </Elements\>

**Configuration Elements**

 

<configuration\> <configSections\> <sectionGroup name\="SharePoint"\> <section name\="SafeControls" type\=""/> <section name\="RuntimeFilter" type\=""/> <section name\="WebPartLimits" type\=""/> <section name\="WebPartCache" type\=""/> <section name\="WebPartWorkItem" type\=""/> <section name\="WebPartControls" type\=""/> <section name\="SafeMode" type\=""/> <section name\="MergedActions" type\=""/> <section name\="PeoplePickerWildCards" type\=""/> <section name\="SafeControls" type\=""/> <section name\="SafeControls" type\=""/> </sectionGroup\> </configSections\> <SharePoint\> <SafeMode/> <WebPartLimits/> <WebPartCache/> <WebPartControls/> <SafeControls/> <PeoplePickerWildCards/> <MergedActions/> <BlobCache/> <RuntimeFilter/> </SharePoint\> </configuration\>

**SPVirtualPathProvider**

SPVirtualPathProvider allows files to be stored in the database – a term referred to as ‘ung_hosted_’

It can also retrieve pages from the file system ‘G_hosted_’ and pass them to the asp.net aspx page parser

**Pages Types**

Site Pages

- Support customisation
- Have web parts and webpart zones

Application Pages

- Deployed as physical files (aspx) of the front end web server in the following location
- C: \\program files \\common files \\microsoft shared \\web server extensions \\12 \\template \\layouts
- Inherit from LayoutsPageBase

**Debugging WSS Components**

<configuration\> <SharePoint\> <SafeMode CallStack\="true"/> </SharePoint\> <system.web\> <customErrors mode\="Off"/> <compilation debug\="true"/> </system.web\> </configuration\> 

**SPFile**

SPWeb site = SPContext.Current.Web; SPFile homePage = site.GetFile(“default.aspx”) 

**Default.master**

Resides in c: \\program files \\common files \\microsoft shared \\web server  extensions \\12 \\template \\global \\default.master

**Web Part Gallery** 

Document library that contains serialised web parts.  There web parts have a _.webpart_ extension (older WSS 2.0 were serialised using an old version with a .dwp extension)

Editor Web Part has 2 abstract methods to override to implement changes – _ApplyChanges_ & _SynchChanges_

_IWebPartField_ interface is implemented by many WSS web parts -- ie List View & Image

HTTPHandler endpoints are the preferred data source for AJAX applications in which data is returned in a simple XML, text or JSON format representing the data query. This is a subset of the REST architecture pattern (Representational State Transfer).

 IHttpAsynchHandler is the preferred interface for handlers that need long running tasks.

**Lists & Content Types**

The create page contains built in list types broken out into sections including _libraries_, _communications_, _tracking_ and _custom lists_

WSS List definitions

- Document library
- Form library
- Wiki page library
- Picture library
- Announcements
- Contacts
- Discussions
- Links
- Calendar
- Tasks
- Project tasks
- Issue tracking
- Custom list

WSS list bast types

- Standard lists = 0
- Document libraries = 1
- Discussion forums = 3
- Vote/Survey lists = 4
- Issue lists = 5

  Lists store their data in columns (also called fields).  WSS 3.0 introduces site columns which are reusable fields

To have a custom field type recognised a _Field Schema_ file is added in a CAML based syntax into a well known location.

 Field types are defined in files name _fldtypes\_companyName.xml_ that must be deployed in _TEMPLATE\\XML_ directory

 SPQuery query = new SPQuery(); query.ViewFields = @"<FieldRef Name='Title'/><FieldRef Name='Expires'/>"; query.Query = @"<Where> <Lt> <FieldRef Name='Expires' /> <Value Type='DateTime'> <Today /></Value> </Lt> </Where>"; //NB://  <Where><operator><operand /><operand /></operator></Where>

SPQuery object returns a SPListItemCollection ||  SPSiteDataQuery returns a datatable

The built in columns are defined in the build-in fields feature

[Content Types](http://blogs.msdn.com/andrew_may/archive/2006/05/24/SharePointBeta2WhatAreContentTypes.aspx "http://blogs.msdn.com/andrew_may/archive/2006/05/24/SharePointBeta2WhatAreContentTypes.aspx")            --->   [Content Type Ids](http://blogs.msdn.com/andrew_may/archive/2006/06/24/SharePointBeta2WhatAreContentTypeIDs.aspx "http://blogs.msdn.com/andrew_may/archive/2006/06/24/SharePointBeta2WhatAreContentTypeIDs.aspx")

Content types define underlying schema for either an item oin a list or a document in a document library.

Content types are either list based or document based.

A forms library is designed to store XML documents created from InfoPath forms.

A schema file must define content type references and its own separate data scheme as well as views and forms.

Create a custom document library in code -- with 4 extended fields

 

const string LIBRARY\_NAME = "BeazleyTradeDocumentLibrary";  SPSite siteCollection = new SPSite("http://localhost:334");SPWeb web = siteCollection.OpenWeb();if (!ListExists(web.Lists, LIBRARY\_NAME)){Guid listId = siteCollection.RootWeb.Lists.Add(LIBRARY\_NAME, "Beazley Trade Document Library", SPListTemplateType.DocumentLibrary);SPList list = web.Lists\[listId\];list.Fields.Add("BTProductId", SPFieldType.Number, true);list.Fields.Add("BTDocumentVersionNo", SPFieldType.Number, true);list.Fields.Add("BTProductName", SPFieldType.Text, false);list.Fields.Add("TemplateType", SPFieldType.Text, false);SPField btProductIdField = list.Fields\["BTProductId"\];btProductIdField.ShowInDisplayForm = true;btProductIdField.ShowInEditForm = true;btProductIdField.ShowInListSettings = true;btProductIdField.ShowInNewForm = true;btProductIdField.ShowInViewForms = true;SPView defaultView = list.Views\[0\];defaultView.ViewFields.Add(btProductIdField);defaultView.Update();list.ContentTypesEnabled = false;list.OnQuickLaunch = true;list.EnableAttachments = false;list.EnableVersioning = false;list.NoCrawl = true;list.Update();}

 

 

**Office Open XML**

Office Open XML file formats. Top level file such as Hello.docx is know as a _package_.

Inside a package are 2 kinds of internal components: _parts_ and _items_

In general, parts contain content and items contain metadata describing the parts.

Items can be further subdivided into _relationship items_ and _content-type items_

A part is an internal component containing content that is presisted inside the package.

The majority of parts are simple text files serialised as XML with an associated schema.

Open office xml file formats use relationships to define associations between a source and a target part.

A package relationship defines an association between the top level package and a part.

A part relationship defines an association between a parent part and a child part.

 

_Site definition_ -- is the top level component in WSS that aggregates smaller, more modular definitions to create a complete site template that can be used to provision sites.

Site definitions are deployed within the 12\\TEMPLATE\\SITETEMPLATES directory and are referenced in the 12\\TEMPLATE\\<culture>\\XML directory WEBTEMP.XML files where the <culture> folder is the locale identifier 12\\TEMPLATE\\1033\\XML for US English

Global site definitio - provides a common repository for site provision instructions required by every site definition.

The ONET.xml file serves as a high level manifest for the site definition and references the components that are to be used A feature can also be used to attach one or more other features to a site definition through a technique called _feature stapling_ Localisation in WSS is based on language packs that are deployed as solutions packages

A _solution package_is a compressed .cab file with a .wsp extension containing the componentsto be deployed on a target web server. The .wsp file for a solutions package can be built using the MAKECAB operations system utility  by reading the definition from a .ddf file.

The .ddf file defines the output structure of the .wsp file by referencing each file in its source location and its destination location in the .wsp file.

The metadata for a solution package is maintained in a file named manifest.xml that must be added to the root of the .wsp file.  It is the manifest.xml that tells the WSS runtime which template files to copy into the WSS system directories.

 WSS permission definitions

1. Environment permission
2. Security Permission
3. AspNetHostingPermission
4. WebPermission

SPSecurity allows code to run with higher priviledges

 

SPSecurity.RunWithElevitedPrivaledges(

delegate{// code run as SharePoint\\System}

)
