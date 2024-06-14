module KataTests.BankingSystemTests

open Xunit
open Kata.BankingSystem

[<Fact>]
let ``Account can be created`` () =
    let account = Bank.createAccount "uniqueId" 1000
    Assert.Equal("uniqueId", account.Number)
    Assert.Equal(1000, account.Balance)
