import i18n from "i18next";
import { initReactI18next } from "react-i18next";
import en from "./locales/en.json";
import fr from "./locales/fr.json";

const savedLanguage = localStorage.getItem("language");
const navigatorLanguage = navigator.language.split("-")[0];
const defaultLanguage = savedLanguage || navigatorLanguage || "en";

i18n.use(initReactI18next).init({
  resources: {
    en: { translation: en },
    fr: { translation: fr },
  },
  lng: defaultLanguage,
  fallbackLng: "en",
  interpolation: {
    escapeValue: false,
  },
});

export default i18n;
