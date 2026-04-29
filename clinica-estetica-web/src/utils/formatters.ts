export const formatarCpf = (cpf: string) =>
  cpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4');

export const formatarTelefone = (tel: string) =>
  tel.replace(/(\d{2})(\d{5})(\d{4})/, '($1) $2-$3');

export const formatarData = (data: string) =>
  new Date(data).toLocaleDateString('pt-BR');

export const formatarDataHora = (data: string) =>
  new Date(data).toLocaleString('pt-BR', {
    day: '2-digit',
    month: '2-digit',
    year: 'numeric',
    hour: '2-digit',
    minute: '2-digit',
  });

export const statusCor: Record<string, string> = {
  Agendado:      'bg-blue-100 text-blue-700',
  Confirmado:    'bg-green-100 text-green-700',
  EmAtendimento: 'bg-yellow-100 text-yellow-700',
  Concluido:     'bg-purple-100 text-purple-700',
  Cancelado:     'bg-red-100 text-red-700',
  NaoCompareceu: 'bg-gray-100 text-gray-700',
  Ativo:         'bg-green-100 text-green-700',
  Inativo:       'bg-red-100 text-red-700',
};