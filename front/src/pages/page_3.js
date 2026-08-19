import React from "react";
import { Flex } from "antd";
import { useTranslation } from "react-i18next";
import SaleComponent from "../components/sale_panel";

import img_1 from "../img/sale_1.png";
import img_2 from "../img/sale_2.png";

const Page_3 = () => {
  const { t } = useTranslation();

  return (
    <div className="App">
      <Flex
        vertical
        align="center"
      >
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

      </Flex>
    </div>
  );
};

export default Page_3;