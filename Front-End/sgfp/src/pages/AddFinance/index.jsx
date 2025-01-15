import React, { useContext, useState } from "react";
import { AuthContext } from "../../context/AuthContext";
import * as styles from "./AddFinance.module.css";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import { IoArrowBackSharp } from "react-icons/io5";

export default function AddFinance() {
  const [description, setDescription] = useState("");
  const [category, setCategory] = useState(0);
  const [subCategory, setSubCategory] = useState("");
  const [value, setValue] = useState(0);
  const { user } = useContext(AuthContext);
  const navigation = useNavigate();

  async function handleSubmit(e){
    e.preventDefault();
    try {
        if (description === null || category === null || subCategory === null || value === null) {
          alert("Preencha todos os campos!");
          return;
        }
        
  
        const finance = {
          Description: description,
          Categ: parseInt(category, 10),
          UserId: user.sub,
          SubCateg: subCategory,
          Value: value
        };

        console.log(finance);
  
        const response = await axios.post(
          "http://localhost:5086/api/finance",
          finance
        );
  
        if (response.status === 201) {
          alert("Movimentação criada com sucesso");
          navigation("/home");
        } else {
          alert("Erro ao fazer cadastro!");
        }
      } catch (error) {
        alert(error);
      }
    };

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
            <IoArrowBackSharp size={30} className={styles.back} onClick={() => navigation("/home")}/>
            <h2 className={styles.title}>Adicione uma Movimentação</h2>
            <input
              type="text"
              placeholder="Descrição"
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
              placeholder="SubCategoria"
              aria-label="SubCategoria"
              onChange={handleSubCategory}
              className={styles.inputLog}
            />
            <input
              type="number"
              placeholder="Valor"
              aria-label="Valor"
              onChange={handleValue}
              className={styles.inputLog}
            />

            <button type="submit" className={styles.buttonSign} onClick={handleSubmit}>
              Adicionar
            </button>
          </form>
        </div>
      </div>
    </>
  );
}
