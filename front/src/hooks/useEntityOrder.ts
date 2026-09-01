import { useState } from "react";
import { DragEndEvent } from "@dnd-kit/core";
import { arrayMove } from "@dnd-kit/sortable";
import { message } from "antd";

export const useEntityOrder = (
  items: any[],
  setItems: React.Dispatch<React.SetStateAction<any[]>>,
  reorderApi: (id: number, sortOrder: number) => Promise<any>,
  refreshData: () => Promise<void>
) => {
  const [hasOrderChanges, setHasOrderChanges] = useState(false);
  const [pendingMovedItems, setPendingMovedItems] = useState<Map<number, number>>(new Map());
  const [previousItems, setPreviousItems] = useState<any[]>([]);

  const calculateNewSortOrder = (currentItems: any[], oldIndex: number, newIndex: number) => {
    const targetItem = currentItems[oldIndex];
    const reordered = arrayMove(currentItems, oldIndex, newIndex);
    
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

  const handleDragEnd = (event: DragEndEvent) => {
    const { active, over } = event;
    if (!over || active.id === over.id) return;

    const oldIndex = items.findIndex((item) => item.id === active.id);
    const newIndex = items.findIndex((item) => item.id === over.id);

    if (oldIndex !== -1 && newIndex !== -1) {
      setPreviousItems([...items]);
      const { reordered, newSortOrder } = calculateNewSortOrder(items, oldIndex, newIndex);
      
      setItems(reordered);
      
      const itemId = Number(active.id);
      setPendingMovedItems((prev) => new Map(prev).set(itemId, newSortOrder));
      setHasOrderChanges(true);
    }
  };

  const handleSaveOrder = async (successMsg: string, errorMsg: string) => {
    try {
      const promises = Array.from(pendingMovedItems.entries()).map(([id, sortOrder]) =>
        reorderApi(id, sortOrder)
      );

      await Promise.all(promises);
      message.success(successMsg);
      
      setHasOrderChanges(false);
      setPendingMovedItems(new Map());
      await refreshData();
    } catch (e) {
      console.error(e);
      message.error(errorMsg);
    }
  };

  const handleCancelOrder = () => {
    if (previousItems.length > 0) {
      setItems(previousItems);
    }
    setPendingMovedItems(new Map());
    setHasOrderChanges(false);
  };

  return {
    hasOrderChanges,
    handleDragEnd,
    handleSaveOrder,
    handleCancelOrder,
  };
};