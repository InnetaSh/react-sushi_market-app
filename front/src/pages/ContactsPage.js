import React, { useEffect } from "react";
import { observer } from "mobx-react-lite";
import { useParams } from 'react-router-dom';
import { Layout, Spin, Flex } from "antd";
import { useTranslation } from "react-i18next";
import '../style.css';

import MenuStore from "../stores/CategoryStore";
import ContactsSection from "@section/ContactsSection/ContactsSection";

const { Content } = Layout;

const ContactsPage = observer(() => {
  const { category } = useParams();
  const { t } = useTranslation();

  useEffect(() => {
    MenuStore.fetchMenu(category);
  }, [category]);

  return (
    <div className="App">
      <Content style={{ padding: '50px' }}>
          <ContactsSection/>
      </Content>
    </div>
  );
});

export default ContactsPage;