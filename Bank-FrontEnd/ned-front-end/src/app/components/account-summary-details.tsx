import { User,Account,Transaction } from "../models/dtos";


export const AccountSummaryDetails = (props)=>{

    const accountDetails ={
        border: "dashed light-green",
        display: "flex"
        
    }

    return(
        <div style={accountDetails}>
        <p>Balance: {props.account.Balance}</p>
        <button>Withdraw</button>
        <button>Deposit</button>
        <button>History</button>
        <button>Manage</button>
        </div>
    )
}