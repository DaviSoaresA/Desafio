import React, { useContext } from "react";
import "../../global.css";
import ImagemLogin from "../../assets/ImagemLogin.png";
import * as styles from "./Login.module.css";
import { FiEye } from "react-icons/fi";
import { FiEyeOff } from "react-icons/fi";
import { useState } from "react";
import axios from "axios";
import { AuthContext } from "../../context/AuthContext";
import { useNavigate } from "react-router-dom";

export default function Login() {
  const { login } = useContext(AuthContext);
  const [showPassword, setShowPassword] = useState(false);
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const navigation = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      if (email === null || password === null) {
        alert("Preencha todos os campos!");
      }

      const credentials = {
        Email: email,
        Password: password,
      };

      const response = await axios.post(
        "http://localhost:5086/api/login",
        credentials
      );

      if (response.status === 200) {
        login(response.data.token);
        alert(response.data.message);
        navigation("/home");

      }
    } catch (error) {
      alert("Erro ao fazer login");
    }
  };

  function handleEmail(e) {
    setEmail(e.target.value);
  }
  function handlePassword(e) {
    setPassword(e.target.value);
  }

  return (
    <>
      <div className={styles.container}>
        <img
          src={ImagemLogin}
          alt="Icone de Grupos de pessoas"
          className={styles.image}
        />
        <div className={styles.LoginForm}>
          <form className={styles.form}>
            <h2 className={styles.title}>
              Sistema de Gerenciamento de Finanças Pessoais
            </h2>
            <input
              type="text"
              placeholder="Digite o seu Email"
              aria-label="Email"
              onChange={handleEmail}
              className={styles.inputLog}
            />
            <div className={styles.passwordArea}>
              <input
                type={showPassword ? "text" : "password"}
                placeholder="Digite a sua Senha"
                aria-label="Senha"
                onChange={handlePassword}
              />
              {!showPassword ? (
                <FiEye
                  size={24}
                  className={styles.icon}
                  onClick={() => setShowPassword(true)}
                />
              ) : (
                <FiEyeOff
                  size={24}
                  className={styles.icon}
                  onClick={() => setShowPassword(false)}
                />
              )}
            </div>
            <button type="submit" className={styles.buttonLog} onClick={handleSubmit}>Entrar</button>
            <p className={styles.registerSent}>Não tem uma conta?<span className={styles.registerButton} onClick={() => navigation("/signin")}>Cadastre-se</span></p>
          </form>
        </div>
      </div>
    </>
  );
}
