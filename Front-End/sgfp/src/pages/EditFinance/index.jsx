import axios from "axios";
import { jwtDecode } from "jwt-decode";
import React, { useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import * as styles from "./EditFinance.module.css";
import { IoArrowBackSharp } from "react-icons/io5";

export default function EditFinance() {
  const { id } = useParams();
  const [description, setDescription] = useState("");
  const [category, setCategory] = useState(0);
  const [subCategory, setSubCategory] = useState("");
  const [value, setValue] = useState(0);
  const { user } = jwtDecode(localStorage.getItem("token"));
  const navigation = useNavigate();

  async function handleSubmit(e) {
    e.preventDefault();
    try {
      if (
        description === null ||
        category === null ||
        subCategory === null ||
        value === null
      ) {
        alert("Preencha todos os campos!");
        return;
      }

      const finance = {
        Description: description,
        Categ: parseInt(category, 10),
        UserId: user.sub,
        SubCateg: subCategory,
        Value: value,
      };

      const response = await axios.put(
        `http://localhost:5086/api/finance/${id}`,
        finance
      );

      if (response.status === 200) {
        alert("Movimentação alterada com sucesso");
        navigation("/home");
      } 
    } catch (error) {
      alert("Erro ao tentar alterar Finança");
    }
  }

  function handleDescription(e) {
    setDescription(e.target.value);
  }

  function handleCategory(e) {
    setCategory(e.target.value);
  }
  function handleSubCategory(e) {
    setSubCategory(e.target.value);
  }
  function handleValue(e) {
    setValue(e.target.value);
  }

  return (
    <>
      <div className={styles.container}>
        <div className={styles.signForm}>
          <form className={styles.form}>
            <IoArrowBackSharp size={30} className={styles.back} onClick={() => navigation("/perfil")}/>
            <h2 className={styles.title}>Edite a Movimentação</h2>
            <input
              type="text"
              placeholder="Nova Descrição"
              aria-label="Descrição"
              onChange={handleDescription}
              className={styles.inputLog}
            />
            <div className={styles.categories}>
              <div>
                <input
                  type="radio"
                  aria-label="Despesa"
                  value={-1}
                  name="categoria"
                  onChange={handleCategory}
                  className={styles.inputLog}
                />
                <label>Despesa</label>
              </div>
              <div>
                <input
                  type="radio"
                  aria-label="Receita"
                  value={1}
                  name="categoria"
                  onChange={handleCategory}
                  className={styles.inputLog}
                />
                <label>Receita</label>
              </div>
            </div>
            <input
              type="text"
              placeholder="Nova SubCategoria"
              aria-label="SubCategoria"
              onChange={handleSubCategory}
              className={styles.inputLog}
            />
            <input
              type="number"
              placeholder="Novo Valor"
              aria-label="Valor"
              onChange={handleValue}
              className={styles.inputLog}
            />

            <button
              type="submit"
              className={styles.buttonSign}
              onClick={handleSubmit}
            >
              Adicionar
            </button>
          </form>
        </div>
      </div>
    </>
  );
}
