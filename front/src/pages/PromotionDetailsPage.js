import { Flex } from "antd";
import { useTranslation } from "react-i18next";

import PromotionDetails from "@section/PromotionDetails/PromotionDetails";

import { PROMOTION_DATA } from '@mocks/promotionsData';

const PromotionDetailsPage = () => {
  const { t } = useTranslation();

  return (
    <div className="App">
      <Flex
        vertical
        align="center"
      >
        <PromotionDetails offers={PROMOTION_DATA} />
      </Flex>
    </div>
  );
};

export default PromotionDetailsPage;