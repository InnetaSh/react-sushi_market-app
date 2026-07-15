import React from "react";
import { Layout } from "antd";
import { useTranslation } from "react-i18next"; 
import TopComponent from "../components/top_panel";
import SaleComponent from "../components/sale_panel";
import BottomComponent from "../components/bottom_panel";

import img_1 from "../img/sale_1.png";
import img_2 from "../img/sale_2.png";

const Page_3 = () => {
  const { t } = useTranslation();

  return (
    <Layout className="App">
      <TopComponent />
      <div style={{ height: '200px' }}></div>
      
      <div className="flex">
        <SaleComponent
          src1={img_1} width1='500px' height1='500px' 
          title1={t("PAGE_3_TEXT.SALE_1_DATE")} 
          text1={t("PAGE_3_TEXT.SALE_1_NAME")}
          src2={img_2} width2='500px' height2='500px' 
          title2={t("PAGE_3_TEXT.SALE_2_DATE")} 
          text2={t("PAGE_3_TEXT.SALE_2_NAME")} 
        />
      </div>

      <BottomComponent
        bigText={t("PAGE_1_TEXT.RESTAURANTS_TITLE")}
        smallText={t("PAGE_1_TEXT.BOTTOM_MSG")} 
      />
    </Layout>
  );
};

export default Page_3;