import React from "react";
import '../style.css';
import OrangeBlock from "./orangeBlock";
import ImgComponent from "./imgComponent";
import SmallTextComponent from "./small_text";
import BigTextComponent from "./big_text";
import ButtonGreenComponent from "./buttonGreenComponent";


export default function ImgListItemComponent({imgSrc,smallText,bigText,onClick}) {
    return <div className="imgListSection">
        <div>
            <div className="imgListItemComtainer" >
                <OrangeBlock className="orangeBlockRight" />
                <div className="imgItem">
                    <ImgComponent
                        src={imgSrc}
                        width='100%'
                        height='300px'
                    />
                </div>
            </div>
            <a href="#" className="link_imgList"  >
                <div className="linkText_imgList">
                    <SmallTextComponent size='18px' text={smallText}/>
                    <BigTextComponent text={bigText}/>
                </div>

                <ButtonGreenComponent name="stock" id="stock" text="Перейти в меню" onClick={onClick}/>
            </a>
        </div>
    </div>



    // </div >;
}