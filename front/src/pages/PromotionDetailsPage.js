import { Flex } from "antd";

import PromotionDetails from "@section/PromotionDetails/PromotionDetails";

const PromotionDetailsPage = () => {
  return (
    <div className="App">
      <Flex
        vertical
        align="center"
      >
        <PromotionDetails />
      </Flex>
    </div>
  );
};

export default PromotionDetailsPage;