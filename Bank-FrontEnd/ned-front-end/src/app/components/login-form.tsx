import { useState,useContext,createContext } from "react";
import { user1,user2 } from "../models/mocked-models";
import { CurrentUserContext } from "../page";




export default function LoginForm(){

    return (
        <div title="Welcome">
            <LoginButton/>
            </div>
    )
}


function LoginButton (){
    const {
        currentUser,
        setCurrentUser
    } = useContext(CurrentUserContext);

    return (
        <button className="button" onClick={() => {
            setCurrentUser({user: user2})
        }}>Login</button>
    )
}