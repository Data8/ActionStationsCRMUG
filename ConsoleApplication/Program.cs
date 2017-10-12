using System.Configuration;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.ServiceModel.Description;

namespace ConsoleApplication
{
    class Program
    {
        static void Main()
        {
            // Connect to CRM
            var org = new OrganizationServiceProxy(
                new Uri(ConfigurationManager.AppSettings["OrgUri"]),
                null,
                new ClientCredentials
                {
                    UserName =
                    {
                        UserName = ConfigurationManager.AppSettings["Username"],
                        Password = ConfigurationManager.AppSettings["Password"]
                    }
                },
                null
                );

            // Enable using early-bound types to send the request for our custom action
            org.EnableProxyTypes();

            // Find any accounts that have a Suggested Credit Limit but not a Credit Limit
            var qry = new QueryExpression("account")
            {
                ColumnSet = new ColumnSet("new_suggestedcreditlimit")
            };
            qry.Criteria.AddCondition("new_suggestedcreditlimit", ConditionOperator.NotNull);
            qry.Criteria.AddCondition("creditlimit", ConditionOperator.Null);

            foreach (var account in org.RetrieveMultiple(qry).Entities)
            {
                // Invoke the new_ApplyCreditLimit custom action to set the Credit Limit to the maximum
                // Suggested Credit Limit
                org.Execute(new new_ApplyCreditLimitRequest
                {
                    Target = account.ToEntityReference(),
                    RequestedCreditLimit = account.GetAttributeValue<Money>("new_suggestedcreditlimit")
                });
            }
        }
    }
}
