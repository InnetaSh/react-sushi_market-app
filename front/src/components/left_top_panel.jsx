import React from "react";
import '../style.css';
import BigTextComponent from "./big_text";
import SmallTextComponent from "./small_text";
 
export default function LeftTopComponent({ imageUrl }) {
    return <a className="rightTopPanel">
        <div className = "flex">
            <div>
            <img
              src={imageUrl}  
              alt="logo"
              className="img"
            />
            </div>
            <div className = "logoName">
            <BigTextComponent size="30px" text="OSAMA" />
            <SmallTextComponent size="15px" text="sushi-bar" />
            </div>
        </div>
    </a>;
}