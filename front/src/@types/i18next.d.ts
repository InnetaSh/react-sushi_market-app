import "i18next";

declare module "i18next" {
  interface CustomTypeOptions {
    defaultNS: "translation";
    resources: {
      translation: {
        PAGE_1_TEXT: {
          TITLE_1: string;
          TITLE_2: string;
          TITLE_3: string;
          BTN_ACTIONS: string;
          RESTAURANTS_TITLE: string;
          RESTAURANTS_DESC_1: string;
          RESTAURANTS_DESC_2: string;
          BTN_READ_MORE: string;
          BOTTOM_MSG: string;
          BOTTOM_DESC: string;
        };
        PAGE_3_TEXT: {
          SALE_1_DATE: string;
          SALE_1_NAME: string;
          SALE_2_DATE: string;
          SALE_2_NAME: string;
        };
        UI_TEXT: {
          LOADING: string;
        };
        HEADER: {
          PHONE_LABEL: string;
          PHONE_NUMBER: string;
        };
      };
    };
  }
}