import { useState, useEffect } from "react"
import { apiWithAuth } from "../Axios/api";
import { useNavigate } from "react-router-dom";
import { jwtDecode } from "jwt-decode";

export default function MainApp() {

    const navigate = useNavigate();
    const [errorMessage, setErrorMessage] = useState(null);
    const [city, setCity] = useState("");
    const [date, setDate] = useState("");
    const [info, setInfo] = useState({});
    const [isAdmin, setIsAdmin] = useState(false);

    const getUserId = () => {
        const token = localStorage.getItem("token");
        const decodedToken = jwtDecode(token);
        return decodedToken.sub;
    };

    useEffect(() => {
        const token = localStorage.getItem("token");
        if (token == null) {
            setErrorMessage("You must login before access this page!");
            setTimeout(() => {
                navigate('/');
            }, 5000);
        }
    }, [navigate])

    useEffect(() => {
        const token = localStorage.getItem("token");
        if (token) {
            const decodedToken = jwtDecode(token);
            if (decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]
                == "Admin") {
                setIsAdmin(true);
            }
        }
    }, [])

    async function handleSubmit(e) {
        e.preventDefault();
        const response = await apiWithAuth.get("/SolarWatch", {
            params: {
                cityName: city,
                date: date
            }
        });
        const data = response.data;
        setInfo(data);
    }

    function handleLogout() {
        localStorage.removeItem("token");
        navigate("/")
    }

    return (
        <>
            {errorMessage ? (<h1 className="error-message"> {errorMessage} </h1>) : (
                <>
                    <div className="main-header">
                        {isAdmin && <button className="admin-button" onClick={() => navigate("/admin")}> ADMIN INTERFACE </button>}
                        <button className="logout-button" onClick={() => handleLogout()}> LOGOUT </button>
                        <img className="profile-image" src="/profile.png" onClick={() => navigate(`/profile/${getUserId()}`)} />
                    </div>
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
                            {Object.keys(info).length == 0 ? "" : (
                                <>
                                    <div className="solar-info-top">
                                        <p className="city-info"> {info.city.name} - {info.date}</p>
                                    </div>
                                    <div className="solar-info-bottom">
                                        <div className="left">
                                            <p className="solar-data"> {info.sunrise} </p>
                                            <p className="solar-data"> SUNRISE </p>
                                        </div>
                                        <button className="add-to-favorite"> Add To Favorite </button>
                                        <div className="right">
                                            <p className="solar-data"> {info.sunset} </p>
                                            <p className="solar-data"> SUNSET </p>
                                        </div>
                                    </div>
                                </>
                            )
                            }
                        </div>
                    </div>
                </>
            )}
        </>
    )
}