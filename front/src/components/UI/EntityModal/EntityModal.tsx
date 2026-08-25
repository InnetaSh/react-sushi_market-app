import React, { useEffect, useState } from "react";
import { observer } from "mobx-react-lite";
import { Modal, Form, Input, Upload, Select } from "antd";
import { CameraOutlined } from "@ant-design/icons";
import { useTranslation } from "react-i18next";
import CancelBtn from '@img/utils/Cancel_btn.svg';
import TextArea from "antd/es/input/TextArea";
import ButtonOrangeBrdr from "@UI/ButtonOrangeBrdr/ButtonOrangeBrdr";
import styles from "./EntityModal.module.scss";

interface EntityModalProps {
  isOpen: boolean;
  onClose: () => void;
  onSave: (values: any) => Promise<void> | void;
  editingItem: any;
  type?: "category" | "product";
  currentLang?: string;
  categories?: any[];
}

export const EntityModal: React.FC<EntityModalProps> = observer(({
  isOpen,
  onClose,
  onSave,
  editingItem,
  type = "product",
  currentLang = "ua",
  categories = [],
}) => {
  const { t } = useTranslation();
  const [form] = Form.useForm();
  const [loading, setLoading] = useState(false);
  const [fileList, setFileList] = useState<any[]>([]);

  const isEn = currentLang === "en";

  useEffect(() => {
    if (isOpen) {
      if (editingItem) {
        form.setFieldsValue({
          title: isEn ? (editingItem.titleEn || editingItem.title || "") : (editingItem.titleUa || editingItem.title || ""),
          description: isEn ? (editingItem.descriptionEn || editingItem.description || "") : (editingItem.descriptionUa || editingItem.description || ""),
          price: editingItem.price || "",
          weightOrVolume: editingItem.weightOrVolume || "",
          categoryId: editingItem.categoryId || undefined,
        });

        if (editingItem.imgSrc) {
          setFileList([{ uid: '-1', name: 'image.png', status: 'done', url: editingItem.imgSrc }]);
        } else {
          setFileList([]);
        }
      } else {
        form.resetFields();
        setFileList([]);
      }
    }
  }, [isOpen, editingItem, form, isEn]);

  const handleUploadChange = ({ fileList: newFileList }: any) => {
    setFileList(newFileList);
  };

  const onSuccessfulSubmit = async (values: any) => {
    setLoading(true);
    try {
      const payload = {
        ...values,
        imageFile: fileList[0]?.originFileObj || null,
      };

      await onSave(payload);
      onClose();
    } catch (e) {
      console.error(e);
    } finally {
      setLoading(false);
    }
  };

  const currentPreviewUrl = fileList[0]?.originFileObj
    ? URL.createObjectURL(fileList[0].originFileObj)
    : fileList[0]?.url;

  return (
    <Modal
      className={styles.addModal}
      open={isOpen}
      onCancel={onClose}
      footer={null}
      closable={true}
      width={500}
      closeIcon={<img src={CancelBtn} alt="close" className={styles.closeIcon} />}
    >
      <Form form={form} onFinish={onSuccessfulSubmit} layout="vertical">

        <Form.Item label={t("MODAL.LABEL_IMAGE", "Зображення")}>
          <Upload
            beforeUpload={() => false}
            fileList={fileList}
            onChange={handleUploadChange}
            maxCount={1}
            showUploadList={false}
          >
            <div className={styles.uploadContainer}>
              {currentPreviewUrl ? (
                <>
                  <img
                    src={currentPreviewUrl}
                    alt="art"
                    className={styles.previewImage}
                  />
                  <div className={styles.uploadOverlay}>
                    <CameraOutlined className={styles.overlayIcon} />
                    <span className={styles.overlayText}>
                      {t("MODAL.CHANGE_PHOTO", "Змінити фото")}
                    </span>
                  </div>
                </>
              ) : (
                <div className={styles.uploadPlaceholder}>
                  <CameraOutlined className={styles.placeholderIcon} />
                  <div>{t("MODAL.UPLOAD_IMAGE", "Завантажити зображення")}</div>
                </div>
              )}
            </div>
          </Upload>
        </Form.Item>

        <Form.Item
          name="title"
          label={t("MODAL.LABEL_TITLE", "Назва")}
          rules={[{ required: true, message: t("MODAL.ERROR_TITLE", "Введіть назву") }]}
        >
          <Input showCount maxLength={150} placeholder={t("MODAL.PLACEHOLDER_TITLE", "Введіть назву...")} />
        </Form.Item>

        {type === "product" && (
          <Form.Item name="description" label={t("MODAL.LABEL_DESCRIPTION", "Опис")}>
            <TextArea rows={4} maxLength={400} showCount placeholder={t("MODAL.PLACEHOLDER_DESCRIPTION", "Введіть опис...")} />
          </Form.Item>
        )}

        {type === "product" && (
          <>
            <Form.Item
              name="categoryId"
              label={t("MODAL.LABEL_CATEGORY", "Категорія")}
              rules={[{ required: true, message: t("MODAL.ERROR_CATEGORY", "Будь ласка, оберіть категорію!") }]}
            >
              <Select
                placeholder={t("MODAL.PLACEHOLDER_CATEGORY", "Оберіть категорію")}
                popupClassName={styles.selectDropdownPopup}
                options={categories.map((cat: any) => {
                  const labelText = isEn ? (cat.titleEn || cat.title) : (cat.titleUa || cat.title);
                  return {
                    value: cat.id,
                    label: <span style={{ color: 'var(--color-text-dark, #444444)', opacity: 1, fontWeight: 400 }}>{labelText}</span>,
                  };
                })}
              />
            </Form.Item>

            <Form.Item name="price" label={t("MODAL.LABEL_PRICE", "Ціна (грн)")}>
              <Input placeholder={t("MODAL.PLACEHOLDER_PRICE", "Введіть ціну...")} />
            </Form.Item>

            <Form.Item name="weightOrVolume" label={t("MODAL.LABEL_WEIGHT", "Вага / Об'єм")}>
              <Input placeholder={t("MODAL.PLACEHOLDER_WEIGHT", "Наприклад: 250 г")} />
            </Form.Item>
          </>
        )}

        <div className={styles.buttonContainer}>
          <ButtonOrangeBrdr
            className={styles.saveButton}
            text={t("MODAL.BTN_SAVE", "Зберегти")}
            onClick={() => form.submit()}
            loading={loading}
          />
        </div>
      </Form>
    </Modal>
  );
});

export default EntityModal;