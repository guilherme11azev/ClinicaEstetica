import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useForm } from 'react-hook-form';
import toast from 'react-hot-toast';
import { useAuth } from '../contexts/AuthContext';
import { Input } from '../components/ui/Input';
import { Button } from '../components/ui/Button';

interface LoginForm {
  email: string;
  senha: string;
}

export function Login() {
  const { login } = useAuth();
  const navigate = useNavigate();
  const [carregando, setCarregando] = useState(false);

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<LoginForm>();

  const onSubmit = async (data: LoginForm) => {
    setCarregando(true);
    try {
      await login(data.email, data.senha);
      navigate('/');
    } catch {
      toast.error('E-mail ou senha inválidos.');
    } finally {
      setCarregando(false);
    }
  };

  return (
    <div className="min-h-screen bg-gradient-to-br from-purple-50 to-purple-100
      flex items-center justify-center p-4">
      <div className="w-full max-w-md">
        <div className="bg-white rounded-2xl shadow-xl p-8">
          {/* Logo */}
          <div className="text-center mb-8">
            <div className="w-16 h-16 bg-purple-100 rounded-2xl flex items-center
              justify-center mx-auto mb-4">
              <span className="text-3xl">✨</span>
            </div>
            <h1 className="text-2xl font-bold text-gray-900">Clínica Estética</h1>
            <p className="text-gray-500 text-sm mt-1">Faça login para continuar</p>
          </div>

          {/* Formulário */}
          <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
            <Input
              label="E-mail"
              type="email"
              placeholder="admin@clinica.com"
              erro={errors.email?.message}
              {...register('email', {
                required: 'E-mail é obrigatório.',
                pattern: {
                  value: /^[^\s@]+@[^\s@]+\.[^\s@]+$/,
                  message: 'E-mail inválido.',
                },
              })}
            />

            <Input
              label="Senha"
              type="password"
              placeholder="••••••••"
              erro={errors.senha?.message}
              {...register('senha', {
                required: 'Senha é obrigatória.',
                minLength: {
                  value: 6,
                  message: 'Senha deve ter pelo menos 6 caracteres.',
                },
              })}
            />

            <Button
              type="submit"
              className="w-full mt-2"
              tamanho="lg"
              carregando={carregando}
            >
              Entrar
            </Button>
          </form>
        </div>

        <p className="text-center text-xs text-gray-400 mt-6">
          Sistema de Gestão — Clínica Estética
        </p>
      </div>
    </div>
  );
}