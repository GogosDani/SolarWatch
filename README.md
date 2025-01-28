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

### Without Docker

1. Start the database using Docker:
           docker-compose up -d db
   
2, Navigate to the backend directory and start the backend server:

        cd SolarWatch
        dotnet run

3, Navigate to the frontend directory and start the React app:
        cd frontend/vite-project
        npm install
        npm start

### Using Docker
1, Build and run the containers using Docker Compose:
        docker-compose build
        docker-compose up





