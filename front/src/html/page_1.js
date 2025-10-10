import { useEffect, useState } from "react";
import { useNavigate } from 'react-router-dom';
import React from "react";
import '../style.css';


import TopComponent from "../components/top_panel"
import CentrComponent from "../components/centr_panel";
import ListComponent from "../components/list_panel";
import CentrComponent_Bottom from "../components/centr_panel_Bottom";
import BottomComponent from "../components/bottom_panel";

import imageUrl1 from "../img/img1.png";





function Page_1() {
    const [imagesList, setImagesList] = useState([]);



    


 const navigate = useNavigate();

 const sentCategory = (title) => {
  navigate(`/menu/search/category/${title}`);  
};


    useEffect(() => {
        fetch(`http://localhost:5256/api/Market`)
          .then(response => response.json())
          .then((data) => {
            const updatedData = data.map((item) => {
              item.imgSrc = `http://localhost:5256/${item.imgSrc}`; 
              return item;
            });
            setImagesList(updatedData);
          })
          .catch(error => console.error("Exception: ", error));
      }, []);


  return (
    <div className="App">
      <header className="flexColumn">
        <TopComponent />
        <div style={{ height: '200px' }}></div>
        <CentrComponent
          imageUrl1={imageUrl1}
          smallText="класика"
          bigText_1="ЯПОНСКОЙ"
          bigText_2="КУХНИ"
          btnText="акции" />
        <ListComponent imgListData={imagesList}  onClick={sentCategory}/>
        <CentrComponent_Bottom
          imageUrl1={imageUrl1}
          bigText="НАШИ РЕСТОРАНЫ"
          smallText_1="Суши-бар - современная классика японской кухни"
          smallText_2="Здесь все устроено так, чтобы гостям было приятно провести время в непринужденной атмосфере и получить удовольствие от кухни, отдыха и общения. Меню отличается разнообразием блюд, изобилием вкусов и деликатным отношением к японской кухне"
          btnText="читать подробнее" />
        <BottomComponent
          bigText="НАШИ РЕСТОРАНЫ"
          smallText="Мы дорожим каждым клиентом" />
      </header>
    </div>
  );
}




export default Page_1;