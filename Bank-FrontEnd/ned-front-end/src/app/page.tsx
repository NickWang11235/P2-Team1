import Image from "next/image";
import styles from "./page.module.css";

import Basic from "./components/basic";
import UserAccountOverview from "./components/user-account-overview";


// mocked variables
import { user1,user2 } from "./models/mocked-models";
import { account1,account2,account3 } from "./models/mocked-models";

export default function Home() {

  return (
    // <Basic/>
    <>
      <UserAccountOverview {...user1}/>
    </>
  );
}
