import { useState, useContext, createContext, useEffect } from "react";
import { User } from "../models/dtos";
import { user1, user2 } from "../models/mocked-models";
import { CurrentUserContext } from "../page";

export default function LoginForm() {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [userToSubmit,setUserToSubmit] = useState<User>({Username:"",Password:""});
    const baseUri = 'localhost:3000/';

    const {
        currentUser,
        setCurrentUser
    } = useContext(CurrentUserContext);

    

    const handleLogin = () => {


        fetch(baseUri+"login",{
            method:"POST",
            headers:{
                "Content-Type":"application/json",
            },
            body: JSON.stringify(userToSubmit),
            })
            .then((response)=> response.json())
            .then((data)=>{
                setCurrentUser(data);
                console.log(data);
            })
        // Simple authentication logic
        // if (username === "1" && password === "notPassword123!") {
        //     // Set user1 in context
        //     setCurrentUser({ user: user1 });
        // } else if (username === "2" && password === "notPassword123!") {
        //     // Set user2 in context
        //     setCurrentUser({ user: user2 });
        // } else {
        //     // Handle failed login
        //     alert("Invalid username or password");
        // }
    };

    const consoleThePrint = () =>
    {

    }

    return (
            <div className="login-form">
                <div className="login-form-container">
                    <h1>Login</h1>
                    <div>
                        <label>
                            <span>Username:</span>
                            <input
                                type="text"
                                value={userToSubmit.Username}
                                // value={username}
                                // onChange={(e) => setUsername(e.target.value)}
                                onChange={(e) => {
                                    setUserToSubmit({Username:e.target.value,Password:userToSubmit.Password});
                                    // console.log(userToSubmit.Username);
                                    // console.log(userToSubmit.Password);
                                }}
                            />
                        </label>
                    </div>
                    <div>
                        <label>
                            <span>Password:</span>
                            <input
                                type="password"
                                value={userToSubmit.Password}
                                // onChange={(e) => setPassword(e.target.value)}
                                onChange={(e) => {
                                    setUserToSubmit({Username:userToSubmit.Username,Password:e.target.value});
                                    // console.log(userToSubmit.Username);
                                    // console.log(userToSubmit.Password);
                                }}
                            />
                        </label>
                    </div>
                    <button className="button" onClick={handleLogin}>
                        Login
                    </button>
                </div>
            </div>
        );
}

