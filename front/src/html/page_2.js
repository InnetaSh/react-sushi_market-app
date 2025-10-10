import { useEffect, useState } from "react";
import { useParams } from 'react-router-dom';
import { useNavigate } from 'react-router-dom';
import React from "react";
import '../style.css';
import TopComponent from "../components/top_panel"
import MenuComponent from "../components/menu_panel";
import ButtonGreenSmall from "../components/buttonGreen-small"


function Page_2() {
  const navigate = useNavigate();

  const [imagesList, setImagesList] = useState([]);
  const { category } = useParams();

  useEffect(() => {
    const fetchData = async () => {
      const url = category
        ? `http://localhost:5292/api/Menu/search/category/${category}`
        : `http://localhost:5292/api/Menu`;
      try {
        const response = await fetch(url);
        const data = await response.json();
        const updatedData = data.map((item) => {
          item.imgSrc = `http://localhost:5292/img/menu/${item.imgSrc}`;
          return item;
        });
        setImagesList(updatedData);
      } catch (error) {
        console.error("Exception: ", error);
      }
    };
    fetchData();
  }, [category]);

  return (
    <div className="App">
      <header className="flexColumn">

        <TopComponent />

        <div style={{ height: '200px' }}></div>
        
        <MenuComponent imgListData={imagesList} width='250px' height='250px' />

      </header>
    </div>
  );
}

export default Page_2;
