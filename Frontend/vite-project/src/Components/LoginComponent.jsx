import { useState } from "react"
export default function LoginComponent({ handleLogin, setLogin }) {
    const [userData, setUserData] = useState({ password: "", email: "" });

    return (
        <>
            <form className="auth-form" onSubmit={(e) => handleLogin(userData, e)}>
                <button type="button" className="exit-button" onClick={e => setLogin(prev => !prev)}> X </button>
                <div className="container">
                    <label className="auth-label" htmlFor="username-input"> Email:</label>
                    <input className="auth-input" type="text" id="email-input" onChange={(e) => setUserData(prev => ({ ...prev, email: e.target.value }))} />
                    <label className="auth-label" htmlFor="email-input"> Password:</label>
                    <input className="auth-input" type="password" id="password-input" onChange={(e) => setUserData(prev => ({ ...prev, password: e.target.value }))} />
                    <button className="submit-button"> Login </button>
                </div>
            </form>
        </>
    )
}