import React, { useContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { VscHome } from "react-icons/vsc";
import { GrMoney } from "react-icons/gr";
import { BsPersonCircle } from "react-icons/bs";
import { ImExit } from "react-icons/im";
import { Line } from "react-chartjs-2";
import * as styles from "./Finances.module.css";
import axios from "axios";
import { jwtDecode } from "jwt-decode";
import { AuthContext } from "../../context/AuthContext";
import { CategoryScale, Chart, Legend, LinearScale, LineElement, PointElement, Tooltip } from "chart.js";

Chart.register(LineElement, CategoryScale, LinearScale, PointElement, Tooltip, Legend);

export default function Finances() {
  const navigation = useNavigate();
  const [expenses, setExpenses] = useState([]);
  const [revenues, setRevenues] = useState([]);
  const {logout} = useContext(AuthContext);

  const handleLogout = (e) =>{
    e.preventDefault();
    logout();
    navigation("/")
  }
  
  const data = {
    labels: ["Jan", "Feb", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez"],
    datasets: [
      {
        label: "Despesas",
        data: expenses.map((e) => e.value * -1),
        fill: false,
        backgroundColor: "#aa000080",
        borderColor: "#aa0000"
      },
      {
        label: "Receitas",
        data: revenues.map((r) => r.value),
        fill: false,
        backgroundColor: "#00aa0080",
        borderColor: "#00aa00"
      },
    ]
  }

  useEffect(() => {
    const user = jwtDecode(localStorage.getItem("token"));
    
    const fetchExpense = async () =>{
      try{

        const response = await axios.get(`http://localhost:5086/api/finance/expense/${user.sub}`);
        if (response.status === 200) {
          setExpenses(response.data);
        }
      }
      catch (err){
        alert("Erro ao buscar despesas");
      }
    }
    const fetchRevenue = async () =>{
      try{

        const response = await axios.get(`http://localhost:5086/api/finance/revenue/${user.sub}`);
        if (response.status === 200) {
          
          setRevenues(response.data);
        }
      }
      catch (err){
        alert("Erro ao buscar receitas");
      }
    }
    fetchExpense();
    fetchRevenue();
  }, [])

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
            <GrMoney size={48} />
            <h2>Finanças</h2>
          </header>
          <Line data={data}/>
        </div>
      </div>
    </>
  );
}
