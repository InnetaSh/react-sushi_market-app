import React from "react";
import '../style.css';
import BigTextComponent from "./big_text";
import SmallTextComponent from "./small_text";
import ButtonOrangeComponent from "./ButtonOrangeComponent";
 
export default function RightTopComponent({onClick}) {
    return <div className="leftTopPanel">
        
            <div className = "flexColumn">
            <SmallTextComponent size="15px" text="Phone" />
            <BigTextComponent size="30px" text="8(050)000-00-00" />
            </div>
            <ButtonOrangeComponent className = "orangeBtn" name ="menu" id ="menu" onClick={onClick}/>
    </div>;
}