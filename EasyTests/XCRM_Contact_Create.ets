#DropDB XCRM_EasyTest

#Application XCRMFullWin
#Application XCRMFullWeb

#IncludeFile XCRM_Login.inc

*Action Navigation(Contacts)

*Action New

*FillForm
 Last Name = Contact Last Name
 First Name = Contact First Name
 Email = test@test.com
 Office Phone = 555-112

*Action Save and Close

*Action Navigation(Contacts)

*CheckTable
 Columns = Full Name, Office Phone
 Row = Contact First Name Contact Last Name, 555-112