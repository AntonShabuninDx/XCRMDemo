#DropDB XCRM_EasyTest

#Application XCRMFullWin
#Application XCRMFullWeb

#IncludeFile XCRM_Login.inc

*Action Navigation(Contacts)
*Action New

*FillForm
 Email = Contact Email
 Last Name = Contact Last Name
 First Name = Contact First Name

*Action Save and Close

*Action Navigation(Reports.Reports)

*ProcessRecord
 Report Name = Contacts Report

*Sleep 10000

#IfDef XCRMFullWin
*HandleDialog
 Close = True
#EndIf

#IfDef XCRMFullWeb
*Action Close
#EndIf

*CheckTable
 Columns = Report Name
 Row = Contacts Report

*Action Navigation(Contacts)

*SelectRecords
 Columns = Full Name
 Row[0] = Contact First Name Contact Last Name

*Action Show in Report

*Sleep 10000

#IfDef XCRMFullWin
*HandleDialog
 Close = True
#EndIf

#IfDef XCRMFullWeb
*Action Close
#EndIf

*CheckTable
 Columns = Full Name
 Row = Contact First Name Contact Last Name