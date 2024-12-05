import React from "react";
import ReactDOM from "react-dom/client";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import "./main.css";
import FrontPage from "./Pages/FrontPage";

const router = createBrowserRouter([
  {
    path: "/",
    element: <FrontPage />,
    children: [
      {
        path: "/",
        element: <FrontPage />,
      }]
  }])

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(

  <RouterProvider router={router} />

);