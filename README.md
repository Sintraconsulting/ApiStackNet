# Api Stack Net: the api accellerator
[![Build status](https://ci.appveyor.com/api/projects/status/g3yoa32469vf11f9/branch/master?svg=true)](https://ci.appveyor.com/project/zeppaman/apistacknet/branch/master)  [![nuget](https://img.shields.io/badge/nuget-download-blue.svg)](https://www.nuget.org/packages/ApiStackNet/)

ApiStackNet is a non-invasive api accellerator. Compared to other frameworks it doesn't exclude the possibility to create vanilla code. It's purpose is to:

- avoid redundand code, 
- give a solid stack to all developers,
- keep a clean architecture.

What is included out of the box:

- three level architecture scaffold ( DAL, BLL, API)
- swagger support
- entity framework 6 support (database first or code first)
- service pattern scaffold

With ApiStackNet you can scaffold a rest web service just writing the only part where you bring value. So, you just need to define data model and mapping it to the business model to get all working!


# Getting Started

TODO: tutorial here (add nuget step)

# Build and Test
TEST Postman:
The following tips refer to those test that need an "hand configuration" before to proceed: 
- GetById: set in the Request url bar the Id of the entity to get;
- GetList: just send the request to run the test;
- Edit: set in the Request url bar the Id of the entity to edit and check the corrispondence between the hand setted Id and the parameter "Id" in the body of the request;
- Save: just send the request to run the test, using the json in the body;
- Delete: set in the Request url bar the Id of the entity to delete;
- DeleteByEntity: just send the request to run the test, using the json in the body.

# Contribute

Any contribution is welcome, open an issue to start.

# License

ApiStackNet is released under LGPL License.