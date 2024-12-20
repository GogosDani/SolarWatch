import { useState, useEffect } from "react"
import { useNavigate } from "react-router-dom";
import { jwtDecode } from "jwt-decode";
import AdminHeader from "../Components/AdminHeader"
import api from "../Axios/api";
import './AdminPage.scss'

export default function AdminPage() {
    const [cities, setCities] = useState([]);
    const [solars, setSolars] = useState([]);
    const [cityPage, setCityPage] = useState(1);
    const [solarPage, setSolarPage] = useState(1);
    const [errorMessage, setErrorMessage] = useState("");


    // Check if the logged user have admin role
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

    // Get city datas by page
    useEffect(() => {
        async function GetCityDatas() {
            const response = await api.get(`/api/city/${cityPage}`)
            setCities(prev => response.data);
        }
        GetCityDatas();
    }, [cityPage])


    // Get solar datas by page
    useEffect(() => {
        async function GetSolarDatas() {
            const response = await api.get(`/api/solar/${solarPage}`)
            setSolars(prev => response.data);
        }
        GetSolarDatas();
    }, [solarPage])

    async function deleteCity(id) {
        const response = await api.delete(`/api/city/${id}`);
        setCities(prev => prev.filter(c => c.id != id));
    }

    async function deleteSolar(id) {
        const response = await api.delete(`/api/solar/${id}`);
        setSolars(prev => prev.filter(s => s.id != id));
    }


    return (
        <>
            {errorMessage ? (<h1 className="error-message"> {errorMessage} </h1>) : (
                <>
                    <AdminHeader />
                    <div className="admin-page-div">
                        <table className="solars">
                            <thead>
                                <tr>
                                    <th> ID </th>
                                    <th> DATE </th>
                                    <th> SUNSET </th>
                                    <th> SUNRISE </th>
                                    <th> CITYID </th>
                                    <th> EDIT </th>
                                    <th> DELETE </th>
                                </tr>
                            </thead>
                            <tbody>
                                {solars.map(solar => <tr key={solar.id}>
                                    <td> {solar.id} </td>
                                    <td> {solar.date} </td>
                                    <td> {solar.sunset} </td>
                                    <td> {solar.sunrise} </td>
                                    <td> {solar.cityId} </td>
                                    <td ><button className="edit-button"> EDIT </button></td>
                                    <td><button className="delete-button" onClick={(e) => deleteSolar(solar.id)} > DELETE </button></td>
                                </tr>
                                )}
                                <tr className="nav-buttons-div">
                                    <td>  <button className="nav-buttons" onClick={() => solarPage == 1 ? "" : setSolarPage(prev => prev - 1)}> PREV </button></td>
                                    <td> <button className="nav-buttons" onClick={() => solars.length < 8 ? "" : setSolarPage(prev => prev + 1)}> NEXT </button> </td>
                                </tr>

                            </tbody>

                        </table>
                        <table className="cities">
                            <thead>
                                <tr>
                                    <th> ID </th>
                                    <th> NAME </th>
                                    <th> LONGITUDE </th>
                                    <th> LATITUDE </th>
                                    <th> EDIT </th>
                                    <th> DELETE </th>
                                </tr>
                            </thead>
                            <tbody>
                                {cities.map(city => <tr key={city.id}>
                                    <td> {city.id} </td>
                                    <td> {city.name} </td>
                                    <td> {city.longitude} </td>
                                    <td> {city.latitude} </td>
                                    <td ><button className="edit-button"> EDIT </button></td>
                                    <td><button className="delete-button" onClick={(e) => deleteCity(city.id)}> DELETE </button></td>
                                </tr>)}
                                <tr className="nav-buttons-div">
                                    <td> <button className="nav-buttons" onClick={() => cityPage == 1 ? "" : setCityPage(prev => prev - 1)}> PREV </button> </td>
                                    <td> <button className="nav-buttons" onClick={() => cities.length < 8 ? "" : setCityPage(prev => prev + 1)}> NEXT </button>  </td>
                                </tr>
                            </tbody>
                        </table>
                    </div >
                </>
            )}
        </>
    )
}




