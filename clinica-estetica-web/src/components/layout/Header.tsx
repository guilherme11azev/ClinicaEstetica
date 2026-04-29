interface HeaderProps {
  titulo: string;
  subtitulo?: string;
}

export function Header({ titulo, subtitulo }: HeaderProps) {
  return (
    <div className="mb-6">
      <h1 className="text-2xl font-bold text-gray-900">{titulo}</h1>
      {subtitulo && <p className="text-sm text-gray-500 mt-1">{subtitulo}</p>}
    </div>
  );
}