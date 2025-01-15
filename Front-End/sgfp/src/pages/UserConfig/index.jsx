import axios from "axios";
import { jwtDecode } from "jwt-decode";
import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { IoArrowBackSharp } from "react-icons/io5";
import * as styles from "./UserConfig.module.css";

export default function UserConfig() {
  const navigation = useNavigate();
  const [email, setEmail] = useState("");
  const [name, setName] = useState("");
  const user = jwtDecode(localStorage.getItem("token"));

  const handleDelete = async (e) => {
    e.preventDefault();
    try {
        const response = await axios.delete(`http://localhost:5086/api/user/${user.sub}`);
    
          if (response.status === 204) {
            alert("Conta Desfeita com sucesso");
            navigation("/");
          }
    } catch (error) {
        alert("Erro ao tentar desfazer a conta");
    }
  }

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      if (email === null || name === null) {
        alert("Preencha todos os campos!");
        return;
      }

      const credentials = {
        Name: name,
        Email: email,
      };

      const response = await axios.put(
        `http://localhost:5086/api/user/${user.sub}`,
        credentials
      );

      if (response.status === 204) {
        alert("Alteração feita com sucesso");
        navigation("/perfil");
      }
    } catch (error) {
      alert("Erro ao tentar mudar as informações");
    }
  };

  function handleEmail(e) {
    setEmail(e.target.value);
  }
  function handleName(e) {
    setName(e.target.value);
  }

  return (
    <>
      <div className={styles.container}>
        <div className={styles.signForm}>
          <form className={styles.form}>
            <IoArrowBackSharp size={30} className={styles.back} onClick={() => navigation("/perfil")}/>
            <h2 className={styles.title}>Altere ou Desfaça a conta</h2>
            <input
              type="text"
              placeholder="Digite o novo Nome"
              aria-label="Nome"
              onChange={handleName}
              className={styles.inputLog}
            />
            <input
              type="text"
              placeholder="Digite o novo Email"
              aria-label="Email"
              onChange={handleEmail}
              className={styles.inputLog}
            />
            <div className={styles.buttons}>
              <button
                type="submit"
                className={styles.buttonSign}
                onClick={handleSubmit}
              >
                Alterar
              </button>
              <button
                type="submit"
                className={styles.buttonDelete}
                onClick={handleDelete}
              >
                Desfazer Conta
              </button>
            </div>
          </form>
        </div>
      </div>
    </>
  );
}
