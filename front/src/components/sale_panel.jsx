import React from "react";
import OrangeBlock from "./orangeBlock";
import ImgSaleComponent from "./imgSaleComponent";



export default function SaleComponent({ 
    src1,width1, height1,title1,text1,
    src2,width2, height2,title2,text2,
 }) {
    return (<div class="section">
        <div class="section_containerCentrComponent" id="iychbh36i_0">
            <div className="containerCentrComponent">
            <div className="containerMenuComponent_item" style={{ width: '100%' }}>
                    <div style={{ zIndex: -1, }}>
                        <OrangeBlock className="orangeBlock" />
                    </div>

                    <div className="containerImg flex">
                    <ImgSaleComponent src={src1} width = {width1} height = {height1} title = {title1} text={text1} />
                    <ImgSaleComponent src={src2} width = {width2} height = {height2} title = {title2} text={text2} />
                    </div>
                </div>
            </div>
        </div>
    </div>
    );
}