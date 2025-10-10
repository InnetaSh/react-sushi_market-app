import React from "react";
import '../style.css';

export default function BigTextComponent({ size, text }) {
    return <span className="bigText" style={{ fontSize: size }}>{text}</span>
     
}