module KataTests.BankingSystemTests

open Xunit
open Kata.BankingSystem

[<Fact>]
let ``Account can be created`` () =
    let account = Bank.createAccount "uniqueId" 1000
    Assert.Equal("uniqueId", account.Number)
    
[<Fact>]
let ``Can check your balance`` () =
    let account = Bank.createAccount "uniqueId" 1000
    let balance = Bank.checkBalance account
    Assert.Equal(1000, balance)

[<Fact>]
let ``Deposit should be added to account balance`` () =
    let account = Bank.createAccount "uniqueId" 1000
    let accountAfterDeposit = Bank.deposit account 100
    Assert.Equal(1100, Bank.checkBalance accountAfterDeposit)

[<Fact>]
let ``Withdrawal should be removed from account balance`` () =
    let account = Bank.createAccount "uniqueId" 1000
    let accountAfterWithdrawal = Bank.withdrawal account 100
    match accountAfterWithdrawal with
    | Some account -> Assert.Equal(900, Bank.checkBalance account)
    | _ -> Assert.Fail()

[<Fact>]
let ``Withdrawal should not be allowed when the balance int the account is less`` () =
    let account = Bank.createAccount "uniqueId" 10
    let accountAfterWithdrawal = Bank.withdrawal account 100
    Assert.Equal(None, accountAfterWithdrawal)