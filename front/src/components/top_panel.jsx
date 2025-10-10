import React from "react";
import { useNavigate } from 'react-router-dom';
import '../style.css';
import LeftTopComponent from "./left_top_panel";
import RightTopComponent from "./right_top_panel";
import logo from "../img/logo.png";



export default function TopComponent() {
    const navigate = useNavigate();

    const handleClick = () => {
        navigate(`/menu`);
      };


    return <div className="sectionTopComponent">
        <div className="topPanel">
            <LeftTopComponent imageUrl={logo} />
            <RightTopComponent onClick={handleClick}/>
        </div>
    </div>

}