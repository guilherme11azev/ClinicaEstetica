import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';
import { Layout } from '../components/layout/Layout';
import { Login } from '../pages/Login';
import { Dashboard } from '../pages/Dashboard';
import { PacientesPage } from '../pages/pacientes/PacientesPage';
import { ProfissionaisPage } from '../pages/profissionais/ProfissionaisPage';
import { AgendamentosPage } from '../pages/agendamentos/AgendamentosPage';

function RotaProtegida({ children }: { children: React.ReactNode }) {
  const { isAutenticado, carregando } = useAuth();

  if (carregando) {
    return (
      <div className="min-h-screen flex items-center justify-center">
        <div className="animate-spin rounded-full h-10 w-10
          border-4 border-primary-200 border-t-primary-600" />
      </div>
    );
  }

  return isAutenticado ? <>{children}</> : <Navigate to="/login" replace />;
}

export function AppRoutes() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/login" element={<Login />} />

        <Route
          path="/"
          element={
            <RotaProtegida>
              <Layout />
            </RotaProtegida>
          }
        >
          <Route index element={<Dashboard />} />
          <Route path="pacientes" element={<PacientesPage />} />
          <Route path="profissionais" element={<ProfissionaisPage />} />
          <Route path="agendamentos" element={<AgendamentosPage />} />
        </Route>

        <Route path="*" element={<Navigate to="/" replace />} />
      </Routes>
    </BrowserRouter>
  );
}