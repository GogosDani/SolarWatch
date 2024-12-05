import { useState, useEffect } from "react"
export default function RegisterComponent({ handleRegister, setRegister }) {
    const [userData, setUserData] = useState({ username: "", password: "", confirmPassword: "", email: "" });
    const [samePasswords, setSamePasswords] = useState(true);
    const [isPasswordValid, setIsPasswordValid] = useState(false);
    useEffect(() => {
        setSamePasswords(userData.password === userData.confirmPassword);
    }, [userData.password, userData.confirmPassword])

    useEffect(() => {
        setIsPasswordValid(userData.password.length >= 8);
    }, [userData.password])


    return (
        <>
            <form onSubmit={(e) => handleRegister(userData, e)}>
                <button type="button" className="exit-button" onClick={e => setRegister(prev => !prev)}> X </button>
                <label htmlFor="username-input"> Username:</label>
                <input type="text" id="username-input" onChange={(e) => setUserData(prev => ({ ...prev, username: e.target.value }))} />
                <label htmlFor="email-input"> Email:</label>
                <input type="text" id="email-input" onChange={(e) => setUserData(prev => ({ ...prev, email: e.target.value }))} />
                <label htmlFor="password-input"> Password:</label>
                <input type="password" id="password-input" onChange={(e) => setUserData(prev => ({ ...prev, password: e.target.value }))} />
                <label htmlFor="confirmPassword-input"> Confirm password:</label>
                <input type="password" id="confirmPassword-input" onChange={(e) => setUserData(prev => ({ ...prev, confirmPassword: e.target.value }))} />
                <button disabled={!samePasswords || !isPasswordValid} className="submit-button"> Register </button>
                <div className="error-messages">
                    {samePasswords ? "" : <p> Password inputs must match! </p>}
                    {isPasswordValid ? "" : <p> Password must be at least 8 characters long! </p>}
                </div>
            </form>
        </>
    )
}