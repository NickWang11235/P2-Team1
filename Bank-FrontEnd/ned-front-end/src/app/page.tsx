"use client";
import Image from "next/image";
import styles from "./page.module.css";
import { createContext,useContext,useState} from "react";

import Basic from "./components/basic";
import UserAccountOverview from "./components/user-account-overview";


// mocked variables
import { user1,user2 } from "./models/mocked-models";
import { account1,account2,account3 } from "./models/mocked-models";
import LoginForm from "./components/login-form";

export const CurrentUserContext = createContext(null);

export default function Home() {

  const [currentUser,setCurrentUser] = useState(null);

  return (
    // <Basic/>
    <CurrentUserContext.Provider
      value={{
        currentUser,
        setCurrentUser
      }}>
        {currentUser === null ? 
          <LoginForm/>: 
      <UserAccountOverview/>
        }
    </CurrentUserContext.Provider>
  );
}
