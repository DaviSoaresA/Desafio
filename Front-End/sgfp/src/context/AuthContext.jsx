import { createContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

export const AuthContext = createContext();

export const AuthProvider = ({children}) => {
    const [authToken, setAuthToken] = useState(null);
    const [user, setUser] = useState(null);

    useEffect(() =>{
        const token = localStorage.getItem("token");
        if (token) {
            setAuthToken(token);
            const userData = parseJwt(token);
            setUser(userData);
        }
    }, []);

    const login = (token) => {
        setAuthToken(token);
        localStorage.setItem("token", token);
        const userData = parseJwt(token);
        setUser(userData);
    };

    const logout = () => {
        setAuthToken(null);
        setUser(null);

        localStorage.removeItem("token");
    };

    
    return (
        <AuthContext.Provider value={{ authToken, user, login, logout}}>
            {children}
        </AuthContext.Provider>
    )
}
    const parseJwt = (token) => {
        try {
            const base64Url = token.split(".")[1];
            const base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/");
            const jsonPayLoad = decodeURIComponent(atob(base64).split("").map((c) => `%${("00" + c.charCodeAt(0).toString(16)).slice(-2)}`).join(""));
            return JSON.parse(jsonPayLoad);
        } catch (error) {
            console.error("Erro na autenticação: ", e);
            return null;
        }
    };