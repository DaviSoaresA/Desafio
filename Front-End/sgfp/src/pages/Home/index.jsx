import React, { useContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { VscHome } from "react-icons/vsc";
import { GrMoney } from "react-icons/gr";
import { BsPersonCircle } from "react-icons/bs";
import { MdOutlineAccountBalanceWallet } from "react-icons/md";
import { BsPlusCircle } from "react-icons/bs";
import { ImExit } from "react-icons/im";
import * as styles from "./Home.module.css";
import { jwtDecode } from "jwt-decode";
import axios from "axios";
import { AuthContext } from "../../context/AuthContext";

export default function Home() {
  const navigation = useNavigate();
  const [balance, setBalance] = useState(0);
  const {logout} = useContext(AuthContext);
  
    const handleLogout = (e) =>{
      e.preventDefault();
      logout();
      navigation("/")
    }

  useEffect(() => {
    const user = jwtDecode(localStorage.getItem("token"));

    const fetchBalance = async () => {
      try {
        const response = await axios.get(
          `http://localhost:5086/api/finance/user/${user.sub}`
        );

        if (response.status === 200) {
          const count = response.data.reduce((acc, t) => {
            if (t.categ === -1) {
              return acc + t.value * -1;
            } else {
              return acc + t.value;
            }
          }, 0);
          setBalance(count);
        }
      } catch (error) {
        alert("Erro ao buscar saldo do usuário.");
      }
    }
    fetchBalance();
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
            <VscHome size={48} />
            <h2>Home</h2>
          </header>
          <div className={styles.balance}>
            <MdOutlineAccountBalanceWallet size={48} />
            <BsPlusCircle
              size={50}
              className={styles.addFinance}
              onClick={() => navigation("/addF")}
            />
            <p>R$ {balance.toFixed(2)}</p>
          </div>
        </div>
      </div>
    </>
  );
}
