import { useEffect, useState } from "react";
import { useParams } from 'react-router-dom';
import React from "react";
import '../style.css';
import TopComponent from "../components/top_panel"
import SaleComponent from "../components/sale_panel";
import BottomComponent from "../components/bottom_panel";

import img_1 from "../img/sale_1.png";
import img_2 from "../img/sale_2.png";

function Page_3() {


  return (
    <div className="App">
      <header className="flexColumn">
        <TopComponent />
        <div style={{ height: '200px' }}></div>
        <div className="flex">
          <SaleComponent
            src1={img_1} width1='500px' height1='500px' title1='с 20 ноября по 30 декабря' text1='Каждый пятый'
            src2={img_2} width2='500px' height2='500px' title2='с 10 октября по 10 ноября' text2='10% на вынос' />
        </div>

        <BottomComponent
          bigText="НАШИ РЕСТОРАНЫ"
          smallText="Мы дорожим каждым клиентом" />
      </header>
    </div>
  );
}

export default Page_3;
