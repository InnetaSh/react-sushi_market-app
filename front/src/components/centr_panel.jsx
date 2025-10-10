import React from "react";
import { useNavigate } from 'react-router-dom';
import OrangeBlock from "./orangeBlock";
import SmallTextComponent from "./small_text";
import BigTextComponent from "./big_text";
import ButtonGreenComponent from "./buttonGreenComponent";



export default function CentrComponent({imageUrl1,smallText,bigText_1,bigText_2,btnText }) {
    const navigate = useNavigate();

    const handleClick = () => {
        navigate(`/sale`);
      };



    return <div class="section">
        <div class="section_containerCentrComponent" id="iychbh36i_0">
            <div className="containerCentrComponent">
                <div className="containerCentrComponent_item">
                    <div style={{ zIndex: -1, }}>
                        <OrangeBlock className="orangeBlock"/>
                    </div>

                    <div class="containerImg">
                        <div className="flexColumn containerImgLeft">
                            <SmallTextComponent size="15px" text={smallText} />
                            <div className="flexColumn" >
                                <BigTextComponent size="30px" text={bigText_1} />
                                <BigTextComponent size="30px" text={bigText_2} />
                            </div>

                            <ButtonGreenComponent name = "stock" id="stock" text ={btnText} onClick={handleClick}/>
                        </div>
                        <div class="imgFit">
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