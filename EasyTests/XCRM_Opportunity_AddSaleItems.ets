#DropDB XCRM_EasyTest

#Application XCRMFullWin
#Application XCRMFullWeb

#IncludeFile XCRM_Login.inc

*Action Navigation(Products)
*Action New

*FillForm
 Name = Product1
 Price = 100

*Action Save and Close

#IncludeFile XCRM_AddAccount.inc

*Action Navigation(Opportunities)

*Action New

*FillForm
 Name = Opportunity Topic
 Potential Customer = Account1

*Action Sale Items

*Action Sale Items.New

*FillForm
 Product = Product1
 Quantity = 3

#IfDef XCRMFullWin
*CheckFieldValues
 Amount = $300.00
 
*Action Save and Close
#EndIf

#IfDef XCRMFullWeb
*CheckFieldValues
 Amount = 300
 
*Action OK
#EndIf

*Action Save and Close

#IfDef XCRMFullWeb
*Action Navigation(Opportunities)
#EndIf

*Action View(Open Opportunities)

*Action Filter by Text(Opportunity Topic)

*ProcessRecord
 Topic = Opportunity Topic

*CheckFieldValues
 Estimated Revenue = $300.00