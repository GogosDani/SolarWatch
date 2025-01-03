import { useState, useEffect } from "react"
import { useNavigate } from "react-router-dom";
import LoginComponent from "../Components/LoginComponent";
import RegisterComponent from "../Components/RegisterComponent";
export default function Page() {
    const [login, setLogin] = useState(false);
    const [register, setRegister] = useState(false);
    const [isSuccessful, setIsSuccessful] = useState(false);
    const [registrationFailed, setRegistrationFailed] = useState(false);
    const navigate = useNavigate();

    useEffect(() => {
        if (isSuccessful) {
            setRegister(false);
            setTimeout(() => {
                setIsSuccessful(false);
            }, 3000);
        }
    }, [isSuccessful])

    useEffect(() => {
        if (registrationFailed) {
            setTimeout(() => {
                setRegistrationFailed(false);
            }, 3000);
        }
    }, [registrationFailed])


    async function handleLogin(userData, e) {
        e.preventDefault();
        const response = await fetch("https://localhost:44325/Auth/login", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                password: userData.password,
                email: userData.email
            })
        })
        if (response.ok) {
            const data = await response.json();
            localStorage.setItem('token', data.token);
            navigate("/app")
        }
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
        if (response.ok) {
            setIsSuccessful(true);
        } else {
            setRegistrationFailed(true);
        }
    }


    return (
        <div className="front-page-div">
            <div className="title-div">
                <h1 className="title"> SolarWatch App </h1>
            </div>
            <div className="auth-buttons-div">
                <button className="auth-buttons" onClick={() => setRegister(prev => !prev)}> Register </button>
                <button className="auth-buttons" onClick={() => setLogin(prev => !prev)}> Login </button>
            </div>
            {login ? <LoginComponent handleLogin={handleLogin} setLogin={setLogin} /> : ""}
            {register ? <RegisterComponent handleRegister={handleRegister} setRegister={setRegister} /> : ""}
            {isSuccessful && (
                <div className="success-popup">
                    <p className="success-text">Registration successful!</p>
                </div>
            )}
            {registrationFailed && (
                <div className="failed-popup">
                    <p className="failed-text">User already exists!</p>
                </div>
            )}
        </div>


    )
}

