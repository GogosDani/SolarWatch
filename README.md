# SOLARWATCH

## About The Project

SOLARWATCH allows users to register and log in to explore sunrise and sunset data for any city in the world on a specific date.  

### Key Features:
- **User Registration and Login**: Users can create an account and log in to access features.
- **Sunrise and Sunset Data**: Enter a city and date to retrieve accurate sunrise and sunset information.
- **Admin Page**: Registered admins can:
  - Add, edit, or delete data in the database.
  - Access admin-only features through a protected interface.
- **Profile Page**: Add/Remove favorite solar datas, change profile picture (AWS S3), change password.

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
![Forecast Input Screen](https://github.com/user-attachments/assets/7298bfdf-68f9-46d0-8c5d-e40bc2fb4481)

### Admin Dashboard
![Admin Dashboard](https://github.com/user-attachments/assets/8aaa5f61-7671-4ec0-8ad2-565475b7d1ff)

### Profile Page
![ProfilePage](https://github.com/user-attachments/assets/769eacf7-0df3-455a-8557-8a2cb86a9e88)

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



1. Create Frontend .env file, in frontend/vite-project folder, according to the .env.sample
        Should contain the backend URL

2. Build docker compose:
   
       docker compose build

4. Start docker compose:

         docker compose up

6. Access the app: Open your browser, navigate to http://localhost:4000.


### With Terminal

1. Start database


2. start the backend server:

        dotnet run
        remember the server URL

3. Navigate to the frontend directory and create .env according to the .env.sample

        cd ..
        cd frontend/vite-project
        .ENV file should contain the backend URL.

4. Install npm and start the server

        npm install
        npm run dev

5. Access the app: Open your browser, navigate to http://localhost:4000.




