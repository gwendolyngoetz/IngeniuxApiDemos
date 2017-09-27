# Setup notes

## Required DLLs

Ingeniux files are not redistributable so they are not included. To install the following dlls need to be copied over from the bin folder under DSS_Preview to the lib folder in this repository.

Sample location >> c:\igxsites\cms90\[cms site name]\site\DSS_Preview\bin

* AWSSDK.dll
* AWSSDK.xml
* Esent.Interop.dll
* Esent.Interop.pdb
* Esent.Interop.xml
* FiftyOne.Foundation.dll
* GeoAPI.dll
* HtmlAgilityPack.dll
* HtmlAgilityPack.pdb
* HtmlAgilityPack.xml
* ICSharpCode.NRefactory.CSharp.dll
* ICSharpCode.NRefactory.CSharp.xml
* ICSharpCode.NRefactory.dll
* ICSharpCode.NRefactory.xml
* Ingeniux.CMS.CSAPI.dll
* Ingeniux.CMS.Common.dll
* Jint.Raven.dll
* Jint.Raven.pdb
* Lucene.Net.Contrib.Spatial.NTS.dll
* Lucene.Net.Contrib.Spatial.NTS.pdb
* Lucene.Net.Contrib.Spatial.NTS.xml
* Lucene.Net.dll
* Lucene.Net.pdb
* Lucene.Net.xml
* Microsoft.AspNet.SignalR.Core.dll
* Microsoft.AspNet.SignalR.Core.xml
* Microsoft.CompilerServices.AsyncTargetingPack.Net4.dll
* Microsoft.CompilerServices.AsyncTargetingPack.Net4.xml
* Microsoft.Data.Edm.dll
* Microsoft.Data.Edm.xml
* Microsoft.Data.OData.dll
* Microsoft.Data.OData.xml
* Microsoft.Owin.dll
* Microsoft.Owin.xml
* Microsoft.Web.Infrastructure.dll
* Microsoft.WindowsAzure.Storage.dll
* Microsoft.WindowsAzure.Storage.xml
* Mono.Cecil.dll
* MoreLinq.dll
* MoreLinq.xml
* NLog.dll
* NLog.xml
* NetTopologySuite.dll
* Owin.dll
* PowerCollections.dll
* Raven.Abstractions.dll
* Raven.Abstractions.pdb
* Raven.Abstractions.xml
* Raven.Client.Embedded.dll
* Raven.Client.Embedded.pdb
* Raven.Client.Embedded.xml
* Raven.Client.Lightweight.dll
* Raven.Client.Lightweight.pdb
* Raven.Client.Lightweight.xml
* Raven.Database.dll
* Raven.Database.pdb
* Raven.Database.xml
* ShoQuan.dll
* Spatial4n.Core.NTS.dll
* Spatial4n.Core.NTS.pdb
* Spatial4n.Core.NTS.xml
* System.Reactive.Core.dll
* System.Reactive.Core.xml
* System.Reactive.Interfaces.dll
* System.Reactive.Interfaces.xml

## Schemas

The schemas used in these samples can be imported from the IGXSchemas folder and must be installed for the tool to run.

## ApiBasics Project

This project has the simplest examples creating pages. ContentStore and UserID are hard-coded and will need to be adjusted for your test environment.

## IngeniuxApiDemos Project

This project has the processing samples connecting to a database, Seattle Open Data service, and Slickplan. There are config files under the Files/Config folder that can be updated to run in your environment.

## Setting up the database

In the database folder run the initialize.sql script to populate a test database.

### Running the Sample

1. Compile the project
2. Open a command prompt
3. cd C:\[path\to\repository]\IngeniuxApiDemos\bin\Debug
4. IngeniuxApiDemos.exe --configFilePath=..\..\Files\Config\dataImportFromDatabase.json
5. IngeniuxApiDemos.exe --configFilePath=..\..\Files\Config\dataImportParksOpenData.json
6. IngeniuxApiDemos.exe --configFilePath=..\..\Files\Config\dataImportSlickplan.json

## Links

* https://slickplan.com

* https://data.seattle.gov

* https://data.seattle.gov/resource/p262-qazs.json


