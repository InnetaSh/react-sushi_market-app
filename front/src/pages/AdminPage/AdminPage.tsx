import React, { useEffect, useState } from "react";
import { observer } from "mobx-react-lite";
import { Layout, message, Tabs } from "antd";
import { useTranslation } from "react-i18next";
import {
  KeyboardSensor,
  PointerSensor,
  useSensor,
  useSensors,
} from "@dnd-kit/core";
import {
  sortableKeyboardCoordinates,
  useSortable,
} from "@dnd-kit/sortable";
import { CSS } from "@dnd-kit/utilities";

import CategoryStore from "@stores/CategoryStore";
import ProductStore from "@stores/ProductStore";
import CategoryApi from "@/api/categoryApi";
import ProductApi from "@/api/productApi";
import { EntityModal } from "@UI/EntityModal/EntityModal";

import { useEntityOrder } from "@hooks/useEntityOrder";
import { CategoriesTab } from "./components/CategoriesTab";
import { ProductsTab } from "./components/ProductsTab";
import styles from "./AdminPage.module.scss";

const { Content } = Layout;

const SortableEntityRow = ({ id, children }: { id: number | string; children: React.ReactNode }) => {
  const { attributes, listeners, setNodeRef, transform, transition, isDragging } = useSortable({ id });

  const style = {
    transform: CSS.Transform.toString(transform),
    transition,
    zIndex: isDragging ? 10 : 1,
    opacity: isDragging ? 0.6 : 1,
  };

  return (
    <div ref={setNodeRef} style={style} {...attributes} {...listeners}>
      {children}
    </div>
  );
};

const AdminPage = observer(() => {
  const { t, i18n } = useTranslation();
  const currentLang = i18n.language;
  const isEn = currentLang === "en";

  const [activeTab, setActiveTab] = useState("categories");
  const [selectedCategoryId, setSelectedCategoryId] = useState<number | null>(null);

  const [localCategories, setLocalCategories] = useState<any[]>([]);
  const [localProducts, setLocalProducts] = useState<any[]>([]);

  const sensors = useSensors(
    useSensor(PointerSensor, { activationConstraint: { distance: 5 } }),
    useSensor(KeyboardSensor, { coordinateGetter: sortableKeyboardCoordinates })
  );

  const [modalConfig, setModalConfig] = useState<{
    isOpen: boolean;
    type: "category" | "product";
    item: any | null;
  }>({
    isOpen: false,
    type: "category",
    item: null,
  });

  const API_URL = process.env.REACT_APP_API_URL || "http://localhost:5292/api";
  const BASE_HOST = API_URL.replace(/\/api\/?$/, "");

  const formatImageUrl = (itemOrString: any) => {
    const rawImg =
      typeof itemOrString === "string"
        ? itemOrString
        : itemOrString?.imgSrc ||
          itemOrString?.ImgSrc ||
          itemOrString?.imageUrl ||
          itemOrString?.ImageUrl ||
          itemOrString?.image ||
          "";

    if (!rawImg) return undefined;
    return rawImg.startsWith("http") ? rawImg : `${BASE_HOST}${rawImg.startsWith("/") ? "" : "/"}${rawImg}`;
  };

  useEffect(() => {
    CategoryStore.fetchCategories();
    ProductStore.fetchProducts();
  }, []);

  useEffect(() => {
    setLocalCategories(CategoryStore.categories);
  }, [CategoryStore.categories]);

  useEffect(() => {
    setLocalProducts(ProductStore.products);
  }, [ProductStore.products]);

  useEffect(() => {
    if (selectedCategoryId) {
      CategoryStore.fetchCategoryWithProducts(selectedCategoryId);
    }
  }, [selectedCategoryId]);


  const categoriesOrder = useEntityOrder(
    localCategories,
    setLocalCategories,
    (id, sortOrder) => CategoryApi.reorderCategory(id, sortOrder),
    async () => {
      await CategoryStore.fetchCategories();
      await ProductStore.fetchProducts();
      if (selectedCategoryId) {
        await CategoryStore.fetchCategoryWithProducts(selectedCategoryId);
      }
    }
  );

  const productsOrder = useEntityOrder(
    localProducts,
    setLocalProducts,
    (id, sortOrder) => ProductApi.reorderProduct(id, sortOrder),
    async () => {
      await CategoryStore.fetchCategories();
      await ProductStore.fetchProducts();
      if (selectedCategoryId) {
        await CategoryStore.fetchCategoryWithProducts(selectedCategoryId);
      }
    }
  );

  const handleSaveOrder = async () => {
    if (activeTab === "categories") {
      await categoriesOrder.handleSaveOrder(
        t("ADMIN_PAGE.SUCCESS_ORDER_UPDATE", "Порядок успішно збережено"),
        t("ADMIN_PAGE.ERROR_SAVE", "Помилка збереження порядку")
      );
    } else {
      await productsOrder.handleSaveOrder(
        t("ADMIN_PAGE.SUCCESS_ORDER_UPDATE", "Порядок успішно збережено"),
        t("ADMIN_PAGE.ERROR_SAVE", "Помилка збереження порядку")
      );
    }
  };

  const handleCancelOrder = () => {
    if (activeTab === "categories") {
      categoriesOrder.handleCancelOrder();
    } else {
      productsOrder.handleCancelOrder();
    }
  };

  const handleOpenModal = (type: "category" | "product", item: any = null) => {
    const itemWithFormattedImage = item ? { ...item, imgSrc: formatImageUrl(item) } : null;
    setModalConfig({ isOpen: true, type, item: itemWithFormattedImage });
  };

  const handleCloseModal = () => {
    setModalConfig({ isOpen: false, type: "category", item: null });
  };

  const handleSaveEntity = async (values: any) => {
    try {
      const { type, item } = modalConfig;

      if (type === "category") {
        const formData = new FormData();
        if (item) formData.append("Id", item.id.toString());

        const currentTitle = values.title || "";
        if (isEn) {
          formData.append("TitleEn", currentTitle);
          formData.append("TitleUa", item?.titleUa || currentTitle);
        } else {
          formData.append("TitleUa", currentTitle);
          formData.append("TitleEn", item?.titleEn || currentTitle);
        }

        if (values.sortOrder !== undefined && values.sortOrder !== null) {
          formData.append("SortOrder", values.sortOrder.toString());
        } else if (item?.sortOrder !== undefined) {
          formData.append("SortOrder", item.sortOrder.toString());
        }

        if (values.imageFile instanceof File) {
          formData.append("Image", values.imageFile);
        }

        if (item) {
          await CategoryApi.updateCategory(item.id, formData);
          message.success(t("ADMIN_PAGE.SUCCESS_CATEGORY_UPDATE", "Категорію успішно оновлено"));
        } else {
          await CategoryApi.createCategory(formData);
          message.success(t("ADMIN_PAGE.SUCCESS_CATEGORY_CREATE", "Категорію успішно створено"));
        }
        await CategoryStore.fetchCategories();
      } else {
        const formData = new FormData();
        if (item) formData.append("Id", item.id.toString());

        const currentTitle = values.title || "";
        const currentDesc = values.description || "";

        if (isEn) {
          formData.append("TitleEn", currentTitle);
          formData.append("TitleUa", item?.titleUa || currentTitle);
          formData.append("DescriptionEn", currentDesc);
          formData.append("DescriptionUa", item?.descriptionUa || currentDesc);
        } else {
          formData.append("TitleUa", currentTitle);
          formData.append("TitleEn", item?.titleEn || currentTitle);
          formData.append("DescriptionUa", currentDesc);
          formData.append("DescriptionEn", item?.descriptionEn || currentDesc);
        }

        formData.append("Price", (values.price || item?.price || 0).toString());
        formData.append("WeightOrVolume", values.weightOrVolume || item?.weightOrVolume || "");

        const finalCategoryId = values.categoryId || item?.categoryId || selectedCategoryId;
        if (finalCategoryId) {
          formData.append("CategoryId", finalCategoryId.toString());
        } else {
          message.error(t("ADMIN_PAGE.ERROR_SELECT_CATEGORY", "Будь ласка, оберіть категорію для продукту!"));
          return;
        }

        if (values.sortOrder !== undefined && values.sortOrder !== null) {
          formData.append("SortOrder", values.sortOrder.toString());
        } else if (item?.sortOrder !== undefined) {
          formData.append("SortOrder", item.sortOrder.toString());
        }

        if (values.imageFile instanceof File) {
          formData.append("Image", values.imageFile);
        }

        if (item) {
          await ProductApi.updateProduct(item.id, formData);
          message.success(t("ADMIN_PAGE.SUCCESS_PRODUCT_UPDATE", "Продукт успішно оновлено"));
        } else {
          await ProductApi.createProduct(formData);
          message.success(t("ADMIN_PAGE.SUCCESS_PRODUCT_CREATE", "Продукт успішно створено"));
        }

        await ProductStore.fetchProducts();
        if (selectedCategoryId) {
          await CategoryStore.fetchCategoryWithProducts(selectedCategoryId);
        }
      }

      handleCloseModal();
    } catch (e) {
      console.error(e);
      message.error(t("ADMIN_PAGE.ERROR_SAVE", "Помилка збереження даних"));
    }
  };

  const handleDeleteCategory = async (id: number) => {
    try {
      await CategoryApi.deleteCategory(id);
      message.success(t("ADMIN_PAGE.SUCCESS_CATEGORY_DELETE", "Категорію видалено"));
      await CategoryStore.fetchCategories();
      if (selectedCategoryId === id) setSelectedCategoryId(null);
    } catch (e) {
      console.error(e);
      message.error(t("ADMIN_PAGE.ERROR_DELETE", "Помилка видалення"));
    }
  };

  const handleDeleteProduct = async (id: number) => {
    try {
      await ProductApi.deleteProduct(id);
      message.success(t("ADMIN_PAGE.SUCCESS_PRODUCT_DELETE", "Продукт видалено"));
      await ProductStore.fetchProducts();
      if (selectedCategoryId) await CategoryStore.fetchCategoryWithProducts(selectedCategoryId);
    } catch (e) {
      console.error(e);
      message.error(t("ADMIN_PAGE.ERROR_DELETE", "Помилка видалення"));
    }
  };

  const selectedCategoryObj = CategoryStore.categories.find((c: any) => c.id === selectedCategoryId);

  const categoriesTabContent = (
    <CategoriesTab
      t={t}
      isEn={isEn}
      localCategories={localCategories}
      selectedCategoryId={selectedCategoryId}
      selectedCategoryObj={selectedCategoryObj}
      currentCategoryProducts={CategoryStore.currentCategoryProducts}
      hasOrderChanges={categoriesOrder.hasOrderChanges}
      sensors={sensors}
      formatImageUrl={formatImageUrl}
      SortableEntityRow={SortableEntityRow}
      onOpenModal={handleOpenModal}
      onSelectCategory={setSelectedCategoryId}
      onDeleteCategory={handleDeleteCategory}
      onDeleteProduct={handleDeleteProduct}
      onDragEnd={categoriesOrder.handleDragEnd}
      onSaveOrder={handleSaveOrder}
      onCancelOrder={handleCancelOrder}
    />
  );

  const productsTabContent = (
    <ProductsTab
      t={t}
      isEn={isEn}
      localProducts={localProducts}
      categories={CategoryStore.categories}
      hasOrderChanges={productsOrder.hasOrderChanges}
      sensors={sensors}
      formatImageUrl={formatImageUrl}
      SortableEntityRow={SortableEntityRow}
      onOpenModal={handleOpenModal}
      onDeleteProduct={handleDeleteProduct}
      onDragEnd={productsOrder.handleDragEnd}
      onSaveOrder={handleSaveOrder}
      onCancelOrder={handleCancelOrder}
    />
  );

  const tabItems = [
    { key: "categories", label: t("ADMIN_PAGE.TAB_CATEGORIES", "Категорії"), children: categoriesTabContent },
    { key: "products", label: t("ADMIN_PAGE.TAB_PRODUCTS", "Продукти"), children: productsTabContent },
  ];

  return (
    <Layout className={styles.adminContainer}>
      <Content className={styles.adminCard}>
        <h2>{t("ADMIN_PAGE.TITLE", "Адмін-панель Sushi Market")}</h2>
        <Tabs activeKey={activeTab} onChange={setActiveTab} style={{ marginTop: "16px" }} items={tabItems} />

        <EntityModal
          isOpen={modalConfig.isOpen}
          type={modalConfig.type}
          editingItem={modalConfig.item}
          categories={CategoryStore.categories}
          currentLang={currentLang}
          onClose={handleCloseModal}
          onSave={handleSaveEntity}
        />
      </Content>
    </Layout>
  );
});

export default AdminPage;