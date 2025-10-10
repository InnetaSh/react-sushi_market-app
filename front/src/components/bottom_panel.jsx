import React from "react";
import OrangeBlock from "./orangeBlock";
import SmallTextComponent from "./small_text";
import BigTextComponent from "./big_text";
import ImgComponent from "./imgComponent";

import imageUrl1 from "../img/magazine_1.png";
import imageUrl2 from "../img/magazine_2.png";
import imageUrl3 from "../img/magazine_3.png";
import imageUrl4 from "../img/magazine_4.png";

const imgListData = [
    { imgSrc: imageUrl1, width: '100%', height: 'auto' },
    { imgSrc: imageUrl2, width: '100%', height: 'auto' },
    { imgSrc: imageUrl3, width: '100%', height: 'auto' },
    { imgSrc: imageUrl4, width: '100%', height: 'auto' },

];


export default function BottomComponent({ bigText, smallText }) {
    return <div class="section_Bottom">
        <div class="section_containerCentrComponent" id="iychbh36i_0">
            <div className="containerCentrComponent">
                <div className="containerCentrComponent_item" style={{ width: '90%' }}>
                    <div style={{ zIndex: -1, }}>
                        <OrangeBlock className="orangeBlock" />
                    </div>

                    <div class="containerImg">
                        <div className=" containerImgBottom">
                            <div className="flexLeft">
                                <BigTextComponent size="30px" text={bigText} />
                                <SmallTextComponent size="15px" text={smallText} />
                            </div>
                            <div className="flex ">
                                    {imgListData.map((item, index) => (
                                        <div className="imgBottom">
                                            <ImgComponent
                                                key={index}
                                                src={item.imgSrc}
                                                width={item.width}
                                                height={item.height}
                                            />
                                        </div>
                                    ))}
                            </div>


                        </div>
                    </div>
                </div>

            </div>

        </div>
    </div>;
}