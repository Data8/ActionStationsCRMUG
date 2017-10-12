# ActionStationsCRMUG

This repo contacts the source code for the Action Stations! session at CRMUG Summit 2017 - an
introduction to custom actions in Microsoft Dynamics CRM/365

There are two projects in this solution:

## ActionStationsCRMUG

This project contains a plugin that is attached to the `new_ApplyCreditLimit` message and demonstrates how code in a plugin can
access the parameters of a custom action, and how any exceptions thrown during the plugin execution cause the effect of the custom
action to be rolled back.

It also contains an HTML web resource that demonstrates calling the custom action using the Web API.

## ConsoleApplication

This project demonstrates how to invoke the custom action from a C# SDK-based application.