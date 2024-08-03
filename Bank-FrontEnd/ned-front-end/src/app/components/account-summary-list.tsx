import { User,Account,Transaction } from "../models/dtos";
import AccountSummary from "./account-summary";



export default function AccountSummaryList(accountList:Account[]){


    // const listOfAccountSummaries = accountList && accountList.map && accountList.map((account,index)=>{
    //     return(
    //         <li key={index}>
    //             <AccountSummary{...account}/>
    //         </li>
    //     );
    // })

    // const listToUse : Account[] = accountList;


    // const listOfAccountSummaries =   listToUse.map((account,index)=>{
    //     return(
    //         <li key={index}>
    //             <AccountSummary{...account}/>
    //         </li>
    //     );
    // })

    const accountSummaryStyling ={
        display: "flex",
        flexDirection: "column",
        // justifyContent: "center",
        alignItems: "center",

    }

    const listOfAccountSummaries = (
       <>
            <li>
                <AccountSummary{...accountList[0]}/>
            </li>
            <li>
                <AccountSummary{...accountList[1]}/>
            </li>
        </>
    )


    return(
        <ul style={accountSummaryStyling}>
            {listOfAccountSummaries}
        </ul>
        // <div>
        //     <AccountSummary {...accountList[0]}/>
        // </div>
    )

}