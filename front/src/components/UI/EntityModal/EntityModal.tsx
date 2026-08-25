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
      closeIcon={<img src={CancelBtn} alt="close" style={{ width: '16px', height: '16px' }} />}
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
            <div 
              style={{
                position: 'relative',
                width: '100%',
                height: '200px',
                borderRadius: '8px',
                overflow: 'hidden',
                cursor: 'pointer',
                backgroundColor: '#f5f5f5',
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'center',
                border: '1px dashed #d9d9d9'
              }}
            >
              {currentPreviewUrl ? (
                <>
                  <img
                    src={currentPreviewUrl}
                    alt="art"
                    style={{ width: '100%', height: '100%', objectFit: 'cover' }}
                  />
                  <div 
                    style={{
                      position: 'absolute',
                      top: 0,
                      left: 0,
                      width: '100%',
                      height: '100%',
                      backgroundColor: 'rgba(0, 0, 0, 0.4)',
                      color: '#fff',
                      display: 'flex',
                      flexDirection: 'column',
                      alignItems: 'center',
                      justifyContent: 'center',
                      opacity: 0,
                      transition: 'opacity 0.2s ease-in-out',
                    }}
                    onMouseEnter={(e) => (e.currentTarget.style.opacity = '1')}
                    onMouseLeave={(e) => (e.currentTarget.style.opacity = '0')}
                  >
                    <CameraOutlined style={{ fontSize: '28px', marginBottom: '6px' }} />
                    <span style={{ fontSize: '14px', fontWeight: 500 }}>
                      {t("MODAL.CHANGE_PHOTO", "Змінити фото")}
                    </span>
                  </div>
                </>
              ) : (
                <div style={{ textAlign: 'center', color: '#8c8c8c' }}>
                  <CameraOutlined style={{ fontSize: '24px', marginBottom: '6px' }} />
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
                options={categories.map((cat: any) => ({
                  value: cat.id,
                  label: isEn ? (cat.titleEn || cat.title) : (cat.titleUa || cat.title),
                }))}
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