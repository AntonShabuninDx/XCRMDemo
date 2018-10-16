#DropDB XCRM_EasyTest

#Application XCRMFullWin
#Application XCRMFullWeb

#IncludeFile XCRM_Login.inc

#IncludeFile XCRM_AddAccount.inc

*Action Navigation(Opportunities)

*Action New

*FillForm
 Name = Opportunity Topic
 Potential Customer = Account1
 
*Action Save and Close

*Action Navigation(Opportunities)

*Action View(Open Opportunities)

*SelectRecords
 Columns = Topic
 Row = Opportunity Topic

*Action Close Opportunity...

*FillForm
 Status = Won
 Actual Revenue = 100
 Close Date = 1/1/2008

*Action OK

*Action Navigation(Opportunities)

*Action View(Closed Opportunities)

*CheckTable
 Columns = Topic, Status
 Row = Opportunity Topic, Won
