import React from "react";
import ReactDOM from "react-dom/client";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import "./main.css";
import FrontPage from "./Pages/FrontPage";
import MainApp from "./Pages/MainApp";
import AdminPage from "./Pages/AdminPage";
import CityCreator from "./Pages/CityCreator";
import SolarCreator from "./Pages/SolarCreator";
import SolarEditor from "./Pages/SolarEditor";
import CityEditor from "./Pages/CityEditor";
import ProfilePage from "./Pages/ProfilePage";
import "./index.css";

const router = createBrowserRouter([
  {
    path: "/",
    element: <FrontPage />
  },
  {
    path: "/app",
    element: <MainApp />
  },
  {
    path: "/admin",
    element: <AdminPage />
  },
  {
    path: "/create/city",
    element: <CityCreator />
  },
  {
    path: "/create/solar",
    element: <SolarCreator />
  },
  {
    path: "/edit/solar/:id",
    element: <SolarEditor />
  },
  {
    path: "/edit/city/:id",
    element: <CityEditor />
  },
  {
    path: "/profile/:userId",
    element: <ProfilePage />
  }
])

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
  <RouterProvider router={router} />
);