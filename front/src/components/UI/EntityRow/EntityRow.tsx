import React from "react";
import { Button, Popconfirm } from "antd";
import { useTranslation } from "react-i18next";
import styles from "./EntityRow.module.scss";

interface EntityRowProps {
  sortOrder?: number;
  imgSrc?: string;
  title: string;
  subtitle?: string;
  categoryName?: string;
  price?: string | number;
  weight?: string;
  isSelected?: boolean;
  onClick?: () => void;
  onEdit: () => void;
  onDelete: () => void;
}

export const EntityRow: React.FC<EntityRowProps> = ({
  sortOrder,
  imgSrc,
  title,
  subtitle,
  categoryName,
  price,
  weight,
  isSelected,
  onClick,
  onEdit,
  onDelete,
}) => {
  const { t } = useTranslation();

  return (
    <div
      className={`${styles.row} ${isSelected ? styles.selectedRow : ""}`}
      onClick={onClick}
    >
      {sortOrder !== undefined && <div className={styles.colNumber}>{sortOrder}</div>}
      
      {imgSrc && (
        <div className={styles.colImage}>
          <img src={imgSrc} alt="" className={styles.img} />
        </div>
      )}

      <div className={styles.colInfo}>
        <span className={styles.itemTitle}>{title}</span>
        {subtitle && <span className={styles.itemDescription}>{subtitle}</span>}
      </div>

      {categoryName && <div className={styles.colCategory}>{categoryName}</div>}

      {(price !== undefined || weight) && (
        <div className={styles.colPrice}>
          {price !== undefined && (
            <span className={styles.price}>
              {price} {t("ENTITY_ROW.CURRENCY", "грн")}
            </span>
          )}
          {weight && <span className={styles.weight}>{weight}</span>}
        </div>
      )}

      <div className={styles.colActions} onClick={(e) => e.stopPropagation()}>
        <Button type="link" className={styles.actionLink} onClick={onEdit}>
          {t("ENTITY_ROW.EDIT", "Змінити")}
        </Button>
        <Popconfirm
          title={t("ENTITY_ROW.DELETE_TITLE", "Видалити?")}
          description={t("ENTITY_ROW.DELETE_DESC", "Цю дію неможливо скасувати.")}
          onConfirm={onDelete}
          okText={t("ENTITY_ROW.YES", "Так")}
          cancelText={t("ENTITY_ROW.NO", "Ні")}
        >
          <Button type="link" className={styles.actionLink}>
            {t("ENTITY_ROW.DELETE", "Видалити")}
          </Button>
        </Popconfirm>
      </div>
    </div>
  );
};