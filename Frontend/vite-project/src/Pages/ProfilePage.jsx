export default function ProfilePage() {
    return (
        <div className="flex flex-row gap-5 m-12 w-[calc(100vw-6rem)] h-[calc(100vh-6rem)]">
            <div className="flex-[35] bg-red-300"></div>
            <div className="flex-[65] bg-red-500">
                <table class="table-auto w-full">
                    <thead>
                        <tr>
                            <th class="w-full">FAVORITES</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr class="w-full">
                            <td class="px-4 py-2">Budapest</td>
                            <td class="px-4 py-2">2024.01.25</td>
                            <td class="px-4 py-2">6:53:63 AM</td>
                            <td class="px-4 py-2">9:53:52 PM</td>
                            <td class="px-4 py-2">
                                <button class="bg-blue-500 text-white px-4 py-2 rounded">DELETE</button>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div >

    )
}