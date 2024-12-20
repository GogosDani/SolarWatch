import CityForm from "../Components/CityForm"
import { useState, useEffect } from "react";
import api from "../Axios/api";
import { useNavigate } from "react-router-dom";
import { jwtDecode } from "jwt-decode";
export default function CityCreator() {

    const navigate = useNavigate();
    const [errorMessage, setErrorMessage] = useState("")

    useEffect(() => {
        const token = localStorage.getItem("token");
        const decodedToken = jwtDecode(token);
        if (decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] != "Admin") {
            setErrorMessage("You are not allowed to visit this page!");
            setTimeout(() => {
                navigate('/');
            }, 5000);
        }
    }, [])

    async function postCity(newData, e) {
        e.preventDefault();
        const response = await api.post("/api/city",
            JSON.stringify(newData),
            {
                headers: {
                    "Content-Type": "application/json"
                }
            }
        );
        if (response.status == 200) {
            navigate("/admin")
        }
    }

    return (
        <>
            {errorMessage ? (<h1 className="error-message"> {errorMessage} </h1>) : (
                <>
                    <button onClick={(e) => navigate("/admin")} className="back-button"> BACK </button>
                    <CityForm apiMethod={postCity} />
                </>

            )}

        </>
    )
}
