#DropDB XCRM_EasyTest

#Application XCRMFullWin
#Application XCRMFullWeb

#IncludeFile XCRM_Login.inc

*Action Navigation(Roles)

*ProcessRecord
 Name = Administrators
 
*OptionalAction Edit
 
*Action Type Permissions

*Action Type Permissions.New

*FillForm
 Target Type = User	
 Read = True
 Write = True
 Create = True
 Delete = True
 Navigate = True

*OptionalAction OK

#IfDef XCRMFullWin
*Action Save and Close
#EndIf

*Action Save and Close

*Action Navigation(Roles)

*ProcessRecord
 Name = Administrators

*Action Type Permissions

*CheckTable Type Permissions
 Columns = Object, Create, Read, Write, Delete, Navigate
 Row = User, True, True, True, True, True
