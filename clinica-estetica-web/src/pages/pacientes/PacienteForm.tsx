import { useEffect } from 'react';
import { useForm } from 'react-hook-form';
import { useMutation } from '@tanstack/react-query';
import toast from 'react-hot-toast';
import { pacientesApi } from '../../api/endpoints/pacientes';
import { Input } from '../../components/ui/Input';
import { Button } from '../../components/ui/Button';
import type { PacienteDto, CriarPacienteDto } from '../../types';

interface PacienteFormProps {
  paciente: PacienteDto | null;
  onSucesso: () => void;
  onCancelar: () => void;
}

type FormData = CriarPacienteDto;

export function PacienteForm({ paciente, onSucesso, onCancelar }: PacienteFormProps) {
  const isEdicao = !!paciente;

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<FormData>();

  useEffect(() => {
    if (paciente) {
      reset({
        nomeCompleto:   paciente.nomeCompleto,
        cpf:            paciente.cpf,
        dataNascimento: paciente.dataNascimento.split('T')[0],
        telefone:       paciente.telefone,
        whatsApp:       paciente.whatsApp ?? '',
        email:          paciente.email ?? '',
        endereco:       paciente.endereco ?? '',
        genero:         paciente.genero ?? '',
        observacoes:    paciente.observacoes ?? '',
      });
    } else {
      reset({});
    }
  }, [paciente, reset]);

  const mutacaoCriar = useMutation({
    mutationFn: (dto: CriarPacienteDto) => pacientesApi.criar(dto),
    onSuccess: () => {
      toast.success('Paciente cadastrado com sucesso!');
      onSucesso();
    },
    onError: (erro: any) => {
      toast.error(erro?.response?.data?.mensagem ?? 'Erro ao cadastrar paciente.');
    },
  });

  const mutacaoAtualizar = useMutation({
    mutationFn: (dto: Partial<CriarPacienteDto>) =>
      pacientesApi.atualizar(paciente!.id, dto),
    onSuccess: () => {
      toast.success('Paciente atualizado com sucesso!');
      onSucesso();
    },
    onError: () => toast.error('Erro ao atualizar paciente.'),
  });

  const carregando = mutacaoCriar.isPending || mutacaoAtualizar.isPending;

  const onSubmit = (data: FormData) => {
    if (isEdicao) {
      const { cpf, dataNascimento, ...dadosAtualizaveis } = data;
      mutacaoAtualizar.mutate(dadosAtualizaveis);
    } else {
      mutacaoCriar.mutate(data);
    }
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
      <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">

        <div className="sm:col-span-2">
          <Input
            label="Nome Completo *"
            placeholder="Maria da Silva"
            erro={errors.nomeCompleto?.message}
            {...register('nomeCompleto', {
              required: 'Nome é obrigatório.',
            })}
          />
        </div>

        <Input
          label="CPF *"
          placeholder="00000000000"
          disabled={isEdicao}
          erro={errors.cpf?.message}
          {...register('cpf', {
            required: 'CPF é obrigatório.',
            minLength: { value: 11, message: 'CPF inválido.' },
          })}
        />

        <Input
          label="Data de Nascimento *"
          type="date"
          disabled={isEdicao}
          erro={errors.dataNascimento?.message}
          {...register('dataNascimento', {
            required: 'Data de nascimento é obrigatória.',
          })}
        />

        <Input
          label="Telefone *"
          placeholder="11999999999"
          erro={errors.telefone?.message}
          {...register('telefone', {
            required: 'Telefone é obrigatório.',
          })}
        />

        <Input
          label="WhatsApp"
          placeholder="11999999999"
          {...register('whatsApp')}
        />

        <Input
          label="E-mail"
          type="email"
          placeholder="email@exemplo.com"
          erro={errors.email?.message}
          {...register('email', {
            pattern: {
              value: /^[^\s@]+@[^\s@]+\.[^\s@]+$/,
              message: 'E-mail inválido.',
            },
          })}
        />

        <Input
          label="Gênero"
          placeholder="Feminino, Masculino..."
          {...register('genero')}
        />

        <div className="sm:col-span-2">
          <Input
            label="Endereço"
            placeholder="Rua, número, bairro, cidade..."
            {...register('endereco')}
          />
        </div>

        <div className="sm:col-span-2 flex flex-col gap-1">
          <label className="text-sm font-medium text-gray-700">
            Observações
          </label>
          <textarea
            rows={3}
            placeholder="Observações gerais sobre o paciente..."
            className="w-full px-3 py-2 border border-gray-300 rounded-lg text-sm
              outline-none focus:ring-2 focus:ring-purple-200
              focus:border-purple-400 resize-none"
            {...register('observacoes')}
          />
        </div>

      </div>

      <div className="flex justify-end gap-3 pt-2 border-t border-gray-100">
        <Button
          type="button"
          variante="secondary"
          onClick={onCancelar}
          disabled={carregando}
        >
          Cancelar
        </Button>
        <Button type="submit" carregando={carregando}>
          {isEdicao ? 'Salvar Alterações' : 'Cadastrar Paciente'}
        </Button>
      </div>
    </form>
  );
}