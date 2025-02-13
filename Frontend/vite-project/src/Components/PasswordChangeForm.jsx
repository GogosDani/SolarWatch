import { useState } from "react";
import { apiWithAuth } from "../Axios/api";

export default function PasswordChangeForm({ setShowPwdChangeForm, setSuccessPwdChange }) {

    const [changePwdData, setChangePwdData] = useState({ confirmNewPassword: "", newPassword: "", currentPassword: "" });
    const [errorMessage, setErrorMessage] = useState("");

    async function changePassword(e) {
        try {
            e.preventDefault();
            if (changePwdData.confirmNewPassword != changePwdData.newPassword) {
                setErrorMessage("New passwords must match");
                return;
            }
            if (changePwdData.newPassword.length < 8) {
                setErrorMessage("New password too short");
                return;
            }
            const response = await apiWithAuth.patch("/api/user", {
                currentPassword: changePwdData.currentPassword,
                newPassword: changePwdData.newPassword
            });
            if (response.status === 200) {
                setShowPwdChangeForm(false);
                setSuccessPwdChange(true);
                setTimeout(() => {
                    setSuccessPwdChange(false)
                }, 3000)
            }
        } catch (error) {
            if (error.response && error.response.data) {
                const firstError = Object.values(error.response.data)
                setErrorMessage(firstError || "An error occurred during registration.");
            } else {
                setErrorMessage("Unexpected error. Please try again.");
            }
        }
    }

    return (
        <form className="auth-form" onSubmit={(e) => changePassword(e)}>
            <div className="flex flex-row gap-2">
                <button type="button" className="exit-button" onClick={e => setShowPwdChangeForm(prev => !prev)}> X </button>
                <div className="text-red-700"> {errorMessage} </div>
            </div>
            <div className="container">
                <label className="auth-label" htmlFor="username-input"> Current password:</label>
                <input className="auth-input" type="password" id="email-input" onChange={(e) => setChangePwdData(prev => ({ ...prev, currentPassword: e.target.value }))} />
                <label className="auth-label" htmlFor="email-input"> New password:</label>
                <input className="auth-input" type="password" id="password-input" onChange={(e) => setChangePwdData(prev => ({ ...prev, newPassword: e.target.value }))} />
                <label className="auth-label" htmlFor="email-input"> Confirm new password:</label>
                <input className="auth-input" type="password" id="password-input" onChange={(e) => setChangePwdData(prev => ({ ...prev, confirmNewPassword: e.target.value }))} />
                <button className="submit-button"> CHANGE </button>
            </div>
        </form>
    )
}