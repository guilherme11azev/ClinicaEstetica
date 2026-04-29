import api from '../axios';
import { ProfissionalDto } from '../../types';

export const profissionaisApi = {
  obterTodos: async (apenasAtivos = false): Promise<ProfissionalDto[]> => {
    const { data } = await api.get<ProfissionalDto[]>('/profissionais', {
      params: { apenasAtivos },
    });
    return data;
  },

  obterPorId: async (id: string): Promise<ProfissionalDto> => {
    const { data } = await api.get<ProfissionalDto>(`/profissionais/${id}`);
    return data;
  },

  criar: async (dto: Omit<ProfissionalDto, 'id' | 'status'>): Promise<ProfissionalDto> => {
    const { data } = await api.post<ProfissionalDto>('/profissionais', dto);
    return data;
  },

  atualizar: async (id: string, dto: Partial<ProfissionalDto>): Promise<void> => {
    await api.put(`/profissionais/${id}`, dto);
  },

  inativar: async (id: string): Promise<void> => {
    await api.patch(`/profissionais/${id}/inativar`);
  },
};