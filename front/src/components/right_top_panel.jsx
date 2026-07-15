import React from "react";
import '../style.css';
import BigTextComponent from "./big_text";
import SmallTextComponent from "./small_text";
import ButtonOrangeComponent from "./ButtonOrangeComponent";
import { ButtonOrange } from './ButtonOrange/ButtonOrange.tsx';

import icon from "../img/icon.png";

export default function RightTopComponent({ onClick }) {
    return <div className="leftTopPanel">

        <div className="flexColumn">
            <SmallTextComponent size="15px" text="Phone" />
            <BigTextComponent size="30px" text="8(050)000-00-00" />
        </div>
        <ButtonOrange
            text="MENU"
            onClick={onClick}
            name="menu"
            id="menu"
            width="150px"
            icon={<img src={icon} alt="icon" style={{ width: 20, height: 20 }} />}
        />
        <ButtonOrange
            text="MENU"
            onClick={onClick}
            name="menu"
            id="menu"
            width="150px"
            icon={<img src={icon} alt="icon" style={{ width: 20, height: 20 }} />}
        />
    </div>;
}