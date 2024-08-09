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
            <img src="https://images.pexels.com/photos/45201/kitty-cat-kitten-pet-45201.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500"
             className="user-image" alt="User Image"/>
            <h1 className="user-name">{currentUser.name}</h1>
        </div>
    );
}