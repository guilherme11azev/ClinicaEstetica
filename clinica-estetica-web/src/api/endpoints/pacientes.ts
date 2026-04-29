
import api from "../axios";
import type { ProfDto, CriarProfissionalDto } from "../../types";

export const pacientesApi = {
  obterTodos: async (busca?: string): Promise<PacienteDto[]> => {
    const { data } = await api.get<PacienteDto[]>('/pacientes', {
      params: busca ? { busca } : {},
    });
    return data;
  },

  obterPorId: async (id: string): Promise<PacienteDto> => {
    const { data } = await api.get<PacienteDto>(`/pacientes/${id}`);
    return data;
  },

  criar: async (dto: CriarPacienteDto): Promise<PacienteDto> => {
    const { data } = await api.post<PacienteDto>('/pacientes', dto);
    return data;
  },

  atualizar: async (
    id: string,
    dto: Partial<CriarPacienteDto>
  ): Promise<void> => {
    await api.put(`/pacientes/${id}`, dto);
  },

  inativar: async (id: string): Promise<void> => {
    await api.patch(`/pacientes/${id}/inativar`);
  },
};