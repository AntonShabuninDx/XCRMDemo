#DropDB XCRM_EasyTest

#Application XCRMFullWin
#Application XCRMFullWeb

#IncludeFile XCRM_Login.inc

*Action Navigation(Products)
*Action New

*FillForm
 Name = Product Name
 Price = 100

*Action Save and Close

*Action Navigation(Products)

*CheckTable
 Columns = Name, Price
 Row = Product Name, $100.00