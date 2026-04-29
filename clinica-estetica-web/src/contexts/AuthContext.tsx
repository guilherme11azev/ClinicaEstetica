import {
  createContext,
  useContext,
  useState,
  useEffect,
} from 'react';
import type { ReactNode } from 'react';
import type { TokenDto } from '../types';
import { authApi } from '../api/endpoints/auth';

interface AuthContextData {
  usuario: TokenDto | null;
  login: (email: string, senha: string) => Promise<void>;
  logout: () => void;
  isAutenticado: boolean;
  carregando: boolean;
}

const AuthContext = createContext<AuthContextData>({} as AuthContextData);

export function AuthProvider({ children }: { children: ReactNode }) {
  const [usuario, setUsuario] = useState<TokenDto | null>(null);
  const [carregando, setCarregando] = useState(true);

  useEffect(() => {
    const token = localStorage.getItem('token');
    const usuarioSalvo = localStorage.getItem('usuario');

    if (token && usuarioSalvo) {
      setUsuario(JSON.parse(usuarioSalvo));
    }

    setCarregando(false);
  }, []);

  const login = async (email: string, senha: string) => {
    const dados = await authApi.login({ email, senha });
    localStorage.setItem('token', dados.token);
    localStorage.setItem('usuario', JSON.stringify(dados));
    setUsuario(dados);
  };

  const logout = () => {
    localStorage.removeItem('token');
    localStorage.removeItem('usuario');
    setUsuario(null);
  };

  return (
    <AuthContext.Provider
      value={{
        usuario,
        login,
        logout,
        isAutenticado: !!usuario,
        carregando,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
}

export const useAuth = () => useContext(AuthContext);