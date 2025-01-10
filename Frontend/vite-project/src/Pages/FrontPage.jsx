import { useState, useEffect } from "react"
import { useNavigate } from "react-router-dom";
import LoginComponent from "../Components/LoginComponent";
import RegisterComponent from "../Components/RegisterComponent";
import { api } from "../Axios/api"

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
<<<<<<< HEAD
        const response = await fetch("http://backend-1:8080/Auth/login", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
=======
        try {
            const response = await api.post("/Auth/login", {
>>>>>>> main
                password: userData.password,
                email: userData.email
            });

            if (response.status === 200) {
                const data = response.data;
                localStorage.setItem('token', data.token);
                navigate("/app");
            }
        } catch (error) {
            console.error("Login failed:", error);
        }
    }

    async function handleRegister(userData, e) {
        e.preventDefault();
        try {
            const response = await api.post("/Auth/register", {
                username: userData.username,
                password: userData.password,
                email: userData.email
            });
            if (response.status === 200) {
                setIsSuccessful(true);
            }
        } catch (error) {
            console.error("Registration failed:", error);
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

