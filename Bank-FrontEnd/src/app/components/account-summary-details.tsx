import React, { useContext, useState } from 'react';
import { User, Account, Transaction } from "../models/dtos";
import { CurrentUserContext } from '../page';

export const AccountSummaryDetails = (props) => {
    const [balance, setBalance] = useState(props.account.balance);

    const baseUri = 'http://localhost:5203/';
    const {currentUser} = useContext(CurrentUserContext);


    const handleWithdraw = () => {
        const amount = parseFloat(prompt('Enter amount to withdraw:'));
        if (isNaN(amount) || amount <= 0) {
            alert('Invalid amount');
            return;
        }

        if (amount > balance) {
            alert('Insufficient funds');
            return;
        }

        // Update the balance
        
        fetch(baseUri+`Users/${currentUser.UserId}/withdraw?accountId=${props.account.accountId}&amount=${amount}`,{
            method:"PATCH",
            headers:{
                'Content-Type':'application/json;charset=UTF-8'
            },
        })
        .then((response)=>response.json())
        .then((data)=>{
            setBalance(data.amount);
            console.log(data);
        })
    };

    const handleDeposit = () => {
        const amount = parseFloat(prompt('Enter amount to deposit:'));
        if (isNaN(amount) || amount <= 0) {
            alert('Invalid amount');
            return;
        }

        // Update the balance
        
        fetch(baseUri+`Users/${currentUser.UserId}/deposit?accountId=${props.account.accountId}&amount=${amount}`,{
            method:"PATCH",
            headers:{
                'Content-Type':'application/json;charset=UTF-8'
            },
        })
        .then((response)=>response.json())
        .then((data)=>{
            setBalance(data.amount);
            console.log(data);
        })
    };

    return (
        <div className="account-details" style={{ border: "dashed lightgreen", display: "flex" }}>
            <p className="balance">Balance: ${balance.toFixed(2)}</p>
            <div className="button-group">
                <button className="action-button" onClick={handleWithdraw}>Withdraw</button>
                <button className="action-button" onClick={handleDeposit}>Deposit</button>
                <button className="action-button">History</button>
                <button className="action-button">Manage</button>
            </div>
        </div>
    );
};