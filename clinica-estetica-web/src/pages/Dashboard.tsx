import { Header } from '../components/layout/Header';
import { Calendar, Users, UserCog, CheckCircle } from 'lucide-react';

const cards = [
  { label: 'Agendamentos Hoje', valor: '—', icon: Calendar,     cor: 'bg-blue-50 text-blue-600' },
  { label: 'Pacientes',         valor: '—', icon: Users,        cor: 'bg-green-50 text-green-600' },
  { label: 'Profissionais',     valor: '—', icon: UserCog,      cor: 'bg-purple-50 text-purple-600' },
  { label: 'Concluídos Hoje',   valor: '—', icon: CheckCircle,  cor: 'bg-orange-50 text-orange-600' },
];

export function Dashboard() {
  return (
    <div>
      <Header
        titulo="Dashboard"
        subtitulo="Visão geral da clínica"
      />

      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6">
        {cards.map(({ label, valor, icon: Icon, cor }) => (
          <div key={label}
            className="bg-white rounded-xl border border-gray-200 p-6
              flex items-center gap-4 shadow-sm">
            <div className={`w-12 h-12 rounded-xl flex items-center
              justify-center ${cor}`}>
              <Icon size={22} />
            </div>
            <div>
              <p className="text-sm text-gray-500">{label}</p>
              <p className="text-2xl font-bold text-gray-900">{valor}</p>
            </div>
          </div>
        ))}
      </div>

      <div className="mt-8 bg-white rounded-xl border border-gray-200 p-6 shadow-sm">
        <p className="text-gray-400 text-sm text-center py-8">
          Os indicadores serão exibidos aqui na Etapa 9 com dados reais da API.
        </p>
      </div>
    </div>
  );
}