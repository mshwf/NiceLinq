# NiceLinq
The library contains two methods that I used to use whenever I thought of the `IN` and `NOT IN` operators in SQL, their implementations are simple
but hides a repetive code.
For example, if you want to get list of accounts fall in range of `Id`s, use it like this:

```csharp
var selectedAccounts = listOfAccounts.In(x => x.Id, 1,2,3,4);
//this will get you a list of accounts whose Ids are 1,2,3,4
```

you also can pass a `List<int>` rather array of parameters:
```csharp
List<int> ids = bank.GetAccountIds(1);
var selectedAccounts = listOfAccounts.In(x => x.Id, ids);
```
