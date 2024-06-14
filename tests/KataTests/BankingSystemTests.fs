module KataTests.BankingSystemTests

open Xunit
open Kata.BankingSystem

[<Fact>]
let ``Account can be created`` () =
    let account = Bank.createAccount "uniqueId" 1000
    Assert.Equal("uniqueId", account.Number)
    Assert.Equal(1000, account.Balance)

[<Fact>]
let ``Deposit should be added to account balance`` () =
    let account = Bank.createAccount "uniqueId" 1000
    let accountAfterDeposit = Bank.deposit account 100
    Assert.Equal(1100, accountAfterDeposit.Balance)