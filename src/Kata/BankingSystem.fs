namespace Kata.BankingSystem

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
            History: List<Transaction>
        }

    let createAccount accountNumber initialBalance =
        { Number = accountNumber; History = [ { Type = Deposit; Amount = initialBalance } ] }
        
            
    let checkBalance (account: Account) =
        account.History
        |> List.map (fun transaction ->
                                     match transaction.Type with
                                     | Withdrawal -> -transaction.Amount
                                     | Deposit -> transaction.Amount)
        |> List.sum

    let deposit account amount =
        { account with History = { Type = Deposit; Amount = amount } :: account.History }
        
    let withdrawal account amount =
        if checkBalance account > amount then
            Some { account with History = { Type = Withdrawal; Amount = amount } :: account.History }
        else
            None
