import { InputHTMLAttributes, forwardRef } from 'react';

interface InputProps extends InputHTMLAttributes<HTMLInputElement> {
  label?: string;
  erro?: string;
}

export const Input = forwardRef<HTMLInputElement, InputProps>(
  ({ label, erro, className = '', ...props }, ref) => (
    <div className="flex flex-col gap-1">
      {label && (
        <label className="text-sm font-medium text-gray-700">{label}</label>
      )}
      <input
        ref={ref}
        {...props}
        className={`
          w-full px-3 py-2 border rounded-lg text-sm outline-none transition
          ${erro
            ? 'border-red-400 focus:ring-2 focus:ring-red-200'
            : 'border-gray-300 focus:ring-2 focus:ring-primary-200 focus:border-primary-400'
          }
          disabled:bg-gray-50 disabled:text-gray-500
          ${className}
        `}
      />
      {erro && <span className="text-xs text-red-500">{erro}</span>}
    </div>
  )
);

Input.displayName = 'Input';