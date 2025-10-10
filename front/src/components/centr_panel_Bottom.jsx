import React from "react";
import OrangeBlock from "./orangeBlock";
import SmallTextComponent from "./small_text";
import BigTextComponent from "./big_text";
import ButtonGreenComponent from "./buttonGreenComponent";


export default function CentrComponent_Bottom({imageUrl1,bigText,smallText_1,smallText_2,btnText }) {
    return <div class="section">
        <div class="section_containerCentrComponent" >
            <div className="containerCentrComponent">
                <div className="containerCentrComponent_item">
                    <div style={{ zIndex: -1, }}>
                        <OrangeBlock className="orangeBlock"/>
                    </div>

                    <div class="containerImg" >
                        <div className="flexColumn containerImgLeft">
                        <BigTextComponent size="30px" text={bigText} />
                            <div className="flexColumn " >
                                <SmallTextComponent size="15px" text={smallText_1} />
                                <SmallTextComponent size="15px" text={smallText_2} />
                            </div>

                            <ButtonGreenComponent name = "stock" id="stock" text ={btnText} />
                        </div>
                        <div class="imgFit" >
                            <img
                                src={imageUrl1}
                                alt="img"
                                style={{
                                    width: '110%',
                                    height: '550px',
                                    objectFit: 'cover'
                                }}
                            />
                        </div>
                    </div>
                </div>

            </div>

        </div>
    </div>;
}




