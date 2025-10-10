import React from "react";
import '../style.css';

export default function SmallTextComponent({ size, text }) {
    return <span className="smallText" style={{ fontSize: size }}>{text}</span>;
}