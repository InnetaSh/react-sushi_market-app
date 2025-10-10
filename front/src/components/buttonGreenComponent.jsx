import React from "react";
import '../style.css';
import SmallTextComponent from "./small_text";


const sendInput = () => {
    console.log("Button clicked");
  };

export default function ButtonGreenComponent({ name, id, text, onClick  }) {
    const handleClick = onClick || sendInput;

    return (
      <a onClick={handleClick} className="btnGreenBlock">
        <div className="btnGreen">
            <div className="sliding-button" name={name} id={id}>
                <div className="textIndex">
                    <SmallTextComponent size="14px" text={text} />
                </div>
            </div>
        </div>
    </a>
    )
}