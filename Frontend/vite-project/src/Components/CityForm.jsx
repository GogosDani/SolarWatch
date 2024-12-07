import { useState } from "react";
// This component used in both creator and updater page
export default function CityForm({ apiMethod }) {

    const [cityData, setCityData] = useState({});

    return (
        <div className="search-form-div">
            <form className="search-form" onSubmit={(e) => apiMethod(cityData, e)}>
                <label className="search-form-label"> Longitude </label>
                <input className="search-form-input" type="number" onChange={(e) => setCityData(prev => ({ ...prev, longitude: e.target.value }))} />
                <label className="search-form-label"> Latitude </label>
                <input className="search-form-input" type="number" onChange={(e) => setCityData(prev => ({ ...prev, latitude: e.target.value }))} />
                <label className="search-form-label"> Name </label>
                <input className="search-form-input" type="text" onChange={(e) => setCityData(prev => ({ ...prev, name: e.target.value }))} />
                <button className="search-submit"> Submit </button>
            </form>
        </div>
    )
}