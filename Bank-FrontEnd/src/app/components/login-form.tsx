import { useState, useContext, createContext, useEffect } from "react";
import { Userd } from "../models/dtos";
import { user1, user2 } from "../models/mocked-models";
import { CurrentUserContext } from "../page";


export default function LoginForm() {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [userToSubmit,setUserToSubmit] = useState<Userd>({
                                                        userId:0,
                                                        username:"",
                                                        password:"",
                                                        name: "",
                                                        accounts: []});
    const baseUri = 'http://localhost:5203/';

    const {
        currentUser,
        setCurrentUser
    } = useContext(CurrentUserContext);

    

    const handleLogin = () => {


        fetch(baseUri+"Users/login",{
            method:"POST",
            // mode: "no-cors",
            headers:{
                'Content-Type':'application/json;charset=UTF-8',
                // 'Access-Control-Allow-Origin': null,
            },
            body: JSON.stringify(userToSubmit),
            // body: userToSubmit,
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

        fetch(baseUri+"Users",{
            method:"GET",
            mode: "no-cors",
            headers:{
                "Content-Type":"application/json",
                // 'Access-Control-Allow-Origin': null,
            },
            })
            .then((response)=> response.json())
            .then((data)=>{
                console.log(data);
            })
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
                                value={userToSubmit.username}
                                // value={username}
                                // onChange={(e) => setUsername(e.target.value)}
                                onChange={(e) => {
                                    setUserToSubmit({
                                        userId:0,
                                        username:e.target.value,
                                        password:userToSubmit.password,
                                        name: "",
                                        accounts: []});
                                    // console.log(userToSubmit.Username);
                                    // console.log(userToSubmit.Password);
                                    console.log(JSON.stringify(userToSubmit));
                                }}
                            />
                        </label>
                    </div>
                    <div>
                        <label>
                            <span>Password:</span>
                            <input
                                type="password"
                                value={userToSubmit.password}
                                // onChange={(e) => setPassword(e.target.value)}
                                onChange={(e) => {
                                    setUserToSubmit({
                                        userId:0,
                                        username:userToSubmit.username,
                                        password:e.target.value,
                                        name: "",
                                        accounts: []});
                                    // console.log(userToSubmit.Username);
                                    // console.log(userToSubmit.Password);
                                    console.log(JSON.stringify(userToSubmit));
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

