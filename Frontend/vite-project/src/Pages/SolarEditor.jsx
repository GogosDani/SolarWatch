import { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { jwtDecode } from "jwt-decode";
import SolarDataForm from "../Components/SolarDataform"
import { apiWithAuth } from "../Axios/api";

export default function SolarEditor() {
    const { id } = useParams();
    const [solar, setSolar] = useState({});
    const navigate = useNavigate();

    useEffect(() => {
        async function getSolar() {
            const response = await apiWithAuth.get(`/api/solar/getbyid/${id}`);
            const data = await response.data
            setSolar(prev => data);
        }
        getSolar();
    }, [])


    async function editData(newData, e) {
        e.preventDefault();
        const response = await apiWithAuth.put("/api/solar",
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
            <SolarDataForm solar={solar} apiMethod={editData} />
        </>
    )
}