import React, { useEffect } from "react";
import { observer } from "mobx-react-lite";
import { useNavigate } from 'react-router-dom';
import { useTranslation } from "react-i18next";
import { Spin } from "antd";
import '../style.css';

import MarketStore from "../stores/MarketStore";
import TopComponent from "../components/top_panel";
import CentrComponent from "../components/centr_panel";
import ListComponent from "../components/list_panel";
import CentrComponent_Bottom from "../components/centr_panel_Bottom";
import BottomComponent from "../components/bottom_panel";

import Header from '../components/Header/Header';
import Footer from '../components/Footer/Footer';

import imageUrl1 from "../img/img1.png";

const Page_1 = observer(() => {
  const navigate = useNavigate();
  const { t } = useTranslation();

  const sentCategory = (title) => {
    navigate(`/menu/search/category/${title}`);
  };

  useEffect(() => {
    MarketStore.fetchMarketData();
  }, []);

  return (
    <div className="App">
      <header className="flexColumn">

        <Header />

        <div style={{ height: '200px' }}></div>

        <CentrComponent
          imageUrl1={imageUrl1}
          smallText={t("PAGE_1_TEXT.TITLE_1")}
          bigText_1={t("PAGE_1_TEXT.TITLE_2")}
          bigText_2={t("PAGE_1_TEXT.TITLE_3")}
          btnText={t("PAGE_1_TEXT.BTN_ACTIONS")}
        />

        {MarketStore.loading ? (
          <div style={{ textAlign: 'center', padding: '50px' }}>
            <Spin size="large" />
          </div>
        ) : (
          <ListComponent
            imgListData={MarketStore.imagesList}
            onClick={sentCategory}
          />
        )}

        <CentrComponent_Bottom
          imageUrl1={imageUrl1}
          bigText={t("PAGE_1_TEXT.RESTAURANTS_TITLE")}
          smallText_1={t("PAGE_1_TEXT.RESTAURANTS_DESC_1")}
          smallText_2={t("PAGE_1_TEXT.RESTAURANTS_DESC_2")}
          btnText={t("PAGE_1_TEXT.BTN_READ_MORE")}
        />
        <Footer />

      </header>
    </div>
  );
});

export default Page_1;