import { useState, useEffect } from "react";
import { apiWithAuth } from "../Axios/api"
export default function ProfilePage() {

    const [favorites, setFavorites] = useState([]);

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

    return (
        <div className="flex flex-row gap-5 m-12 w-[calc(100vw-6rem)] h-[calc(100vh-6rem)]">
            <div className="flex-[35] bg-white bg-opacity-15 border-2 border-[rgba(255,255,255,0.1)] rounded-3xl">
                <img src="../../public/profile.png" className=" border-2 border-gray-300 rounded-full h-48 block mx-auto mt-4" />
                <div className="px-8 mt-16">
                    <p className="py-1 text-xl font-bold"> Username </p>
                    <p className="py-1 text-xl font-bold"> Email </p>
                    <p className="py-1 text-xl font-bold"> Password </p>
                    <div className="flex flex-row justify-between w-full mt-5">
                        <button className="w-40 h-12 bg-blue-600 text-white font-bold px-4 py-2 rounded mt-5" > Profile Picture</button>
                        <button className="w-40 h-12 bg-blue-600 text-white font-bold px-4 py-2 rounded mt-5"> Reset Password </button>
                    </div>
                </div>
            </div>
            <div className="flex-[65] bg-white bg-opacity-15 border-2 border-[rgba(255,255,255,0.1)] rounded-3xl">
                <table className="w-full table-fixed border-collapse">
                    <thead className="h-16">
                        <tr>
                            <th className="w-1/5"></th>
                            <th className="w-1/5"></th>
                            <th className="w-1/5 text-2xl text-black font-bold">FAVORITES</th>
                            <th className="w-1/5"></th>
                            <th className="w-1/5"></th>
                        </tr>
                    </thead>

                    <tbody>
                        {favorites.map(f =>
                            <tr className="even:bg-slate-300 odd:bg-slate-400">
                                <td className="px-4 py-2 text-black font-bold">{f.solar.city.name}</td>
                                <td className="px-4 py-2 text-black font-bold">{f.solar.date}</td>
                                <td className="px-4 py-2 text-black font-bold">{f.solar.sunrise}</td>
                                <td className="px-4 py-2 text-black font-bold">{f.solar.sunset}</td>
                                <td className="px-4 py-2 text-black font-bold">
                                    <button className="bg-red-500 text-white px-4 py-2 rounded">DELETE</button>
                                </td>
                            </tr>
                        )}

                    </tbody>
                </table>
            </div>
        </div >

    )
}