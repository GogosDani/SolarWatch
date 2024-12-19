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
                        <tr>
                            <td> a </td>
                            <td> b </td>
                            <td> a </td>
                            <td> b </td>
                            <td> b </td>
                            <td ><button className="edit-button"> EDIT </button></td>
                            <td><button className="delete-button"> DELETE </button></td>
                        </tr>
                        <tr>
                            <td> a </td>
                            <td> b </td>
                            <td> a </td>
                            <td> b </td>
                            <td> b </td>
                            <td ><button className="edit-button"> EDIT </button></td>
                            <td><button className="delete-button"> DELETE </button></td>
                        </tr>
                        <div className="nav-buttons-div">
                            <button className="nav-buttons"> PREV </button>
                            <button className="nav-buttons"> NEXT </button>
                        </div>
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
                        <tr>
                            <td> 1 </td>
                            <td> Szigethalom</td>
                            <td> 10.10</td>
                            <td> 10.10</td>
                            <td ><button className="edit-button"> EDIT </button></td>
                            <td><button className="delete-button"> DELETE </button></td>

                        </tr>
                        <tr>
                            <td> 2 </td>
                            <td> Ráckeve</td>
                            <td> 10.10</td>
                            <td> 41.41</td>
                            <td ><button className="edit-button"> EDIT </button></td>
                            <td><button className="delete-button"> DELETE </button></td>
                        </tr>
                        <div className="nav-buttons-div">
                            <button className="nav-buttons"> PREV </button>
                            <button className="nav-buttons"> NEXT </button>
                        </div>
                    </tbody>

                </table>
            </div >

        </>
    )
}