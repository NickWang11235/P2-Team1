import { useContext } from "react";
import { User,Account,Transaction } from "../models/dtos";
import { CurrentUserContext } from "../page";




export default function UserInterface(){
    
    const userInterfaceStyle = {
        height: "100%",
        position: "fixed",
        // padding: "80px",

    }
    const imageStyles = {
        maxHeight: "250px"
    }

    const {currentUser} = useContext(CurrentUserContext);

    return(

        <div className="user-profile">
            <img src={currentUser.user.ImageUrl} className="user-image" alt="User Image"/>
            <h1 className="user-name">{currentUser.user.Name}</h1>
        </div>
    );
}