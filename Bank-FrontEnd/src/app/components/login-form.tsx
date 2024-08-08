import { useState, useContext, createContext } from "react";
import { user1, user2 } from "../models/mocked-models";
import { CurrentUserContext } from "../page";

export default function LoginForm() {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");

    const {
        currentUser,
        setCurrentUser
    } = useContext(CurrentUserContext);

    const handleLogin = () => {
        // Simple authentication logic
        if (username === "1" && password === "notPassword123!") {
            // Set user1 in context
            setCurrentUser({ user: user1 });
        } else if (username === "2" && password === "notPassword123!") {
            // Set user2 in context
            setCurrentUser({ user: user2 });
        } else {
            // Handle failed login
            alert("Invalid username or password");
        }
    };

    return (
            <div className="login-form">
                <div className="login-form-container">
                    <h1>Login</h1>
                    <div>
                        <label>
                            <span>Username:</span>
                            <input
                                type="text"
                                value={username}
                                onChange={(e) => setUsername(e.target.value)}
                            />
                        </label>
                    </div>
                    <div>
                        <label>
                            <span>Password:</span>
                            <input
                                type="password"
                                value={password}
                                onChange={(e) => setPassword(e.target.value)}
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

