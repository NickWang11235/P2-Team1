import { User,Account,Transaction } from "../models/dtos";


export default function AccountSummaryDetails(account:Account){

    const accountDetails ={
        border: "dashed light-green",
        
    }

    return(
        <div style={accountDetails}>
        <p>Balance: {account.Balance}</p>
        <button>Withdraw</button>
        <button>Deposit</button>
        <button>History</button>
        <button>Manage</button>
        </div>
    )
}