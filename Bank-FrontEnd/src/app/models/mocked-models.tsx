import { User,Account,Transaction } from "./dtos";



export const user1: User ={
    UserId: 1,
    Password: 'notPassword123!',
    Name: "Mr. Whiskers",
    Accounts: [],
    // ImageUrl: "https://images.pexels.com/photos/45201/kitty-cat-kitten-pet-45201.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500",
};


export const user2: User ={
    UserId: 2,
    Password: 'notPassword123!',
    Name: "Lil' Fretcho",
    Accounts: [],
    // ImageUrl: "https://i.natgeofe.com/n/548467d8-c5f1-4551-9f58-6817a8d2c45e/NationalGeographic_2572187_square.jpg",
};


export const account1: Account = {
    AccountId: 69,
    Balance: 58.96,
    AccountType: "CLOWN",
    PrimaryUserId: 1,
    Users: [user1],
}


export const account2: Account = {
    AccountId: 666,
    Balance: 86.86,
    AccountType: "CHECKING",
    PrimaryUserId: 1,
    Users: [user1,user2],
}

export const account3: Account = {
    AccountId: 42,
    Balance: 3000.96,
    AccountType: "SAVING",
    PrimaryUserId: 2,
    Users: [user2],
}
export const account4: Account = {
    AccountId: 48,
    Balance: 11.96,
    AccountType: "CLOWN",
    PrimaryUserId: 2,
    Users: [user1,user2],
}
export const account5: Account = {
    AccountId: 420,
    Balance: 1234.56,
    AccountType: "CHECKING",
    PrimaryUserId: 2,
    Users: [user2],
}

user1.Accounts=[account1,account2,account4];
user2.Accounts=[account2,account3,account4,account5]