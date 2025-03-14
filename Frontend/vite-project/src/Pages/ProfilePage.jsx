import { useState, useEffect, useRef } from "react";
import { useParams, useNavigate } from "react-router-dom"
import { jwtDecode } from "jwt-decode";
import { apiWithAuth } from "../Axios/api"
import PasswordChangeForm from "../Components/PasswordChangeForm";
export default function ProfilePage() {

    const [favorites, setFavorites] = useState([]);
    const [userData, setUserData] = useState({});
    const [showPwdChangeForm, setShowPwdChangeForm] = useState(false);
    const [successPwdChange, setSuccessPwdChange] = useState(false);
    const userId = useParams();
    const navigate = useNavigate();
    const fileInputRef = useRef(null);

    const handleButtonClick = () => {
        fileInputRef.current.click();
    };

    const handleFileChange = async (event) => {
        const file = event.target.files[0];
        if (file) {
            const allowedTypes = ["image/jpeg", "image/png", "image/gif"];
            if (!allowedTypes.includes(file.type)) {
                console.error("Incorrect file format!");
                return;
            }
            const formData = new FormData();
            formData.append("picture", file);
            try {
                const response = await apiWithAuth.post("/api/profile-picture", formData, {
                    headers: {
                        "Content-Type": "multipart/form-data",
                    },
                });
                if (response.status !== 200) {
                    console.error("Couldn't change the profile picture");
                } else {
                    console.log("Profile picture updated successfully!");
                    setUserData(prev => ({
                        ...prev,
                        profilePictureUrl: `${response.data}?t=${new Date().getTime()}`
                    }));

                }
            } catch (error) {
                console.error("Error uploading the file:", error);
            }
        }
    };


    // If we are not logged in or if we want to search for other user's page, it redirect us to the home page.
    useEffect(() => {
        const token = localStorage.getItem("token");
        if (token == null) navigate("/")
        const decodedToken = jwtDecode(token);
        if (decodedToken.sub !== userId.userId) {
            navigate('/app');
        }
    }, [navigate])


    useEffect(() => {
        async function getFavorites() {
            const response = await apiWithAuth.get(`/api/favorites`);
            if (response.status == 200) {
                setFavorites(await response.data);
            }
            else {
                console.error("Couldn't get favorites");
            }
        }
        getFavorites();
    }, [])

    useEffect(() => {
        async function getUserDatas() {
            try {
                const response = await apiWithAuth.get("/api/user");
                if (response.status === 200) {
                    setUserData(await response.data);
                }
                else {
                    console.error("Couldn't get user data!")
                }
            }
            catch (error) {
                console.error("Something went wrong while fetching user datas:", error)
            }
        }
        getUserDatas();
    }, [])

    async function deleteFavorite(favoriteId) {
        try {
            const response = await apiWithAuth.delete(`/api/favorites/${favoriteId}`);
            if (response.status === 200) {
                setFavorites(prev => prev.filter(f => f.id != favoriteId));
            }
            else {
                console.error("Couldn't remove favorite!")
            }
        } catch (error) {
            console.error("Something went wrong:", error)
        }
    }



    return (
        <>
            <button type="button" class="text-white bg-[#4285F4] hover:bg-[#4285F4]/90 focus:ring-4 focus:outline-none focus:ring-[#4285F4]/50 font-medium rounded-lg text-sm  text-center w-24 h-8 absolute top-2 left-2" onClick={() => navigate("/app")}> BACK </button>
            <div className="flex flex-row gap-5 mx-12 mt-12 w-[calc(100vw-6rem)] h-[calc(100vh-6rem)]">
                <div className="flex-[35] bg-white bg-opacity-15 border-2 border-[rgba(255,255,255,0.1)] rounded-3xl">
                    <img src={userData.profilePictureUrl != null ? userData.profilePictureUrl : "../public/profile.jpg"} className=" border-2 border-gray-300 rounded-full h-48 w-48 block mx-auto mt-4" />
                    <div className="px-8 mt-16">
                        <p className="py-1 text-xl font-bold"> Username: {userData.userName} </p>
                        <p className="py-1 text-xl font-bold"> Email: {userData.email} </p>
                        <div className="flex flex-row justify-between w-full mt-5">
                            <button onClick={handleButtonClick} className="w-44 h-12 bg-blue-600 text-white font-bold px-4 py-2 rounded mt-5"> Profile Picture</button>
                            <button className="w-44 h-12 bg-blue-600 text-white font-bold px-4 py-2 rounded mt-5" onClick={() => setShowPwdChangeForm(true)}> Change Password </button>
                            <input
                                type="file"
                                ref={fileInputRef}
                                onChange={handleFileChange}
                                style={{ display: 'none' }}
                            />
                        </div>
                        {successPwdChange && <p className="text-green-600 font-bold bg-white mt-2 text-center"> Password changed successfully </p>}
                    </div>
                </div>
                <div className="flex-[65] h-full bg-white bg-opacity-15 border-2 border-[rgba(255,255,255,0.1)] rounded-3xl flex flex-col">
                    <div className="text-black text-2xl font-bold flex-[1] flex justify-center items-center"> FAVORITES </div>
                    <div className="h-full overflow-auto flex-[9] [&::-webkit-scrollbar]:w-2
  [&::-webkit-scrollbar-track]:bg-gray-300
  [&::-webkit-scrollbar-thumb]:bg-gray-500">
                        <table className="w-full table-fixed h-80 border-separate border-spacing-0">
                            <tbody className="h-80">
                                {favorites.map((f, index) =>
                                    <tr key={f.id}>
                                        <td className="px-4 py-2 text-black font-bold">{f.solar.city.name}</td>
                                        <td className="px-4 py-2 text-black font-bold">{f.solar.date}</td>
                                        <td className="px-4 py-2 text-black font-bold">{f.solar.sunrise}</td>
                                        <td className="px-4 py-2 text-black font-bold">{f.solar.sunset}</td>
                                        <td className="px-4 py-2 text-black font-bold">
                                            <button className="bg-red-500 text-white px-4 py-2 rounded" onClick={(e) => deleteFavorite(f.id)}>DELETE</button>
                                        </td>
                                    </tr>
                                )}
                            </tbody>
                        </table>
                    </div >
                </div>
                {showPwdChangeForm && <PasswordChangeForm setShowPwdChangeForm={setShowPwdChangeForm} setSuccessPwdChange={setSuccessPwdChange} />}
            </div>
        </>
    )
}