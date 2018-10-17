# SOAPMessageBuilder
Windows forms application for reading WSDL definition and building request messages based on message type schemas.

Application uses as XML instance building this Microsoft sample code.
https://msdn.microsoft.com/en-us/library/aa302296.aspx 


- [Installing](#installing)
- [Building](#building)
- [Contributing](#contributing)
- [Change Log](#change-log)

# Installing

Application is self hosted and does not require any external components, except .NET Framework (.NET Framework 4.7.1) to be installed to computer it is executed.
So download the source ocde and build it with Visual Studio (version 2017 used in development). 

# Tasks

## SOAP Message Builder
Application takes as a parameter WSDL URL, SOAP binding name and SOAP operation name. UI contains two functions that are fetching the WSDL XML
from the URL and showing it's content. And SOAP message generation, that extract information out fro mWSDL and generates SOAP message instance XML.

Current version does not support login for WSDL endpoint, even UI has the controls for it. Second thing that is under construction is
inline XML schema definition support and support for looking SOAP 1.2 elements in WSDL. Also only Style: 'Document' & use: 'literal' is supported.

### Properties

| Property | Type | Description | Example |
| -------- | -------- | -------- | -------- |
| WSDL Url | string | Full WSDL URL. | 'http://host:10002/Service1.svc?wsdl' |
| SOAP Binding name | string | Content of binding XML node 'name' -attribute. | 'Service1' |
| SOAP Operation name | string | Name -attribute od binding element wsdl:operation -element. | 'GetData' |
| User Name | string | User name for login to WSDL endpoint. (Not in use) | 'user1' |
| Password | string  | Password for login to WSDL endpoint. (Not in use) | 'password1' |
| Needs login | bool | If value is set, uses user name and password to login into WSDL-endpoint. | false |

### Returns

| Property | Type | Description | Example |
| -------- | -------- | -------- | -------- |
| SOAP message | string | SOAP envelope XML as a string. | |

# Building

Clone a copy of the repo

`git clone https://github.com/RipaOnGit/SOAPMessageBuilder.git`

Restore dependencies

`N/A`

Rebuild the project

Run build.

`SOAPMessageBuilder2\SOAPMessageBuilder\bin\Release`

# Contributing
When contributing to this repository, please first discuss the change you wish to make via issue, email, or any other method with the owners of this repository before making a change.

1. Fork the repo on GitHub
2. Clone the project to your own machine
3. Commit changes to your own branch
4. Push your work back up to your fork
5. Submit a Pull request so that we can review your changes

NOTE: Be sure to merge the latest from "upstream" before making a pull request!

# Change Log

| Version | Changes |
| ----- | ----- |
| 1.0.0.0 | Initial commit. |
| 1.1.0.0 | Separation of business logic from the UI application to own component DLL. |
