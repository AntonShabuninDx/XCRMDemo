#DropDB XCRM_EasyTest

#Application XCRMFullWin
#Application XCRMFullWeb

#IncludeFile XCRM_Login.inc

*Action Navigation(Users)
*Action New

*FillForm
 Last Name = User Last Name
 First Name = User First Name
 User Name = User

*Action Roles.Link
*SelectRecords Role
 Columns = Name
 Row = Administrators
*Action OK
*Action Save and Close

*Action Navigation(Users)

*CheckTable
 Columns = Display Name
 Row = User First Name User Last Name
