export interface TokenDto {
  token: string;
  nomeUsuario: string;
  perfil: string;
  expiracao: string;
}

export interface PacienteDto {
  id: string;
  nomeCompleto: string;
  cpf: string;
  dataNascimento: string;
  telefone: string;
  whatsApp?: string;
  email?: string;
  endereco?: string;
  genero?: string;
  observacoes?: string;
  status: string;
  criadoEm: string;
}

export interface CriarPacienteDto {
  nomeCompleto: string;
  cpf: string;
  dataNascimento: string;
  telefone: string;
  whatsApp?: string;
  email?: string;
  endereco?: string;
  genero?: string;
  observacoes?: string;
}

export interface ProfissionalDto {
  id: string;
  nomeCompleto: string;
  cpf: string;
  telefone: string;
  email?: string;
  cargo?: string;
  especialidades?: string;
  status: string;
}

export interface AgendamentoDto {
  id: string;
  pacienteId: string;
  nomePaciente: string;
  profissionalId: string;
  nomeProfissional: string;
  procedimentoId: string;
  nomeProcedimento: string;
  dataHoraInicio: string;
  dataHoraFimPrevista: string;
  status: string;
  observacoes?: string;
}

export interface CriarAgendamentoDto {
  pacienteId: string;
  profissionalId: string;
  procedimentoId: string;
  dataHoraInicio: string;
  observacoes?: string;
}

export interface ApiErro {
  mensagem: string;
}