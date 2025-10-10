import React from "react";
import '../style.css';

export default function ImgSaleComponent({ src, width, height, title, text }) {
    return (
        <div className="imgSaleContainer">
            <div className="saleComponent">
                <div className="imgTransform">
                    <img
                        src={src}
                        alt="img"
                        style={{
                            width: width,
                            height: height,
                            objectFit: 'cover'
                        }}
                    />
                </div>
                <div className=" flexColumn">
                    <h4>{title}</h4>
                    <span>{text}</span>
                </div>
            </div>

        </div>
    );
}