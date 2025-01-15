import React, { useContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import * as styles from "./Perfil.module.css";
import { VscHome } from "react-icons/vsc";
import { GrMoney } from "react-icons/gr";
import { BsPersonCircle } from "react-icons/bs";
import { FaGear } from "react-icons/fa6";
import { ImExit } from "react-icons/im";
import { jwtDecode } from "jwt-decode";
import { HiPencilAlt } from "react-icons/hi";
import { FaTrashAlt } from "react-icons/fa";
import axios from "axios";
import { AuthContext } from "../../context/AuthContext";

export default function Perfil() {
  const navigation = useNavigate();
  const user = jwtDecode(localStorage.getItem("token"));
  const [finances, setFinances] = useState([]);
  const {logout} = useContext(AuthContext);
  
    const handleLogout = (e) =>{
      e.preventDefault();
      logout();
      navigation("/")
    }

  const handleDeleteF = async (e, id) => {
    e.preventDefault();
    
    
    try {
        const response = await axios.delete(`http://localhost:5086/api/finance/${id}`);

        if (response.status === 204) {
            alert("Finança deletada com sucesso!");
        }
    } catch (error) {
        alert("Erro ao deletar Finança");
    }
  }

  useEffect(() => {
    const fetchFinance = async () => {
      try {
        const response = await axios.get(
          `http://localhost:5086/api/finance/user/${user.sub}`
        );

        if (response.status === 200) {
          setFinances(response.data);
          
        }
      } catch (error) {
        alert("Erro ao buscar finanças")
      }
    }
    fetchFinance();
  }, []);
  return (
    <>
      <div className={styles.container}>
        <div className={styles.sideBar}>
          <div className={styles.sideBarNav}>
            <div
              className={styles.sideBarNavItem}
              onClick={() => navigation("/home")}
            >
              <VscHome size={24} />
              <p>Home</p>
            </div>
            <div
              className={styles.sideBarNavItem}
              onClick={() => navigation("/finances")}
            >
              <GrMoney size={24} />
              <p>Finanças</p>
            </div>
            <div
              className={styles.sideBarNavItem}
              onClick={() => navigation("/perfil")}
            >
              <BsPersonCircle size={24} />
              <p>Perfil</p>
            </div>
            <div
              className={styles.sideBarNavItem}
              onClick={handleLogout}
            >
              <ImExit size={24} />
              <p>Logout</p>
            </div>
          </div>
        </div>
        <div className={styles.principal}>
          <header className={styles.header}>
            <FaGear size={24} className={styles.config} onClick={() => navigation("/userConfig")}/>
            <BsPersonCircle size={48} />
            <h2>{user.name}</h2>
          </header>
          <div className={styles.financesList}>
            {finances.map((f, index) => (
                <div key={index} className={styles.financeItem}>
                    <p>Descrição: {f.description}</p>
                    <p>Categoria: {f.categ === -1? "Despesa" : "Receita"}</p>
                    <p>Valor: {f.value.toFixed(2)}</p>
                    <div className={styles.actions}>
                        <HiPencilAlt size={20} className={styles.edit} onClick={() => navigation(`/editF/${f.id}`)}/>
                        <FaTrashAlt size={20} className={styles.delete} onClick={(e) => handleDeleteF(e, f.id)}/>
                    </div>
                </div>
            ))}
          </div>
        </div>
      </div>
    </>
  );
}
