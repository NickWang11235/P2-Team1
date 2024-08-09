import { User,Account,Transaction } from "../models/dtos";
import {AccountSummaryDetails } from "./account-summary-details";

export const AccountSummary = (props)=> {

    let imageSrc = "";

    // Determine image source based on account type
    if (props.account.AccountType === "SAVING") {
        imageSrc = "https://cdn-icons-png.flaticon.com/512/584/584052.png";
    } else if (props.account.AccountType === "CHECKING") {
        imageSrc = "https://cdn-icons-png.flaticon.com/512/10384/10384161.png";
    } else if (props.account.AccountType === "CLOWN") {
        imageSrc = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ0unitLDK9Bzp4rgrIn9a4Q1Kdz1muTrBBtg&s";
    }
    

    const icon ={
        maxHeight: "75px",
        borderRadius:"15%",
        backgroundColor:"white",
        padding: "5px",
    }
    const iconAndTitle = {
        border: "dashed orange",
        position:"relative",
        float: "left",
        height:"100%",
    }

    const accountSummary ={
        border: "solid red",
        borderRadius:"15%",
        backgroundColor:"gray",
        padding:"10px",
        margin: "5px",
    }

    const accountDetails ={
        border: "dashed red",
        
    }

    return (
        <div className="accountSummary">
            <div className="iconAndTitle">
            <img className="icon" src={imageSrc} alt="Account Icon" style={icon} />                <h3>{props.account.AccountType}</h3>
            </div>
            <AccountSummaryDetails account={props.account} />
        </div>
    );

}
