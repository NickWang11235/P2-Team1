import { User,Account,Transaction } from "../models/dtos";

export default function AccountSummary(account:Account){

    let imageSrc = "";
    // if(account.AccountType === "CHECKING") imageSrc = "accountImages/checking.png";
    // else if(account.AccountType === "SAVING") imageSrc = "./accountImages/savings.png";
    // else if(account.AccountType === "CLOWN") imageSrc = "./accountImages/clown.png";
    if(account.AccountType === "SAVING") imageSrc = "https://cdn-icons-png.flaticon.com/512/584/584052.png";
    else if(account.AccountType === "CHECKING") imageSrc = "https://cdn-icons-png.flaticon.com/512/10384/10384161.png";
    else if(account.AccountType === "CLOWN") imageSrc = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ0unitLDK9Bzp4rgrIn9a4Q1Kdz1muTrBBtg&s";
    

    const iconStyling ={
        maxHeight: "75px",
    }

    return (
        <div>
            <img src={imageSrc} style={iconStyling}></img>
            <h3>{account.AccountType}</h3>
        </div>
    );

}
