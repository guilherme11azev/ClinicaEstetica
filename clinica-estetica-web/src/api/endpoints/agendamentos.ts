import api from '../axios';
import { AgendamentoDto, CriarAgendamentoDto } from '../../types';

export const agendamentosApi = {
  obterPorPeriodo: async (inicio: string, fim: string): Promise<AgendamentoDto[]> => {
    const { data } = await api.get<AgendamentoDto[]>('/agendamentos', {
      params: { inicio, fim },
    });
    return data;
  },

  obterPorProfissional: async (
    profissionalId: string,
    data: string
  ): Promise<AgendamentoDto[]> => {
    const { data: result } = await api.get<AgendamentoDto[]>(
      `/agendamentos/profissional/${profissionalId}`,
      { params: { data } }
    );
    return result;
  },

  obterPorPaciente: async (pacienteId: string): Promise<AgendamentoDto[]> => {
    const { data } = await api.get<AgendamentoDto[]>(
      `/agendamentos/paciente/${pacienteId}`
    );
    return data;
  },

  criar: async (dto: CriarAgendamentoDto): Promise<AgendamentoDto> => {
    const { data } = await api.post<AgendamentoDto>('/agendamentos', dto);
    return data;
  },

  cancelar: async (id: string, motivo: string): Promise<void> => {
    await api.patch(`/agendamentos/${id}/cancelar`, { motivoCancelamento: motivo });
  },

  atualizarStatus: async (id: string, novoStatus: string): Promise<void> => {
    await api.patch(`/agendamentos/${id}/status`, JSON.stringify(novoStatus), {
      headers: { 'Content-Type': 'application/json' },
    });
  },
};