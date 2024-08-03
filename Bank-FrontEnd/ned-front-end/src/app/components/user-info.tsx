import { User,Account,Transaction } from "../models/dtos";




export default function UserInterface(user:User){
    
    const userInterfaceStyle = {
        height: "100%",
        position: "fixed",
        // padding: "80px",

    }
    const imageStyles = {
        maxHeight: "250px"
    }

    return(

        <div style={userInterfaceStyle}>
            <img src={user.ImageUrl} style={imageStyles} alt="User Image"/>
            <h1 className="user">{user.Name}</h1>
        </div>
    );
}