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

    const accountListStyle ={
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
        margin: "5px",
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
        <div className="accountList" style={accountListStyle}>
        <ul>
            {listOfAccountSummaries}
        </ul>
        </div>
        // <div>
        //     <AccountSummary {...accountList[0]}/>
        // </div>
    )

}