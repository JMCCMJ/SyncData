# SYNC DATA API
## A simple API that demonstrates adding and updating forms with logging and authentication and syncing this data with sub applications

Both client and api need a file "appsettings.json" which will contain information for authentication through Azure Active Directory

Client

`{
  "InstanceId": "https://login.microsoftonline.com/{0}",
  "TenantId": "<TenantId>",
  "ClientId": "<ClientId>",
  "ClientSecret": "<ClientSecret>",
  "BaseAddress": "<BaseAddress>",
  "ResourceId": "<ResourceId>"
}`

API

`{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  "AAD": {
    "ResourceId": "<ResourceId>",
    "InstanceId": "https://login.microsoftonline.com/",
    "TenantId": "<TenantId>"
  }
}`

## Questions for Client

1. How or when are the sub applications updated and why?

2. Do they read from the same database?

3. Is FieldId unique for each form? Or can 2 different forms have a field with the same FieldId? 

4. Can the sub application update forms and fields? Does this propagate to other sub applications or the main application?

## Notes
- Currently using SqLite, so published app service does not currently work local DB file