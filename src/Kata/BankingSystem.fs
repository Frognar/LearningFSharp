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
            Balance: int
            History: List<Transaction>
        }

    let createAccount accountNumber initialBalance =
        { Number = accountNumber; Balance = initialBalance; History = [ { Type = Deposit; Amount = initialBalance } ] }