import { useEffect, useState } from "react";
// This component used in both creator and updater page
export default function SolarForm({ apiMethod }) {

    const [solarData, setSolarData] = useState({});
    const [validTime, setValidTime] = useState(false)
    const [errorMessage, setErrorMessage] = useState(null);

    useEffect(() => {
        setValidTime(solarData.sunrise &&
            timeFormatValidator(solarData.sunrise) &&
            solarData.sunset &&
            timeFormatValidator(solarData.sunset))
    }, [solarData])


    useEffect(() => {
        if (validTime == false) setErrorMessage("Please provide the correct time format!");
        else {
            setErrorMessage(null);
        }
    }, [validTime])

    return (
        <div className="search-form-div">
            {errorMessage && (<p className="format-error-message"> {errorMessage} </p>)}
            <form className="search-form" onSubmit={(e) => apiMethod(solarData, e)}>
                <label className="search-form-label"> City Id </label>
                <input className="search-form-input" type="number" onChange={(e) => setSolarData(prev => ({ ...prev, cityId: e.target.value }))} />
                <label className="search-form-label"> Date </label>
                <input className="search-form-input" type="date" onChange={(e) => setSolarData(prev => ({ ...prev, date: e.target.value }))} />
                <label className="search-form-label"> Sunrise </label>
                <input className="search-form-input" type="text" onChange={(e) => setSolarData(prev => ({ ...prev, sunrise: e.target.value }))} />
                <label className="search-form-label"> Sunrise </label>
                <input className="search-form-input" type="text" onChange={(e) => setSolarData(prev => ({ ...prev, sunset: e.target.value }))} />
                <button disabled={!validTime} className="search-submit"> Submit </button>
            </form>
        </div >
    )
}

function timeFormatValidator(date) {
    if (date.length == 0) return false;
    if (date.split(" ").pop() != "AM" && date.split(" ").pop() != "PM") return false;
    if (date.split(":")[0].length != 1 && date.split(":")[1] != 2 && date.split(":")[2] != 2) return false;
    return true;
}
