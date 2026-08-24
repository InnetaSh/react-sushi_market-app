import React, { useEffect, useState } from "react";
import { observer } from "mobx-react-lite";
import { Modal, Form, Input } from "antd";
import CancelBtn from '@img/utils/Cancel_btn.svg';
import FormItem from "antd/es/form/FormItem";
import TextArea from "antd/es/input/TextArea";
import ButtonOrangeBrdr from "@UI/ButtonOrangeBrdr/ButtonOrangeBrdr"; // Укажите верный путь к вашей кнопке
import styles from "./EntityModal.module.scss";

interface EntityModalProps {
  isOpen: boolean;
  onClose: () => void;
  onSave: (values: any) => Promise<void> | void;
  editingItem: any; // Сюда передается объект данных, у которого есть imgSrc (или url) и текст
  type?: "category" | "product";
}

export const EntityModal = observer(({
  isOpen,
  onClose,
  onSave,
  editingItem,
  type = "product",
}) => {
  const [form] = Form.useForm();
  const [loading, setLoading] = useState(false);

  const isEdit = !!editingItem;

  useEffect(() => {
    if (isOpen) {
      if (editingItem) {
        form.setFieldsValue({
          title: editingItem.title || "",
          description: editingItem.description || "",
        });
      } else {
        form.resetFields();
      }
    }
  }, [isOpen, editingItem, form]);

  const onSuccessfulSubmit = async (values: any) => {
    setLoading(true);
    try {
      await onSave({
        ...editingItem,
        ...values,
      });
      onClose();
    } catch (e) {
      console.error(e);
    } finally {
      setLoading(false);
    }
  };

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
      {/* <h2>Додаткові дані</h2> */}

      {editingItem?.imgSrc && (
        <img
          src={editingItem.imgSrc}
          alt="art"
          style={{ width: '100%', marginBottom: '20px', borderRadius: '8px', objectFit: 'cover' }}
        />
      )}

      <Form form={form} onFinish={onSuccessfulSubmit} layout="vertical">
        <FormItem 
          name="title" 
          label="Назва"
          rules={[{ required: true, message: 'Введіть назву' }]}
        >
          <Input showCount maxLength={150} placeholder="Введіть назву..." />
        </FormItem>

        <FormItem name="description" label="Опис">
          <TextArea rows={4} maxLength={400} showCount placeholder="Введіть опис..." />
        </FormItem>

        <div className={styles.buttonContainer}>
          <ButtonOrangeBrdr
            className={styles.saveButton}
            text="Зберегти"
            onClick={() => form.submit()}
            loading={loading} 
          />
        </div>
      </Form>
    </Modal>
  );
});

export default EntityModal;