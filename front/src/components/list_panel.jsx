import React from "react";
import '../style.css';
import ImgListItemComponent from "./imgListItemComponent";
 

 
export default function ListComponent({imgListData, onClick}) {
    return <div className="listPanel">
         {imgListData.map((item, index) => (
        <ImgListItemComponent
          key={index}  
          imgSrc ={item.imgSrc}
          smallText={item.count}
          bigText={item.title}
          onClick={() => onClick(item.title)} 
        />
      ))}
    </div>;
}