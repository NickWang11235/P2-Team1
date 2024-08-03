import { User,Account,Transaction } from "../models/dtos";
import AccountSummary from "./account-summary";
import UserInterface from "./user-info";
import AccountSummaryList from "./account-summary-list";



export default function UserAccountOverview(user:User){

    const userAccountOverviewStyles = {
        padding: "80px",
    }

    return(
        <div style={userAccountOverviewStyles}>
            <UserInterface {...user}/>
            <AccountSummaryList {...user.Accounts}/>
        </div>

    )

}