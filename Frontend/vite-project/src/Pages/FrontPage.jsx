import { useState, useEffect } from "react"
import { useNavigate } from "react-router-dom";
import LoginComponent from "../Components/LoginComponent";
import RegisterComponent from "../Components/RegisterComponent";
import { api } from "../Axios/api"

export default function Page() {
    const [login, setLogin] = useState(false);
    const [register, setRegister] = useState(false);
    const [isSuccessful, setIsSuccessful] = useState(false);
    const [authError, setAuthError] = useState("");
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
        if (authError != "") {
            setTimeout(() => {
                setAuthError("")
            }, 4000);
        }
    }, [authError])


    async function handleLogin(userData, e) {
        e.preventDefault();
        try {
            const response = await api.post("/Auth/login", {
                password: userData.password,
                email: userData.email
            });

            if (response.status === 200) {
                const data = response.data;
                localStorage.setItem('token', data.token);
                navigate("/app");
            }
        } catch (error) {
            if (error.response && error.response.data) {
                const firstError = Object.values(error.response.data)
                setAuthError(firstError || "An error occurred during registration.");
            } else {
                setAuthError("Unexpected error. Please try again.");
            }
        }
    }

    async function handleRegister(userData, e) {
        e.preventDefault();
        if (userData.password != userData.confirmPassword) {
            setAuthError("Passwords must match!")
            return;
        }
        if (userData.password.length < 8) {
            setAuthError("Password length must be at least 8 character!");
            return;
        }
        try {
            const response = await api.post("/Auth/register", {
                username: userData.username,
                password: userData.password,
                email: userData.email
            });
            if (response.status === 201) {
                setIsSuccessful(true);
            }
        } catch (error) {
            if (error.response && error.response.data) {
                const firstError = Object.values(error.response.data)
                setAuthError(firstError || "An error occurred during registration.");
            } else {
                setAuthError("Unexpected error. Please try again.");

            }
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
            {authError != "" && (
                <div className="failed-popup">
                    <p className="failed-text">{authError}!</p>
                </div>
            )}
        </div>


    )
}

