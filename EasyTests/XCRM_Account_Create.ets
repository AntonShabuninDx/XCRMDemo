#DropDB XCRM_EasyTest

#Application XCRMFullWin
#Application XCRMFullWeb

#IncludeFile XCRM_Login.inc

*Action Navigation(Accounts)

*Action New

*FillForm
 Account Name = Account Name
 City = City
 Office Phone = 555-555

*Action Contacts

*Action Contacts.New

*FillForm
 Last Name = Contact Last Name
 First Name = Contact First Name
 Email = test@test.com

#IfDef XCRMFullWin
*Action Save and Close
#EndIf

#IfDef XCRMFullWeb
*Action OK
;B188072
*Action Contacts
#EndIf

*CheckTable Contacts
 Columns = Full Name
 Row = Contact First Name Contact Last Name

*FillForm
 Primary Contact =  Contact First Name Contact Last Name

*Action Save and Close

*Action Navigation(Accounts)

*Action Refresh

*CheckTable
 Columns = Account Name, Primary Contact, Office Phone
 Row = Account Name, Contact First Name Contact Last Name, 555-555