﻿﻿
## Demo.Test


# CRUD Tests
Run the CRUD test to control the behaviour of the library. 
The checking procedure follows two steps:
- proceed throgh break points on every method call;
- check directly on the database if the results correspond to the behaviour we want the library has for each method call.

Before executing the tests "OrderCRUDTest" and "OrderDetailCRUDTest" check for the Foreign Key parameters on the database and, eventually, 
change them in the test code with the data you have.
The FK scheme reference we have to keep in mind is the following:
- Order.UserId -> User.Id;
- OrderDetail.OrderId -> Order.Id;
- OrderDetail.ProductId -> Product.Id;
If there's no data to fulfill the FK parameters, before proceding with tests populate the tabels "USER" and "PRODUCT".

Through the test code there's a comment "to check" beside the points where a check for the data is needed before proceeding.

The tests on "List" method are only in "UserCRUDTest.cs".


# Infos
The current test ambient refers to the "ApiStackNet.Demo" Project. 