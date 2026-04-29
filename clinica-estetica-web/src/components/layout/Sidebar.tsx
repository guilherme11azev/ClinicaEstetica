import { NavLink } from 'react-router-dom';
import {
  LayoutDashboard,
  Users,
  UserCog,
  Calendar,
  Stethoscope,
  Package,
  LogOut,
} from 'lucide-react';
import { useAuth } from '../../contexts/AuthContext';

const menus = [
  { label: 'Dashboard',     icon: LayoutDashboard, path: '/' },
  { label: 'Pacientes',     icon: Users,           path: '/pacientes' },
  { label: 'Profissionais', icon: UserCog,         path: '/profissionais' },
  { label: 'Agendamentos',  icon: Calendar,        path: '/agendamentos' },
  { label: 'Procedimentos', icon: Stethoscope,     path: '/procedimentos' },
  { label: 'Produtos',      icon: Package,         path: '/produtos' },
];

export function Sidebar() {
  const { usuario, logout } = useAuth();

  return (
    <aside className="w-64 min-h-screen bg-white border-r border-gray-200 flex flex-col">
      {/* Logo */}
      <div className="px-6 py-5 border-b border-gray-200">
        <h1 className="text-lg font-bold text-purple-700">✨ Clínica Estética</h1>
        <p className="text-xs text-gray-500 mt-0.5">{usuario?.perfil}</p>
      </div>

      {/* Menus */}
      <nav className="flex-1 px-3 py-4 space-y-1">
        {menus.map(({ label, icon: Icon, path }) => (
          <NavLink
            key={path}
            to={path}
            end={path === '/'}
            className={({ isActive }) =>
              `flex items-center gap-3 px-3 py-2.5 rounded-lg text-sm
              font-medium transition-colors duration-150
              ${isActive
                ? 'bg-purple-50 text-purple-700'
                : 'text-gray-600 hover:bg-gray-100 hover:text-gray-900'
              }`
            }
          >
            <Icon size={18} />
            {label}
          </NavLink>
        ))}
      </nav>

      {/* Usuário + Logout */}
      <div className="px-3 py-4 border-t border-gray-200">
        <div className="px-3 py-2 mb-1">
          <p className="text-sm font-medium text-gray-900 truncate">
            {usuario?.nomeUsuario}
          </p>
        </div>
        <button
          onClick={logout}
          className="flex items-center gap-3 px-3 py-2.5 w-full rounded-lg
            text-sm font-medium text-gray-600 hover:bg-red-50 hover:text-red-600
            transition-colors duration-150"
        >
          <LogOut size={18} />
          Sair
        </button>
      </div>
    </aside>
  );
}