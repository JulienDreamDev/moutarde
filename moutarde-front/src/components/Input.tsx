import react, { isValidElement, cloneElement } from "react";
import clsx from "clsx";
import { CircleX } from "lucide-react";

export interface InputProps {
  type?: "text" | "password" | "email" | "number";
  placeholder?: string;
  label?: string;
  disabled?: boolean;
  error?: string;
  value?: string;
  helperText?: string;
  leftIcon?: React.ReactNode;
  rightIcon?: React.ReactNode;
  onChange?: (event: React.ChangeEvent<HTMLInputElement>) => void;
  onBlur?: (event: React.FocusEvent<HTMLInputElement>) => void;
  className?: string;
}

const Input: React.FC<InputProps> = ({
  type = "text",
  placeholder,
  label,
  disabled = false,
  error = false,
  value,
  helperText,
  onChange,
  onBlur,
  className,
  leftIcon,
  rightIcon,
}) => {
  const renderIcon = (icon: React.ReactNode) => {
    if (!icon) return null;
    if (isValidElement(icon)) {
      return cloneElement(icon as React.ReactElement<any>, {
        size: 13,
      });
    }
    return icon;
  };

  const style = clsx(
    "rounded-lg p-2 outline transition-colors",
    disabled && "cursor-not-allowed opacity-50",
    error && "outline-ketchup-500",
    !error &&
      "focus:outline-moutarde-600 outline-neutral-300 dark:outline-neutral-700",
    leftIcon && "pl-8",
    rightIcon && "pr-8",
    className,
  );

  return (
    <div>
      {label && (
        <label className="mb-1 block text-sm font-medium">{label}</label>
      )}
      <div className="relative">
        {leftIcon && (
          <span className="text-moutarde-700 dark:text-moutarde-300 absolute top-1/2 left-2 -translate-y-1/2 transform">
            {renderIcon(leftIcon)}
          </span>
        )}
        <input
          type={type}
          disabled={disabled}
          className={style}
          size={24}
          placeholder={placeholder}
          onChange={onChange}
          onBlur={onBlur}
          value={value}
          title={helperText}
        />
        {rightIcon && (
          <span className="text-moutarde-700 dark:text-moutarde-300 absolute top-1/2 right-2 -translate-y-1/2 transform">
            {renderIcon(rightIcon)}
          </span>
        )}
      </div>
      {error && (
        <p className="text-ketchup-500 mt-1 flex items-center gap-1 text-sm">
          <CircleX size={16} className="text-ketchup-500" />
          {error}
        </p>
      )}
    </div>
  );
};

export default Input;
