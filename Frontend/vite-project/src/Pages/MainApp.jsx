import { useState, useEffect } from "react"
import api from "../Axios/api"

export default function MainApp() {

    const [city, setCity] = useState("");
    const [date, setDate] = useState("");

    async function handleSubmit(e) {
        e.preventDefault();
        const response = await api.get("/SolarWatch", {
            params: {
                cityName: city,
                date: date
            }
        });
        const data = response.data;
        console.log(data);
    }

    return (
        <div className="main-app-div">
            <div className="search-form-div">
                <p className="title-solar"> SolarWatch </p>
                <form className="search-form" onSubmit={(e) => handleSubmit(e)}>
                    <label className="search-form-label"> Type a city here </label>
                    <input className="search-form-input" required onChange={(e) => setCity(e.target.value)} />
                    <label className="search-form-label"> Add the date </label>
                    <input className="search-form-input" required onChange={(e) => setDate(e.target.value)} type="date" />
                    <button className="search-submit" disabled={city === "" ? true : false}> Submit </button>
                </form>
            </div>
            <div className="solar-info-div">
                <h2> INFOS </h2>
            </div>
        </div>

    )
}