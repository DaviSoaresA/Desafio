import React from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import Login from "../pages/Login";
import Register from "../pages/Register";
import Home from "../pages/Home";
import Finances from "../pages/Finances";
import AddFinance from "../pages/AddFinance";
import Perfil from "../pages/Perfil";
import UserConfig from "../pages/UserConfig";
import EditFinance from "../pages/EditFinance";

export default function AppRoutes() {
  return (
    <>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/signin" element={<Register />} />
        <Route path="/home" element={<Home />} />
        <Route path="/finances" element={<Finances />} />
        <Route path="/addF" element={<AddFinance />} />
        <Route path="/perfil" element={<Perfil />} />
        <Route path="/userConfig" element={<UserConfig />} />
        <Route path="/editF/:id" element={<EditFinance />} />
      </Routes>
    </>
  );
}
