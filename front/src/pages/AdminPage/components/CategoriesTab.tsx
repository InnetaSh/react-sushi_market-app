import React from "react";
import { Button } from "antd";
import { DndContext, closestCenter } from "@dnd-kit/core";
import { SortableContext, verticalListSortingStrategy } from "@dnd-kit/sortable";
import { EntityRow } from "@UI/EntityRow/EntityRow";
import ButtonOrangeBrdr from "@UI/ButtonOrangeBrdr/ButtonOrangeBrdr";
import styles from "../AdminPage.module.scss";

interface CategoriesTabProps {
  t: any;
  isEn: boolean;
  localCategories: any[];
  selectedCategoryId: number | null;
  selectedCategoryObj: any;
  currentCategoryProducts: any[];
  hasOrderChanges: boolean;
  sensors: any;
  formatImageUrl: (item: any) => string | undefined;
  SortableEntityRow: React.FC<{ id: number | string; children: React.ReactNode }>;
  onOpenModal: (type: "category" | "product", item?: any) => void;
  onSelectCategory: (id: number | null) => void;
  onDeleteCategory: (id: number) => void;
  onDeleteProduct: (id: number) => void;
  onDragEnd: (event: any) => void;
  onSaveOrder: () => void;
  onCancelOrder: () => void;
}

export const CategoriesTab: React.FC<CategoriesTabProps> = ({
  t,
  isEn,
  localCategories,
  selectedCategoryId,
  selectedCategoryObj,
  currentCategoryProducts,
  hasOrderChanges,
  sensors,
  formatImageUrl,
  SortableEntityRow,
  onOpenModal,
  onSelectCategory,
  onDeleteCategory,
  onDeleteProduct,
  onDragEnd,
  onSaveOrder,
  onCancelOrder,
}) => {
  return (
    <div>
      <div className={styles.addContainer} style={{ display: "flex", justifyContent: "space-between", alignItems: "center" }}>
        <Button type="link" className={styles.addLink} onClick={() => onOpenModal("category")}>
          {t("ADMIN_PAGE.BTN_ADD_CATEGORY", "+ Додати категорію")}
        </Button>
        {hasOrderChanges && (
          <div style={{ display: "flex", gap: "8px", alignItems: "center" }}>
            <Button type="link" className={styles.cancelBtn} onClick={onCancelOrder}>
              {t("ADMIN_PAGE.BTN_CANCEL_ORDER", "Скасувати")}
            </Button>
            <ButtonOrangeBrdr onClick={onSaveOrder} text={t("ADMIN_PAGE.BTN_SAVE_ORDER", "Зберегти порядок")} />
          </div>
        )}
      </div>

      <DndContext sensors={sensors} collisionDetection={closestCenter} onDragEnd={onDragEnd}>
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
                      onClick={() => onSelectCategory(isSelected ? null : cat.id)}
                      onEdit={() => onOpenModal("category", cat)}
                      onDelete={() => onDeleteCategory(cat.id)}
                    />
                  </SortableEntityRow>

                  {isSelected && (
                    <div className={styles.nestedSubProducts}>
                      <div className={styles.subProductsHeader}>
                        <h3>
                          {t("ADMIN_PAGE.PRODUCTS_OF_CATEGORY", "Продукти категорії:")}{" "}
                          {(isEn ? selectedCategoryObj?.titleEn : selectedCategoryObj?.titleUa) || selectedCategoryObj?.title}
                        </h3>
                        <Button type="link" className={styles.addLink} onClick={() => onOpenModal("product")}>
                          {t("ADMIN_PAGE.BTN_ADD_PRODUCT_TO_CAT", "+ Додати продукт у категорію")}
                        </Button>
                      </div>

                      <div className={styles.listContainer}>
                        {currentCategoryProducts.length > 0 ? (
                          currentCategoryProducts.map((prod: any) => (
                            <EntityRow
                              key={prod.id}
                              sortOrder={prod.sortOrder}
                              imgSrc={formatImageUrl(prod)}
                              title={(isEn ? prod.titleEn : prod.titleUa) || prod.title}
                              subtitle={(isEn ? prod.descriptionEn : prod.descriptionUa) || prod.description}
                              price={prod.price}
                              weight={prod.weightOrVolume}
                              onEdit={() => onOpenModal("product", prod)}
                              onDelete={() => onDeleteProduct(prod.id)}
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
};