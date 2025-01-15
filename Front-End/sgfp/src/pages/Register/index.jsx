import React from "react";
import * as styles from "./Register.module.css";
import { FiEye } from "react-icons/fi";
import { FiEyeOff } from "react-icons/fi";
import { useState } from "react";
import "../../global.css";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import { IoArrowBackSharp } from "react-icons/io5";

export default function Register() {
  const [showPassword, setShowPassword] = useState(false);
  const [email, setEmail] = useState("");
  const [name, setName] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const navigation = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      if (email === null || password === null || name === null || confirmPassword === null) {
        alert("Preencha todos os campos!");
        return;
      }

      const credentials = {
        Name: name,
        Email: email,
        Password: password,
        ConfirmPassword: confirmPassword
      };

      const response = await axios.post(
        "http://localhost:5086/api/user",
        credentials
      );

      if (response.status >= 200 && response < 300) {
        alert("Bem-Vindo!" + response.data.name);
        navigation("/login");
      } else {
        alert("Erro ao fazer cadastro!");
      }
    } catch (error) {
      alert("Erro: ", error);
    }
  };

  function handleEmail(e) {
    setEmail(e.target.value);
  }

  function handlePassword(e) {
    setPassword(e.target.value);
  }
  function handleConfirmPassword(e) {
    setConfirmPassword(e.target.value);
  }
  function handleName(e) {
    setName(e.target.value);
  }

  return (
    <>
      <div className={styles.container}>
        <div className={styles.signForm}>
          <form className={styles.form}>
          <IoArrowBackSharp size={30} className={styles.back} onClick={() => navigation("/")}/>
            <h2 className={styles.title}>
              Cadastre-se
            </h2>
            <input
              type="text"
              placeholder="Digite o seu Nome"
              aria-label="Nome"
              onChange={handleName}
              className={styles.inputLog}
            />
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
            <div className={styles.passwordArea}>
              <input
                type={showPassword ? "text" : "password"}
                placeholder="Confirme a sua senha"
                aria-label="Confirma Senha"
                onChange={handleConfirmPassword}
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
            <button type="submit" className={styles.buttonSign} onClick={handleSubmit}>Cadastrar-se</button>
          </form>
        </div>
      </div>
    </>
  );
}
