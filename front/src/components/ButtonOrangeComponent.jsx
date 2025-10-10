import React from "react";
import '../style.css';
import BigTextComponent from "./big_text";
import ImgComponent from "./imgComponent";

import icon from "../img/icon.png";
 
export default function ButtonOrangeComponent({ className, name, id, onClick}) {
    return <button type="button" className={className} name={name} id={id} onClick={onClick}>
        <div className="btnFlex">
        <BigTextComponent size="20px" text="MENU" />
        <ImgComponent src ={icon} width='20px' height='20px'/>
        </div>
        </button>
       
   
}