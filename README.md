

# Automated API Test Project

> Created By Abdirashiid Jama

## About

This project performs automated testing on Sikoia API's

## Stack
- [C#](https://learn.microsoft.com/en-us/dotnet/csharp/)
- [Nunit](https://www.nuget.org/packages/NUnit/3.13.3)
- [Newtonsoft.json](https://www.nuget.org/packages/Newtonsoft.Json)
- [System.Net.Http](https://www.nuget.org/packages/System.Net.Http)

## Automation Coverage

### Client Functionality

- CreateClient
- GetClient
- PromoteClient

### Sales Functionality

- CreateSale
- GetSale

### Automation Run Results
| CreateClient  | ✓ |
| GetClient | ✓ |
| PromoteClient  | ✓ |
| CreateSale  | ✘ |
| GetSale  | ✘ |

> Manual testing has been also performed using Postman which will be included in this repository.

#### Test Failures Explained

**CreateSale** has test has failed due to the following error:

> You cannot associate any sale to a prospect. Please promote this prospect to client first

However the clientID provided to the API is indeed active and has been promoted, this is an issue with the API to be fixed.

**GetSale** fails due to having no existing saleID to provide as I am not able to obtain one from **CreateSale**.


## Folder Structure

![](https://i.imgur.com/w95tFtB.jpg)

The Solutions is broken up in two project, one that handles the API's and one that handles the tests.

## Further Changes

There are some further changes and refactoring I would wish to have done if I had more time such as:

 - Refactoring code
 - More variety in test cases eg. Different names, dates, account etc.
 - Negative test cases
 - I have shown two ways of making requests, one using my model and one using and replacing and existing JSON file, one which is more ideal than the other.
 - Simplifying the code and avoid repeats
 - More in-depth verification
 - Expanding test coverage

## Addressing Testing Complex Workflows

When it comes to testing more complex workflow I believe communication and defining clear requirements are important. When objectives have been specified and any issues addressed, I can create a test plan to from which I can write automated tests. Automation testing should first cover critical functionality, and then branch out to have as much coverage as possible. There might be a requirement for manual testing as well in scenarios which cannot be automated. 
