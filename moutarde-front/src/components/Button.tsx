import React from "react";
import clsx from "clsx";
import { LoaderCircle } from "lucide-react";

export interface ButtonProps {
  variant?: "primary" | "secondary" | "danger" | "ghost";
  size?: "sm" | "md" | "lg";
  disabled?: boolean;
  loading?: boolean;
  onClick?: (event: React.MouseEvent<HTMLElement>) => void;
  children?: React.ReactNode;
  className?: string;
}

const Button: React.FC<ButtonProps> = ({
  variant = "primary",
  size = "md",
  disabled = false,
  loading = false,
  onClick,
  children,
  className,
}) => {
  const style = clsx(
    "flex justify-center gap-2 rounded-lg transition-colors active:scale-98",
    variant === "primary" && "bg-moutarde-600 hover:bg-moutarde-700 text-white",
    variant === "secondary" &&
      "bg-neutral-600 font-semibold text-white hover:bg-neutral-700",
    variant === "danger" && "bg-ketchup-500 hover:bg-ketchup-600 text-white",
    variant === "ghost" && "hover:underline",
    size === "sm" && "px-2 text-sm",
    size === "md" && "px-4 py-2 text-base font-semibold",
    size === "lg" && "px-6 py-3 text-lg font-semibold",
    disabled || loading ? "cursor-not-allowed opacity-50" : "cursor-pointer",
    className,
  );

  return (
    <button className={style} onClick={onClick} disabled={disabled || loading}>
      {loading && <LoaderCircle className="animate-spin" />}
      {children}
    </button>
  );
};

export default Button;
