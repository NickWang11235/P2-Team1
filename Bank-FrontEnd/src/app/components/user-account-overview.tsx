import { User,Account,Transaction } from "../models/dtos";
import AccountSummary from "./account-summary";
import UserInterface from "./user-info";
import AccountSummaryList from "./account-summary-list";
import { CurrentUserContext } from "../page";



export default function UserAccountOverview(){

    const userAccountOverviewStyles = {
        padding: "80px",
    }

    return(
        <div style={userAccountOverviewStyles}>
            <UserInterface />
            <AccountSummaryList/>
        </div>

    )

}