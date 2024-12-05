import { useState, useEffect } from "react"
import LoginComponent from "../Components/LoginComponent";
import RegisterComponent from "../Components/RegisterComponent";
export default function Page() {
    const [login, setLogin] = useState(false);
    const [register, setRegister] = useState(false);

    return (
        <div className="front-page-div">
            <div className="title-div">
                <h1 className="title"> SolarWatch App </h1>
            </div>
            <div className="auth-buttons-div">
                <button className="auth-buttons" onClick={() => setRegister(prev => !prev)}> Register </button>
                <button className="auth-buttons" onClick={() => setLogin(prev => !prev)}> Login </button>
            </div>
            {login ? <LoginComponent /> : ""}
            {register ? <RegisterComponent handleRegister={handleRegister} /> : ""}
        </div>


    )
}
async function handleRegister(userData, e) {
    e.preventDefault();
    const response = await fetch("https://localhost:44325/Auth/register", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            username: userData.username,
            password: userData.password,
            email: userData.email
        })
    })
    const data = await response.json();
    console.log(data);
}

function handleLogin(userData) {

}