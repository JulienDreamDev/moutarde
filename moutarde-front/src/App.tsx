import { useState, useEffect } from "react";
import { useTranslation } from "react-i18next";

function App() {
  const { t, i18n } = useTranslation();
  const [theme, setTheme] = useState<"light" | "dark">("light");

  useEffect(() => {
    const savedTheme = localStorage.getItem("theme") as "light" | "dark" | null;
    if (savedTheme) {
      setTheme(savedTheme);
      document.documentElement.classList.toggle("dark", savedTheme === "dark");
    } else {
      const prefersDark = window.matchMedia(
        "(prefers-color-scheme: dark)",
      ).matches;
      setTheme(prefersDark ? "dark" : "light");
      document.documentElement.classList.toggle("dark", prefersDark);
    }
  }, []);

  const toggleTheme = () => {
    const newTheme = theme === "light" ? "dark" : "light";
    setTheme(newTheme);
    document.documentElement.classList.toggle("dark", newTheme === "dark");
    localStorage.setItem("theme", newTheme);
  };

  return (
    <div className="flex min-h-screen flex-col items-center justify-center gap-6">
      <h1>{t("common.welcome")}</h1>
      <button
        className="bg-moutarde-600 hover:bg-moutarde-500 rounded-lg px-8 py-3 font-semibold text-white transition-colors"
        onClick={toggleTheme}
      >
        〽️ - Moutarde
      </button>
    </div>
  );
}

export default App;
