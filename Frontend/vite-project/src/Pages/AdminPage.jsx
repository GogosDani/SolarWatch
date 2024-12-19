import { useState, useEffect } from "react"
import AdminHeader from "../Components/AdminHeader"
import './AdminPage.scss'

export default function AdminPage() {
    const [cities, setCities] = useState({});
    const [solars, setSolars] = useState({});

    useEffect(() => {

    }, [])

    useEffect(() => {

    }, [])

    return (
        <>
            <AdminHeader />
            <div className="admin-page-div">
                <div className="solars">
                    <div className="solar">
                        <p> a </p>
                        <p> b </p>
                    </div>
                    <div className="solar">
                        <p> c </p>
                        <p> d </p>
                    </div>
                </div>
                <div className="cities">
                    <div className="city">
                        <p> Name: Szigethalom</p>
                        <p> Latitude: 10.10</p>
                        <p> Longitude: 10.10</p>

                    </div>
                    <div className="city">
                        <p> Name: Ráckeve</p>
                        <p> Latitude: 10.10</p>
                        <p> Longitude: 41.41</p>
                    </div>
                </div>
            </div>

        </>
    )
}