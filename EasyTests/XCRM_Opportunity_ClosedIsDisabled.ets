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

*Action Save

*Action Close Opportunity...

*FillForm
 Status = Won
 Actual Revenue = 100
 Close Date = 1/1/2008

*Action OK

*CheckFieldValues
 Name = Opportunity Topic

!FillForm
 Topic = Opportunity Topic 1