# SOLARWATCH

## About The Project

SOLARWATCH allows users to register and log in to explore sunrise and sunset data for any city in the world on a specific date.  

### Key Features:
- **User Registration and Login**: Users can create an account and log in to access features.
- **Sunrise and Sunset Data**: Enter a city and date to retrieve accurate sunrise and sunset information.
- **Admin Page**: Registered admins can:
  - Add, edit, or delete data in the database.
  - Access admin-only features through a protected interface.

## Built With

- **Backend**: [ASP.NET Core](https://dotnet.microsoft.com/en-us/apps/aspnet) (with Identity Framework and Entity Framework)
- **Frontend**: [React.js](https://reactjs.org/)
- **Containerization**: [Docker](https://www.docker.com/) and Docker Compose
- **Deployment**: [AWS](https://aws.amazon.com/)

---

## Screenshots

### Main User Interface
![Main User Interface](https://github.com/user-attachments/assets/8ba6218e-395e-41db-893f-4a7d91242cd3)

### Forecast Input Screen
![Forecast Input Screen](https://github.com/user-attachments/assets/2495d7a6-92fb-41ef-9c33-2b66ba03a3ca)

### Admin Dashboard
![Admin Dashboard](https://github.com/user-attachments/assets/8aaa5f61-7671-4ec0-8ad2-565475b7d1ff)

---

## Prerequisites

Make sure you have the following installed:
1. [Docker](https://www.docker.com/)
2. [Node.js](https://nodejs.org/)
3. [.NET 8 SDK](https://dotnet.microsoft.com/)

---

## Start the App

0. : Prepare dotenv file
        If you're running the app using Docker, go to the main SolarWatch folder. Copy the .env.sample file and rename it to .env.
        If you're running the app without Docker, go to the SolarWatchBackend folder. Copy the .env.sample file and rename it to .env.
        These .env files contain the necessary environment variables for the app to function properly.

### Using Docker

1. Build docker compose: docker compose build

2. Run DB: docker compose up db

3. Open backend folder, use migrations: cd SolarWatchBackend --> dotnet ef database update --context "SolarApiContext"     2, dotnet ef database update --context "UsersContext"

4. Create Frontend .env file, in frontend/vite-project folder, according to the .env.sample
        Should contain the backend URL

5. Step back, run docker compose: cd ..  --> docker compose up

6. Access the app: Open your browser, navigate to http://localhost:4000.


### With Terminal

1. Start the database using Docker:

           docker-compose up -d db

   
2. Navigate to the backend directory and use migrations:

        cd SolarWatchBackend
        dotnet ef database update --context "SolarApiContext"
        dotnet ef database update --context "UsersContext"

3. start the backend server:

        dotnet run
        remember the server URL

4. Navigate to the frontend directory and create .env according to the .env.sample

        cd ..
        cd frontend/vite-project
        .ENV file should contain the backend URL.

5. Install npm and start the server

        npm install
        npm run dev

6. Access the app: Open your browser, navigate to http://localhost:4000.




