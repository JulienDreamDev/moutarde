export interface ButtonProps {
  variant?: "primary" | "secondary" | "danger" | "ghost";
  size?: "sm" | "md" | "lg";
  disabled?: boolean;
  loading?: boolean;
  onClick?: (event: React.MouseEvent<HTMLElement>) => void;
  children?: React.ReactNode;
}

const Button: React.FC<ButtonProps> = ({
  variant,
  size,
  disabled,
  loading,
  onClick,
  children,
}) => {
  return (
    <button
      className="bg-moutarde-600 hover:bg-moutarde-500 cursor-pointer rounded-lg px-8 py-3 font-semibold text-white transition-colors"
      onClick={onClick}
      disabled={disabled}
    >
      {children}
    </button>
  );
};

export default Button;
