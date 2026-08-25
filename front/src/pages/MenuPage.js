import { useEffect, useState } from "react";
import { observer } from "mobx-react-lite";
import { useParams, useNavigate } from 'react-router-dom';
import { Layout, Spin, Tabs } from "antd";
import { useTranslation } from "react-i18next";
import '../style.css';


import CategoryStore from "../stores/CategoryStore";
import SubmenuSection from "../components/sections/SubmenuSection/SubmenuSection";

const { Content } = Layout;

const MenuPage = observer(() => {
  const { id } = useParams();
  const navigate = useNavigate();
  const { t, i18n } = useTranslation();
  const currentLang = i18n.language;


  const [currentPage, setCurrentPage] = useState(1);

  useEffect(() => {
    CategoryStore.fetchCategories();

    if (id) {
      CategoryStore.fetchCategoryWithProducts(id);
    } else {
      CategoryStore.fetchCategoriesWithProducts();
    }
  }, [id]);

  const handleCategoryChange = (activeKey) => {
    setCurrentPage(1);

    if (activeKey === "all") {
      navigate('/menu');
    } else {
      navigate(`/menu/search/category/${activeKey}`);
    }
  };

  const tabItems = [
    { key: "all", label: currentLang === 'en' ? "All" : "Всі" },
    ...CategoryStore.categories.map((cat) => ({
      key: String(cat.id),
      label: (currentLang === 'en' ? cat.titleEn : cat.titleUa) || cat.title
    }))
  ];

  const getProductsToDisplay = () => {
    if (id) {
      return CategoryStore.currentCategoryProducts || [];
    }
    return CategoryStore.categoriesWithProducts.flatMap(cat => cat.products || []);
  };

  return (
    <div className="App">
      <Content style={{ padding: '50px' }}>
        <div style={{ marginBottom: '30px', textAlign: 'center' }}>
          <Tabs
            activeKey={id ? String(id) : "all"}
            onChange={handleCategoryChange}
            centered
            items={tabItems}
            size="large"
          />
        </div>

        {CategoryStore.loading && getProductsToDisplay().length === 0 ? (
          <div style={{ textAlign: 'center', marginTop: '50px' }}>
            <Spin size="large" tip={t("UI_TEXT.LOADING")} />
          </div>
        ) : (
          <SubmenuSection
            key={`${id || 'all'}-${currentPage}`}
            menuItems={getProductsToDisplay()}
            currentPage={currentPage}
            setCurrentPage={setCurrentPage}
          />
        )}
      </Content>
    </div>
  );
});

export default MenuPage;