import { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { jwtDecode } from "jwt-decode";
import CityForm from "../Components/CityForm"
import { apiWithAuth } from "../Axios/api";

export default function CityEditor() {
    const { id } = useParams();
    const [city, setCity] = useState({});
    const navigate = useNavigate();

    useEffect(() => {
        async function getCity() {
            const response = await apiWithAuth.get(`/api/city/${id}`);
            const data = await response.data
            setCity(prev => data);
        }
        getCity();
    }, [])


    async function editData(newData, e) {
        e.preventDefault();
        const response = await apiWithAuth.put("/api/city",
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
            <button onClick={(e) => navigate("/admin")} className="back-button"> BACK </button>
            <CityForm city={city} apiMethod={editData} />
        </>
    )
}