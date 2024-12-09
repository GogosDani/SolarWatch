import { useNavigate } from "react-router-dom"

export default function AdminHeader() {
    const navigate = useNavigate();
    return (
        <div className="admin-header">
            <button className="admin-interface-buttons" onClick={() => navigate("/app")}> BACK </button>
            <button className="admin-interface-buttons" onClick={() => navigate("/create/city")}> ADD CITY </button>
            <button className="admin-interface-buttons" onClick={() => navigate("/create/solar")}> ADD SOLAR </button>
        </div>
    )
}