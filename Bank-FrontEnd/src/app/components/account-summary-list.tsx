import { User,Account,Transaction } from "../models/dtos";
import { AccountSummary} from "./account-summary";
import { CurrentUserContext } from "../page";
import { useContext } from "react";




export default function AccountSummaryList(){


    // const listOfAccountSummaries = accountList && accountList.map && accountList.map((account,index)=>{
    //     return(
    //         <li key={index}>
    //             <AccountSummary{...account}/>
    //         </li>
    //     );
    // })

    // const listToUse : Account[] = accountList;

    const {currentUser} = useContext(CurrentUserContext);


    const listOfAccountSummaries = currentUser.user.Accounts.map((account,index)=>{
        return(
            <li key={index}>
                <AccountSummary account = {account}/>
            </li>
        );
    })

    const accountListStyle ={
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
        margin: "5px",
    }


    return(
        <div className="accountList" style={accountListStyle}>
        <ul>
            {listOfAccountSummaries}
        </ul>
        </div>
    )

}