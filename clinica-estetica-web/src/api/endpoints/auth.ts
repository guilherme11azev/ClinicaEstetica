import api from '../axios';
import { TokenDto } from '../../types';

interface LoginDto {
  email: string;
  senha: string;
}

export const authApi = {
  login: async (dto: LoginDto): Promise<TokenDto> => {
    const { data } = await api.post<TokenDto>('/auth/login', dto);
    return data;
  },
};