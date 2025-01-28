import { useState, useEffect } from "react"
export default function RegisterComponent({ handleRegister, setRegister }) {
    const [userData, setUserData] = useState({ username: "", password: "", confirmPassword: "", email: "" });


    return (
        <>
            <form className="auth-form" onSubmit={(e) => handleRegister(userData, e)}>
                <button type="button" className="exit-button" onClick={e => setRegister(prev => !prev)}> X </button>
                <label className="auth-label" htmlFor="username-input"> Username:</label>
                <input className="auth-input" type="text" id="username-input" onChange={(e) => setUserData(prev => ({ ...prev, username: e.target.value }))} />
                <label className="auth-label" htmlFor="email-input"> Email:</label>
                <input className="auth-input" type="text" id="email-input" onChange={(e) => setUserData(prev => ({ ...prev, email: e.target.value }))} />
                <label className="auth-label" htmlFor="password-input"> Password:</label>
                <input className="auth-input" type="password" id="password-input" onChange={(e) => setUserData(prev => ({ ...prev, password: e.target.value }))} />
                <label className="auth-label" htmlFor="confirmPassword-input"> Confirm password:</label>
                <input className="auth-input" type="password" id="confirmPassword-input" onChange={(e) => setUserData(prev => ({ ...prev, confirmPassword: e.target.value }))} />
                <button className="submit-button"> Register </button>
            </form>

        </>
    )
}