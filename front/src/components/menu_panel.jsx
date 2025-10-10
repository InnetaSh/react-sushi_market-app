import React from "react";
import OrangeBlock from "./orangeBlock";

import ImgComponentLeftText from "./imgComponentLeftText";


export default function MenuComponent({ imgListData, width, height }) {
    return (<div class="section">
        <div class="section_containerCentrComponent" id="iychbh36i_0">
            <div className="containerCentrComponent">
                <div className="containerMenuComponent_item">
                    <div style={{ zIndex: -1, }}>
                        <OrangeBlock className="orangeBlock" />
                    </div>

                    <div className="containerImg flexColumn">
                    {imgListData.length > 0 ? (
                        imgListData.map((item, index) => {

                            const isEven = index % 2 === 0;


                            const styles = {
                                float: isEven ? 'right' : 'left',
                                marginRight: isEven ? '0px' : '20%',
                                marginLeft: isEven ? '20%' : '0px',
                            };

                            return (
                                <ImgComponentLeftText
                                    key={index}
                                    src={item.imgSrc}
                                    width={width}
                                    height={height}
                                    float={styles.float}
                                    marginRight={styles.marginRight}
                                    marginLeft={styles.marginLeft}
                                    bigText={item.title}
                                    smallText_1={item.about_1}
                                    smallText_2={item.about_2}
                                    smallText_3={item.price}
                                    />
                                );
                            })
                        ) : (
                            <h3 className="no-data-text">
                               this menu is temporarily empty
                            </h3>
                        )}
                    </div>
                </div>
            </div>
        </div>
    </div>
    );
}