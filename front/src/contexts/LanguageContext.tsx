import { createContext, useContext, useEffect, useState, ReactNode } from "react";
import { i18n as I18nextInstance } from "i18next";
import i18nConfig from "../i18n";


interface LanguageContextType {
  language: string;
  setLanguage: (lang: string) => void;
}

const LanguageContext = createContext<LanguageContextType | null>(null);

interface LanguageProviderProps {
  children: ReactNode;
}

export function LanguageProvider({ children }: LanguageProviderProps) {
 const i18n = i18nConfig as any;
  const [language, setLanguage] = useState<string>(() => {
    return localStorage.getItem("lang") || "uk";
  });

useEffect(() => {
  if (i18n && typeof i18n.changeLanguage === 'function') {
    i18n.changeLanguage(language);
  }
  localStorage.setItem("lang", language);
}, [language]);

  const value = { language, setLanguage };

  return (
    <LanguageContext.Provider value={value}>
      {children}
    </LanguageContext.Provider>
  );
}

export function useLanguage() {
  const context = useContext(LanguageContext);
  if (!context) {
    throw new Error("useLanguage must be used within LanguageProvider");
  }
  return context;
}