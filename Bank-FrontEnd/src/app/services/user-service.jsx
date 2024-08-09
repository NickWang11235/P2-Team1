
'use client'
import {User} from '../models/dtos';
import axios from 'axios';

import { useState, useContext, useEffect  } from "react";

const baseUri = `https://freetestapi.com/api/v1/weathers`


export default function UserService(){

    const [weatherData, setWeatherData] = useState(null);

    useEffect(() => {
        fetch(baseUri, {
        method: "GET",
        headers: {
            "Content-Type":"application/json"
        },
        })
        .then((response) => response.json())
        .then((data) => {
            setWeatherData(data[0]);
            console.log(data);
        })
        .catch((error) => console.log(error));
    }, []);

    return(
        <div>
            <p>
                {weatherData.city}
            </p>
        </div>
    )
}