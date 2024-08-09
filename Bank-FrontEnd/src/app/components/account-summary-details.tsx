import React, { useState } from 'react';
import { User, Account, Transaction } from "../models/dtos";

export const AccountSummaryDetails = (props) => {
    const [balance, setBalance] = useState(props.account.balance);

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
        setBalance(balance - amount);
    };

    const handleDeposit = () => {
        const amount = parseFloat(prompt('Enter amount to deposit:'));
        if (isNaN(amount) || amount <= 0) {
            alert('Invalid amount');
            return;
        }

        // Update the balance
        setBalance(balance + amount);
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