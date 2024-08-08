'use client'; // Ensure the component is rendered on the client side

import React, { useEffect, useState } from 'react';


//Data to test. 
interface Transaction {
    transactionId: number;
    fromAccount: string;
    toAccount: string;
    amount: number;
    time: string;
}

const Transactions: React.FC = () => {
    const [transactions, setTransactions] = useState<Transaction[]>([]);

    useEffect(() => {
        // Example array of transaction data. Replace with actual data as needed.
        const fetchedTransactions: Transaction[] = [
            {
                transactionId: 12345,
                fromAccount: 'Account A',
                toAccount: 'Account B',
                amount: 250.75,
                time: new Date().toLocaleString(),
            },
            {
                transactionId: 12346,
                fromAccount: 'Account C',
                toAccount: 'Account D',
                amount: 75.50,
                time: new Date().toLocaleString(),
            },
            // Add more transactions as needed
        ];

        setTransactions(fetchedTransactions);
    }, []);

    return (
        <div>
  
            <div className="transaction-container">
                <h1 className="transaction-title">Transaction History</h1>
                <table className="transaction-table">
                    <thead>
                        <tr>
                            <th>Transaction ID</th>
                            <th>From Account</th>
                            <th>To Account</th>
                            <th>Amount</th>
                            <th>Time</th>
                        </tr>
                    </thead>
                    <tbody>
                        {transactions.map(transaction => (
                            <tr key={transaction.transactionId}>
                                <td>{transaction.transactionId}</td>
                                <td>{transaction.fromAccount}</td>
                                <td>{transaction.toAccount || 'N/A'}</td>
                                <td>${transaction.amount.toFixed(2)}</td>
                                <td>{transaction.time}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
};

export default Transactions;