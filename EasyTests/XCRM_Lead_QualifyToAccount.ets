#DropDB XCRM_EasyTest

#Application XCRMFullWin
#Application XCRMFullWeb

#IncludeFile XCRM_Login.inc

#IncludeFile XCRM_AddLead.inc

*Action Navigation(Leads)

*Action View(Open Leads)

*SelectRecords
 Columns = Name
 Row = Lead1First Lead1Last

*Action Qualify Lead...

*FillForm
 Account = True

*Action OK

*Action Navigation(Accounts)

*CheckTable
 Columns = Account Name
 Row = Lead1CompanyName

