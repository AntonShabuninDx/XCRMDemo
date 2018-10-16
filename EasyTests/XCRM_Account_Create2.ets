#DropDB XCRM_EasyTest

;#Application XCRMFullWin
#Application XCRMFullWeb

#IncludeFile XCRM_Login.inc

#IncludeFile XCRM_AddContact.inc

*Action Navigation(Accounts)

*Action New

*FillForm
 Account Name = Account Name

*Action Contacts

*Action Contacts.Link

*SelectRecords
 Columns = Full Name
 Row = Contact1First Contact1Last
*Action OK

*Action Save

;B188072
*Action Contacts

*CheckTable Contacts
 Columns = Full Name
 Row = Contact1First Contact1Last

*Action Save and Close

*Action Navigation(Accounts)

*CheckTable
 Columns = Account Name
 Row = Account Name