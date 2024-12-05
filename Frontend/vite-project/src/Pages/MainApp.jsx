import { useState, useEffect } from "react"

export default function MainApp() {

    const [city, setCity] = useState("");
    const [date, setDate] = useState("");

    async function handleSubmit(e) {
        e.preventDefault();
        const response = await fetch(`https://localhost:44325/SolarWatch?cityName=${city}&date=${date}`);
        const data = await response.json();
        console.log(data);
    }

    return (
        <div className="search-form-div">
            <form onSubmit={(e) => handleSubmit(e)}>
                <label> Type a city here </label>
                <input required onChange={(e) => setCity(e.target.value)} />
                <label> Add the date </label>
                <input required onChange={(e) => setDate(e.target.value)} type="date" />
                <button disabled={city === "" ? true : false}> Submit </button>
            </form>
        </div>
    )
}