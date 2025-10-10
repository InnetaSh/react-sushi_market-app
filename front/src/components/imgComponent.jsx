import React from "react";
import '../style.css';

export default function ImgComponent({ src, width, height }) {
    return (
        <div>
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
    );
}