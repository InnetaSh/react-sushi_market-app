import React from "react";
import { Button } from "antd";
import { DndContext, closestCenter } from "@dnd-kit/core";
import { SortableContext, verticalListSortingStrategy } from "@dnd-kit/sortable";
import { EntityRow } from "@UI/EntityRow/EntityRow";
import ButtonOrangeBrdr from "@UI/ButtonOrangeBrdr/ButtonOrangeBrdr";
import styles from "../AdminPage.module.scss";

interface ProductsTabProps {
  t: any;
  isEn: boolean;
  localProducts: any[];
  categories: any[];
  hasOrderChanges: boolean;
  sensors: any;
  formatImageUrl: (item: any) => string | undefined;
  SortableEntityRow: React.FC<{ id: number | string; children: React.ReactNode }>;
  onOpenModal: (type: "category" | "product", item?: any) => void;
  onDeleteProduct: (id: number) => void;
  onDragEnd: (event: any) => void;
  onSaveOrder: () => void;
  onCancelOrder: () => void;
}

export const ProductsTab: React.FC<ProductsTabProps> = ({
  t,
  isEn,
  localProducts,
  categories,
  hasOrderChanges,
  sensors,
  formatImageUrl,
  SortableEntityRow,
  onOpenModal,
  onDeleteProduct,
  onDragEnd,
  onSaveOrder,
  onCancelOrder,
}) => {
  return (
    <div>
      <div className={styles.addContainer} style={{ display: "flex", justifyContent: "space-between", alignItems: "center" }}>
        <Button type="link" className={styles.addLink} onClick={() => onOpenModal("product")}>
          {t("ADMIN_PAGE.BTN_ADD_PRODUCT", "+ Додати продукт")}
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
        <SortableContext items={localProducts.map((p) => p.id)} strategy={verticalListSortingStrategy}>
          <div className={styles.listContainer}>
            {localProducts.map((prod: any) => {
              const category = categories.find((c: any) => c.id === prod.categoryId);
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
                    onEdit={() => onOpenModal("product", prod)}
                    onDelete={() => onDeleteProduct(prod.id)}
                  />
                </SortableEntityRow>
              );
            })}
          </div>
        </SortableContext>
      </DndContext>
    </div>
  );
};