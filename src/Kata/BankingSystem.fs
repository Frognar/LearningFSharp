namespace Kata.BankingSystem

open System.Reflection

module Bank =
    type TransactionType =
            | Deposit
            | Withdrawal
            
    type Transaction =
        {
            Type : TransactionType
            Amount: int
        }

    type Account =
        {
            Number: string
            Balance: int
            History: List<Transaction>
        }

    let createAccount accountNumber initialBalance =
        { Number = accountNumber; Balance = initialBalance; History = [ { Type = Deposit; Amount = initialBalance } ] }

    let deposit account amount =
        { account with Balance = account.Balance + amount; History = { Type = Deposit; Amount = amount } :: account.History }
        
    let withdrawal account amount =
        if account.Balance > amount then
            Some { account with Balance = account.Balance - amount; History = { Type = Withdrawal; Amount = amount } :: account.History }
        else
            None
            
    let checkBalance (account: Account) =
        account.History
        |> List.map (fun transaction ->
                                     match transaction.Type with
                                     | Withdrawal -> -transaction.Amount
                                     | Deposit -> transaction.Amount)
        |> List.sum
