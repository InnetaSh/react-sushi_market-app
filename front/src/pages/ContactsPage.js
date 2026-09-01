import { observer } from "mobx-react-lite";
import { Layout } from "antd";
import '../style.scss';

import ContactsSection from "@section/ContactsSection/ContactsSection";

const { Content } = Layout;

const ContactsPage = observer(() => {
  return (
    <div className="App">
      <Content style={{ padding: '50px' }}>
          <ContactsSection/>
      </Content>
    </div>
  );
});

export default ContactsPage;