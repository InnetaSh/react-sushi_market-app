import React from "react";
import '../style.css';
import SmallTextComponent from "./small_text";
import BigTextComponent from "./big_text";

export default function ImgComponentLeftText({ src, width, height,float,marginRight,marginLeft, bigText, smallText_1, smallText_2,smallText_3 }) {
    return (<div class="sectionImgWihtText">
                <div className=" containerImgWihtText">
                <img
                        src={src}
                        alt="img"
                        style={{
                            width: width,
                            height: height,
                            objectFit: 'cover',
                            float: float,
                            marginRight:marginRight,
                            marginLeft:marginLeft
                        }}
                    />
                    <div className="flexColumnLeft" >
                    <BigTextComponent size="30px" text={bigText} />
                        <SmallTextComponent size="18px" text={smallText_1} />
                        <SmallTextComponent size="18px" text= {smallText_2} />
                        <SmallTextComponent size="18px" text= {smallText_3} />
                    </div>
                    
                </div>
            </div>
            );
}