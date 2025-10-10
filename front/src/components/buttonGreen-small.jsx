import React from "react";
import '../style.css';
import SmallTextComponent from "./small_text";



export default function ButtonGreenSmall({text, onClick  }) {
    

    return (
      <a onClick={onClick} className="btnGreenBlock">
        <div className="btnGreen">
            <div className="sliding-button">
                <div className="textIndex">
                    <SmallTextComponent size="14px" text={text} />
                </div>
            </div>
        </div>
    </a>
    )
}