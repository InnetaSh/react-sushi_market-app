import { useEffect, useState } from "react";
import { useParams } from 'react-router-dom'; 
import React from "react";
import '../style.css';
import TopComponent from "../components/top_panel"
import MenuComponent from "../components/menu_panel";



function Page_2() {

 const [imagesList, setImagesList] = useState([]);
 const { category } = useParams();

 useEffect(() => {
  const fetchData = async () => {
    const url = category 
      ? `http://localhost:5256/api/Menu/search/category/${category}` 
      : `http://localhost:5256/api/Menu`; 
    try {
      const response = await fetch(url);
      const data = await response.json();
      const updatedData = data.map((item) => {
        item.imgSrc = `http://localhost:5256/img/menu/${item.imgSrc}`;
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
        <MenuComponent  imgListData={imagesList}  width='250px' height='250px' />

      </header>
    </div>
  );
}

export default Page_2;
