import React, { useEffect, useState } from "react";
import { observer } from "mobx-react-lite";
import { Layout, message, Button, Tabs } from "antd";
import { useTranslation } from "react-i18next";
import {
  DndContext,
  closestCenter,
  KeyboardSensor,
  PointerSensor,
  useSensor,
  useSensors,
  DragEndEvent,
} from "@dnd-kit/core";
import {
  arrayMove,
  SortableContext,
  sortableKeyboardCoordinates,
  verticalListSortingStrategy,
  useSortable,
} from "@dnd-kit/sortable";
import { CSS } from "@dnd-kit/utilities";

import CategoryStore from "@stores/CategoryStore";
import ProductStore from "@stores/ProductStore";
import CategoryApi from "@api/CategoryApi";
import ProductApi from "@api/ProductApi";
import { EntityModal } from "@UI/EntityModal/EntityModal";
import { EntityRow } from "@UI/EntityRow/EntityRow";
import ButtonOrangeBrdr  from "@UI/ButtonOrangeBrdr/ButtonOrangeBrdr";
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

  const [hasOrderChanges, setHasOrderChanges] = useState(false);
  const [pendingMovedItems, setPendingMovedItems] = useState<Map<number, number>>(new Map());

  const [previousCategories, setPreviousCategories] = useState<any[]>([]);
  const [previousProducts, setPreviousProducts] = useState<any[]>([]);

  const sensors = useSensors(
    useSensor(PointerSensor, {
      activationConstraint: {
        distance: 5,
      },
    }),
    useSensor(KeyboardSensor, {
      coordinateGetter: sortableKeyboardCoordinates,
    })
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

  const calculateNewSortOrder = (items: any[], oldIndex: number, newIndex: number) => {
    const targetItem = items[oldIndex];
    const reordered = arrayMove(items, oldIndex, newIndex);
    
    const prevItem = reordered[newIndex - 1];
    const nextItem = reordered[newIndex + 1];

    let newSortOrder = targetItem.sortOrder;

    if (prevItem && nextItem) {
      newSortOrder = (Number(prevItem.sortOrder) + Number(nextItem.sortOrder)) / 2;
    } else if (prevItem) {
      newSortOrder = Number(prevItem.sortOrder) + 10;
    } else if (nextItem) {
      newSortOrder = Number(nextItem.sortOrder) / 2 || 1;
    }

    return { reordered, newSortOrder };
  };

  const handleDragEndCategories = (event: DragEndEvent) => {
    const { active, over } = event;
    if (!over || active.id === over.id) return;

    const oldIndex = localCategories.findIndex((c) => c.id === active.id);
    const newIndex = localCategories.findIndex((c) => c.id === over.id);

    if (oldIndex !== -1 && newIndex !== -1) {
      setPreviousCategories([...localCategories]);

      const { reordered, newSortOrder } = calculateNewSortOrder(localCategories, oldIndex, newIndex);
      
      setLocalCategories(reordered);
      
      const itemId = Number(active.id);
      setPendingMovedItems((prev) => new Map(prev).set(itemId, newSortOrder));
      setHasOrderChanges(true);
    }
  };

  const handleSaveOrder = async () => {
    try {
      const promises = Array.from(pendingMovedItems.entries()).map(async ([id, sortOrder]) => {
        if (activeTab === "categories") {
          return CategoryApi.reorderCategory(id, sortOrder);
        } else {
          return ProductApi.reorderProduct(id, sortOrder);
        }
      });

      await Promise.all(promises);
      message.success(t("ADMIN_PAGE.SUCCESS_ORDER_UPDATE", "Порядок успішно збережено"));
      
      setHasOrderChanges(false);
      setPendingMovedItems(new Map());

      await CategoryStore.fetchCategories();
      await ProductStore.fetchProducts();
      if (selectedCategoryId) {
        await CategoryStore.fetchCategoryWithProducts(selectedCategoryId);
      }
    } catch (e) {
      console.error(e);
      message.error(t("ADMIN_PAGE.ERROR_SAVE", "Помилка збереження порядку"));
    }
  };

  const handleDragEndProducts = (event: DragEndEvent) => {
    const { active, over } = event;
    if (!over || active.id === over.id) return;

    const oldIndex = localProducts.findIndex((p) => p.id === active.id);
    const newIndex = localProducts.findIndex((p) => p.id === over.id);

    if (oldIndex !== -1 && newIndex !== -1) {
      setPreviousProducts([...localProducts]);

      const { reordered, newSortOrder } = calculateNewSortOrder(localProducts, oldIndex, newIndex);

      setLocalProducts(reordered);

      const itemId = Number(active.id);
      setPendingMovedItems((prev) => new Map(prev).set(itemId, newSortOrder));
      setHasOrderChanges(true);
    }
  };

  const handleCancelOrder = () => {
    if (activeTab === "categories" && previousCategories.length > 0) {
      setLocalCategories(previousCategories);
    } else if (activeTab === "products" && previousProducts.length > 0) {
      setLocalProducts(previousProducts);
    }
    
    setPendingMovedItems(new Map());
    setHasOrderChanges(false);
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
    <div>
      <div className={styles.addContainer} style={{ display: "flex", justifyContent: "space-between", alignItems: "center" }}>
        <Button type="link" className={styles.addLink} onClick={() => handleOpenModal("category")}>
          {t("ADMIN_PAGE.BTN_ADD_CATEGORY", "+ Додати категорію")}
        </Button>
        {hasOrderChanges && (
          <div style={{ display: "flex", gap: "8px" }}>
            <Button 
            type="link"
            className={styles.cancelBtn}
            onClick={handleCancelOrder}>
              {t("ADMIN_PAGE.BTN_CANCEL_ORDER", "Скасувати")}
            </Button>
            <ButtonOrangeBrdr 
              onClick={handleSaveOrder} 
              text={t("ADMIN_PAGE.BTN_SAVE_ORDER", "Зберегти порядок")} 
            />
          </div>
        )}
      </div>

      <DndContext sensors={sensors} collisionDetection={closestCenter} onDragEnd={handleDragEndCategories}>
        <SortableContext items={localCategories.map((c) => c.id)} strategy={verticalListSortingStrategy}>
          <div className={styles.listContainer}>
            {localCategories.map((cat: any) => {
              const isSelected = selectedCategoryId === cat.id;

              return (
                <React.Fragment key={cat.id}>
                  <SortableEntityRow id={cat.id}>
                    <EntityRow
                      sortOrder={cat.sortOrder}
                      imgSrc={formatImageUrl(cat)}
                      title={(isEn ? cat.titleEn : cat.titleUa) || cat.title}
                      isSelected={isSelected}
                      onClick={() => setSelectedCategoryId(isSelected ? null : cat.id)}
                      onEdit={() => handleOpenModal("category", cat)}
                      onDelete={() => handleDeleteCategory(cat.id)}
                    />
                  </SortableEntityRow>

                  {isSelected && (
                    <div className={styles.nestedSubProducts}>
                      <div className={styles.subProductsHeader}>
                        <h3>
                          {t("ADMIN_PAGE.PRODUCTS_OF_CATEGORY", "Продукти категорії:")}{" "}
                          {(isEn ? selectedCategoryObj?.titleEn : selectedCategoryObj?.titleUa) || selectedCategoryObj?.title}
                        </h3>
                        <Button type="link" className={styles.addLink} onClick={() => handleOpenModal("product")}>
                          {t("ADMIN_PAGE.BTN_ADD_PRODUCT_TO_CAT", "+ Додати продукт у категорію")}
                        </Button>
                      </div>

                      <div className={styles.listContainer}>
                        {CategoryStore.currentCategoryProducts.length > 0 ? (
                          CategoryStore.currentCategoryProducts.map((prod: any) => (
                            <EntityRow
                              key={prod.id}
                              sortOrder={prod.sortOrder}
                              imgSrc={formatImageUrl(prod)}
                              title={(isEn ? prod.titleEn : prod.titleUa) || prod.title}
                              subtitle={(isEn ? prod.descriptionEn : prod.descriptionUa) || prod.description}
                              price={prod.price}
                              weight={prod.weightOrVolume}
                              onEdit={() => handleOpenModal("product", prod)}
                              onDelete={() => handleDeleteProduct(prod.id)}
                            />
                          ))
                        ) : (
                          <div style={{ padding: "12px 0", color: "#8c8c8c", fontSize: "14px" }}>
                            {t("ADMIN_PAGE.NO_PRODUCTS_IN_CATEGORY", "У цій категорії поки немає продуктів")}
                          </div>
                        )}
                      </div>
                    </div>
                  )}
                </React.Fragment>
              );
            })}
          </div>
        </SortableContext>
      </DndContext>
    </div>
  );

  const productsTabContent = (
    <div>
      <div className={styles.addContainer} style={{ display: "flex", justifyContent: "space-between", alignItems: "center" }}>
        <Button type="link" className={styles.addLink} onClick={() => handleOpenModal("product")}>
          {t("ADMIN_PAGE.BTN_ADD_PRODUCT", "+ Додати продукт")}
        </Button>
        {hasOrderChanges && (
          <div style={{ display: "flex", gap: "8px" }}>
            <Button onClick={handleCancelOrder}>
              {t("ADMIN_PAGE.BTN_CANCEL_ORDER", "Скасувати")}
            </Button>
            <ButtonOrangeBrdr 
              onClick={handleSaveOrder} 
              text={t("ADMIN_PAGE.BTN_SAVE_ORDER", "Зберегти порядок")} 
            />
          </div>
        )}
      </div>

      <DndContext sensors={sensors} collisionDetection={closestCenter} onDragEnd={handleDragEndProducts}>
        <SortableContext items={localProducts.map((p) => p.id)} strategy={verticalListSortingStrategy}>
          <div className={styles.listContainer}>
            {localProducts.map((prod: any) => {
              const category = CategoryStore.categories.find((c: any) => c.id === prod.categoryId);
              const catName = category ? (isEn ? category.titleEn : category.titleUa) || category.title : "—";

              return (
                <SortableEntityRow key={prod.id} id={prod.id}>
                  <EntityRow
                    sortOrder={prod.sortOrder}
                    imgSrc={formatImageUrl(prod)}
                    title={(isEn ? prod.titleEn : prod.titleUa) || prod.title}
                    subtitle={(isEn ? prod.descriptionEn : prod.descriptionUa) || prod.description}
                    categoryName={catName}
                    price={prod.price}
                    weight={prod.weightOrVolume}
                    onEdit={() => handleOpenModal("product", prod)}
                    onDelete={() => handleDeleteProduct(prod.id)}
                  />
                </SortableEntityRow>
              );
            })}
          </div>
        </SortableContext>
      </DndContext>
    </div>
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