import React from "react";
import { Flex } from "antd";
import { useTranslation } from "react-i18next";

import PromotionDetails from "@section/PromotionDetails/PromotionDetails";


import img_1 from "@img/sale_1.png";
import img_2 from "@img/sale_2.png";

const PromotionDetailsPage = () => {
  const { t } = useTranslation();

  const offers = [
    {
      image: img_1,
      title: t("PAGE_3_TEXT.SALE_1_DATE"),
      description:
        t("PAGE_3_TEXT.SALE_1_NAME"),
    },
    {
      image: img_2,
      title: t("PAGE_3_TEXT.SALE_2_DATE"),
      description:
        t("PAGE_3_TEXT.SALE_2_NAME"),
    },
  ];

  return (
    <div className="App">
      <Flex
        vertical
        align="center"
      >
        <PromotionDetails offers={offers} />
      </Flex>
    </div>
  );
};

export default PromotionDetailsPage;