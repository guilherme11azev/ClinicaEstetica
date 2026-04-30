import { useState } from 'react';
import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import toast from 'react-hot-toast';
import { UserPlus, Search, UserX } from 'lucide-react';
import { pacientesApi } from '../../api/endpoints/pacientes';
import { Header } from '../../components/layout/Header';
import { Button } from '../../components/ui/Button';
import { Input } from '../../components/ui/Input';
import { Badge } from '../../components/ui/Badge';
import { Modal } from '../../components/ui/Modal';
import { PacienteForm } from './PacienteForm';
import type { PacienteDto } from '../../types';
import {
  formatarCpf,
  formatarTelefone,
  formatarData,
  statusCor,
} from '../../utils/formatters';

export function PacientesPage() {
  const queryClient = useQueryClient();
  const [busca, setBusca] = useState('');
  const [termoBusca, setTermoBusca] = useState('');
  const [modalAberto, setModalAberto] = useState(false);
  const [pacienteEditando, setPacienteEditando] = useState<PacienteDto | null>(null);

  const { data: pacientes = [], isLoading } = useQuery({
    queryKey: ['pacientes', termoBusca],
    queryFn: () => pacientesApi.obterTodos(termoBusca || undefined),
  });

  const mutacaoInativar = useMutation({
    mutationFn: (id: string) => pacientesApi.inativar(id),
    onSuccess: () => {
      toast.success('Paciente inativado com sucesso.');
      queryClient.invalidateQueries({ queryKey: ['pacientes'] });
    },
    onError: () => toast.error('Erro ao inativar paciente.'),
  });

  const handleBuscar = () => setTermoBusca(busca);

  const handleKeyDown = (e: React.KeyboardEvent) => {
    if (e.key === 'Enter') handleBuscar();
  };

  const handleNovoPaciente = () => {
    setPacienteEditando(null);
    setModalAberto(true);
  };

  const handleEditar = (paciente: PacienteDto) => {
    setPacienteEditando(paciente);
    setModalAberto(true);
  };

  const handleFecharModal = () => {
    setModalAberto(false);
    setPacienteEditando(null);
  };

  const handleSucesso = () => {
    handleFecharModal();
    queryClient.invalidateQueries({ queryKey: ['pacientes'] });
  };

  const handleInativar = (id: string) => {
    if (confirm('Deseja realmente inativar este paciente?')) {
      mutacaoInativar.mutate(id);
    }
  };

  return (
    <div>
      <Header
        titulo="Pacientes"
        subtitulo="Gerencie os pacientes da clínica"
      />

      {/* Toolbar */}
      <div className="flex flex-col sm:flex-row gap-3 mb-6">
        <div className="flex gap-2 flex-1">
          <Input
            placeholder="Buscar por nome, CPF, telefone ou e-mail..."
            value={busca}
            onChange={(e) => setBusca(e.target.value)}
            onKeyDown={handleKeyDown}
            className="flex-1"
          />
          <Button variante="secondary" onClick={handleBuscar}>
            <Search size={16} />
            Buscar
          </Button>
        </div>
        <Button onClick={handleNovoPaciente}>
          <UserPlus size={16} />
          Novo Paciente
        </Button>
      </div>

      {/* Tabela */}
      <div className="bg-white rounded-xl border border-gray-200 shadow-sm overflow-hidden">
        {isLoading ? (
          <div className="flex items-center justify-center py-16">
            <div className="animate-spin rounded-full h-8 w-8
              border-4 border-purple-200 border-t-purple-600" />
          </div>
        ) : pacientes.length === 0 ? (
          <div className="flex flex-col items-center justify-center py-16 text-gray-400">
            <UserX size={40} className="mb-3 opacity-40" />
            <p className="text-sm">Nenhum paciente encontrado.</p>
            <p className="text-xs mt-1">
              Clique em "Novo Paciente" para cadastrar.
            </p>
          </div>
        ) : (
          <div className="overflow-x-auto">
            <table className="w-full text-sm">
              <thead className="bg-gray-50 border-b border-gray-200">
                <tr>
                  <th className="text-left px-6 py-3 font-medium text-gray-600">
                    Nome
                  </th>
                  <th className="text-left px-6 py-3 font-medium text-gray-600">
                    CPF
                  </th>
                  <th className="text-left px-6 py-3 font-medium text-gray-600">
                    Telefone
                  </th>
                  <th className="text-left px-6 py-3 font-medium text-gray-600">
                    Nascimento
                  </th>
                  <th className="text-left px-6 py-3 font-medium text-gray-600">
                    Status
                  </th>
                  <th className="text-right px-6 py-3 font-medium text-gray-600">
                    Ações
                  </th>
                </tr>
              </thead>
              <tbody className="divide-y divide-gray-100">
                {pacientes.map((paciente) => (
                  <tr
                    key={paciente.id}
                    className="hover:bg-gray-50 transition-colors"
                  >
                    <td className="px-6 py-4">
                      <div>
                        <p className="font-medium text-gray-900">
                          {paciente.nomeCompleto}
                        </p>
                        {paciente.email && (
                          <p className="text-xs text-gray-400 mt-0.5">
                            {paciente.email}
                          </p>
                        )}
                      </div>
                    </td>
                    <td className="px-6 py-4 text-gray-600">
                      {formatarCpf(paciente.cpf)}
                    </td>
                    <td className="px-6 py-4 text-gray-600">
                      {formatarTelefone(paciente.telefone)}
                    </td>
                    <td className="px-6 py-4 text-gray-600">
                      {formatarData(paciente.dataNascimento)}
                    </td>
                    <td className="px-6 py-4">
                      <Badge
                        texto={paciente.status}
                        className={statusCor[paciente.status] ?? ''}
                      />
                    </td>
                    <td className="px-6 py-4">
                      <div className="flex items-center justify-end gap-2">
                        <Button
                          variante="ghost"
                          tamanho="sm"
                          onClick={() => handleEditar(paciente)}
                        >
                          Editar
                        </Button>
                        {paciente.status === 'Ativo' && (
                          <Button
                            variante="danger"
                            tamanho="sm"
                            onClick={() => handleInativar(paciente.id)}
                            carregando={mutacaoInativar.isPending}
                          >
                            Inativar
                          </Button>
                        )}
                      </div>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
      </div>

      {/* Rodapé com contagem */}
      {pacientes.length > 0 && (
        <p className="text-xs text-gray-400 mt-3 text-right">
          {pacientes.length} paciente{pacientes.length !== 1 ? 's' : ''} encontrado{pacientes.length !== 1 ? 's' : ''}
        </p>
      )}

      {/* Modal */}
      <Modal
        aberto={modalAberto}
        onFechar={handleFecharModal}
        titulo={pacienteEditando ? 'Editar Paciente' : 'Novo Paciente'}
        tamanho="lg"
      >
        <PacienteForm
          paciente={pacienteEditando}
          onSucesso={handleSucesso}
          onCancelar={handleFecharModal}
        />
      </Modal>
    </div>
  );
}